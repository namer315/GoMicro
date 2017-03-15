namespace GoMicro.Forex.Models
{
    public class CommonResult
    {
        public CommonResult(bool success, string message)
        {
            this.Success = success;
            this.Message = message;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
