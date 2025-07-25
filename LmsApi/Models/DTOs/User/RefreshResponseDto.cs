namespace LmsApi.Models.DTOs.User
{
    public class RefreshResponseDto
    {
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
    }
}
