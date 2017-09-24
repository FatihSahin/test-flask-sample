using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.SessionState;
using TestFlask.Client.Config;
using TestFlask.Client.Models;

namespace TestFlask.Client.Controllers
{
    public class AssistantController : Controller
    {
        private readonly TestFlaskClientConfig config;
        private readonly TestFlaskClientContext context;

        public AssistantController()
        {
            config = TestFlaskClientConfig.Instance;
            context = TestFlaskClientContext.Current;
        }

        [HttpGet]
        public ActionResult ToggleView()
        {
            context.IsViewExpanded = !context.IsViewExpanded;

            return new EmptyResult();
        }
    }
}
