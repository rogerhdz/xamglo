using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace XamarinPO.Api.Controllers
{
    public class TestApiController : ApiController
    {
        [HttpPost]
        public IHttpActionResult TestConnection()
        {
            return Ok("Connected");
        }

        [HttpPost]
        public IHttpActionResult TestRequest([FromBody]string value)
        {
            return Ok(string.Format("Api Response is: {0} at {1}", value, DateTime.Now.ToString("dd/MM/yyyy mm:ss")));
        }
    }
}
