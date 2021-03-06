﻿using System;
using System.Threading;
using System.Web.Http;
using XamarinPO.Api.Extensions;

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
            return Ok($"Api Response is: {value} at {DateTime.Now:dd/MM/yyyy mm:ss}");
        }

        [HttpGet]
        [TimeoutFilter(3000)]
        public string TestTimeout()
        {
            Thread.Sleep(5000);
            return "no timeout";
        }
    }
}