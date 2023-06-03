using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.ReservationDtos;
using BookShop.ModelsLayer.DataAccessLayer.DataBaseModels;
using BookShop.ModelsLayer.DataAccessLayer.Dtos;
using BookShop.ModelsLayer.Exceptions;
using Infrastructure.Tools;

namespace BookShop.ModelsLayer.BusinessLogicLayer.DtosExtension
{
    public static class ReservationDtoExtension
    {
        public static ReservationFilter ConvertToReservationFilter(this ReservedBookFilterDto filter)
        {
            if (filter.Pagination.PageNo == null || filter.Pagination.PageSiz == null)
            {
                throw new PaginationException();
            }

            var paginationFilter = new PaginationFilter((int)filter.Pagination.PageSiz, (int)filter.Pagination.PageNo);

            return new ReservationFilter
            {
                Id = filter.Id,
                UserAccountId = filter.UserAccountId,
                LastChange = filter.LastChangeTime,
                ComfirmationTime = filter.ComfirmationTime,
                Pagination = paginationFilter,
                Status = filter.Status?.ConvertToReservationStatus(),
            };
        }

        public static ReservationStatusVariety ConvertToReservationStatusVariety(this ReservationStatus status)
        {
            return status switch
            {
                ReservationStatus.Pending => ReservationStatusVariety.Pending,
                ReservationStatus.Confirmed => ReservationStatusVariety.Confirmed,
                ReservationStatus.Canceled => ReservationStatusVariety.Canceled,

                _ => throw Helper.CreateExceptionForInvaidEnumValue(status),
            };
        }

        public static ReservationStatus ConvertToReservationStatus(this ReservationStatusVariety status)
        {
            return status switch
            {
                ReservationStatusVariety.Pending => ReservationStatus.Pending,
                ReservationStatusVariety.Confirmed => ReservationStatus.Confirmed,
                ReservationStatusVariety.Canceled => ReservationStatus.Canceled,

                _ => throw Helper.CreateExceptionForInvaidEnumValue(status),
            };
        }
    }
}
