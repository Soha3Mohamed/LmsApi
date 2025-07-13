using LmsApi.Helpers;
using LmsApi.Mappings;
using LmsApi.Models.Data;
using LmsApi.Models.DTOs.User;
using LmsApi.Models.Entities;
using LmsApi.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LmsApi.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly string _key = "YourSuperSecurityKey123";
        public UserService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public ServiceResult<List<GetUserDto>> GetAllUsers()
        {
            var allUsers = _dbContext.Users.Select(u => MappingExtensions.ToDto(u)).ToList();


            if (allUsers == null || !allUsers.Any())
            {
                return ServiceResult<List<GetUserDto>>.Fail("No users found.");
            }
            return ServiceResult<List<GetUserDto>>.Ok(allUsers);      
        } //done

        public ServiceResult<GetUserDto> GetUserByEmail(string email)
        {
            var user = _dbContext.Users.Include(u => u.Enrollments).FirstOrDefault(u => u.Email == email);
            if(user == null)
            {
                return ServiceResult<GetUserDto>.Fail("No user found with this email");
            }
            var userDto = user.ToDto();
            return ServiceResult<GetUserDto>.Ok(userDto);
        } //done

        public ServiceResult<GetUserDto> GetUserById(int id)
        {
            var user = _dbContext.Users.Include(u => u.Enrollments).FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return ServiceResult<GetUserDto>.Fail($"No user found with this id : {id}");
            }
            var userDto = user.ToDto();
            return ServiceResult<GetUserDto>.Ok(userDto);
        } //done

        public ServiceResult<GetUserDto> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
        public ServiceResult<string> DeleteUser(int id)
        {
            var user = _dbContext.Users.Find(id);
            if(user == null)
            {
                return ServiceResult<string>.Fail($"No user was found with this id: {id}");
            }
            var userDto = user.ToDto();
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
            return ServiceResult<string>.Ok("User deleted successfully");
        } //done

        public ServiceResult<AuthResponseDto> Register(AddUserDto userDto, string password)
        {
            var existingUser = _dbContext.Users.FirstOrDefault(u => u.Email == userDto.Email);
            if(existingUser != null)
            {
                return ServiceResult<AuthResponseDto>.Fail("User with this email already exists.");
            }
            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                PasswordHash = PasswordHelper.Hash(password), // Hash the password
                UserRole = userDto.UserRole,
                CreatedAt = DateTime.UtcNow
            };
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            var token = GenerateToken(user);
            return ServiceResult<AuthResponseDto>.Ok(new AuthResponseDto
            {
                Token = token,
                Email = user.Email,
                Role = user.UserRole.ToString()
            });

        }//done

        public ServiceResult<AuthResponseDto> Authenticate(AuthRequestDto authRequestDto)
        {
            var user = _dbContext.Users.FirstOrDefault(u=>u.Email == authRequestDto.Email && u.PasswordHash == authRequestDto.Password);
            if(!PasswordHelper.VerifyPassword(user.PasswordHash, authRequestDto.Password))
            {
                return ServiceResult<AuthResponseDto>.Fail("Invalid Credentials");
            }
            var token = GenerateToken(user);
            //var userDto = user.ToDto();
            //what is the type in ServiceResult? should it be bool or what ?
            //
            return ServiceResult<AuthResponseDto>.Ok(new AuthResponseDto
            {
                Token = token,
                Email = user.Email,
                Role = user.UserRole.ToString()
            });
        } //done

        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserRole.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "your-app",
                audience:"your-users",
                claims: claims ,
                expires: DateTime.UtcNow.AddHours(1),signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        } //done

        public ServiceResult<string> RefreshToken(string token)
        {
            throw new NotImplementedException();
        }

        public ServiceResult<GetUserDto> ChangeEmail(int userId, string newEmail)
        {
            var user = _dbContext.Users.Find(userId);
            if (user == null)
                return ServiceResult<GetUserDto>.Fail("User not found.");

            var existingEmailUser = _dbContext.Users.FirstOrDefault(u => u.Email == newEmail);
            if (existingEmailUser != null)
                return ServiceResult<GetUserDto>.Fail("Email already in use.");

            user.Email = newEmail;
            _dbContext.SaveChanges();
            var userDto = user.ToDto();

            return ServiceResult<GetUserDto>.Ok(userDto);
        }//done


        public ServiceResult<User> ChangePassword(int userId, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
        public ServiceResult<string> ResetPassword(string email, string newPassword)
        {
            throw new NotImplementedException();
        }

    }
}
