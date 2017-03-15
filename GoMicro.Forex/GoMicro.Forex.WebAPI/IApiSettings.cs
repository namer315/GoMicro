namespace GoMicro.Forex.WebApi
{
    public interface IApiSettings
    {
        string Url { get; set; }
        int AskTimeOut { get; set; }
    }
}
