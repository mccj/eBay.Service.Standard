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
	public class GetSellingManagerInventoryCall : ApiCall
	{

		#region Constructors
		/// <summary>
		/// 
		/// </summary>
		public GetSellingManagerInventoryCall()
		{
			ApiRequest = new GetSellingManagerInventoryRequestType();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ApiContext">The <see cref="ApiCall.ApiContext"/> for this API Call of type <see cref="ApiContext"/>.</param>
		public GetSellingManagerInventoryCall(ApiContext ApiContext)
		{
			ApiRequest = new GetSellingManagerInventoryRequestType();
			this.ApiContext = ApiContext;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Retrieves a paginated list containing details of a user's Selling Manager Inventory. This call is subject to change without notice; the deprecation process is inapplicable to this call. The user must have a Selling Manager Pro subscription to use this call.
		/// </summary>
		/// 
		/// <param name="Sort">
		/// This field is used if the seller would like to sort Selling Manager Inventory results based on a specific aspect such as Product Name, Average Price, etc. See <b>SellingManagerProductSortCodeType</b> to read more about the available sorting options.
		/// </param>
		///
		/// <param name="FolderID">
		/// This field is included if the seller wants to view Selling Manager Inventory from a specific folder.
		/// </param>
		///
		/// <param name="Pagination">
		/// This container is used if the seller would like to control how many products are returned per page and which page to view.
		/// </param>
		///
		/// <param name="SortOrder">
		/// This field allows the seller to sort in ascending or descending order (based on the selected aspect in the <b>Sort</b> field).
		/// </param>
		///
		/// <param name="Search">
		/// This container is used if the seller would like to search for Selling Manager Inventory based on certain identifiers like Item ID, listing title, buyer user ID, etc. The seller will specify one of the supported search types in <b>SellingManagerSearchTypeCodeType</b>, and then provides the value for that search type.
		/// </param>
		///
		/// <param name="StoreCategoryID">
		/// This field is used if the seller would like to retrieve all Selling Manager Inventory listed in a specific eBay Store Category.
		/// </param>
		///
		/// <param name="FilterList">
		/// One or more <b>Filter</b> fields can be used to retrieve Selling Manager Inventory that is in a certain state, such as active listings, inactive listings, or listings that are low in stock.
		/// </param>
		///
		public DateTime GetSellingManagerInventory(SellingManagerProductSortCodeType Sort, long FolderID, PaginationType Pagination, SortOrderCodeType SortOrder, SellingManagerSearchType Search, long StoreCategoryID, SellingManagerInventoryPropertyTypeCodeType[] FilterList)
		{
			this.Sort = Sort;
			this.FolderID = FolderID;
			this.Pagination = Pagination;
			this.SortOrder = SortOrder;
			this.Search = Search;
			this.StoreCategoryID = StoreCategoryID;
			this.FilterList = FilterList;

			Execute();
			return ApiResponse.InventoryCountLastCalculatedDate;
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
		/// Gets or sets the <see cref="GetSellingManagerInventoryRequestType"/> for this API call.
		/// </summary>
		public GetSellingManagerInventoryRequestType ApiRequest
		{ 
			get { return (GetSellingManagerInventoryRequestType) AbstractRequest; }
			set { AbstractRequest = value; }
		}

		/// <summary>
		/// Gets the <see cref="GetSellingManagerInventoryResponseType"/> for this API call.
		/// </summary>
		public GetSellingManagerInventoryResponseType ApiResponse
		{ 
			get { return (GetSellingManagerInventoryResponseType) AbstractResponse; }
		}

		
 		/// <summary>
		/// Gets or sets the <see cref="GetSellingManagerInventoryRequestType.Sort"/> of type <see cref="SellingManagerProductSortCodeType"/>.
		/// </summary>
		public SellingManagerProductSortCodeType Sort
		{ 
			get { return ApiRequest.Sort; }
			set { ApiRequest.Sort = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetSellingManagerInventoryRequestType.FolderID"/> of type <see cref="long"/>.
		/// </summary>
		public long FolderID
		{ 
			get { return ApiRequest.FolderID; }
			set { ApiRequest.FolderID = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetSellingManagerInventoryRequestType.Pagination"/> of type <see cref="PaginationType"/>.
		/// </summary>
		public PaginationType Pagination
		{ 
			get { return ApiRequest.Pagination; }
			set { ApiRequest.Pagination = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetSellingManagerInventoryRequestType.SortOrder"/> of type <see cref="SortOrderCodeType"/>.
		/// </summary>
		public SortOrderCodeType SortOrder
		{ 
			get { return ApiRequest.SortOrder; }
			set { ApiRequest.SortOrder = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetSellingManagerInventoryRequestType.Search"/> of type <see cref="SellingManagerSearchType"/>.
		/// </summary>
		public SellingManagerSearchType Search
		{ 
			get { return ApiRequest.Search; }
			set { ApiRequest.Search = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetSellingManagerInventoryRequestType.StoreCategoryID"/> of type <see cref="long"/>.
		/// </summary>
		public long StoreCategoryID
		{ 
			get { return ApiRequest.StoreCategoryID; }
			set { ApiRequest.StoreCategoryID = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetSellingManagerInventoryRequestType.Filter"/> of type <see cref="SellingManagerInventoryPropertyTypeCodeTypeCollection"/>.
		/// </summary>
		public SellingManagerInventoryPropertyTypeCodeType[] FilterList
		{ 
			get { return ApiRequest.Filter; }
			set { ApiRequest.Filter = value; }
		}
		
		
 		/// <summary>
		/// Gets the returned <see cref="GetSellingManagerInventoryResponseType.InventoryCountLastCalculatedDate"/> of type <see cref="DateTime"/>.
		/// </summary>
		public DateTime InventoryCountLastCalculatedDate
		{ 
			get { return ApiResponse.InventoryCountLastCalculatedDate; }
		}
		
 		/// <summary>
		/// Gets the returned <see cref="GetSellingManagerInventoryResponseType.SellingManagerProduct"/> of type <see cref="SellingManagerProductTypeCollection"/>.
		/// </summary>
		public SellingManagerProductType[] SellingManagerProductList
		{ 
			get { return ApiResponse.SellingManagerProduct; }
		}
		
 		/// <summary>
		/// Gets the returned <see cref="GetSellingManagerInventoryResponseType.PaginationResult"/> of type <see cref="PaginationResultType"/>.
		/// </summary>
		public PaginationResultType PaginationResult
		{ 
			get { return ApiResponse.PaginationResult; }
		}
		

		#endregion

		
	}
}
