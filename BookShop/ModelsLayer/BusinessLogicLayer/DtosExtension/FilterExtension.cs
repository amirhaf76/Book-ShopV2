using BookShop.ModelsLayer.Dtos.FilterDtos;

namespace BookShop.ModelsLayer.DtosExtension
{
    public static class FilterExtension
    {
        public static int GetSkipNumber(this PaginationFilter paginationFilter)
        {
            return (paginationFilter.GetPageNumber() - 1) * paginationFilter.PageSize;
        }

        public static int GetPageNumber(this PaginationFilter paginationFilter)
        {
            return paginationFilter.PageNumber == 0 ? 1 : paginationFilter.PageNumber;
        }

        public static bool Validate(this PaginationFilter paginationFilter)
        {
            return paginationFilter.GetPageNumber() >= 0 && paginationFilter.GetSkipNumber() >= 0;
        }
    }


}
