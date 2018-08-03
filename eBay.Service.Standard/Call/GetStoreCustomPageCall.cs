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
	public class GetStoreCustomPageCall : ApiCall
	{

		#region Constructors
		/// <summary>
		/// 
		/// </summary>
		public GetStoreCustomPageCall()
		{
			ApiRequest = new GetStoreCustomPageRequestType();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ApiContext">The <see cref="ApiCall.ApiContext"/> for this API Call of type <see cref="ApiContext"/>.</param>
		public GetStoreCustomPageCall(ApiContext ApiContext)
		{
			ApiRequest = new GetStoreCustomPageRequestType();
			this.ApiContext = ApiContext;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// This call is used to retrieve a specific custom page or all custom pages created for a seller's eBay Store. The eBay seller must have an eBay Store subscription to use this call.
		/// </summary>
		/// 
		/// <param name="PageID">
		/// This field is used if the seller wants to retrieved detailed information on a specific custom page. If no <b>PageID</b> is specified, then all of the seller's custom pages are returned, but without the page content.
		/// </param>
		///
		public StoreCustomPageType[] GetStoreCustomPage(long PageID)
		{
			this.PageID = PageID;

			Execute();
			return ApiResponse.CustomPageArray;
		}


		/// <summary>
		/// For backward compatibility with old wrappers.
		/// </summary>
		public StoreCustomPageType[] GetStoreCustomPage()
		{
			Execute();
			return CustomPageList;
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
		/// Gets or sets the <see cref="GetStoreCustomPageRequestType"/> for this API call.
		/// </summary>
		public GetStoreCustomPageRequestType ApiRequest
		{ 
			get { return (GetStoreCustomPageRequestType) AbstractRequest; }
			set { AbstractRequest = value; }
		}

		/// <summary>
		/// Gets the <see cref="GetStoreCustomPageResponseType"/> for this API call.
		/// </summary>
		public GetStoreCustomPageResponseType ApiResponse
		{ 
			get { return (GetStoreCustomPageResponseType) AbstractResponse; }
		}

		
 		/// <summary>
		/// Gets or sets the <see cref="GetStoreCustomPageRequestType.PageID"/> of type <see cref="long"/>.
		/// </summary>
		public long PageID
		{ 
			get { return ApiRequest.PageID; }
			set { ApiRequest.PageID = value; }
		}
		
		
 		/// <summary>
		/// Gets the returned <see cref="GetStoreCustomPageResponseType.CustomPageArray"/> of type <see cref="StoreCustomPageTypeCollection"/>.
		/// </summary>
		public StoreCustomPageType[] CustomPageList
		{ 
			get { return ApiResponse.CustomPageArray; }
		}
		

		#endregion

		
	}
}
