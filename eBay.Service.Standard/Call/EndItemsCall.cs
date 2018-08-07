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
using System.Collections.Generic;
using System.Runtime.InteropServices;
using eBay.Service.Core.Sdk;
using eBay.Service.Core.Soap;
using eBay.Service.EPS;
using eBay.Service.Util;

#endregion

namespace eBay.Service.Call
{

    /// <summary>
    /// 
    /// </summary>
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class EndItemsCall : ApiCall
    {

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public EndItemsCall()
        {
            ApiRequest = new EndItemsRequestType();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ApiContext">The <see cref="ApiCall.ApiContext"/> for this API Call of type <see cref="ApiContext"/>.</param>
        public EndItemsCall(ApiContext ApiContext)
        {
            ApiRequest = new EndItemsRequestType();
            this.ApiContext = ApiContext;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// The <b>EndItems</b> call is used to end up to 10 specified eBay listings before the date and time at which those listings would normally end per the listing duration.
        /// </summary>
        /// 
        /// <param name="EndItemRequestContainerList">
        /// An <b>EndItemRequestContainer</b> container is required for each eBay listing that the seller plans to end through the <b>EndItems</b> call. Up to 10 eBay listings can be ended with one <b>EndItems</b> call.
        /// </param>
        ///
        public List<EndItemResponseContainerType> EndItems(List<EndItemRequestContainerType> EndItemRequestContainerList)
        {
            this.EndItemRequestContainerList = EndItemRequestContainerList;

            Execute();
            return ApiResponse.EndItemResponseContainer;
        }



        #endregion




        #region Properties
        /// <summary>
        /// The base interface object.
        /// </summary>
        /// <remarks>This property is reserved for users who have difficulty querying multiple interfaces.</remarks>
        public ApiCall ApiCallBase
        {
            get { return this; }
        }

        /// <summary>
        /// Gets or sets the <see cref="EndItemsRequestType"/> for this API call.
        /// </summary>
        public EndItemsRequestType ApiRequest
        {
            get { return (EndItemsRequestType)AbstractRequest; }
            set { AbstractRequest = value; }
        }

        /// <summary>
        /// Gets the <see cref="EndItemsResponseType"/> for this API call.
        /// </summary>
        public EndItemsResponseType ApiResponse
        {
            get { return (EndItemsResponseType)AbstractResponse; }
        }


        /// <summary>
        /// Gets or sets the <see cref="EndItemsRequestType.EndItemRequestContainer"/> of type <see cref="List<EndItemRequestContainerType>"/>.
        /// </summary>
        public List<EndItemRequestContainerType> EndItemRequestContainerList
        {
            get { return ApiRequest.EndItemRequestContainer; }
            set { ApiRequest.EndItemRequestContainer = value; }
        }


        /// <summary>
        /// Gets the returned <see cref="EndItemsResponseType.EndItemResponseContainer"/> of type <see cref="List<EndItemResponseContainerType>"/>.
        /// </summary>
        public List<EndItemResponseContainerType> EndItemResponseContainerList
        {
            get { return ApiResponse.EndItemResponseContainer; }
        }


        #endregion


    }
}
