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
        ServiceResult<GetUserDto> UpdateUser(int userId, AddUserDto userDto);
        ServiceResult<string> DeleteUser(int id);

        // Authentication
        ServiceResult<AuthResponseDto> Authenticate(AuthRequestDto authRequestDto);
        ServiceResult<AuthResponseDto> Register(AddUserDto userDto, string password);
        string GenerateAccessToken(User user);//after successful login in Authenticate
        string GenerateRefreshToken();
        public ServiceResult<AuthResponseDto> GenerateTokens(User user);
        public ServiceResult<AuthResponseDto> RefreshToken(RefreshRequestDto refreshDto);
        // Account management
        ServiceResult<string> ChangePassword(int userId, string oldPassword, string newPassword);
        ServiceResult<GetUserDto> ChangeEmail(int userId, string newEmail);
        ServiceResult<string> ResetPassword(string email, string newPassword);

        public ServiceResult<RefreshResponseDto> Logout(int userId);

        // Email (later)
        //ServiceResult<string> SendPasswordResetEmail(string email);
        //ServiceResult<string> VerifyEmail(string email, string verificationCode);
        //ServiceResult<string> ResendVerificationEmail(string email);
    }
}
