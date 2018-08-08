
using System;

namespace eBay.Service
{
    public partial class eBayClient
    {
        public eBayClient()
        {
            securityHeader.eBayAuthToken = "";
        }
        private Core.Soap.CustomSecurityHeaderType securityHeader { get; } = new Core.Soap.CustomSecurityHeaderType();
        public Core.Soap.SiteCodeType SiteCode { get; set; } = Core.Soap.SiteCodeType.US;
        private T handleRequest<T>(T abstractRequest) where T : Core.Soap.AbstractRequestType
        {
            abstractRequest.Version = "1065";
            abstractRequest.MessageID = System.Guid.NewGuid().ToString();
            return abstractRequest;
        }
        private string getUrl(string apiName, Core.Soap.SiteCodeType site)
        {
            var url = String.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}?callname={1}&siteid={2}&client=netsoap",
                     "https://api.ebay.com/wsapi", apiName, Util.SiteUtility.GetSiteID(site).ToString(System.Globalization.CultureInfo.InvariantCulture));
            return url;
        }
        private Core.Soap.eBayAPIInterface GeteBayAPIClient(string url, double? timeout = null)
        {
            return null;//Core.Sdk.eBayAPIInstance.Instance.GeteBayAPIClient(url);
        }
        //public Core.Soap.GetApiAccessRulesResponseType GetApiAccessRules(Core.Soap.GetApiAccessRulesRequestType request)
        //{
        //    var apiName = "GetApiAccessRules";
        //    var url = getUrl(apiName, SiteCode);
        //    var client = this.GeteBayAPIClient(url);
        //    var requestInfo = new Core.Soap.GetApiAccessRulesRequest
        //    {
        //        GetApiAccessRulesRequestType = handleRequest(request),
        //        RequesterCredentials = this.securityHeader
        //    };
        //    var response = client.GetApiAccessRules(requestInfo);
        //    return response.GetApiAccessRulesResponseType;
        //}
    }
}
