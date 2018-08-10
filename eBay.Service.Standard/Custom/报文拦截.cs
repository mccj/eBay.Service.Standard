#region Copyright
//	Copyright (c) 2013 eBay, Inc.
//	
//	This program is licensed under the terms of the eBay Common Development and
//	Distribution License (CDDL) Version 1.0 (the "License") and any subsequent  
//	version thereof released by eBay.  The then-current version of the License can be 
//	found at http://www.opensource.org/licenses/cddl1.php and in the eBaySDKLicense 
//	file that is under the eBay SDK ../docs directory
#endregion

#region Namespaces
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
//using System.Web.Services.Protocols;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
//using System.Web.Services.Protocols;

#endregion

namespace eBay.Service.Core.Sdk
{
    public class ContextPropagationBehavior : IEndpointBehavior
    {
        private SoapMessage 报文;

        public ContextPropagationBehavior(SoapMessage 报文)
        {
            this.报文 = 报文;
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            //throw new NotImplementedException();
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.ClientMessageInspectors.Add(new MyMessageInspector(this.报文));
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            //throw new NotImplementedException();
        }

        public void Validate(ServiceEndpoint endpoint)
        {
            //throw new NotImplementedException();
        }
    }
    public class MyMessageInspector : System.ServiceModel.Dispatcher.IClientMessageInspector
    {
        private SoapMessage  _soapMessage;

        public MyMessageInspector(SoapMessage soapMessage)
        {
            this._soapMessage = soapMessage;
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            this._soapMessage.receiveMessage(reply.ToString());
            //throw new NotImplementedException();
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            this._soapMessage.sendMessage(request.ToString());
            //MessageBuffer buffer = request.CreateBufferedCopy(Int32.MaxValue);
            //request = buffer.CreateMessage();
            //Console.WriteLine("Sending:\n{0}", buffer.CreateMessage().ToString());

            //throw new NotImplementedException();
            return null;
        }
    }
    public class SoapMessage
    {
        internal void sendMessage(string message)
        {
            SendMessage?.Invoke(message);
            //_发送.Add(s);
        }
        internal void receiveMessage(string message)
        {
            ReceiveMessage?.Invoke(message);
            //_接收.Add(s);
        }
        //private List<string> _发送 = new List<string>();
        //private List<string> _接收 = new List<string>();
        //public string[] 发送 { get { return _发送.ToArray(); } }
        //public string[] 接收 { get { return _接收.ToArray(); } }

        public Action<string> SendMessage { get; set; }
        public Action<string> ReceiveMessage { get; set; }
    }
}
