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
	public class EndItemCall : ApiCall
	{

		#region Constructors
		/// <summary>
		/// 
		/// </summary>
		public EndItemCall()
		{
			ApiRequest = new EndItemRequestType();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ApiContext">The <see cref="ApiCall.ApiContext"/> for this API Call of type <see cref="ApiContext"/>.</param>
		public EndItemCall(ApiContext ApiContext)
		{
			ApiRequest = new EndItemRequestType();
			this.ApiContext = ApiContext;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Ends the specified item listing before the date and time at which it would normally end per the listing duration.
		/// </summary>
		/// 
		/// <param name="ItemID">
		/// Unique item ID that identifies the listing that you want to end.
		/// </param>
		///
		/// <param name="EndingReason">
		/// The seller's reason for ending the listing early is input into this required field.
		/// </param>
		///
		/// <param name="SellerInventoryID">
		/// This field was previously only used to identify and end Half.com listings, and since the Half.com site has been shut down, this element is no longer applicable.
		/// </param>
		///
		public DateTime EndItem(string ItemID, EndReasonCodeType EndingReason, string SellerInventoryID)
		{
			this.ItemID = ItemID;
			this.EndingReason = EndingReason;
			this.SellerInventoryID = SellerInventoryID;

			Execute();
			return ApiResponse.EndTime.Value;
		}


		/// <summary>
		/// For backward compatibility with old wrappers.
		/// </summary>
		public void EndItem(string ItemID, EndReasonCodeType EndingReason)
		{
			this.ItemID = ItemID;
			this.EndingReason = EndingReason;
			Execute();
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
		/// Gets or sets the <see cref="EndItemRequestType"/> for this API call.
		/// </summary>
		public EndItemRequestType ApiRequest
		{ 
			get { return (EndItemRequestType) AbstractRequest; }
			set { AbstractRequest = value; }
		}

		/// <summary>
		/// Gets the <see cref="EndItemResponseType"/> for this API call.
		/// </summary>
		public EndItemResponseType ApiResponse
		{ 
			get { return (EndItemResponseType) AbstractResponse; }
		}

		
 		/// <summary>
		/// Gets or sets the <see cref="EndItemRequestType.ItemID"/> of type <see cref="string"/>.
		/// </summary>
		public string ItemID
		{ 
			get { return ApiRequest.ItemID; }
			set { ApiRequest.ItemID = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="EndItemRequestType.EndingReason"/> of type <see cref="EndReasonCodeType"/>.
		/// </summary>
		public EndReasonCodeType EndingReason
		{ 
			get { return ApiRequest.EndingReason.Value; }
			set { ApiRequest.EndingReason = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="EndItemRequestType.SellerInventoryID"/> of type <see cref="string"/>.
		/// </summary>
		public string SellerInventoryID
		{ 
			get { return ApiRequest.SellerInventoryID; }
			set { ApiRequest.SellerInventoryID = value; }
		}
		
		
 		/// <summary>
		/// Gets the returned <see cref="EndItemResponseType.EndTime"/> of type <see cref="DateTime"/>.
		/// </summary>
		public DateTime EndTime
		{ 
			get { return ApiResponse.EndTime.Value; }
		}
		

		#endregion

		
	}
}
