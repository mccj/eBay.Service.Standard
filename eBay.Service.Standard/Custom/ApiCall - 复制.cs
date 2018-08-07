
using System;

namespace eBay.Service
{
    public class eBayClient
    {
        public Core.Soap.GetApiAccessRulesResponseType GetApiAccessRules()
        {
            var apiName = "GetApiAccessRules";
            var url = getUrl(apiName, Core.Soap.SiteCodeType.US);
            var client = Core.Sdk.eBayAPIInstance.Instance.GeteBayAPIClient(url);

            var ccc = new Core.Soap.CustomSecurityHeaderType
            {
                eBayAuthToken = ""
            };
            var request = new Core.Soap.GetApiAccessRulesRequest
            {
                GetApiAccessRulesRequestType = new Core.Soap.GetApiAccessRulesRequestType
                {
                    Version = "1065",
                    MessageID = System.Guid.NewGuid().ToString(),
                },
                RequesterCredentials = ccc
            };
            var dddd = client.GetApiAccessRules(request).GetApiAccessRulesResponseType;
            return dddd;
        }
        private string getUrl(string apiName, Core.Soap.SiteCodeType site)
        {
            var url = String.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}?callname={1}&siteid={2}&client=netsoap",
                     "https://api.ebay.com/wsapi", apiName, Util.SiteUtility.GetSiteID(site).ToString(System.Globalization.CultureInfo.InvariantCulture));
            return url;
        }
    }
}
