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
    public class GetOrderTransactionsCall : ApiCall
    {

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        public GetOrderTransactionsCall()
        {
            ApiRequest = new GetOrderTransactionsRequestType();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ApiContext">The <see cref="ApiCall.ApiContext"/> for this API Call of type <see cref="ApiContext"/>.</param>
        public GetOrderTransactionsCall(ApiContext ApiContext)
        {
            ApiRequest = new GetOrderTransactionsRequestType();
            this.ApiContext = ApiContext;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// The base request type for the <b>GetOrderTransactions</b> call. This call retrieves detailed information about one or more orders. All recent orders can be retrieved, or the seller can search based on <b>OrderID</b> value(s), <b>ItemID</b> value(s), <b>OrderLineItemID</b> value(s), or by <b>SKU</b> value(s).
        /// </summary>
        /// 
        /// <param name="ItemTransactionIDArrayList">
        /// This container is used if the seller wants to search for one or more specific order line items. An <b>ItemTransactionID</b> container is required for each order line item that is to be retrieved.  An order line item can be identified with an <b>ItemID</b>/<b>TransactionID</b> pair, with a <b>OrderLineItemID</b> value, or with a <b>SKU</b> value (if a SKU exists for the order line item).
        /// </param>
        ///
        /// <param name="OrderIDArrayList">
        /// This container is used if the seller wants to search for one or more specific orders. An <b>OrderID</b> field is required for each order that is to be retrieved.  Up to 20 <b>OrderID</b> fields can be used.
        /// </param>
        ///
        /// <param name="Platform">
        /// <span class="tablenote"><b>Note: </b> This field's purpose is to allow the seller to retrieve only eBay listings or only Half.com listings instead of both order types. Since the Half.com site has been shut down, this field is no longer necessary to use since eBay orders will be the only orders that are retrieved.
        /// </span>
        /// The default behavior of <b>GetOrderTransactions</b> is to retrieve all orders originating from eBay.com and Half.com. If the user wants to retrieve only eBay.com order line items or Half.com order line items, this filter can be used to perform that function. Inserting <code>eBay</code> into this field will restrict retrieved order line items to those originating on eBay.com, and inserting <code>Half</code> into this field will restrict retrieved order line items to those originating on Half.com.
        /// </param>
        ///
        /// <param name="IncludeFinalValueFees">
        /// This field is included and set to <code>true</code> if the seller wishes to include the Final Value Fee (FVF) for each order in the response. A Final Value Fee is calculated based on total amount of the sale, including the final price of the item and shipping/handling charges. This fee is charged to a seller's account immediately upon creation of an order line item.
        /// </param>
        ///
        public List<OrderType> GetOrderTransactions(List<ItemTransactionIDType> ItemTransactionIDArrayList, List<string> OrderIDArrayList, TransactionPlatformCodeType Platform, bool IncludeFinalValueFees)
        {
            this.ItemTransactionIDArrayList = ItemTransactionIDArrayList;
            this.OrderIDArrayList = OrderIDArrayList;
            this.Platform = Platform;
            this.IncludeFinalValueFees = IncludeFinalValueFees;

            Execute();
            return ApiResponse.OrderArray;
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
        /// Gets or sets the <see cref="GetOrderTransactionsRequestType"/> for this API call.
        /// </summary>
        public GetOrderTransactionsRequestType ApiRequest
        {
            get { return (GetOrderTransactionsRequestType)AbstractRequest; }
            set { AbstractRequest = value; }
        }

        /// <summary>
        /// Gets the <see cref="GetOrderTransactionsResponseType"/> for this API call.
        /// </summary>
        public GetOrderTransactionsResponseType ApiResponse
        {
            get { return (GetOrderTransactionsResponseType)AbstractResponse; }
        }


        /// <summary>
        /// Gets or sets the <see cref="GetOrderTransactionsRequestType.ItemTransactionIDArray"/> of type <see cref="List<ItemTransactionIDType>"/>.
        /// </summary>
        public List<ItemTransactionIDType> ItemTransactionIDArrayList
        {
            get { return ApiRequest.ItemTransactionIDArray; }
            set { ApiRequest.ItemTransactionIDArray =value; }
        }

        /// <summary>
        /// Gets or sets the <see cref="GetOrderTransactionsRequestType.OrderIDArray"/> of type <see cref="List<string>"/>.
        /// </summary>
        public List<string> OrderIDArrayList
        {
            get { return ApiRequest.OrderIDArray; }
            set { ApiRequest.OrderIDArray = value; }
        }

        /// <summary>
        /// Gets or sets the <see cref="GetOrderTransactionsRequestType.Platform"/> of type <see cref="TransactionPlatformCodeType"/>.
        /// </summary>
        public TransactionPlatformCodeType Platform
        {
            get { return ApiRequest.Platform.Value; }
            set { ApiRequest.Platform = value; }
        }

        /// <summary>
        /// Gets or sets the <see cref="GetOrderTransactionsRequestType.IncludeFinalValueFees"/> of type <see cref="bool"/>.
        /// </summary>
        public bool IncludeFinalValueFees
        {
            get { return ApiRequest.IncludeFinalValueFees.Value; }
            set { ApiRequest.IncludeFinalValueFees = value; }
        }
        /// <summary>
        /// Retrieves information about one or more orders or one or more transactions
        /// (or both), thus displacing the need to call GetOrders and GetItemTransactions
        /// separately.
        /// </summary>
        /// 
        /// <param name="ItemTransactionIDArrayList">
        /// An array of ItemTransactionIDs.
        /// </param>
        ///
        public List<OrderType> GetOrderTransactions(List<ItemTransactionIDType> ItemTransactionIDArrayList)
        {
            this.ItemTransactionIDArrayList = ItemTransactionIDArrayList;
            this.OrderIDArrayList = null;

            Execute();
            return ApiResponse.OrderArray;
        }
        /// <summary>
        /// Retrieves information about one or more orders or one or more transactions
        /// (or both), thus displacing the need to call GetOrders and GetItemTransactions
        /// separately.
        /// </summary>
        /// 
        /// <param name="OrderIDArrayList">
        /// An array of OrderIDs.
        /// </param>
        ///
        public List<OrderType> GetOrderTransactions(List<string> OrderIDArrayList)
        {
            this.ItemTransactionIDArrayList = null;
            this.OrderIDArrayList = OrderIDArrayList;

            Execute();
            return ApiResponse.OrderArray;
        }

        /// <summary>
        /// Gets the returned <see cref="GetOrderTransactionsResponseType.OrderArray"/> of type <see cref="List<OrderType>"/>.
        /// </summary>
        public List<OrderType> OrderList
        {
            get { return ApiResponse.OrderArray; }
        }


        #endregion


    }
}
