using LmsApi.Models.DTOs.User;
using LmsApi.Models.Entities;
using Mapster;

namespace LmsApi.Mappings
{
    public static class MappingExtensions
    {
        public static GetUserDto ToDto(this User user )
        {
            return user.Adapt<GetUserDto>();
        }
        public static User ToEntity(this AddUserDto dto) =>
        dto.Adapt<User>();
    }
}
