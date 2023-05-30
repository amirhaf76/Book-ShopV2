using BookShop.ModelsLayer.DataAccessLayer.Dtos;

namespace BookShop.ModelsLayer.BusinessLogicLayer.DtosExtension
{
    public static class FilterExtension
    {
        public static PaginationFilter ConvertToPaginationFilter(this PaginationFilterDto pagination)
        {
            if (pagination.PageSiz == null || pagination.PageNo == null)
            {
                return new PaginationFilter(10, 1);
            }

            return new PaginationFilter((int)pagination.PageSiz, (int)pagination.PageNo);
        }
    }
}
