using System.Web.Http;
using GoMicro.Forex.Models;

namespace GoMicro.Forex.WebApi.Controllers
{
    //[RoutePrefix("Home/v1")]
     [RoutePrefix("Ping/")]
    class PingController : ApiController
    {
        private readonly IApiSettings _ApiSettings;

        public PingController(IApiSettings ApiSettings)
        {
            _ApiSettings = ApiSettings;
        }

        [HttpGet]
        //[Route("Ping")]
        [Route("index")]
        public CommonResult Get()
        {
            return new CommonResult(true,"Pong");
        }        
    }
}
