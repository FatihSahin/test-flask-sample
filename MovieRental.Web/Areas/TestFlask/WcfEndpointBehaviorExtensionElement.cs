﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Web;

namespace TestFlask.Assistant.WcfExtensions
{
    public class WcfEndpointBehaviorExtensionElement : BehaviorExtensionElement
    {
        protected override object CreateBehavior()
        {
            return new WcfEndpointBehavior();
        }

        public override Type BehaviorType
        {
            get
            {
                return typeof(WcfEndpointBehavior);
            }
        }
    }
}