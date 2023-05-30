using BookShop.ModelsLayer.Exceptions;

namespace BookShop.ModelsLayer.BusinessLogicLayer.Dtos.FilterDtos
{
    public class PaginationFilter
    {
        private readonly int _pageSize;
        private readonly int _pageNumber;

        private readonly int _maxPageSize = 50;

        public PaginationFilter(int pageSize, int pageNumber, int maxPageSize) : this(pageSize, pageNumber)
        {
            if (maxPageSize >= 0)
            {
                throw new Exception("Maximum skip size must be greater than or equal to zero!");
            }

            _maxPageSize = maxPageSize;
        }

        public PaginationFilter(int pageSize, int pageNumber)
        {
            _pageSize = pageSize;
            _pageNumber = pageNumber;

            if (!Validate())
            {
                throw new PaginationException(this);
            }
        }

        public IQueryable<T> AddPaginationTo<T>(IQueryable<T> queryable)
        {
            return queryable
                .Skip(GetSkipSize())
                .Take(PageSize);
        }

        public int PageSize { get => _pageSize; }

        public int PageNumber
        {
            get
            {
                return _pageNumber == 0 ? 1 : _pageNumber;
            }
        }

        public int GetSkipSize()
        {
            return (PageNumber - 1) * _pageSize;
        }

        public bool Validate()
        {
            var skipSize = GetSkipSize();

            return PageNumber > 0 && PageSize <= _maxPageSize && skipSize >= 0;
        }
    }
}