namespace AERP.Web.UI.Helper
{
    public class GSTAuthTokenResponse
    {
        public int Status { get; set; }
        public Data Data { get; set; }
        public object ErrorDetails { get; set; }
        public object InfoDtls { get; set; }

        public bool AuthTokenStatus { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class Data
    {
        public string ClientId { get; set; }
        public string UserName { get; set; }
        public string AuthToken { get; set; }
        public string Sek { get; set; }
        public string TokenExpiry { get; set; }
    }
}