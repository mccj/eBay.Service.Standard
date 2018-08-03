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
	public class GetItemsAwaitingFeedbackCall : ApiCall
	{

		#region Constructors
		/// <summary>
		/// 
		/// </summary>
		public GetItemsAwaitingFeedbackCall()
		{
			ApiRequest = new GetItemsAwaitingFeedbackRequestType();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ApiContext">The <see cref="ApiCall.ApiContext"/> for this API Call of type <see cref="ApiContext"/>.</param>
		public GetItemsAwaitingFeedbackCall(ApiContext ApiContext)
		{
			ApiRequest = new GetItemsAwaitingFeedbackRequestType();
			this.ApiContext = ApiContext;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// This is the base request type of the <b>GetItemsAwaitingFeedback</b> call. This call retrieves all completed order line items for which the user (buyer or seller) still needs to leave Feedback for their order partner.
		/// </summary>
		/// 
		/// <param name="Sort">
		/// This field allows the user to control how the order line items are returned in the response.
		/// Valid values are:
		/// <br/><br/>
		/// <br/><b>EndTime</b>
		/// <br/><b>EndTimeDescending</b>
		/// <br/><b>FeedbackLeft</b>
		/// <br/><b>FeedbackLeftDescending</b>
		/// <br/><b>FeedbackReceived</b>
		/// <br/><b>FeedbackReceivedDescending</b>
		/// <br/><b>Title</b>
		/// <br/><b>TitleDescending</b>
		/// <br/><b>UserID</b>
		/// <br/><b>UserIDDescending</b>
		/// <br/><br/>
		/// The user can read the <a href="types/ItemSortTypeCodeType.html">ItemSortTypeCodeType</a> definition for more information on these sort values.
		/// </param>
		///
		/// <param name="Pagination">
		/// This container can be used if the user only wants to see a subset of order line item results. In this container, the user will specify the number of order line items to return per page of data, and will specify the specific page of data they want to view with each call.
		/// </param>
		///
		public PaginatedTransactionArrayType GetItemsAwaitingFeedback(ItemSortTypeCodeType Sort, PaginationType Pagination)
		{
			this.Sort = Sort;
			this.Pagination = Pagination;

			Execute();
			return ApiResponse.ItemsAwaitingFeedback;
		}


		/// <summary>
		/// For backward compatibility with old wrappers.
		/// </summary>
		public PaginatedTransactionArrayType GetItemsAwaitingFeedback(PaginationType Pagination)
		{
			this.Pagination = Pagination;
			Execute();
			return ItemsAwaitingFeedback;
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
		/// Gets or sets the <see cref="GetItemsAwaitingFeedbackRequestType"/> for this API call.
		/// </summary>
		public GetItemsAwaitingFeedbackRequestType ApiRequest
		{ 
			get { return (GetItemsAwaitingFeedbackRequestType) AbstractRequest; }
			set { AbstractRequest = value; }
		}

		/// <summary>
		/// Gets the <see cref="GetItemsAwaitingFeedbackResponseType"/> for this API call.
		/// </summary>
		public GetItemsAwaitingFeedbackResponseType ApiResponse
		{ 
			get { return (GetItemsAwaitingFeedbackResponseType) AbstractResponse; }
		}

		
 		/// <summary>
		/// Gets or sets the <see cref="GetItemsAwaitingFeedbackRequestType.Sort"/> of type <see cref="ItemSortTypeCodeType"/>.
		/// </summary>
		public ItemSortTypeCodeType Sort
		{ 
			get { return ApiRequest.Sort; }
			set { ApiRequest.Sort = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetItemsAwaitingFeedbackRequestType.Pagination"/> of type <see cref="PaginationType"/>.
		/// </summary>
		public PaginationType Pagination
		{ 
			get { return ApiRequest.Pagination; }
			set { ApiRequest.Pagination = value; }
		}
		
		
 		/// <summary>
		/// Gets the returned <see cref="GetItemsAwaitingFeedbackResponseType.ItemsAwaitingFeedback"/> of type <see cref="PaginatedTransactionArrayType"/>.
		/// </summary>
		public PaginatedTransactionArrayType ItemsAwaitingFeedback
		{ 
			get { return ApiResponse.ItemsAwaitingFeedback; }
		}
		

		#endregion

		
	}
}
