using LmsApi.Helpers;
using LmsApi.Models.DTOs.User;
using LmsApi.Models.Entities;

namespace LmsApi.Services.Interfaces
{
    public interface IUserService
    {
        // Basic user operations
        ServiceResult<List<GetUserDto>> GetAllUsers();
        ServiceResult<GetUserDto> GetUserById(int id);
        ServiceResult<GetUserDto> GetUserByEmail(string email);
        ServiceResult<GetUserDto> UpdateUser(User user);
        ServiceResult<string> DeleteUser(int id);

        // Authentication
        ServiceResult<AuthResponseDto> Authenticate(AuthRequestDto authRequestDto);
        ServiceResult<AuthResponseDto> Register(AddUserDto userDto, string password);
        string GenerateToken(User user);//after successful login in Authenticate
        ServiceResult<string> RefreshToken(string token);


        // Account management
        ServiceResult<User> ChangePassword(int userId, string oldPassword, string newPassword);
        ServiceResult<GetUserDto> ChangeEmail(int userId, string newEmail);
        ServiceResult<string> ResetPassword(string email, string newPassword);

        // Email (later)
        //ServiceResult<string> SendPasswordResetEmail(string email);
        //ServiceResult<string> VerifyEmail(string email, string verificationCode);
        //ServiceResult<string> ResendVerificationEmail(string email);
    }
}
