using System.Web.Http;

namespace GoMicro.Forex.HostService
{
    [RoutePrefix("Ping/")]
    public class PingController : ApiController
    {
        [HttpGet]
        [Route("index")]
        public string Get() { return "Pong"; }
        // url testing for postman
        //http://localhost:9000/api/ping/index
    }
}
