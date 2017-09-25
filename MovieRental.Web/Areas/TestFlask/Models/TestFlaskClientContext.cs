using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestFlask.Client.Config;

namespace TestFlask.Client.Models
{
    public class TestFlaskClientContext
    {
        private const string SessionKey = "TestFlask-ClientContext";
        private TestFlaskClientConfig config;

        public TestFlaskClientContext()
        {
            config = TestFlaskClientConfig.Instance;
        }
        
        public static TestFlaskClientContext Current
        {
            get
            {
                if (HttpContext.Current.Session[SessionKey] == null)
                {
                    HttpContext.Current.Session[SessionKey] = new TestFlaskClientContext();                    
                }

                return HttpContext.Current.Session[SessionKey] as TestFlaskClientContext;
            }
        }

        public string ProjectKey => config.Project.Key;

        public bool IsViewExpanded { get; set; }

        public int SelectedScenarioNo { get; set; }

        public bool RecordMode { get; set; }
    }
}