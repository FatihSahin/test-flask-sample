using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestFlask.Aspects;

namespace ForgottenPotatoes.ScoreAPI.Controllers
{
    public class ScoreController : ApiController
    {
        [Playback]
        public double Get(string id)
        {
            return new Random().NextDouble() * 10D;
        }
    }
}
