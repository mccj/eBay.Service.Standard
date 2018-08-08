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
	public class GetPromotionalSaleDetailsCall : ApiCall
	{

		#region Constructors
		/// <summary>
		/// 
		/// </summary>
		public GetPromotionalSaleDetailsCall()
		{
			ApiRequest = new GetPromotionalSaleDetailsRequestType();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ApiContext">The <see cref="ApiCall.ApiContext"/> for this API Call of type <see cref="ApiContext"/>.</param>
		public GetPromotionalSaleDetailsCall(ApiContext ApiContext)
		{
			ApiRequest = new GetPromotionalSaleDetailsRequestType();
			this.ApiContext = ApiContext;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// The base request type for the <b>GetPromotionalSaleDetails</b> call. This call retrieves information about promotional sales set up by an eBay store owner. The eBay store owner has the option to retrieve all promotional sales, a specific promotional sale, or promotional sales in a specific state.
		/// </summary>
		/// 
		/// <param name="PromotionalSaleID">
		/// The unique identifier of the promotional sale to retrieve. This field is used if the eBay store owner only wants to retrieve a specific promotional sale. Any <b>PromotionalSaleStatus</b> fields that are included will be ignored if the <b>PromotionalSaleID</b> field is used.
		/// 
		/// If neither the <b>PromotionalSaleID</b> nor a <b>PromotionalSaleStatus</b> field is used, then all promotional sales for the eBay store owner are returned.
		/// </param>
		///
		/// <param name="PromotionalSaleStatuList">
		/// One or more <b>PromotionalSaleStatus</b> fields are used if the eBay store owner wants to retrieve promotional sales in a specific state, such as <code>Active</code>, <code>Scheduled</code>, or <code>Deleted</code>. A <b>PromotionalSaleStatus</b> field is included for each status. See <a href="types/PromotionalSaleStatusCodeType.html">PromotionalSaleStatusCodeType</a> for a description of the status values that can be used in this field.
		/// 
		/// If neither the <b>PromotionalSaleID</b> nor a <b>PromotionalSaleStatus</b> field is used, then all promotional sales for the eBay store owner are returned.
		/// </param>
		///
		public List<PromotionalSaleType> GetPromotionalSaleDetails(long PromotionalSaleID, List<PromotionalSaleStatusCodeType> PromotionalSaleStatuList)
		{
			this.PromotionalSaleID = PromotionalSaleID;
			this.PromotionalSaleStatuList = PromotionalSaleStatuList;

			Execute();
			return ApiResponse.PromotionalSaleDetails;
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
		/// Gets or sets the <see cref="GetPromotionalSaleDetailsRequestType"/> for this API call.
		/// </summary>
		public GetPromotionalSaleDetailsRequestType ApiRequest
		{ 
			get { return (GetPromotionalSaleDetailsRequestType) AbstractRequest; }
			set { AbstractRequest = value; }
		}

		/// <summary>
		/// Gets the <see cref="GetPromotionalSaleDetailsResponseType"/> for this API call.
		/// </summary>
		public GetPromotionalSaleDetailsResponseType ApiResponse
		{ 
			get { return (GetPromotionalSaleDetailsResponseType) AbstractResponse; }
		}

		
 		/// <summary>
		/// Gets or sets the <see cref="GetPromotionalSaleDetailsRequestType.PromotionalSaleID"/> of type <see cref="long"/>.
		/// </summary>
		public long PromotionalSaleID
		{ 
			get { return ApiRequest.PromotionalSaleID.Value; }
			set { ApiRequest.PromotionalSaleID = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetPromotionalSaleDetailsRequestType.PromotionalSaleStatus"/> of type <see cref="PromotionalSaleStatusCodeTypeCollection"/>.
		/// </summary>
		public List<PromotionalSaleStatusCodeType> PromotionalSaleStatuList
		{ 
			get { return ApiRequest.PromotionalSaleStatus; }
			set { ApiRequest.PromotionalSaleStatus = value; }
		}
		
		
 		/// <summary>
		/// Gets the returned <see cref="GetPromotionalSaleDetailsResponseType.PromotionalSaleDetails"/> of type <see cref="PromotionalSaleTypeCollection"/>.
		/// </summary>
		public List<PromotionalSaleType> PromotionalSaleDetailList
		{ 
			get { return ApiResponse.PromotionalSaleDetails; }
		}
		

		#endregion

		
	}
}
