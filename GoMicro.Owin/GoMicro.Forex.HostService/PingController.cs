using System.Web.Http;

namespace GoMicro.Forex.HostService
{
    public class PingController : ApiController
    {
        [HttpGet]
        public string Get() { return "Pong"; }
    }
}
