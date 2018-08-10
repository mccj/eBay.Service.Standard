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
using eBay.Service.Core.Soap;
using System;

#endregion

namespace eBay.Service.Core.Sdk
{
    internal class eBayAPIInstance
    {
        public static eBayAPIInstance Instance { get; } = new eBayAPIInstance();

        System.ServiceModel.BasicHttpsBinding binding = new System.ServiceModel.BasicHttpsBinding()
        {
            //CloseTimeout = TimeSpan.FromMilliseconds(Timeout),
            MaxBufferPoolSize = int.MaxValue,
            MaxBufferSize = int.MaxValue,
            MaxReceivedMessageSize = int.MaxValue
        };
        private readonly System.ServiceModel.ChannelFactory<Core.Soap.eBayAPIInterface> channelFactory;
        public eBayAPIInstance()
        {
            var endpointAddress = new System.ServiceModel.EndpointAddress("https://api.ebay.com/wsapi");
            channelFactory = new System.ServiceModel.ChannelFactory<Core.Soap.eBayAPIInterface>(binding, endpointAddress);
            var 报文 = new Core.Sdk.SoapMessage();
            channelFactory.Endpoint.EndpointBehaviors.Add(new Core.Sdk.ContextPropagationBehavior(报文));
        }
        public Core.Soap.eBayAPIInterface GeteBayAPIClient(string url, double? timeout = null)
        {
            if (timeout.HasValue)
                binding.CloseTimeout = TimeSpan.FromMilliseconds(timeout.Value);

            var endpointAddress = new System.ServiceModel.EndpointAddress(url);
            return channelFactory.CreateChannel(endpointAddress);
        }
    }
}
