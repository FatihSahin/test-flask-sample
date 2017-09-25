using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.SessionState;
using TestFlask.Client.Config;
using TestFlask.Client.Models;
using TestFlask.Models.Entity;

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

        [HttpGet]
        public JsonResult GetScenarios() {
            List<Scenario> scenarios = new List<Scenario>();

            scenarios.Add(new Scenario { ScenarioNo = 1, ScenarioName = "Scenario One" });
            scenarios.Add(new Scenario { ScenarioNo = 2, ScenarioName = "Scenario Two" });

            return Json(scenarios, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateNewScenario(string scenarioName)
        {
            //save scenario 
            return Json(true);
        }

        [HttpPost]
        public PartialViewResult Steps(int scenarioNo)
        {
            context.SelectedScenarioNo = scenarioNo;

            List<Step> steps = new List<Step>();

            steps.Add(new Step { StepNo = scenarioNo * 10 + 1, StepName = "Step1", CreatedOn = DateTime.Now });
            steps.Add(new Step { StepNo = scenarioNo * 10 + 2, StepName = "Step2", CreatedOn = DateTime.Now });
            steps.Add(new Step { StepNo = scenarioNo * 10 + 3, StepName = "Step3", CreatedOn = DateTime.Now });

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
