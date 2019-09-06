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

namespace eBay.Service.Core.Soap
{
    public partial class ItemType
    {
        public bool? OutOfStockControl { get; set; }
        public bool? PostCheckoutExperienceEnabled { get; set; }
        public bool? IntangibleItem { get; set; }
        //public bool? PrivateListing { get; set; }
        public int? GiftIcon { get; set; }

    }
    public class GetApiAccessRulesInfo
    {
        private readonly GetApiAccessRulesRequestType request = new GetApiAccessRulesRequestType();
        //public 
    }
}
