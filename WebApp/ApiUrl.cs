namespace WebApp
{
    public class ApiUrl
    {
        public string Url { get; set; }
        public static readonly ApiUrl apiBaseUrl = new ApiUrl() { Url = "https://localhost:7122/api/game" };
    }
}
