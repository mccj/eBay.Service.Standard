using eBay.Service.Call;
using eBay.Service.Core.Sdk;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var eBayToken = "";
            var context = new ApiContext { ApiCredential = new ApiCredential { eBayToken = eBayToken  }, CallRetry= {  MaximumRetries=5 } };
            GetApiAccessRulesCall apicall = new GetApiAccessRulesCall(context);
            var rules = apicall.GetApiAccessRules();

            
        }
    }
}
