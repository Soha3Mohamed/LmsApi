using LmsApi.Services.Implementation;
using LmsApi.Services.Interfaces;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LmsApi.Extensions
{
    public static class ServiceExtensions
    {
        // i will add some more services that i don't completely understand but want to see what it does
        // 1. CORS: it is supposed to restrict access from different domains(who can send requests to my application)

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                       builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader());
            });
        }
        public static void ConfigureIIS(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }

        public static void Addauthentication(this IServiceCollection services)
        {
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = "your-app",
                        ValidAudience = "your-users",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperVerySecurityKeyTest12345678")),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }
        public static void Addauthorization(this IServiceCollection services)
        {
            services.AddAuthorization(); // Enables [Authorize]
        }

        public static void AddServices(this IServiceCollection services)
        {
            //Custom Services are here
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ILessonService, LessonService>();
        }

    }
}


//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = "your-app",
//            ValidAudience = "your-users",
//            IssuerSigningKey = new SymmetricSecurityKey(
//                Encoding.UTF8.GetBytes("YourSuperSecretKey123"))
//        };
//    });

//builder.Services.AddAuthorization();
//builder.Services.AddSingleton<IUserService, UserService>(); // Add our service

//var app = builder.Build();
//app.UseAuthentication();
//app.UseAuthorization();
