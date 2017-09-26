using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Web;

namespace TestFlask.Assistant.WcfExtensions 
{
    public class WcfMessageInspector : IClientMessageInspector
    {
        /// <summary>
        /// Enables inspection or modification of a message before a request message is sent to a service.
        /// </summary>
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            HttpRequestMessageProperty property = new HttpRequestMessageProperty();

            property.Headers["User-Agent"] = "value";
            request.Properties.Add(HttpRequestMessageProperty.Name, property);

            return null;
        }

        /// <summary>
        /// Enables inspection or modification of a message after a reply message is received but prior to passing it back to the client application.
        /// </summary>
        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            // Nothing special here
        }
    }
}