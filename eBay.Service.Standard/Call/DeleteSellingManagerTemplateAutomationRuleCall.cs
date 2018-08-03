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
	public class DeleteSellingManagerTemplateAutomationRuleCall : ApiCall
	{

		#region Constructors
		/// <summary>
		/// 
		/// </summary>
		public DeleteSellingManagerTemplateAutomationRuleCall()
		{
			ApiRequest = new DeleteSellingManagerTemplateAutomationRuleRequestType();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ApiContext">The <see cref="ApiCall.ApiContext"/> for this API Call of type <see cref="ApiContext"/>.</param>
		public DeleteSellingManagerTemplateAutomationRuleCall(ApiContext ApiContext)
		{
			ApiRequest = new DeleteSellingManagerTemplateAutomationRuleRequestType();
			this.ApiContext = ApiContext;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Removes the association of Selling Manager automation rules
		/// to a template. Returns the remaining rules in the response.
		/// This call is subject to change without notice; the
		/// deprecation process is inapplicable to this call.
		/// </summary>
		/// 
		/// <param name="SaleTemplateID">
		/// The unique identifier of the Selling Manager template from which you want to remove automation rules.
		/// You can obtain a <b>SaleTemplateID</b> by calling <b>GetSellingManagerInventory</b>.
		/// </param>
		///
		/// <param name="DeleteAutomatedListingRule">
		/// This field is included and set to <code>true</code> if the user would like to disable the automated listing rule for the Selling Manager template.
		/// </param>
		///
		/// <param name="DeleteAutomatedRelistingRule">
		/// This field is included and set to <code>true</code> if the user would like to disable the automated relisting rule for the Selling Manager template.
		/// </param>
		///
		/// <param name="DeleteAutomatedSecondChanceOfferRule">
		/// This field is included and set to <code>true</code> if the user would like to disable the automated Second Chance Offer rule for the Selling Manager template.
		/// </param>
		///
		public SellingManagerAutoListType DeleteSellingManagerTemplateAutomationRule(long SaleTemplateID, bool DeleteAutomatedListingRule, bool DeleteAutomatedRelistingRule, bool DeleteAutomatedSecondChanceOfferRule)
		{
			this.SaleTemplateID = SaleTemplateID;
			this.DeleteAutomatedListingRule = DeleteAutomatedListingRule;
			this.DeleteAutomatedRelistingRule = DeleteAutomatedRelistingRule;
			this.DeleteAutomatedSecondChanceOfferRule = DeleteAutomatedSecondChanceOfferRule;

			Execute();
			return ApiResponse.AutomatedListingRule;
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
		/// Gets or sets the <see cref="DeleteSellingManagerTemplateAutomationRuleRequestType"/> for this API call.
		/// </summary>
		public DeleteSellingManagerTemplateAutomationRuleRequestType ApiRequest
		{ 
			get { return (DeleteSellingManagerTemplateAutomationRuleRequestType) AbstractRequest; }
			set { AbstractRequest = value; }
		}

		/// <summary>
		/// Gets the <see cref="DeleteSellingManagerTemplateAutomationRuleResponseType"/> for this API call.
		/// </summary>
		public DeleteSellingManagerTemplateAutomationRuleResponseType ApiResponse
		{ 
			get { return (DeleteSellingManagerTemplateAutomationRuleResponseType) AbstractResponse; }
		}

		
 		/// <summary>
		/// Gets or sets the <see cref="DeleteSellingManagerTemplateAutomationRuleRequestType.SaleTemplateID"/> of type <see cref="long"/>.
		/// </summary>
		public long SaleTemplateID
		{ 
			get { return ApiRequest.SaleTemplateID; }
			set { ApiRequest.SaleTemplateID = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="DeleteSellingManagerTemplateAutomationRuleRequestType.DeleteAutomatedListingRule"/> of type <see cref="bool"/>.
		/// </summary>
		public bool DeleteAutomatedListingRule
		{ 
			get { return ApiRequest.DeleteAutomatedListingRule; }
			set { ApiRequest.DeleteAutomatedListingRule = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="DeleteSellingManagerTemplateAutomationRuleRequestType.DeleteAutomatedRelistingRule"/> of type <see cref="bool"/>.
		/// </summary>
		public bool DeleteAutomatedRelistingRule
		{ 
			get { return ApiRequest.DeleteAutomatedRelistingRule; }
			set { ApiRequest.DeleteAutomatedRelistingRule = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="DeleteSellingManagerTemplateAutomationRuleRequestType.DeleteAutomatedSecondChanceOfferRule"/> of type <see cref="bool"/>.
		/// </summary>
		public bool DeleteAutomatedSecondChanceOfferRule
		{ 
			get { return ApiRequest.DeleteAutomatedSecondChanceOfferRule; }
			set { ApiRequest.DeleteAutomatedSecondChanceOfferRule = value; }
		}
		
		
 		/// <summary>
		/// Gets the returned <see cref="DeleteSellingManagerTemplateAutomationRuleResponseType.AutomatedListingRule"/> of type <see cref="SellingManagerAutoListType"/>.
		/// </summary>
		public SellingManagerAutoListType AutomatedListingRule
		{ 
			get { return ApiResponse.AutomatedListingRule; }
		}
		
 		/// <summary>
		/// Gets the returned <see cref="DeleteSellingManagerTemplateAutomationRuleResponseType.AutomatedRelistingRule"/> of type <see cref="SellingManagerAutoRelistType"/>.
		/// </summary>
		public SellingManagerAutoRelistType AutomatedRelistingRule
		{ 
			get { return ApiResponse.AutomatedRelistingRule; }
		}
		
 		/// <summary>
		/// Gets the returned <see cref="DeleteSellingManagerTemplateAutomationRuleResponseType.AutomatedSecondChanceOfferRule"/> of type <see cref="SellingManagerAutoSecondChanceOfferType"/>.
		/// </summary>
		public SellingManagerAutoSecondChanceOfferType AutomatedSecondChanceOfferRule
		{ 
			get { return ApiResponse.AutomatedSecondChanceOfferRule; }
		}
		
 		/// <summary>
		/// Gets the returned <see cref="DeleteSellingManagerTemplateAutomationRuleResponseType.Fees"/> of type <see cref="FeeTypeCollection"/>.
		/// </summary>
		public FeeType[] FeeList
		{ 
			get { return ApiResponse.Fees; }
		}
		

		#endregion

		
	}
}
