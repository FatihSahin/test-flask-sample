using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestFlask.Models.Entity;

namespace TestFlask.Client.Models
{
    public class StepsViewModel
    {
        public TestFlaskClientContext Context { get; set; }
        public IEnumerable<Step> Steps { get; set; }

        public StepsViewModel(TestFlaskClientContext context, IEnumerable<Step> steps)
        {
            Context = context;
            Steps = steps;
        }

    }
}