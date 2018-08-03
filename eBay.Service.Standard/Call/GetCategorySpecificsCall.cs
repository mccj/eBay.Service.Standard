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
	public class GetCategorySpecificsCall : ApiCall
	{

		#region Constructors
		/// <summary>
		/// 
		/// </summary>
		public GetCategorySpecificsCall()
		{
			ApiRequest = new GetCategorySpecificsRequestType();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ApiContext">The <see cref="ApiCall.ApiContext"/> for this API Call of type <see cref="ApiContext"/>.</param>
		public GetCategorySpecificsCall(ApiContext ApiContext)
		{
			ApiRequest = new GetCategorySpecificsRequestType();
			this.ApiContext = ApiContext;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// This is the base request type for the <b>GetCategorySpecifics</b> call. This call is used to retrieve recommended Item Specifics names and values for one or multiple eBay Categories.
		/// </summary>
		/// 
		/// <param name="CategoryIDList">
		/// A unique identifer for an eBay leaf-level category. Recommended Item Specifics names and values will be retrieved for each eBay category that is specified. Up to 100 <b>CategoryID</b> values may be specified in one call.
		/// 
		/// A <b>GetCategorySpecifics</b> call request requires at least one of the following: a <b>CategoryID</b> value, a <b>CategorySpecifics.CategoryID</b> value, or the
		/// <b>CategorySpecificsFileInfo</b> field with its value set to <code>true</code>. <b>CategoryID</b> values and
		/// <b>CategorySpecific.CategoryID</b> values can both be used in the same request.
		/// 
		/// Keep in mind that a high number of specified categories can result in
		/// longer response times and larger result sets. If your call request happens to time out, you might want specify fewer categories. If any
		/// <b>CategoryID</b> values are specified more than once, only the first instance will be used.
		/// </param>
		///
		/// <param name="LastUpdateTime">
		/// This dateTime filter can be included and used if the user only wants to check for recently-added Item Specifics names and values for one or more categories. If this filter is used, the Item Specifics recommendation engine will only check for Item Specifics names and values that have been added/changed since the date that was passed in to this field. If this field is used, the call will not return any Item Specifics data; it will only return the <b>Recommendations.Updated</b> boolean field for each specified eBay category. A <code>true</code> value in this field will indicate that the recommended Item Specifics names/values for the eBay category have changed since the timestamp passed in the <b>LastUpdateTime</b> field, and a <code>false</code> value in this field will indicate that the recommended Item Specifics names/values for the eBay category have not changed since the timestamp passed in the <b>LastUpdateTime</b> field.
		/// 
		/// Typically, you will pass in the timestamp value that was returned the last time you refreshed the list of Item Specifics names and values for the same categories. If the <b>Recommendations.Updated</b> flag returns <code>true</code> for any eBay categories in the response, you will want to call <b>GetCategorySpecifics</b> again for those eBay categories to get the latest names and values. As downloading all the data may affect your application's performance, it may help to only download Item Specifics data for an eBay category if the data has changed since you last checked.
		/// </param>
		///
		/// <param name="MaxNames">
		/// This field can be used if you want to limit the number of Item Specifics names that are returned for each eBay category. If you only wanted to retrieve the three most popular Item Specifics names per category, you would include this field and set its value to <code>3</code>.
		/// </param>
		///
		/// <param name="MaxValuesPerName">
		/// This field can be used if you want to limit the number of Item Specifics values (for each Item Specifics name) that are returned for each eBay category. If you only wanted to retrieve the 10 most popular Item Specifics values per Item Specifics name per category, you would include this field and set its value to <code>10</code>.
		/// </param>
		///
		/// <param name="Name">
		/// This field can be used if you already have an Item Specific name in mind, and you only want the recommended values for this Item Specifics name.
		/// If you specify multiple eBay categories in the request, the recommendation engine may find a matching Item Specifics name for some categories, but not for others. For eBay categories where the Item Specifics name is not found, recommended Item Specifics names and values will be returned. The Item Specifics name passed in this field is case-sensitive, and wildcards are not supported.
		/// </param>
		///
		/// <param name="CategorySpecificList">
		/// This container can be used instead of, or in conjunction with <b>CategoryID</b> values specified at the call request root level. The <b>CategorySpecific</b> container can actually be more powerful since you can pass in multiple Category ID and Item Specifics name combinations, and if you specify <b>CategoryID</b> values at the root level, only one (Item Specifics) <b>Name</b> value can be used.
		/// 
		/// A <b>GetCategorySpecifics</b> call request requires at least one of the following: a <b>CategoryID</b> value, a <b>CategorySpecifics.CategoryID</b> value, or the
		/// <b>CategorySpecificsFileInfo</b> field with its value set to <code>true</code>. <b>CategoryID</b> values and
		/// <b>CategorySpecific.CategoryID</b> values can both be used in the same request.
		/// 
		/// Keep in mind that a high number of specified categories can result in
		/// longer response times and larger result sets. If your call request happens to time out, you might want specify fewer categories. If any
		/// <b>CategorySpecifics.CategoryID</b> values are specified more than once, only the first instance will be used.
		/// </param>
		///
		/// <param name="ExcludeRelationships">
		/// If this boolean field is included and set to <code>true</code>, the <b>Relationship</b> container will not be returned for any Item Specifics name or value recommendations. Relationship recommendations tell you whether an Item Specific has a logical dependency another Item Specific.
		///  
		/// For example, in a clothing category, Size Type could be
		/// recommended as a parent of Size, because Size=Small would
		/// mean something different to buyers depending on whether
		/// Size Type=Petite or Size Type=Plus.
		/// 
		/// In general, it is a good idea to retrieve and use relationship
		/// recommendations, as this data can help buyers find the items
		/// they want more easily.
		/// </param>
		///
		/// <param name="IncludeConfidence">
		/// If this boolean field is included and set to <code>true</code>, eBay's level of confidence in the popularity of each Item Specific name and value for the specified category is returned in the response. Some sellers may find this useful when choosing whether to use eBay's recommendation or their own Item Specifics names or values.
		///  
		/// If this field is used, either one or more <b>CategoryID</b> values and/or one or more <b>CategorySpecific.CategoryID</b> values must be specified. If you try to use this field solely with the <b>CategorySpecificsFileInfo</b> field, the request fails and no <b>TaskReferenceID</b> or <b>FileReferenceID</b> values are returned.
		/// </param>
		///
		/// <param name="CategorySpecificsFileInfo">
		/// If this boolean field is included and set to <code>true</code>, the response includes a <b>FileReferenceID</b> and
		/// <b>TaskReferenceID</b> value. Use these identifiers as inputs to the <b>downloadFile</b>
		/// call in the eBay File Transfer API. That API lets you retrieve
		/// a single (bulk) <b>GetCategorySpecifics</b> response with all the Item
		/// Specifics recommendations available for the requested site ID.
		/// (The <b>downloadFile</b> call downloads a .zip file as an
		/// attachment.)
		/// 
		/// Either the <b>CategorySpecificsFileInfo</b> field or one or more <b>CategoryID</b> and/or <b>CategorySpecific.CategoryID</b> values are required
		/// in a <b>GetCategorySpecifics</b> call. 
		/// 
		/// <span class="tablenote"><b>Note:</b>
		/// You can use the File Transfer API without using or learning
		/// about the Bulk Data Exchange API or other
		/// Large Merchant Services APIs.
		/// </span>
		/// </param>
		///
		public RecommendationsType[] GetCategorySpecifics(String[] CategoryIDList, DateTime LastUpdateTime, int MaxNames, int MaxValuesPerName, string Name, CategoryItemSpecificsType[] CategorySpecificList, bool ExcludeRelationships, bool IncludeConfidence, bool CategorySpecificsFileInfo)
		{
			this.CategoryIDList = CategoryIDList;
			this.LastUpdateTime = LastUpdateTime;
			this.MaxNames = MaxNames;
			this.MaxValuesPerName = MaxValuesPerName;
			this.Name = Name;
			this.CategorySpecificList = CategorySpecificList;
			this.ExcludeRelationships = ExcludeRelationships;
			this.IncludeConfidence = IncludeConfidence;
			this.CategorySpecificsFileInfo = CategorySpecificsFileInfo;

			Execute();
			return ApiResponse.Recommendations;
		}


		/// <summary>
		/// For backward compatibility with old wrappers.
		/// </summary>
		public RecommendationsType[] GetCategorySpecifics(String[] CategoryIDList)
		{
			this.CategoryIDList = CategoryIDList;

			Execute();
			return ApiResponse.Recommendations;
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
		/// Gets or sets the <see cref="GetCategorySpecificsRequestType"/> for this API call.
		/// </summary>
		public GetCategorySpecificsRequestType ApiRequest
		{ 
			get { return (GetCategorySpecificsRequestType) AbstractRequest; }
			set { AbstractRequest = value; }
		}

		/// <summary>
		/// Gets the <see cref="GetCategorySpecificsResponseType"/> for this API call.
		/// </summary>
		public GetCategorySpecificsResponseType ApiResponse
		{ 
			get { return (GetCategorySpecificsResponseType) AbstractResponse; }
		}

		
 		/// <summary>
		/// Gets or sets the <see cref="GetCategorySpecificsRequestType.CategoryID"/> of type <see cref="StringCollection"/>.
		/// </summary>
		public String[] CategoryIDList
		{ 
			get { return ApiRequest.CategoryID; }
			set { ApiRequest.CategoryID = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetCategorySpecificsRequestType.LastUpdateTime"/> of type <see cref="DateTime"/>.
		/// </summary>
		public DateTime LastUpdateTime
		{ 
			get { return ApiRequest.LastUpdateTime; }
			set { ApiRequest.LastUpdateTime = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetCategorySpecificsRequestType.MaxNames"/> of type <see cref="int"/>.
		/// </summary>
		public int MaxNames
		{ 
			get { return ApiRequest.MaxNames; }
			set { ApiRequest.MaxNames = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetCategorySpecificsRequestType.MaxValuesPerName"/> of type <see cref="int"/>.
		/// </summary>
		public int MaxValuesPerName
		{ 
			get { return ApiRequest.MaxValuesPerName; }
			set { ApiRequest.MaxValuesPerName = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetCategorySpecificsRequestType.Name"/> of type <see cref="string"/>.
		/// </summary>
		public string Name
		{ 
			get { return ApiRequest.Name; }
			set { ApiRequest.Name = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetCategorySpecificsRequestType.CategorySpecific"/> of type <see cref="CategoryItemSpecificsTypeCollection"/>.
		/// </summary>
		public CategoryItemSpecificsType[] CategorySpecificList
		{ 
			get { return ApiRequest.CategorySpecific; }
			set { ApiRequest.CategorySpecific = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetCategorySpecificsRequestType.ExcludeRelationships"/> of type <see cref="bool"/>.
		/// </summary>
		public bool ExcludeRelationships
		{ 
			get { return ApiRequest.ExcludeRelationships; }
			set { ApiRequest.ExcludeRelationships = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetCategorySpecificsRequestType.IncludeConfidence"/> of type <see cref="bool"/>.
		/// </summary>
		public bool IncludeConfidence
		{ 
			get { return ApiRequest.IncludeConfidence; }
			set { ApiRequest.IncludeConfidence = value; }
		}
		
 		/// <summary>
		/// Gets or sets the <see cref="GetCategorySpecificsRequestType.CategorySpecificsFileInfo"/> of type <see cref="bool"/>.
		/// </summary>
		public bool CategorySpecificsFileInfo
		{ 
			get { return ApiRequest.CategorySpecificsFileInfo; }
			set { ApiRequest.CategorySpecificsFileInfo = value; }
		}
		
		
 		/// <summary>
		/// Gets the returned <see cref="GetCategorySpecificsResponseType.Recommendations"/> of type <see cref="RecommendationsTypeCollection"/>.
		/// </summary>
		public RecommendationsType[] RecommendationList
		{ 
			get { return ApiResponse.Recommendations; }
		}
		
 		/// <summary>
		/// Gets the returned <see cref="GetCategorySpecificsResponseType.TaskReferenceID"/> of type <see cref="string"/>.
		/// </summary>
		public string TaskReferenceID
		{ 
			get { return ApiResponse.TaskReferenceID; }
		}
		
 		/// <summary>
		/// Gets the returned <see cref="GetCategorySpecificsResponseType.FileReferenceID"/> of type <see cref="string"/>.
		/// </summary>
		public string FileReferenceID
		{ 
			get { return ApiResponse.FileReferenceID; }
		}
		

		#endregion

		
	}
}
