using BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;

namespace BookShop.Test.UnitTest.Core.MockRepositoryPattern
{
    public class MockRepositoryBuilder<TKey, TEntity> where TEntity : class
    {
        private readonly Func<TEntity, TKey> _getKey;
        private readonly Action<TKey, TEntity> _setKey;
        private readonly Func<TKey> _generateKey;
        private readonly Dictionary<TKey, TEntity> _mapTable;

        public MockRepositoryBuilder(Func<TEntity, TKey> getKey, Action<TKey, TEntity> setKey, Func<TKey> generateKey, Dictionary<TKey, TEntity> mapTable)
        {
            _getKey = getKey;
            _generateKey = generateKey;
            _setKey = setKey;
            _mapTable = mapTable;
        }

        public Mock<TRepository> CreateMockBaseRepository<TRepository>()
            where TRepository : class, IBaseRepository<TEntity>
        {
            var mockIBaseRepository = new Mock<TRepository>();

            mockIBaseRepository
               .Setup(x => x.Remove(It.IsAny<TEntity>()))
               .Callback((TEntity e) => _mapTable.Remove(_getKey(e)))
               .Returns((TEntity e) => CreateEntityEntry(e).Entity);

            mockIBaseRepository
                .Setup(x => x.Add(It.IsAny<TEntity>()))
                .Callback((TEntity e) => AddEntry(e))
                .Returns((TEntity e) => CreateEntityEntry(e).Entity);

            mockIBaseRepository
                .Setup(x => x.AddAsync(It.IsAny<TEntity>()))
                .Callback((TEntity e) => AddEntry(e))
                .Returns((TEntity e) => Task.FromResult(CreateEntityEntry(e).Entity));

            mockIBaseRepository
                .Setup(x => x.AddRange(It.IsAny<TEntity[]>()))
                .Callback((TEntity[] es) =>
                {
                    foreach (var e in es)
                    {
                        AddEntry(e);
                    }
                });

            mockIBaseRepository
                .Setup(x => x.AddRange(It.IsAny<IEnumerable<TEntity>>()))
                .Callback((IEnumerable<TEntity> es) =>
                {
                    foreach (var e in es)
                    {
                        AddEntry(e);
                    }
                });

            mockIBaseRepository
                .Setup(x => x.AddRangeAsync(It.IsAny<TEntity[]>()))
                .Callback((TEntity[] es) =>
                {
                    foreach (var e in es)
                    {
                        AddEntry(e);
                    }
                })
                .Returns(Task.CompletedTask);

            mockIBaseRepository
                .Setup(x => x.AddRangeAsync(It.IsAny<IEnumerable<TEntity>>()))
                .Callback((IEnumerable<TEntity> es) =>
                {
                    foreach (var e in es)
                    {
                        AddEntry(e);
                    }
                })
                .Returns(Task.CompletedTask);

            mockIBaseRepository
                .Setup(x => x.Update(It.IsAny<TEntity>()))
                .Callback((TEntity e) => { _mapTable[_getKey(e)] = e; })
                .Returns((TEntity e) => CreateEntityEntry(e).Entity);

            TEntity findEntity(params object[] objs)
            {
                if (objs.Length > 1)
                {
                    throw new Exception("For more than one key there is no behavior!");
                }

                if (objs[0] is TKey key)
                {
                    _mapTable.TryGetValue(key, out TEntity entity);

                    return entity;
                }

                throw new Exception($"Obj type must be {typeof(TKey).Name}!");
            }

            mockIBaseRepository
                .Setup(x => x.Find(It.IsAny<object[]>()))
                .Returns((object[] objs) => findEntity(objs));

            mockIBaseRepository
                .Setup(x => x.FindAsync(It.IsAny<object[]>()))
                .ReturnsAsync((object[] objs) => findEntity());

            return mockIBaseRepository;
        }

        public EntityEntry<TEntity> AddEntry(TEntity entity)
        {
            if (!_mapTable.ContainsKey(_getKey(entity)))
            {
                _setKey(_generateKey(), entity);

                _mapTable.Add(_getKey(entity), entity);
            }

            return CreateEntityEntry(entity);
        }

        public static EntityEntry<TEntity> CreateEntityEntry(TEntity entity)
        {
            var mockEntity = CreateMockEntityEntry();

            mockEntity.Setup(x => x.Entity).Returns(entity);

            return mockEntity.Object;
        }

        private static Mock<EntityEntry<TEntity>> CreateMockEntityEntry()
        {
            return new Mock<EntityEntry<TEntity>>(null);
        }
    }
}
