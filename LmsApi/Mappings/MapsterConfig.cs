using LmsApi.Models.DTOs.User;
using LmsApi.Models.Entities;
using Mapster;

namespace LmsApi.Mappings
{
    public class MapsterConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<User, GetUserDto>.NewConfig()
               
               .Map(dest => dest.UserRole, src => src.UserRole.ToString());


            // Repeat for other mappings
        }
    }
}
