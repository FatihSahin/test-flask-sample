using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using TestFlask.Client.Api;

namespace TestFlask.Client.Controllers
{
    public class TestFlaskController: Controller
    {
        private readonly TestFlaskClientConfig config;

        public TestFlaskController()
        {
            config = TestFlaskClientConfig.Instance;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}