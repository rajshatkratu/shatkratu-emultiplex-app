namespace EMultiplex.Models.Requests
{
    public class UserRefreshTokenRequest
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
