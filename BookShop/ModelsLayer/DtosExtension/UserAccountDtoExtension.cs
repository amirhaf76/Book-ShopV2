using BookShop.ModelsLayer.Dtos.UserAccountDtos;
using BookShop.ModelsLayer.GetwayLayer.RequestResponseModels;

namespace BookShop.ModelsLayer.DtosExtension
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
