using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;

namespace BookShop.ModelsLayer.DataAccessLayer.DataModelRepositoryAbstraction
{
    public interface ITestPermissionRepository
    {
        Task SingleVsFirst();

        Task InsertAndUpdateAsync();

        Task TestEF();

        void TestUpdate(Permission permission);

        int UpdateImmediately();

        void CheckProjectAutors();

        void CheckProjectTitle();

        Task FastTesting();
    }
}
