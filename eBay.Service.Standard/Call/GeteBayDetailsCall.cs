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
	public class GeteBayDetailsCall : ApiCall
	{

		#region Constructors
		/// <summary>
		/// 
		/// </summary>
		public GeteBayDetailsCall()
		{
			ApiRequest = new GeteBayDetailsRequestType();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ApiContext">The <see cref="ApiCall.ApiContext"/> for this API Call of type <see cref="ApiContext"/>.</param>
		public GeteBayDetailsCall(ApiContext ApiContext)
		{
			ApiRequest = new GeteBayDetailsRequestType();
			this.ApiContext = ApiContext;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// This is the base request type for the <b>GeteBayDetails</b> call. This call retrieves the latest eBay feature-related metadata values that are supported when listing items. This metadata includes country codes, currency codes, Item Specifics thresholds, supported Return Policy values, available shipping carriers and shipping service options, and more. This call may be used to keep metadata up-to-date in your applications.
		/// 
		/// In some cases, the data returned in the response will vary according to the eBay site that you use for the request.
		/// </summary>
		/// 
		/// <param name="DetailNameList">
		/// One or more <b>DetailName</b> fields may be used to control the the type of metadata that is returned in the response. If no <b>DetailName</b> fields are used, all metadata will be returned in the response. It is a good idea to familiarize yourself with the metadata that can be returned with <b>GeteBayDetails</b> by reading through the enumeration values in <a href="types/DetailNameCodeType.html">DetailNameCodeType</a>.
		/// </param>
		///
		public void GeteBayDetails(List<DetailNameCodeType?> DetailNameList)
		{
			this.DetailNameList = DetailNameList;

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
		/// Gets or sets the <see cref="GeteBayDetailsRequestType"/> for this API call.
		/// </summary>
		public GeteBayDetailsRequestType ApiRequest
		{ 
			get { return (GeteBayDetailsRequestType) AbstractRequest; }
			set { AbstractRequest = value; }
		}

		/// <summary>
		/// Gets the <see cref="GeteBayDetailsResponseType"/> for this API call.
		/// </summary>
		public GeteBayDetailsResponseType ApiResponse
		{ 
			get { return (GeteBayDetailsResponseType) AbstractResponse; }
		}


        /// <summary>
        /// Gets or sets the <see cref="GeteBayDetailsRequestType.DetailName"/> of type <see cref="List<DetailNameCodeType>"/>.
        /// </summary>
        public List<DetailNameCodeType?> DetailNameList
		{ 
			get { return ApiRequest.DetailName; }
			set { ApiRequest.DetailName = value; }
		}


        /// <summary>
        /// Gets the returned <see cref="GeteBayDetailsResponseType.CountryDetails"/> of type <see cref="List<CountryDetailsType>"/>.
        /// </summary>
        public List<CountryDetailsType> CountryDetailList
		{ 
			get { return ApiResponse.CountryDetails; }
		}

/// <summary>
/// Gets the returned <see cref="GeteBayDetailsResponseType.CurrencyDetails"/> of type <see cref="List<CurrencyDetailsType>"/>.
/// </summary>
public List<CurrencyDetailsType> CurrencyDetailList
		{ 
			get { return ApiResponse.CurrencyDetails; }
		}

        /// <summary>
        /// Gets the returned <see cref="GeteBayDetailsResponseType.DispatchTimeMaxDetails"/> of type <see cref="List<DispatchTimeMaxDetailsType>"/>.
        /// </summary>
        public List<DispatchTimeMaxDetailsType> DispatchTimeMaxDetailList
		{ 
			get { return ApiResponse.DispatchTimeMaxDetails; }
		}

        /// <summary>
        /// Gets the returned <see cref="GeteBayDetailsResponseType.PaymentOptionDetails"/> of type <see cref="List<PaymentOptionDetailsType>"/>.
        /// </summary>
        public List<PaymentOptionDetailsType> PaymentOptionDetailList
		{ 
			get { return ApiResponse.PaymentOptionDetails; }
		}

/// <summary>
/// Gets the returned <see cref="GeteBayDetailsResponseType.RegionDetails"/> of type <see cref="List<RegionDetailsType>"/>.
/// </summary>
public List<RegionDetailsType> RegionDetailList
		{ 
			get { return ApiResponse.RegionDetails; }
		}
		
 		/// <summary>
		/// Gets the returned <see cref="GeteBayDetailsResponseType.ShippingLocationDetails"/> of type <see cref="List<ShippingLocationDetailsType>"/>.
		/// </summary>
		public List<ShippingLocationDetailsType> ShippingLocationDetailList
		{ 
			get { return ApiResponse.ShippingLocationDetails; }
		}
		
 		/// <summary>
		/// Gets the returned <see cref="GeteBayDetailsResponseType.ShippingServiceDetails"/> of type <see cref="List<ShippingServiceDetailsType>"/>.
		/// </summary>
		public List<ShippingServiceDetailsType> ShippingServiceDetailList
		{ 
			get { return ApiResponse.ShippingServiceDetails; }
		}
		
 		/// <summary>
		/// Gets the returned <see cref="GeteBayDetailsResponseType.SiteDetails"/> of type <see cref="List<SiteDetailsType>"/>.
		/// </summary>
		public List<SiteDetailsType> SiteDetailList
		{ 
			get { return ApiResponse.SiteDetails; }
		}
		
 		/// <summary>
		/// Gets the returned <see cref="GeteBayDetailsResponseType.TaxJurisdiction"/> of type <see cref="List<TaxJurisdictionType>"/>.
		/// </summary>
		public List<TaxJurisdictionType> TaxJurisdictionList
		{ 
			get { return ApiResponse.TaxJurisdiction; }
		}
		
 		/// <summary>
		/// Gets the returned <see cref="GeteBayDetailsResponseType.URLDetails"/> of type <see cref="List<URLDetailsType>"/>.
		/// </summary>
		public List<URLDetailsType> URLDetailList
		{ 
			get { return ApiResponse.URLDetails; }
		}
		
 		/// <summary>
		/// Gets the returned <see cref="GeteBayDetailsResponseType.TimeZoneDetails"/> of type <see cref="List<TimeZoneDetailsType>"/>.
		/// </summary>
		public List<TimeZoneDetailsType> TimeZoneDetailList
		{ 
			get { return ApiResponse.TimeZoneDetails; }
		}
		
 		/// <summary>
		/// Gets the returned <see cref="GeteBayDetailsResponseType.ItemSpecificDetails"/> of type <see cref="List<ItemSpecificDetailsType>"/>.
		/// </summary>
		public List<ItemSpecificDetailsType> ItemSpecificDetailList
		{ 
			get { return ApiResponse.ItemSpecificDetails; }
		}
		
 		/// <summary>
		/// Gets the returned <see cref="GeteBayDetailsResponseType.UnitOfMeasurementDetails"/> of type <see cref="List<UnitOfMeasurementDetailsType>"/>.
		/// </summary>
		public List<UnitOfMeasurementDetailsType> UnitOfMeasurementDetailList
		{ 
			get { return ApiResponse.UnitOfMeasurementDetails; }
		}
		
 		/// <summary>
		/// Gets the returned <see cref="GeteBayDetailsResponseType.RegionOfOriginDetails"/> of type <see cref="List<RegionOfOriginDetailsType>"/>.
		/// </summary>
		public List<RegionOfOriginDetailsType> RegionOfOriginDetailList
		{ 
			get { return ApiResponse.RegionOfOriginDetails; }
		}
		
 		/// <summary>
		/// Gets the returned <see cref="GeteBayDetailsResponseType.ShippingPackageDetails"/> of type <see cref="List<ShippingPackageDetailsType>"/>.
		/// </summary>
		public List<ShippingPackageDetailsType> ShippingPackageDetailList
		{ 
			get { return ApiResponse.ShippingPackageDetails; }
		}
		
 		/// <summary>
		/// Gets the returned <see cref="GeteBayDetailsResponseType.ShippingCarrierDetails"/> of type <see cref="List<ShippingCarrierDetailsType>"/>.
		/// </summary>
		public List<ShippingCarrierDetailsType> ShippingCarrierDetailList
		{ 
			get { return ApiResponse.ShippingCarrierDetails; }
		}
		
 		/// <summary>
		/// Gets the returned <see cref="GeteBayDetailsResponseType.ReturnPolicyDetails"/> of type <see cref="ReturnPolicyDetailsType"/>.
		/// </summary>
		public ReturnPolicyDetailsType ReturnPolicyDetails
		{ 
			get { return ApiResponse.ReturnPolicyDetails; }
		}
		
 		/// <summary>
		/// Gets the returned <see cref="GeteBayDetailsResponseType.InternationalReturnPolicyDetails"/> of type <see cref="ReturnPolicyDetailsType"/>.
		/// </summary>
		public ReturnPolicyDetailsType InternationalReturnPolicyDetails
		{ 
			get { return ApiResponse.InternationalReturnPolicyDetails; }
		}

/// <summary>
/// Gets the returned <see cref="GeteBayDetailsResponseType.ListingStartPriceDetails"/> of type <see cref="List<ListingStartPriceDetailsType>"/>.
/// </summary>
public List<ListingStartPriceDetailsType> ListingStartPriceDetailList
		{ 
			get { return ApiResponse.ListingStartPriceDetails; }
		}
		
 		/// <summary>
		/// Gets the returned <see cref="GeteBayDetailsResponseType.BuyerRequirementDetails"/> of type <see cref="List<SiteBuyerRequirementDetailsType>"/>.
		/// </summary>
		public List<SiteBuyerRequirementDetailsType> BuyerRequirementDetailList
		{ 
			get { return ApiResponse.BuyerRequirementDetails; }
		}
		
 		/// <summary>
		/// Gets the returned <see cref="GeteBayDetailsResponseType.ListingFeatureDetails"/> of type <see cref="List<ListingFeatureDetailsType>"/>.
		/// </summary>
		public List<ListingFeatureDetailsType> ListingFeatureDetailList
		{ 
			get { return ApiResponse.ListingFeatureDetails; }
		}
		
 		/// <summary>
		/// Gets the returned <see cref="GeteBayDetailsResponseType.VariationDetails"/> of type <see cref="VariationDetailsType"/>.
		/// </summary>
		public VariationDetailsType VariationDetails
		{ 
			get { return ApiResponse.VariationDetails; }
		}

/// <summary>
/// Gets the returned <see cref="GeteBayDetailsResponseType.ExcludeShippingLocationDetails"/> of type <see cref="List<ExcludeShippingLocationDetailsType>"/>.
/// </summary>
public List<ExcludeShippingLocationDetailsType> ExcludeShippingLocationDetailList
		{ 
			get { return ApiResponse.ExcludeShippingLocationDetails; }
		}
		
 		/// <summary>
		/// Gets the returned <see cref="GeteBayDetailsResponseType.UpdateTime"/> of type <see cref="DateTime"/>.
		/// </summary>
		public DateTime UpdateTime
		{ 
			get { return ApiResponse.UpdateTime.Value; }
		}

/// <summary>
/// Gets the returned <see cref="GeteBayDetailsResponseType.RecoupmentPolicyDetails"/> of type <see cref="List<RecoupmentPolicyDetailsType>"/>.
/// </summary>
public List<RecoupmentPolicyDetailsType> RecoupmentPolicyDetailList
		{ 
			get { return ApiResponse.RecoupmentPolicyDetails; }
		}
		
 		/// <summary>
		/// Gets the returned <see cref="GeteBayDetailsResponseType.ShippingCategoryDetails"/> of type <see cref="List<ShippingCategoryDetailsType>"/>.
		/// </summary>
		public List<ShippingCategoryDetailsType> ShippingCategoryDetailList
		{ 
			get { return ApiResponse.ShippingCategoryDetails; }
		}
		
 		/// <summary>
		/// Gets the returned <see cref="GeteBayDetailsResponseType.ProductDetails"/> of type <see cref="ProductDetailsType"/>.
		/// </summary>
		public ProductDetailsType ProductDetails
		{ 
			get { return ApiResponse.ProductDetails; }
		}
		

		#endregion

		
	}
}
