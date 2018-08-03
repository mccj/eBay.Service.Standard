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
	public class SendInvoiceCall : ApiCall
	{

		#region Constructors
		/// <summary>
		/// 
		/// </summary>
		public SendInvoiceCall()
		{
			ApiRequest = new SendInvoiceRequestType();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ApiContext">The <see cref="ApiCall.ApiContext"/> for this API Call of type <see cref="ApiContext"/>.</param>
		public SendInvoiceCall(ApiContext ApiContext)
		{
			ApiRequest = new SendInvoiceRequestType();
			this.ApiContext = ApiContext;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Enables a seller to send an order invoice to a buyer. Where applicable, updates
		/// to shipping, payment methods, and sales tax made in this request are applied to
		/// the specified order as a whole and to the individual order line items whose data
		/// are stored in individual <b>Transaction</b> objects.
		/// </summary>
		/// 
		/// <param name="ItemID">
		/// Unique identifier for an eBay listing. Unless <b>OrderID</b> or
		/// <b>OrderLineItemID</b> is provided in the request, the <b>ItemID</b> (or <b>SKU</b>) is
		/// required and must be paired with the corresponding <b>TransactionID</b> to
		/// identify a single line item order. For a multiple line item (Combined Invoice) order, <b>OrderID</b> should be used.
		/// </param>
		///
		/// <param name="TransactionID">
		/// Unique identifier for an eBay order line item. An order line item is created once there is a commitment from a buyer to purchase an item. Since an auction listing can only have one order line item during the duration of the listing, the <b>TransactionID</b> for auction listings is always <code>0</code>. Unless <b>OrderID</b> or <b>OrderLineItemID</b> is provided in the request, the <b>TransactionID</b> is required and must be paired with the corresponding <b>ItemID</b> to identify a single line item order. For a multiple line item (Combined Invoice) order, <b>OrderID</b> should be used.
		/// </param>
		///
		/// <param name="OrderID">
		/// A unique identifier that identifies a single line item or multiple line
		/// item (Combined Invoice) order.
		/// 
		/// For a single line item order, the <b>OrderID</b> value is identical to the
		/// <b>OrderLineItemID</b> value that is generated upon creation of the order line
		/// item. For a Combined Invoice order, the <b>OrderID</b> value is created by eBay
		/// when the buyer or seller (sharing multiple, common order line items)
		/// combines multiple order line items into a Combined Invoice order. A
		/// Combined Invoice order can also be created by the seller through the
		/// <b>AddOrder</b> call.
		/// 
		/// Unless the <b>ItemID</b> (or SKU) and corresponding <b>TransactionID</b>, or the
		/// <b>OrderLineItemID</b> is provided in the request to identify a single line
		/// item order, the <b>OrderID</b> must be specified. If <b>OrderID</b> is specified,
		/// <b>OrderLineItemID</b>, <b>ItemID</b>, <b>TransactionID</b>, and <b>SKU</b> are ignored if present
		/// in the same request.
		/// </param>
		///
		/// <param name="InternationalShippingServiceOptionsList">
		/// If the buyer has an International shipping address, use this container
		/// to offer up to four International shipping services (or five if one of them is a Global Shipping Program service). If International
		/// shipping services are offered, (domestic) <b>ShippingServiceOptions</b> should
		/// not be included in the request.
		/// 
		/// </param>
		///
		/// <param name="ShippingServiceOptionsList">
		/// If the buyer has a domestic shipping address, use this container
		/// to offer up to four domestic shipping services. If domestic
		/// shipping services are offered, <b>InternationalShippingServiceOptions</b> should
		/// not be included in the request.
		/// 
		/// </param>
		///
		/// <param name="SalesTax">
		/// This container is used if the seller wishes to apply sales tax to the order. The amount of sales tax applied to the order is dependent on the sales tax rate in the buyer's state and whether sales tax is being applied to the cost of the order only or the cost of the order plus shipping and handling.
		/// </param>
		///
		/// <param name="InsuranceOption">
		/// This field is no longer applicable as it is not longer possible for a seller to offer a buyer shipping insurance.
		/// </param>
		///
		/// <param name="InsuranceFee">
		/// This field is no longer applicable as it is not longer possible for a seller to offer a buyer shipping insurance.
		/// </param>
		///
		/// <param name="PaymentMethodsList">
		/// This optional field allows a US or German seller to add specific payment methods that were not in the original item listing. The only valid values for this field are 'PayPal' for a US listing, or 'MoneyXferAcceptedInCheckout' (CIP+) for a DE listing.
		/// </param>
		///
		/// <param name="PayPalEmailAddress">
		/// If the <b>PaymentMethods</b> field is used and set to <code>PayPal</code>, the seller provides his/her PayPal email address in this field.
		/// </param>
		///
		/// <param name="CheckoutInstructions">
		/// This field allows the seller to provide a message or instructions
		/// regarding checkout/payment, or the return policy.
		/// </param>
		///
		/// <param name="EmailCopyToSeller">
		/// Flag indicating whether or not the seller wishes to receive an email copy of the invoice sent to the buyer.
		/// </param>
		///
		/// <param name="CODCost">
		/// This dollar value indicates the money due from the buyer upon delivery of the item.
		/// 
		/// This field should only be specified in the <b>SendInvoice</b> request if 'COD'
		/// (cash-on-delivery) was the payment method selected by the buyer and it is included
		/// as the <b>PaymentMethods</b> value in the same request.
		/// </param>
		///
		/// <param name="SKU">
		/// The seller's unique identifier for an item that is being tracked by this
		/// SKU. If <b>OrderID</b> or <b>OrderLineItemID</b> are not provided, both <b>SKU</b> (or
		/// <b>ItemID</b>) and corresponding <b>TransactionID</b> must be provided to uniquely
		/// identify a single line item order. For a multiple line item (Combined
		/// Payment) order, <b>OrderID</b> must be used.
		/// 
		/// 
		/// This field can only be used if the <b>Item.InventoryTrackingMethod</b> field
		/// (set with the <b>AddFixedPriceItem</b> or <b>RelistFixedPriceItem</b> calls) is set to
		/// SKU.
		/// </param>
		///
		/// <param name="OrderLineItemID">
		/// A unique identifier for an eBay order line item. This field is created
		/// as soon as there is a commitment to buy from the seller, and its value
		/// is based upon the concatenation of <b>ItemID</b> and <b>TransactionID</b>, with a
		/// hyphen in between these two IDs.
		/// 
		/// 
		/// Unless the <b>ItemID</b> (or <b>SKU</b>) and corresponding <b>TransactionID</b> is used to
		/// identify a single line item order, or the <b>OrderID</b> is used to identify a
		/// single or multiple line item (Combined Invoice) order, the
		/// <b>OrderLineItemID</b> must be specified. For a multiple line item (Combined Invoice) order, <b>OrderID</b> should be used. If <b>OrderLineItemID</b> is specified,
		/// <b>ItemID</b>, <b>TransactionID</b>, and <b>SKU</b> are ignored if present in the same
		/// request.
		/// </param>
		///
		/// <param name="AdjustmentAmount">
		/// This field allows the seller to adjust the total cost of the order to account for an extra charge or to pass down a discount to the buyer.  The currency used in this field must be the same currency of the listing site. A positive value in this field indicates that the amount is an extra charge being paid to the seller by the buyer, and a negative value indicates that the amount is a discount given to the buyer by the seller.
		/// </param>
		///
		public void SendInvoice(string ItemID, string TransactionID, string OrderID, InternationalShippingServiceOptionsType[] InternationalShippingServiceOptionsList, ShippingServiceOptionsType[] ShippingServiceOptionsList, SalesTaxType SalesTax, InsuranceOptionCodeType InsuranceOption, AmountType InsuranceFee, BuyerPaymentMethodCodeType[] PaymentMethodsList, string PayPalEmailAddress, string CheckoutInstructions, bool EmailCopyToSeller, AmountType CODCost, string SKU, string OrderLineItemID, AmountType AdjustmentAmount)
		{
			this.ItemID = ItemID;
			this.TransactionID = TransactionID;
			this.OrderID = OrderID;
			this.InternationalShippingServiceOptionsList = InternationalShippingServiceOptionsList;
			this.ShippingServiceOptionsList = ShippingServiceOptionsList;
			this.SalesTax = SalesTax;
			this.InsuranceOption = InsuranceOption;
			this.InsuranceFee = InsuranceFee;
			this.PaymentMethodsList = PaymentMethodsList;
			this.PayPalEmailAddress = PayPalEmailAddress;
			this.CheckoutInstructions = CheckoutInstructions;
			this.EmailCopyToSeller = EmailCopyToSeller;
			this.CODCost = CODCost;
			this.SKU = SKU;
			this.OrderLineItemID = OrderLineItemID;
			this.AdjustmentAmount = AdjustmentAmount;

			Execute();
			
		}


		/// <summary>
		/// For backward compatibility with old wrappers.
		/// </summary>
		public void SendInvoice(string ItemID, string TransactionID, ShippingServiceOptionsType[] ShippingServiceOptionsList)
		{
			this.ItemID = ItemID;
			this.TransactionID = TransactionID;
			this.ShippingServiceOptionsList = ShippingServiceOptionsList;
			Execute();
		}
		/// <summary>
		/// For backward compatibility with old wrappers.
		/// </summary>
		public void SendInvoice(string OrderID)
		{
			this.OrderID = OrderID;
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
		/// Gets or sets the <see cref="SendInvoiceRequestType"/> for this API call.
		/// </summary>
		public SendInvoiceRequestType ApiRequest
		{ 
			get { return (SendInvoiceRequestType) AbstractRequest; }
			set { AbstractRequest = value; }
		}

		/// <summary>
		/// Gets the <see cref="SendInvoiceResponseType"/> for this API call.
		/// </summary>
		public SendInvoiceResponseType ApiResponse
		{ 
			get { return (SendInvoiceResponseType) AbstractResponse; }
		}

		
 		/// <summary>
		/// Gets or sets the <see cref="SendInvoiceRequestType.ItemID"/> of type <see cref="string"/>.
		/// </summary>
		public string ItemID
		{ 
			get { return ApiRequest.ItemID; }
			set { ApiRequest.ItemID = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="SendInvoiceRequestType.TransactionID"/> of type <see cref="string"/>.
		/// </summary>
		public string TransactionID
		{ 
			get { return ApiRequest.TransactionID; }
			set { ApiRequest.TransactionID = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="SendInvoiceRequestType.OrderID"/> of type <see cref="string"/>.
		/// </summary>
		public string OrderID
		{ 
			get { return ApiRequest.OrderID; }
			set { ApiRequest.OrderID = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="SendInvoiceRequestType.InternationalShippingServiceOptions"/> of type <see cref="InternationalShippingServiceOptionsTypeCollection"/>.
		/// </summary>
		public InternationalShippingServiceOptionsType[] InternationalShippingServiceOptionsList
		{ 
			get { return ApiRequest.InternationalShippingServiceOptions; }
			set { ApiRequest.InternationalShippingServiceOptions = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="SendInvoiceRequestType.ShippingServiceOptions"/> of type <see cref="ShippingServiceOptionsTypeCollection"/>.
		/// </summary>
		public ShippingServiceOptionsType[] ShippingServiceOptionsList
		{ 
			get { return ApiRequest.ShippingServiceOptions; }
			set { ApiRequest.ShippingServiceOptions = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="SendInvoiceRequestType.SalesTax"/> of type <see cref="SalesTaxType"/>.
		/// </summary>
		public SalesTaxType SalesTax
		{ 
			get { return ApiRequest.SalesTax; }
			set { ApiRequest.SalesTax = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="SendInvoiceRequestType.InsuranceOption"/> of type <see cref="InsuranceOptionCodeType"/>.
		/// </summary>
		public InsuranceOptionCodeType InsuranceOption
		{ 
			get { return ApiRequest.InsuranceOption; }
			set { ApiRequest.InsuranceOption = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="SendInvoiceRequestType.InsuranceFee"/> of type <see cref="AmountType"/>.
		/// </summary>
		public AmountType InsuranceFee
		{ 
			get { return ApiRequest.InsuranceFee; }
			set { ApiRequest.InsuranceFee = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="SendInvoiceRequestType.PaymentMethods"/> of type <see cref="BuyerPaymentMethodCodeTypeCollection"/>.
		/// </summary>
		public BuyerPaymentMethodCodeType[] PaymentMethodsList
		{ 
			get { return ApiRequest.PaymentMethods; }
			set { ApiRequest.PaymentMethods = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="SendInvoiceRequestType.PayPalEmailAddress"/> of type <see cref="string"/>.
		/// </summary>
		public string PayPalEmailAddress
		{ 
			get { return ApiRequest.PayPalEmailAddress; }
			set { ApiRequest.PayPalEmailAddress = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="SendInvoiceRequestType.CheckoutInstructions"/> of type <see cref="string"/>.
		/// </summary>
		public string CheckoutInstructions
		{ 
			get { return ApiRequest.CheckoutInstructions; }
			set { ApiRequest.CheckoutInstructions = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="SendInvoiceRequestType.EmailCopyToSeller"/> of type <see cref="bool"/>.
		/// </summary>
		public bool EmailCopyToSeller
		{ 
			get { return ApiRequest.EmailCopyToSeller; }
			set { ApiRequest.EmailCopyToSeller = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="SendInvoiceRequestType.CODCost"/> of type <see cref="AmountType"/>.
		/// </summary>
		public AmountType CODCost
		{ 
			get { return ApiRequest.CODCost; }
			set { ApiRequest.CODCost = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="SendInvoiceRequestType.SKU"/> of type <see cref="string"/>.
		/// </summary>
		public string SKU
		{ 
			get { return ApiRequest.SKU; }
			set { ApiRequest.SKU = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="SendInvoiceRequestType.OrderLineItemID"/> of type <see cref="string"/>.
		/// </summary>
		public string OrderLineItemID
		{ 
			get { return ApiRequest.OrderLineItemID; }
			set { ApiRequest.OrderLineItemID = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="SendInvoiceRequestType.AdjustmentAmount"/> of type <see cref="AmountType"/>.
		/// </summary>
		public AmountType AdjustmentAmount
		{ 
			get { return ApiRequest.AdjustmentAmount; }
			set { ApiRequest.AdjustmentAmount = value; }
		}
		
		

		#endregion

		
	}
}
