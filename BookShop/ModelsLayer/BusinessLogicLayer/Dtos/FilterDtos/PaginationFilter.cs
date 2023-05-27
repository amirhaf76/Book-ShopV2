using BookShop.ModelsLayer.Exceptions;

namespace BookShop.ModelsLayer.Dtos.FilterDtos
{
    public class PaginationFilter
    {
        private readonly int _pageSize;
        private readonly int _pageNumber;

        private readonly int _maxSkipSize = 50;

        public PaginationFilter(int pageSize, int pageNumber, int maxSkipSize) : this(pageSize, pageNumber)
        {
            if (maxSkipSize >= 0)
            {
                throw new Exception("Maximum skip size must be greater than or equal to zero!");
            }

            _maxSkipSize = maxSkipSize;
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

        public int PageSize { get; set; }

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

            return PageNumber > 0 && _maxSkipSize >= skipSize && skipSize >= 0;
        }
    }
}