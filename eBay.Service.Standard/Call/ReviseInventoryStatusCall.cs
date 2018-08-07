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
	public class ReviseInventoryStatusCall : ApiCall
	{

		#region Constructors
		/// <summary>
		/// 
		/// </summary>
		public ReviseInventoryStatusCall()
		{
			ApiRequest = new ReviseInventoryStatusRequestType();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ApiContext">The <see cref="ApiCall.ApiContext"/> for this API Call of type <see cref="ApiContext"/>.</param>
		public ReviseInventoryStatusCall(ApiContext ApiContext)
		{
			ApiRequest = new ReviseInventoryStatusRequestType();
			this.ApiContext = ApiContext;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Enables a seller to change the price and/or quantity of one to four
		/// active, fixed-price listings. The fixed-price listing to modify is identified with the <b>ItemID</b> of the listing and/or the <b>SKU</b> value of the item (if a seller-defined SKU value exists for the listing). If the seller is modifying one or more variations within a multiple-variation listing, the <b>ItemID</b> and <b>SKU</b> fields in the <b>InventoryStatus</b> container become required, with the <b>ItemID</b> value identifying the listing, and the <b>SKU</b> value identifying the specific product variation within that multiple-variation listing. Each variation within a multiple-variation listing requires a seller-defined SKU value.
		/// <br/><br/>
		/// Whether updating the price and/or quantity of a single-variation listing or a specific variation within a multiple-variation listing, the limit of items or item variations that can be modified with one call is four.
		/// </summary>
		/// 
		/// <param name="InventoryStatuList">
		/// One <b>InventoryStatus</b> container is required for each item or item variation that is being revised. Whether updating the price and/or quantity of a single-variation listing or a specific variation within a multiple-variation listing, the limit of items or item variations that can be modified with one call is four.
		/// </param>
		///
		public List<InventoryStatusType> ReviseInventoryStatus(List<InventoryStatusType> InventoryStatuList)
		{
			this.InventoryStatuList = InventoryStatuList;

			Execute();
			return ApiResponse.InventoryStatus;
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
		/// Gets or sets the <see cref="ReviseInventoryStatusRequestType"/> for this API call.
		/// </summary>
		public ReviseInventoryStatusRequestType ApiRequest
		{ 
			get { return (ReviseInventoryStatusRequestType) AbstractRequest; }
			set { AbstractRequest = value; }
		}

		/// <summary>
		/// Gets the <see cref="ReviseInventoryStatusResponseType"/> for this API call.
		/// </summary>
		public ReviseInventoryStatusResponseType ApiResponse
		{ 
			get { return (ReviseInventoryStatusResponseType) AbstractResponse; }
		}

		
 		/// <summary>
		/// Gets or sets the <see cref="ReviseInventoryStatusRequestType.InventoryStatus"/> of type <see cref="InventoryStatusTypeCollection"/>.
		/// </summary>
		public List<InventoryStatusType> InventoryStatuList
		{ 
			get { return ApiRequest.InventoryStatus; }
			set { ApiRequest.InventoryStatus = value; }
		}
		
		
 		/// <summary>
		/// Gets the returned <see cref="ReviseInventoryStatusResponseType.InventoryStatus"/> of type <see cref="InventoryStatusTypeCollection"/>.
		/// </summary>
		public List<InventoryStatusType> InventoryStatuListReturn
		{ 
			get { return ApiResponse.InventoryStatus; }
		}
		
 		/// <summary>
		/// Gets the returned <see cref="ReviseInventoryStatusResponseType.Fees"/> of type <see cref="InventoryFeesTypeCollection"/>.
		/// </summary>
		public List<InventoryFeesType> FeeList
		{ 
			get { return ApiResponse.Fees; }
		}
		

		#endregion

		
	}
}
