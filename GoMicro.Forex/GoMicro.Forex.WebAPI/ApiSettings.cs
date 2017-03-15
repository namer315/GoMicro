namespace GoMicro.Forex.WebApi
{
    public class ApiSettings : IApiSettings
    {
        public string Url { get; set; }
        public int AskTimeOut { get; set; }
    }
}
