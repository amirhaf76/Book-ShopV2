using BookShop.ModelsLayer.BusinessLogicLayer.Dtos.UserAccountDtos;
using BookShop.ModelsLayer.ViewModelLayer.GetwayLayer.RequestResponseModels;

namespace BookShop.ModelsLayer.BusinessLogicLayer.DtosExtension
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
