namespace MTKDotNetCore.AuthAPI.ResponseModels
{
    public class ResponseUser
    {
        public string Username { get; set; }

        public string SessionId { get; set; }

        public DateTime SessionExpired { get; set; }
    }
}
