using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.SessionState;
using TestFlask.Assistant.ApiClient;
using TestFlask.Assistant.Config;
using TestFlask.Assistant.Models;
using TestFlask.Models.Entity;

namespace TestFlask.Assistant.Controllers
{
    public class AssistantController : Controller
    {
        private readonly TestFlaskAssistantConfig config;
        private readonly TestFlaskAssistantContext context;
        private readonly TestFlaskApi api;

        public AssistantController()
        {
            config = TestFlaskAssistantConfig.Instance;
            context = TestFlaskAssistantContext.Current;
            api = new TestFlaskApi();
        }

        [HttpGet]
        public ActionResult ToggleView()
        {
            context.IsViewExpanded = !context.IsViewExpanded;

            return new EmptyResult();
        }

        [HttpGet]
        public JsonResult GetScenarios()
        {
            IEnumerable<Scenario> scenarios = api.GetScenarios();
            return Json(scenarios, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateNewScenario(string scenarioName)
        {
            //save scenario 
            Scenario scenario = new Scenario();
            scenario.ProjectKey = config.Project.Key;
            scenario.ScenarioName = scenarioName;

            Scenario apiScenario = api.CreateScenario(scenario);

            return Json(true);
        }

        [HttpPost]
        public PartialViewResult Steps(int scenarioNo)
        {
            context.CurrentScenarioNo = scenarioNo;

            IEnumerable<Step> steps = api.GetSteps(scenarioNo);

            return PartialView("~/Areas/TestFlask/Views/Assistant/Steps.cshtml", new StepsViewModel(context, steps));
        }

        [HttpPost]
        public JsonResult Record(int scenarioNo, bool record)
        {
            context.RecordMode = record;
            return Json(record);
        }
    }
}
