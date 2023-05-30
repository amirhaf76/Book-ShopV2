using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.FilterDtos;

namespace BookShop.ModelsLayer.Exceptions
{
    public class PaginationException : Exception
    {
        private const string ERROR_MESSAGE_FORMAT = "Pagination amount is not valid!";
        private const string ERROR_DETAIL_FORMAT = "Page number: {0}, Page size: {1}";

        public PaginationException() : base(ERROR_MESSAGE_FORMAT)
        {

        }

        public PaginationException(PaginationFilter paginationFilter) :
            base(string.Format(ERROR_MESSAGE_FORMAT + ERROR_DETAIL_FORMAT, paginationFilter.PageNumber, paginationFilter.PageSize))
        {

        }
    }
}