using BookShop.Models.Dtos.UserAccountDtos;
using BookShop.Models.GetwayLayer.RequestResponseModels;

namespace BookShop.Models.DtosExtension
{
    public static class UserAccountDtoExtension
    {
        public static UserCreationResponse ConvertToUserCreationResponse(this UserCreationResponseDto userCreationResponseDto)
        {
            return new UserCreationResponse
            {
                Id = userCreationResponseDto.Id,
                RegeisteredDate = userCreationResponseDto.RegeisteredDate,
                Username = userCreationResponseDto.UserName,
            };
        }
    }
}
