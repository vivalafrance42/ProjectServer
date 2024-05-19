namespace ProjectServer.DTO
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? token { get; set; }
    }
}
