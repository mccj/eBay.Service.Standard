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
	public class GetCharitiesCall : ApiCall
	{

		#region Constructors
		/// <summary>
		/// 
		/// </summary>
		public GetCharitiesCall()
		{
			ApiRequest = new GetCharitiesRequestType();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ApiContext">The <see cref="ApiCall.ApiContext"/> for this API Call of type <see cref="ApiContext"/>.</param>
		public GetCharitiesCall(ApiContext ApiContext)
		{
			ApiRequest = new GetCharitiesRequestType();
			this.ApiContext = ApiContext;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// This is the base request type for the <b>GetCharities</b> call. This call is used to search for eBay for Charity nonprofit organizations. This call allows users to search for a specific nonprofit organization, or to search for multiple nonprofit organizations from a particular category and/or geographical region, or by using a search string.
		/// </summary>
		/// 
		/// <param name="CharityID">
		/// A unique identification number assigned to a nonprofit
		/// organization when that organization registers with the PayPal Giving Fund. If a <b>CharityID</b> value is used in the request, all other search filters set in the call request will be ignored, as the call will only search for the specified nonprofit organization.
		/// </param>
		///
		/// <param name="CharityName">
		/// This field will accept the full or partial name of a registered nonprofit organization. For example, if you pass in a value of <b>heart</b>, all registered nonprofit organizations with "heart" in their name may be returned in the response. If the <b>MatchType</b> field is used and set to <code>StartsWith</code>, the phrase "heart" must come at the beginning of the nonprofit organization's name. However, if the <b>MatchType</b> field is used and set to <code>Contains</code>, or is not used at all, all registered nonprofit organizations with "heart" in their name should be returned in the response. The string passed in this field is not case-sensitive.
		/// <br/><br/>
		/// </param>
		///
		/// <param name="Query">
		/// This field accept any string up to 350 characters in length. The functionality of this field is similar to the <b>CharityName</b> field, except that the call will also search for the supplied query string within the charity's mission statement (which is returned under the <b>Charity.Mission</b> field in the response) as long as the <b>IncludeDescription</b> field is included in the call request and set to <code>true</code>. If you pass in a value of <b>heart</b>, all registered nonprofit organizations with "heart" in their name or in their mission statement may be returned in the response.
		/// <br/><br/>
		/// The string passed in this field is not case-sensitive. The <b>MatchType</b> field will have no effect if the <b>Query</b> field is used, as the <b>MatchType</b> field will only have an effect if a <b>CharityName</b> string field is included.
		/// <br/><br/>
		/// </param>
		///
		/// <param name="CharityRegion">
		/// This field can be used to search for registered nonprofit organizations within a specified geographical region. Each geographical region will have an integer value associated with it. This integer value is what is passed in to this field.
		/// </param>
		///
		/// <param name="CharityDomain">
		/// This field can be used to search for registered nonprofit organizations within a specified charity domain or category. Each charity domain will have an integer value associated with it. This integer value is what is passed in to this field. Nonprofit organizations can be associated with multiple charity domains.
		/// </param>
		///
		/// <param name="IncludeDescription">
		/// This field should be included and set to <code>true</code> if the <b>Query</b> field is used, and the user wishes to search the charity's name and its mission statement. If this field is omitted or included and set to <code>false</code>, the call will only search charity names for the specified string in the  <b>Query</b> field.
		/// </param>
		///
		/// <param name="MatchType">
		/// This filter is used if the user wants to specify where to look for the string specified in the <b>CharityName</b> field. The enumeration value input into this field controls the type of string matching to use when a value is submitted in the
		/// <b>CharityName</b> field. If this field is omitted, and a <b>CharityName</b> string is used, this parameter value defaults to <code>StartsWith</code>.
		/// <br/><br/>
		/// This field is no applicable when a <b>Query</b> value is used.
		/// <br/><br/>
		/// </param>
		///
		/// <param name="Featured">
		/// This boolean field is used and set to <code>true</code> if the user wants to search for nonprofit organizations currently featured on the eBay for Charity site.
		/// </param>
		///
		/// <param name="CampaignID">
		/// Reserved for future use.
		/// </param>
		///
		public CharityInfoType[] GetCharities(string CharityID, string CharityName, string Query, int CharityRegion, int CharityDomain, bool IncludeDescription, StringMatchCodeType MatchType, bool Featured, long CampaignID)
		{
			this.CharityID = CharityID;
			this.CharityName = CharityName;
			this.Query = Query;
			this.CharityRegion = CharityRegion;
			this.CharityDomain = CharityDomain;
			this.IncludeDescription = IncludeDescription;
			this.MatchType = MatchType;
			this.Featured = Featured;
			this.CampaignID = CampaignID;

			Execute();
			return ApiResponse.Charity;
		}


		/// <summary>
		/// to support the call with out parameters.
		/// </summary>
		public CharityInfoType[] GetCharities()
		{
			Execute();
			return ApiResponse.Charity;
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
		/// Gets or sets the <see cref="GetCharitiesRequestType"/> for this API call.
		/// </summary>
		public GetCharitiesRequestType ApiRequest
		{ 
			get { return (GetCharitiesRequestType) AbstractRequest; }
			set { AbstractRequest = value; }
		}

		/// <summary>
		/// Gets the <see cref="GetCharitiesResponseType"/> for this API call.
		/// </summary>
		public GetCharitiesResponseType ApiResponse
		{ 
			get { return (GetCharitiesResponseType) AbstractResponse; }
		}

		
 		/// <summary>
		/// Gets or sets the <see cref="GetCharitiesRequestType.CharityID"/> of type <see cref="string"/>.
		/// </summary>
		public string CharityID
		{ 
			get { return ApiRequest.CharityID; }
			set { ApiRequest.CharityID = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetCharitiesRequestType.CharityName"/> of type <see cref="string"/>.
		/// </summary>
		public string CharityName
		{ 
			get { return ApiRequest.CharityName; }
			set { ApiRequest.CharityName = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetCharitiesRequestType.Query"/> of type <see cref="string"/>.
		/// </summary>
		public string Query
		{ 
			get { return ApiRequest.Query; }
			set { ApiRequest.Query = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetCharitiesRequestType.CharityRegion"/> of type <see cref="int"/>.
		/// </summary>
		public int CharityRegion
		{ 
			get { return ApiRequest.CharityRegion; }
			set { ApiRequest.CharityRegion = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetCharitiesRequestType.CharityDomain"/> of type <see cref="int"/>.
		/// </summary>
		public int CharityDomain
		{ 
			get { return ApiRequest.CharityDomain; }
			set { ApiRequest.CharityDomain = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetCharitiesRequestType.IncludeDescription"/> of type <see cref="bool"/>.
		/// </summary>
		public bool IncludeDescription
		{ 
			get { return ApiRequest.IncludeDescription; }
			set { ApiRequest.IncludeDescription = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetCharitiesRequestType.MatchType"/> of type <see cref="StringMatchCodeType"/>.
		/// </summary>
		public StringMatchCodeType MatchType
		{ 
			get { return ApiRequest.MatchType; }
			set { ApiRequest.MatchType = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetCharitiesRequestType.Featured"/> of type <see cref="bool"/>.
		/// </summary>
		public bool Featured
		{ 
			get { return ApiRequest.Featured; }
			set { ApiRequest.Featured = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetCharitiesRequestType.CampaignID"/> of type <see cref="long"/>.
		/// </summary>
		public long CampaignID
		{ 
			get { return ApiRequest.CampaignID; }
			set { ApiRequest.CampaignID = value; }
		}
		
		
 		/// <summary>
		/// Gets the returned <see cref="GetCharitiesResponseType.Charity"/> of type <see cref="CharityInfoTypeCollection"/>.
		/// </summary>
		public CharityInfoType[] CharityList
		{ 
			get { return ApiResponse.Charity; }
		}
		

		#endregion

		
	}
}
