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
using System.Collections;
using eBay.Service.Core.Soap;
using eBay.Service.Core.Sdk;
using eBay.Service.Call;
using eBay.Service.Util;
using System.Runtime.InteropServices;
using System.Collections.Generic;
#endregion

namespace eBay.Service.Util
{
	/// <summary>
	/// Summary description for EBayDetailsHelper.
	/// </summary>
	public class EBayDetailsHelper 
	{
	#region Private Fields
	
		private static EBayDetailsHelper _helper;
		private ApiContext _apiContext;
		private SiteCodeType _site;
		private List<DetailNameCodeType> _siteIndependentDetailNames = new List<DetailNameCodeType>(new DetailNameCodeType[] {
																			   DetailNameCodeType.CountryDetails,
																			   DetailNameCodeType.CurrencyDetails, 
																			   DetailNameCodeType.DispatchTimeMaxDetails, 
																			   DetailNameCodeType.ShippingLocationDetails,
																			   DetailNameCodeType.SiteDetails, 
																			   DetailNameCodeType.TimeZoneDetails
																		   });
		private List<DetailNameCodeType>  _siteRelatedDetailNames = new List<DetailNameCodeType>(new DetailNameCodeType[] {
																		   DetailNameCodeType.PaymentOptionDetails, 
																		   DetailNameCodeType.RegionDetails,
																		   DetailNameCodeType.ShippingServiceDetails,
																		   DetailNameCodeType.TaxJurisdiction,
																		   DetailNameCodeType.URLDetails
																	   });

		/* Contains site related hash maps of details arrays by detail name */
		private static Hashtable _SiteRelatedDetailsByName = new Hashtable(5);
		/* Contains site related hash maps of details maps by detail name */
		private static Hashtable _SiteRelatedDetailsMapsByName = new Hashtable(5);
		/*
		 * Details about a specific country. 
		 * GeteBayDetails returns all countries in the system, 
		 * regardless of the site to which you sent the request. 
		 */
		private static Hashtable _CountryDetailsByCountry = new Hashtable();
		private List<CountryDetailsType> _countryDetails = null;

		/*
		 * Details about a specific currency that can be used for listing on an eBay site. 
		 * GeteBayDetails returns all site currencies in the system, 
		 * regardless of the site to which you sent the request
		 */
		private static Hashtable _CurrencyDetailsByCurrency = new Hashtable();
		private List<CurrencyDetailsType> _currencyDetails = null;

		/*
		 * Details about a specific max dispatch time. 
		 * A dispatch time specifies the maximum number of business days a seller commits to for shipping an item to domestic buyers after receiving a cleared payment. 
		 * GeteBayDetails returns all dispatch times in the system, 
		 * regardless of the site to which you sent the request
		 */
		private static Hashtable _DispatchTimeMaxDetailsByDispatchTimeMax = new Hashtable();
        private List<DispatchTimeMaxDetailsType>  _dispatchTimeMaxDetails = null;

		/*
		 * Details about a location or region to which the seller is willing to ship. 
		 * GeteBayDetails returns all shipping locations in the system, 
		 * regardless of the site to which you sent the request
		 */
		private static Hashtable _ShippingLocationDetailsByShippingLocation = new Hashtable();
        private List<ShippingLocationDetailsType>  _shippingLocationDetails = null;

		/*
		 * Details about a specific eBay site. 
		 * GeteBayDetails returns all sites in the system, 
		 * regardless of the site to which you sent the request. 
		 */
		private static Hashtable _SiteDetailsBySite = new Hashtable();
		private List<SiteDetailsType> _siteDetails = null;

		private static Hashtable _TimeZoneDetails = new Hashtable();
		private List<TimeZoneDetailsType> _timeZoneDetails = null;

		/* The following details are site specific: */
	
		/*
		 * Details about a specific buyer payment method. 
		 * GeteBayDetails only returns payment methods 
		 * that are applicable to the site to which you sent the request
		 */
		private static Hashtable _PaymentOptionDetailsMapsBySite = new Hashtable(); // details by payment method
		private static Hashtable _PaymentOptionDetailsBySite = new Hashtable();


	/*
	 * Details about a specific geographical region. 
	 * GeteBayDetails only returns regions that are applicable to the site to which you sent the request. 
	 * However, you should ignore region values for all sites except China.
	 */
//	private static Hashtable _RegionDetailsByRegionID = new Hashtable();
	private static Hashtable _RegionDetailsMapsBySite = new Hashtable();
	private static Hashtable _RegionDetailsBySite = new Hashtable();
	private List<RegionDetailsType> _regionDetails = null;
	
	
	/*
	 * Details about a specific shipping service. 
	 * GeteBayDetails only returns shipping services 
	 * that are applicable to the site to which you sent the request.
	 */
//	private static Hashtable _ShippingServiceDetailsByShippingServiceID = new Hashtable();
	private static Hashtable _ShippingServiceDetailsMapsBySite = new Hashtable();
	private static Hashtable _ShippingServiceDetailsBySite = new Hashtable();
	private List<ShippingServiceDetailsType> _shippingServiceDetails = null;
	
	/*
	 * Details about a specific tax jurisdiction or tax region. 
	 * GeteBayDetails only returns jurisdictions 
	 * that are applicable to the site to which you sent the request.
	 */
//	private static Hashtable _TaxJurisdictionDetailsByJurisdictionID = new Hashtable();
	private static Hashtable _TaxJurisdictionDetailsMapsBySite = new Hashtable();
	private static Hashtable _TaxJurisdictionDetailsBySite = new Hashtable();
	private List<TaxJurisdictionType> _taxJurisdictionDetails = null;
	
	/*
	 * URLDetails Details about a specific eBay URL. 
	 * GeteBayDetails only returns URLs 
	 * that are applicable to the site to which you sent the request.
	 */
	private static Hashtable _URLDetailsMapsBySite = new Hashtable();
	private static Hashtable _URLDetailsBySite = new Hashtable();

		#endregion
		#region Constructors
		/// <summary>
		/// 
		/// </summary>
		private EBayDetailsHelper(ApiContext context) 
		{
			_apiContext = context;
			_site = _apiContext.Site;
			loadSiteIndependentDetails();
			initializeHashMaps();
			loadSiteRelatedDetailsForSite(_site);
		}	
		private EBayDetailsHelper() 
		{
		}
		#endregion

		#region Public Methods

		/// <summary>
		/// 
		/// </summary>
		public static EBayDetailsHelper getInstance(ApiContext context) 
		{
			if(_helper == null) 
			{
				_helper = new EBayDetailsHelper(context);
			}
			return _helper;
		}

		/// <summary>
		/// 
		/// </summary>
	public List<PaymentOptionDetailsType> getPaymentOptionDetailsForSite(SiteCodeType site)  
	{
		Hashtable detalisMap = (Hashtable)_SiteRelatedDetailsByName[DetailNameCodeType.PaymentOptionDetails];
		if(!detalisMap.ContainsKey(site)) 
		{
			List<DetailNameCodeType> detailNames = new List<DetailNameCodeType>(new DetailNameCodeType[] {DetailNameCodeType.PaymentOptionDetails});
			loadPaymentOptionsDetailsForSite(site);
		}
		return (List<PaymentOptionDetailsType>)detalisMap[site];
	}

		/// <summary>
		/// 
		/// </summary>
	public PaymentOptionDetailsType getPaymentOptionDetailsBySiteAndPaymentMethod(SiteCodeType site, BuyerPaymentMethodCodeType paymentMethod)  
	{
		Hashtable detailsMap = (Hashtable)_SiteRelatedDetailsMapsByName[DetailNameCodeType.PaymentOptionDetails];
		if(!detailsMap.ContainsKey(site)) 
		{
			loadPaymentOptionsDetailsForSite(site);
		}
		Hashtable byPaymentMethodMap = (Hashtable)detailsMap[site];
		return (PaymentOptionDetailsType)byPaymentMethodMap[paymentMethod];
	}
		/// <summary>
		/// 
		/// </summary>
	public List<RegionDetailsType> getRegionDetailsForSite(SiteCodeType site)  
	{
		Hashtable detalisMap = (Hashtable)_SiteRelatedDetailsByName[DetailNameCodeType.RegionDetails];
		if(!detalisMap.ContainsKey(site)) 
		{
			List<DetailNameCodeType> detailNames = new List<DetailNameCodeType>(new DetailNameCodeType[] {DetailNameCodeType.RegionDetails});
			loadRegionDetailsForSite(site);
		}
		return (List<RegionDetailsType>)detalisMap[site];
	}
		/// <summary>
		/// 
		/// </summary>
	public RegionDetailsType getRegionDetailsBySiteAndRegionID(SiteCodeType site, String regionId)  
	{
		Hashtable detailsMap = (Hashtable)_SiteRelatedDetailsMapsByName[DetailNameCodeType.RegionDetails];
		if(!detailsMap.ContainsKey(site)) 
		{
			loadRegionDetailsForSite(site);
		}
		Hashtable byRegionIDMap = (Hashtable)detailsMap[site];
		return (RegionDetailsType)byRegionIDMap[regionId];
	}

		/// <summary>
		/// 
		/// </summary>
	public List<ShippingServiceDetailsType> getShippingServiceDetailsForSite(SiteCodeType site)  {
		Hashtable detalisMap = (Hashtable)_SiteRelatedDetailsByName[DetailNameCodeType.ShippingServiceDetails];
		if(!detalisMap.ContainsKey(site)) 
		{
			List<DetailNameCodeType> detailNames = new List<DetailNameCodeType>(new DetailNameCodeType[] {DetailNameCodeType.ShippingServiceDetails});
			loadShippingServiceDetailsForSite(site);
		}
		return (List<ShippingServiceDetailsType>)detalisMap[site];
	}
		/// <summary>
		/// 
		/// </summary>
	public ShippingServiceDetailsType getShippingServiceDetailsBySiteAndShippingServiceID(SiteCodeType site, int shippingServiceID)  
	{
		Hashtable detailsMap = (Hashtable)_SiteRelatedDetailsMapsByName[DetailNameCodeType.ShippingServiceDetails];
		if(!detailsMap.ContainsKey(site)) 
		{
			loadShippingServiceDetailsForSite(site);
		}
		Hashtable byShippingServiceIDMap = (Hashtable)detailsMap[site];
		return (ShippingServiceDetailsType)byShippingServiceIDMap[shippingServiceID];
	}
		/// <summary>
		/// 
		/// </summary>
	public List<TaxJurisdictionType> getTaxJurisdictionDetailsForSite(SiteCodeType site)  
	{
		Hashtable detalisMap = (Hashtable)_SiteRelatedDetailsByName[DetailNameCodeType.TaxJurisdiction];
		if(!detalisMap.ContainsKey(site)) 
		{
			List<DetailNameCodeType> detailNames = new List<DetailNameCodeType>(new DetailNameCodeType[] {DetailNameCodeType.TaxJurisdiction});
			loadTaxJurisdictionDetailsForSite(site);
		}
		return (List<TaxJurisdictionType>)detalisMap[site];
	}
		/// <summary>
		/// 
		/// </summary>
	public TaxJurisdictionType getTaxJurisdictionDetailsBySiteAndJurisdictionID(SiteCodeType site, String jurisdictionID)  
	{
		Hashtable detailsMap = (Hashtable)_SiteRelatedDetailsMapsByName[DetailNameCodeType.TaxJurisdiction];
		if(!detailsMap.ContainsKey(site)) 
		{
			loadTaxJurisdictionDetailsForSite(site);
		}
		Hashtable byJurisdictionIDMap = (Hashtable)detailsMap[site];
		return (TaxJurisdictionType)byJurisdictionIDMap[jurisdictionID];
	}
		/// <summary>
		/// 
		/// </summary>
	public List<URLDetailsType> getURLDetailsForSite(SiteCodeType site)  
	{
		Hashtable detalisMap = (Hashtable)_SiteRelatedDetailsByName[DetailNameCodeType.URLDetails];
		if(!detalisMap.ContainsKey(site)) {
			loadURLDetailsForSite(site);
		}
		return (List<URLDetailsType>)detalisMap[site];
	}
		/// <summary>
		/// 
		/// </summary>
	public URLDetailsType getURLDetailsBySiteAndURLType(SiteCodeType site, URLTypeCodeType URLType)  
	{
		Hashtable detailsMap = (Hashtable)_SiteRelatedDetailsMapsByName[DetailNameCodeType.URLDetails];
		if(!detailsMap.ContainsKey(site)) {
			loadURLDetailsForSite(site);
		}
		Hashtable byURLTypeMap = (Hashtable)detailsMap[site];
		return (URLDetailsType)byURLTypeMap[URLType];
	}
		/// <summary>
		/// 
		/// </summary>
	public CountryDetailsType getTimeZoneDetailsByZone(String zoneId) 
	{
		return (CountryDetailsType)_TimeZoneDetails[zoneId];
	}
		/// <summary>
		/// 
		/// </summary>
	public List<TimeZoneDetailsType> getTimeZoneDetails() 
	{
		return _timeZoneDetails;
	}
		/// <summary>
		/// 
		/// </summary>
	public ShippingLocationDetailsType getShippingLocationDetailsByShipingLocation(String shippingLocation) 
	{
		return (ShippingLocationDetailsType)_ShippingLocationDetailsByShippingLocation[shippingLocation];
	}
		/// <summary>
		/// 
		/// </summary>
	public List<ShippingLocationDetailsType> getShippingLocationDetails() 
	{
		return _shippingLocationDetails;
	}
		/// <summary>
		/// 
		/// </summary>	
	public SiteDetailsType getSiteDetailsBySite(SiteCodeType site) 
	{
		return (SiteDetailsType)_SiteDetailsBySite[site];
	}
		/// <summary>
		/// 
		/// </summary>
	public List<SiteDetailsType> getSiteDetails() 
	{
		return _siteDetails;
	}
		/// <summary>
		/// 
		/// </summary>
	public DispatchTimeMaxDetailsType getDispatchTimeMaxDetailsByDispatchTimeMax(int dispatchTimeMax) 
	{
		return (DispatchTimeMaxDetailsType)_DispatchTimeMaxDetailsByDispatchTimeMax[dispatchTimeMax];
	}

		/// <summary>
		/// 
		/// </summary>
	public List<DispatchTimeMaxDetailsType> getDispatchTimeMaxDetails() 
	{
		return _dispatchTimeMaxDetails;
	}
		/// <summary>
		/// 
		/// </summary>
	
	public CurrencyDetailsType getCurrencyDetailsByCurrencyCode(CurrencyCodeType currencyCode) 
	{
		return (CurrencyDetailsType)_CurrencyDetailsByCurrency[currencyCode];
	}
		/// <summary>
		/// 
		/// </summary>

	public List<CurrencyDetailsType> getCurrencyDetails() 
	{
		return _currencyDetails;
	}
		/// <summary>
		/// 
		/// </summary>
	public CountryDetailsType getCountryDetailsByCountryCode(CountryCodeType countryCode) 
	{
		return (CountryDetailsType)_CountryDetailsByCountry[countryCode];
	}
		/// <summary>
		/// 
		/// </summary>
	public List<CountryDetailsType> getCountryDetails() 
	{
		return _countryDetails;
	}
	#endregion

	#region Private Methods

	private void loadPaymentOptionsDetailsForSite(GeteBayDetailsResponseType resp, SiteCodeType site)  {
		if(resp == null) {
                List < DetailNameCodeType> detailNames = new List<DetailNameCodeType>(new DetailNameCodeType[]{DetailNameCodeType.PaymentOptionDetails});
			resp = makeApiCall(detailNames, site);
		}
            List < PaymentOptionDetailsType> details = resp.PaymentOptionDetails;
		if(details != null) {
			_PaymentOptionDetailsBySite.Add(site, details);
			Hashtable detailsByPaymentMethodMap = new Hashtable();
			for(int i = 0; i < details.Count; i++) {
				PaymentOptionDetailsType detail = details[i];
				detailsByPaymentMethodMap.Add(detail.PaymentOption, detail);
			}
			_PaymentOptionDetailsMapsBySite.Add(site, detailsByPaymentMethodMap);
		}
	}
	private void loadPaymentOptionsDetailsForSite(SiteCodeType site)  {
		loadPaymentOptionsDetailsForSite(null, site);
	}

	private void loadURLDetailsForSite(GeteBayDetailsResponseType resp, SiteCodeType site)  {	
		if(resp == null) {
                List < DetailNameCodeType> detailNames = new List<DetailNameCodeType>(new DetailNameCodeType[] {DetailNameCodeType.URLDetails});
			resp = makeApiCall(detailNames, site);
		}
            List < URLDetailsType> urlDetails = resp.URLDetails;
		if(urlDetails != null) {
			_URLDetailsBySite.Add(site, urlDetails);
			Hashtable detailsByURLTypeMap = new Hashtable();
			for(int i = 0; i < urlDetails.Count; i++) {
				URLDetailsType detail = urlDetails[i];
				detailsByURLTypeMap.Add(detail.URLType, detail);
			}
			_URLDetailsMapsBySite.Add(site, detailsByURLTypeMap);
		}
	}
	
	private void loadURLDetailsForSite(SiteCodeType site)  {	
		loadURLDetailsForSite(null, site);
	}

	private void loadSiteRelatedDetailsForSite(SiteCodeType site)  {
		GeteBayDetailsResponseType resp = makeApiCall(_siteRelatedDetailNames, site);
		_taxJurisdictionDetails = resp.TaxJurisdiction;
		_shippingServiceDetails = resp.ShippingServiceDetails;
		_regionDetails = resp.RegionDetails;
		 
		loadPaymentOptionsDetailsForSite(resp, site);
		loadURLDetailsForSite(resp, site);
		loadTaxJurisdictionDetailsForSite(resp, site);
		loadShippingServiceDetailsForSite(resp, site);
		loadRegionDetailsForSite(resp, site);
	}
	
	private void loadTaxJurisdictionDetailsForSite(GeteBayDetailsResponseType resp, SiteCodeType site)  
	{	
		if(resp == null) 
		{
                List < DetailNameCodeType> detailNames = 
				new List<DetailNameCodeType>(new DetailNameCodeType[] {DetailNameCodeType.TaxJurisdiction});
			resp = makeApiCall(detailNames, site);
		}
            List < TaxJurisdictionType> details = resp.TaxJurisdiction;
		if(details != null) 
		{
			_TaxJurisdictionDetailsBySite.Add(site, details);
			Hashtable detailsByJurisdictionIDMap = new Hashtable();
			for(int i = 0; i < details.Count; i++) 
			{
				TaxJurisdictionType detail = details[i];
				detailsByJurisdictionIDMap.Add(detail.JurisdictionID, detail);
			}
			_TaxJurisdictionDetailsMapsBySite.Add(site, detailsByJurisdictionIDMap);
		}
	}
	private void loadTaxJurisdictionDetailsForSite(SiteCodeType site)  
	{	
		loadTaxJurisdictionDetailsForSite(null, site);
	}
	private void loadShippingServiceDetailsForSite(GeteBayDetailsResponseType resp, SiteCodeType site) 
	{
		if(resp == null) 
		{
                List < DetailNameCodeType> detailNames = 
				new List<DetailNameCodeType>(new DetailNameCodeType[] {DetailNameCodeType.ShippingServiceDetails});
			resp = makeApiCall(detailNames, site);
		}
            List < ShippingServiceDetailsType> details = resp.ShippingServiceDetails;
		if(details != null) 
		{
			_ShippingServiceDetailsBySite.Add(site, details);
			Hashtable detailsByShippingServiceIDMap = new Hashtable();
			for(int i = 0; i < details.Count; i++) 
			{
				ShippingServiceDetailsType detail = details[i];
				detailsByShippingServiceIDMap.Add(detail.ShippingServiceID, detail);
			}
			_ShippingServiceDetailsMapsBySite.Add(site, detailsByShippingServiceIDMap);
		}
	}	
	private void loadShippingServiceDetailsForSite(SiteCodeType site)  
	{	
		loadShippingServiceDetailsForSite(null, site);
	}
	private void loadRegionDetailsForSite(GeteBayDetailsResponseType resp, SiteCodeType site) 
	{	
		if(resp == null) 
		{
                List < DetailNameCodeType> detailNames = 
				new List<DetailNameCodeType>(new DetailNameCodeType[] {DetailNameCodeType.RegionDetails});
			resp = makeApiCall(detailNames, site);
		}
            List < RegionDetailsType> details = resp.RegionDetails;
		if(details != null) 
		{
			_RegionDetailsBySite.Add(site, details);
			Hashtable detailsByRegionIDMap = new Hashtable();
			for(int i = 0; i < details.Count; i++) 
			{
				RegionDetailsType detail = details[i];
				detailsByRegionIDMap.Add(detail.RegionID, detail);
			}
			_RegionDetailsMapsBySite.Add(site, detailsByRegionIDMap);
		}
	}
	private void loadRegionDetailsForSite(SiteCodeType site)  
	{	
		loadRegionDetailsForSite(null, site);
	}
	
	private GeteBayDetailsResponseType makeApiCall(List<DetailNameCodeType> detailNames, SiteCodeType site)  {
		SiteCodeType savedSite = _site;
		_apiContext.Site = site;
		GeteBayDetailsCall api = new GeteBayDetailsCall(_apiContext);
            List < DetailLevelCodeType> detailLevels = new List<DetailLevelCodeType>( new DetailLevelCodeType[] {DetailLevelCodeType.ReturnAll});
		api.DetailLevelList = detailLevels;
		api.GeteBayDetails(detailNames);
		_apiContext.Site = savedSite;
		
		return api.ApiResponse;		
	}
	
	private void loadSiteIndependentDetails()  {
		 GeteBayDetailsResponseType resp = makeApiCall(_siteIndependentDetailNames, _apiContext.Site);
		 _countryDetails = resp.CountryDetails;
		 loadCountryDetails(_countryDetails);
		 _currencyDetails = resp.CurrencyDetails;
		 loadCurrencyDetails(_currencyDetails);
		 _dispatchTimeMaxDetails = resp.DispatchTimeMaxDetails;
		 loadDispatchTimeMaxDetails(_dispatchTimeMaxDetails);
		 _shippingLocationDetails = resp.ShippingLocationDetails;
		 loadShippingLocationDetails(_shippingLocationDetails);
		 _siteDetails = resp.SiteDetails;
		 loadSiteDetails(_siteDetails);
		 _timeZoneDetails = resp.TimeZoneDetails;
		 loadTimeZoneDetails(_timeZoneDetails); 
	}
	private void loadTimeZoneDetails(List<TimeZoneDetailsType> details) {
		if(_TimeZoneDetails.Count == 0) {
			for(int i = 0; i < details.Count; i++) {
				TimeZoneDetailsType detail = details[i];
				_TimeZoneDetails.Add(detail.TimeZoneID, detail);
			}
		}
	}
	private void loadCountryDetails(List<CountryDetailsType> details) {
		if(_CountryDetailsByCountry.Count == 0) {
			for(int i = 0; i < details.Count; i++) {
				CountryDetailsType detail = details[i];
				_CountryDetailsByCountry.Add(detail.Country, detail);
			}
		}
	}
	private void initializeHashMaps() {
		_SiteRelatedDetailsByName.Add(DetailNameCodeType.PaymentOptionDetails, _PaymentOptionDetailsBySite);
		_SiteRelatedDetailsMapsByName.Add(DetailNameCodeType.PaymentOptionDetails, _PaymentOptionDetailsMapsBySite);
		//
		_SiteRelatedDetailsByName.Add(DetailNameCodeType.RegionDetails, _RegionDetailsBySite);
		_SiteRelatedDetailsMapsByName.Add(DetailNameCodeType.RegionDetails, _RegionDetailsMapsBySite);
		//
		_SiteRelatedDetailsByName.Add(DetailNameCodeType.ShippingServiceDetails, _ShippingServiceDetailsBySite);
		_SiteRelatedDetailsMapsByName.Add(DetailNameCodeType.ShippingServiceDetails, _ShippingServiceDetailsMapsBySite);
		//
		_SiteRelatedDetailsByName.Add(DetailNameCodeType.TaxJurisdiction, _TaxJurisdictionDetailsBySite);
		_SiteRelatedDetailsMapsByName.Add(DetailNameCodeType.TaxJurisdiction, _TaxJurisdictionDetailsMapsBySite);
		//
		_SiteRelatedDetailsByName.Add(DetailNameCodeType.URLDetails, _URLDetailsBySite);
		_SiteRelatedDetailsMapsByName.Add(DetailNameCodeType.URLDetails, _URLDetailsMapsBySite);
	}
	private void loadCurrencyDetails(List<CurrencyDetailsType> details) {
		if(_CurrencyDetailsByCurrency.Count == 0) {
			for(int i = 0; i < details.Count; i++) {
				CurrencyDetailsType currency = details[i];
				_CurrencyDetailsByCurrency.Add(currency.Currency, currency);
			}
		}
	}

	private void loadDispatchTimeMaxDetails(List<DispatchTimeMaxDetailsType> details) {
		if(_DispatchTimeMaxDetailsByDispatchTimeMax.Count == 0) {
			for(int i = 0; i < details.Count; i++) {
				DispatchTimeMaxDetailsType dispatch = details[i];
				_DispatchTimeMaxDetailsByDispatchTimeMax.Add(dispatch.DispatchTimeMax, dispatch);
			}
		}
	}

	private void loadSiteDetails(List<SiteDetailsType> details) {
		if(_SiteDetailsBySite.Count == 0) {
			for(int i = 0; i < details.Count; i++) {
				SiteDetailsType site = details[i];
				_SiteDetailsBySite.Add(site.Site, site);
			}
		}
	}

	private void loadShippingLocationDetails(List<ShippingLocationDetailsType> details) {
		if(_ShippingLocationDetailsByShippingLocation.Count == 0) {
			for(int i = 0; i < details.Count; i++) {
				ShippingLocationDetailsType detail = details[i];
				_ShippingLocationDetailsByShippingLocation.Add(detail.ShippingLocation, detail);
			}
		}
	}

		#endregion
	}
}

