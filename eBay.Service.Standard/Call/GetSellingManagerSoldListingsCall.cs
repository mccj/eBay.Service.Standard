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
	public class GetSellingManagerSoldListingsCall : ApiCall
	{

		#region Constructors
		/// <summary>
		/// 
		/// </summary>
		public GetSellingManagerSoldListingsCall()
		{
			ApiRequest = new GetSellingManagerSoldListingsRequestType();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ApiContext">The <see cref="ApiCall.ApiContext"/> for this API Call of type <see cref="ApiContext"/>.</param>
		public GetSellingManagerSoldListingsCall(ApiContext ApiContext)
		{
			ApiRequest = new GetSellingManagerSoldListingsRequestType();
			this.ApiContext = ApiContext;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Returns a Selling Manager user's sold listings.
		/// 
		/// This call is subject to change without notice; the deprecation process is inapplicable to this call. The user must have a Selling Manager Pro subscription to use this call.
		/// </summary>
		/// 
		/// <param name="Search">
		/// This container is used if the seller would like to search for Selling Manager Sele Records based on certain identifiers like Saler Record ID, Item ID, listing title, buyer user ID, etc. The seller will specify one of the supported search types in <b>SellingManagerSearchTypeCodeType</b>, and then provides the value for that search type.
		/// </param>
		///
		/// <param name="StoreCategoryID">
		/// This field is used if the seller would like to retrieve all Selling Manager Sale Records for products listed in a specific eBay Store Category.
		/// </param>
		///
		/// <param name="FilterList">
		/// One or more <b>Filter</b> fields can be used to retrieve Selling Manager Sale Records for orders that are in a certain state. See <b>SellingManagerSoldListingsPropertyTypeCodeType</b> for the supported values.
		/// </param>
		///
		/// <param name="Archived">
		/// This field is included and set to <code>true</code> if the seller would like to retrieve one or more archived orders between 90 and 120 days old.
		/// </param>
		///
		/// <param name="Sort">
		/// This field is used if the seller would like to sort Selling Manager Sale Record results based on a specific aspect such as purchase date, checkout status, total price, etc. See <b>SellingManagerSoldListingsSortTypeCodeType</b> to read more about the available sorting options.
		/// </param>
		///
		/// <param name="SortOrder">
		/// This field allows the seller to sort in ascending or descending order (based on the selected aspect in the <b>Sort</b> field).
		/// </param>
		///
		/// <param name="Pagination">
		/// This container is used if the seller would like to control how many Sale Records are returned per page and which page to view.
		/// </param>
		///
		/// <param name="SaleDateRange">
		/// This container allows the seller to retrieve orders that were purchased within a specified time range. A time range can be set up to 90 days in the past (or up to 120 days if the <b>Archived</b> field is included and set to <code>true</code>.
		/// </param>
		///
		public List<SellingManagerSoldOrderType> GetSellingManagerSoldListings(SellingManagerSearchType Search, long StoreCategoryID, List<SellingManagerSoldListingsPropertyTypeCodeType?> FilterList, bool Archived, SellingManagerSoldListingsSortTypeCodeType Sort, SortOrderCodeType SortOrder, PaginationType Pagination, TimeRangeType SaleDateRange)
		{
			this.Search = Search;
			this.StoreCategoryID = StoreCategoryID;
			this.FilterList = FilterList;
			this.Archived = Archived;
			this.Sort = Sort;
			this.SortOrder = SortOrder;
			this.Pagination = Pagination;
			this.SaleDateRange = SaleDateRange;

			Execute();
			return ApiResponse.SaleRecord;
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
		/// Gets or sets the <see cref="GetSellingManagerSoldListingsRequestType"/> for this API call.
		/// </summary>
		public GetSellingManagerSoldListingsRequestType ApiRequest
		{ 
			get { return (GetSellingManagerSoldListingsRequestType) AbstractRequest; }
			set { AbstractRequest = value; }
		}

		/// <summary>
		/// Gets the <see cref="GetSellingManagerSoldListingsResponseType"/> for this API call.
		/// </summary>
		public GetSellingManagerSoldListingsResponseType ApiResponse
		{ 
			get { return (GetSellingManagerSoldListingsResponseType) AbstractResponse; }
		}

		
 		/// <summary>
		/// Gets or sets the <see cref="GetSellingManagerSoldListingsRequestType.Search"/> of type <see cref="SellingManagerSearchType"/>.
		/// </summary>
		public SellingManagerSearchType Search
		{ 
			get { return ApiRequest.Search; }
			set { ApiRequest.Search = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetSellingManagerSoldListingsRequestType.StoreCategoryID"/> of type <see cref="long"/>.
		/// </summary>
		public long StoreCategoryID
		{ 
			get { return ApiRequest.StoreCategoryID.Value; }
			set { ApiRequest.StoreCategoryID = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetSellingManagerSoldListingsRequestType.Filter"/> of type <see cref="SellingManagerSoldListingsPropertyTypeCodeTypeCollection"/>.
		/// </summary>
		public List<SellingManagerSoldListingsPropertyTypeCodeType?> FilterList
		{ 
			get { return ApiRequest.Filter; }
			set { ApiRequest.Filter = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetSellingManagerSoldListingsRequestType.Archived"/> of type <see cref="bool"/>.
		/// </summary>
		public bool Archived
		{ 
			get { return ApiRequest.Archived.Value; }
			set { ApiRequest.Archived = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetSellingManagerSoldListingsRequestType.Sort"/> of type <see cref="SellingManagerSoldListingsSortTypeCodeType"/>.
		/// </summary>
		public SellingManagerSoldListingsSortTypeCodeType Sort
		{ 
			get { return ApiRequest.Sort.Value; }
			set { ApiRequest.Sort = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetSellingManagerSoldListingsRequestType.SortOrder"/> of type <see cref="SortOrderCodeType"/>.
		/// </summary>
		public SortOrderCodeType SortOrder
		{ 
			get { return ApiRequest.SortOrder.Value; }
			set { ApiRequest.SortOrder = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetSellingManagerSoldListingsRequestType.Pagination"/> of type <see cref="PaginationType"/>.
		/// </summary>
		public PaginationType Pagination
		{ 
			get { return ApiRequest.Pagination; }
			set { ApiRequest.Pagination = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetSellingManagerSoldListingsRequestType.SaleDateRange"/> of type <see cref="TimeRangeType"/>.
		/// </summary>
		public TimeRangeType SaleDateRange
		{ 
			get { return ApiRequest.SaleDateRange; }
			set { ApiRequest.SaleDateRange = value; }
		}
		
		
 		/// <summary>
		/// Gets the returned <see cref="GetSellingManagerSoldListingsResponseType.SaleRecord"/> of type <see cref="SellingManagerSoldOrderTypeCollection"/>.
		/// </summary>
		public List<SellingManagerSoldOrderType> SaleRecordList
		{ 
			get { return ApiResponse.SaleRecord; }
		}
		
 		/// <summary>
		/// Gets the returned <see cref="GetSellingManagerSoldListingsResponseType.PaginationResult"/> of type <see cref="PaginationResultType"/>.
		/// </summary>
		public PaginationResultType PaginationResult
		{ 
			get { return ApiResponse.PaginationResult; }
		}
		

		#endregion

		
	}
}
