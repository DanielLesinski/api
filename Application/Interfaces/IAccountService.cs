using Application.Dto;
using Application.Dto.Account;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        Task RegisterUser(RegisterUserDto registerUserDto);
        Task<string> GenerateJwt(LoginUserDto loginUserDto);
        Task<GetUserDto> GetUser(int id);
        Task<GetUserDto> GetInfoAboutYourself();
        Task<ListUserDto> GetAllUser();
        Task<ListUserDto> SearchByKeyword(string keyword);
        Task UpdateUser (int id, UpdateUserDto updateUserDto);
        Task DeleteUser(int id);

    }
}
