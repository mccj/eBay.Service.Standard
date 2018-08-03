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
	public class GetBestOffersCall : ApiCall
	{

		#region Constructors
		/// <summary>
		/// 
		/// </summary>
		public GetBestOffersCall()
		{
			ApiRequest = new GetBestOffersRequestType();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ApiContext">The <see cref="ApiCall.ApiContext"/> for this API Call of type <see cref="ApiContext"/>.</param>
		public GetBestOffersCall(ApiContext ApiContext)
		{
			ApiRequest = new GetBestOffersRequestType();
			this.ApiContext = ApiContext;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// This is the base request type of the <b>GetBestOffers</b> call. Depending on the input parameters that are used, this call can be used by a seller to retrieve all active Best Offers, all Best Offers on a specific listing, a specific Best Offer for a specific listing, or Best Offers in a specific state.
		/// </summary>
		/// 
		/// <param name="ItemID">
		/// The unique identifier of an eBay listing. If an <b>ItemID</b> is used by itself in the call request, all Best Offers in all states are retrieved for this listing. However, the seller can also combine <b>ItemID</b> and a <b>BestOfferStatus</b> value if that seller only wants to see Best Offers in a specific state. If a <b>BestOfferID</b> field is included in the call request, any <b>ItemID</b> value will be ignored since eBay will only search for and return the Best Offer identified in the <b>BestOfferID</b> field.
		/// <br/><br/>
		/// <span class="tablenote"><b>Note: </b>
		/// Historically, the Best Offer feature has not been available for auction listings, but beginning with Version 1027, which rolled out the first week of August 2017, sellers in the US, UK, and DE sites are able to offer the Best Offer feature in auction listings. The seller can offer Buy It Now or Best Offer in an auction listing, but not both.
		/// </span>
		/// </param>
		///
		/// <param name="BestOfferID">
		/// The unique identifier of a Best Offer. An identifier for a Best Offer is automatically created by eBay once a prospective buyer makes a Best Offer on a Best Offer-enabled listing. If a <b>BestOfferID</b> value is supplied in the call request, any <b>ItemID</b> or <b>BestOfferStatus</b> values will be ignored. Only the Best Offer identified by the <b>BestOfferID</b> value will be returned.
		/// <br/><br/>
		/// <span class="tablenote"><b>Note: </b>
		/// Historically, the Best Offer feature has not been available for auction listings, but beginning with Version 1027, which rolled out the first week of August 2017, sellers in the US, UK, and DE sites are able to offer the Best Offer feature in auction listings. The seller can offer Buy It Now or Best Offer in an auction listing, but not both.
		/// </span>
		/// </param>
		///
		/// <param name="BestOfferStatus">
		/// This field can be used if the seller wants to retrieve Best Offers in a specific state. The typical use case for this field is when the seller wants to retrieve Best Offers in all states for a specific listing. In fact, the <b>All</b> value can only be used if an <b>ItemID</b> value is also supplied in the call request. If a <b>BestOfferID</b> field is included in the call request, any <b>BestOfferStatus</b> value will be ignored since eBay will only search for and return the Best Offer identified in the <b>BestOfferID</b> field.
		/// <br/><br/>
		/// <span class="tablenote"><b>Note: </b>
		/// Historically, the Best Offer feature has not been available for auction listings, but beginning with Version 1027, which rolled out the first week of August 2017, sellers in the US, UK, and DE sites are able to offer the Best Offer feature in auction listings. The seller can offer Buy It Now or Best Offer in an auction listing, but not both.
		/// </span>
		/// </param>
		///
		/// <param name="Pagination">
		/// This container can be used if the seller is expecting that the <b>GetBestOffers</b> call will retrieve a large number of results, so that seller wishes to view just a subset (one page of multiple pages) of those results at a time. See this container's child fields for more information on how pagination is used.
		/// </param>
		///
		public BestOfferType[] GetBestOffers(string ItemID, string BestOfferID, BestOfferStatusCodeType BestOfferStatus, PaginationType Pagination)
		{
			this.ItemID = ItemID;
			this.BestOfferID = BestOfferID;
			this.BestOfferStatus = BestOfferStatus;
			this.Pagination = Pagination;

			Execute();
			return ApiResponse.BestOfferArray;
		}


		/// <summary>
		/// For backward compatibility with old wrappers.
		/// </summary>
		public BestOfferType[] GetBestOffers(string ItemID)
		{
			this.ItemID = ItemID;
			Execute();
			return BestOfferList;
		}
		
		/// <summary>
		/// Gets the returned <see cref="GetBestOffersResponseType.ItemBestOffersArray"/> of type <see cref="ItemBestOffersTypeCollection"/>.
		/// </summary>
		public ItemBestOffersType[] ItemBestOffersList
		{ 
			get {
				if (ApiResponse.ItemBestOffersArray == null)
					return null;
				return ApiResponse.ItemBestOffersArray.ItemBestOffers; }
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
		/// Gets or sets the <see cref="GetBestOffersRequestType"/> for this API call.
		/// </summary>
		public GetBestOffersRequestType ApiRequest
		{ 
			get { return (GetBestOffersRequestType) AbstractRequest; }
			set { AbstractRequest = value; }
		}

		/// <summary>
		/// Gets the <see cref="GetBestOffersResponseType"/> for this API call.
		/// </summary>
		public GetBestOffersResponseType ApiResponse
		{ 
			get { return (GetBestOffersResponseType) AbstractResponse; }
		}

		
 		/// <summary>
		/// Gets or sets the <see cref="GetBestOffersRequestType.ItemID"/> of type <see cref="string"/>.
		/// </summary>
		public string ItemID
		{ 
			get { return ApiRequest.ItemID; }
			set { ApiRequest.ItemID = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetBestOffersRequestType.BestOfferID"/> of type <see cref="string"/>.
		/// </summary>
		public string BestOfferID
		{ 
			get { return ApiRequest.BestOfferID; }
			set { ApiRequest.BestOfferID = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetBestOffersRequestType.BestOfferStatus"/> of type <see cref="BestOfferStatusCodeType"/>.
		/// </summary>
		public BestOfferStatusCodeType BestOfferStatus
		{ 
			get { return ApiRequest.BestOfferStatus; }
			set { ApiRequest.BestOfferStatus = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetBestOffersRequestType.Pagination"/> of type <see cref="PaginationType"/>.
		/// </summary>
		public PaginationType Pagination
		{ 
			get { return ApiRequest.Pagination; }
			set { ApiRequest.Pagination = value; }
		}
		
		
 		/// <summary>
		/// Gets the returned <see cref="GetBestOffersResponseType.BestOfferArray"/> of type <see cref="BestOfferTypeCollection"/>.
		/// </summary>
		public BestOfferType[] BestOfferList
		{ 
			get { return ApiResponse.BestOfferArray; }
		}
		
 		/// <summary>
		/// Gets the returned <see cref="GetBestOffersResponseType.Item"/> of type <see cref="ItemType"/>.
		/// </summary>
		public ItemType Item
		{ 
			get { return ApiResponse.Item; }
		}
		
 		/// <summary>
		/// Gets the returned <see cref="GetBestOffersResponseType.ItemBestOffersArray"/> of type <see cref="ItemBestOffersArrayType"/>.
		/// </summary>
		public ItemBestOffersArrayType ItemBestOffersArray
		{ 
			get { return ApiResponse.ItemBestOffersArray; }
		}
		
 		/// <summary>
		/// Gets the returned <see cref="GetBestOffersResponseType.PageNumber"/> of type <see cref="int"/>.
		/// </summary>
		public int PageNumber
		{ 
			get { return ApiResponse.PageNumber; }
		}
		
 		/// <summary>
		/// Gets the returned <see cref="GetBestOffersResponseType.PaginationResult"/> of type <see cref="PaginationResultType"/>.
		/// </summary>
		public PaginationResultType PaginationResult
		{ 
			get { return ApiResponse.PaginationResult; }
		}
		

		#endregion

		
	}
}
