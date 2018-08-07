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
        {//{"AccessToken":""}
            var eBayToken = "";
            var context = new ApiContext { ApiCredential = new ApiCredential { eBayToken = eBayToken  }, CallRetry= new CallRetry{  MaximumRetries=5 } };
            //GetApiAccessRulesCall apicall = new GetApiAccessRulesCall(context);
            //var rules = apicall.GetApiAccessRules();

            //GetItemCall apical2 = new GetItemCall(context);

            //var rules11 = apical2.GetItem("323269291886");

            var apical3 = new GetAllBiddersCall(context);
           var ssdsfsdf= apical3.GetAllBidders("323269291886", eBay.Service.Core.Soap.GetAllBiddersModeCodeType.ViewAll);
        }
    }
}
