
using System;

namespace eBay.Service
{
    public partial class eBayClient
    {
        public eBayClient(string authToken)
        {
            this.eBayAuthToken = authToken;
            //securityHeader.eBayAuthToken = "";
            eBayAPIInstance();
        }
        public eBayClient(string devId = null, string appId = null, string certId = null, string ruName = null, string authToken = null) : this(authToken)
        {
            securityHeader.Credentials.AppId = appId;
            securityHeader.Credentials.DevId = devId;
            securityHeader.Credentials.AuthCert = certId;
            this.ruName = ruName;
        }
        #region 公共属性
        public string eBayAuthToken { get { return securityHeader.eBayAuthToken; } set { securityHeader.eBayAuthToken = value; } }
        public Core.Soap.SiteCodeType SiteCode { get; set; } = Core.Soap.SiteCodeType.US;
        public Core.Soap.ErrorLanguageCodeType ErrorLanguage { get; set; } = Core.Soap.ErrorLanguageCodeType.zh_CN;
        public Core.Sdk.报文 报文 { get; } = new Core.Sdk.报文();
        public string Version { get; } = "1065";
        public string DevId => securityHeader.Credentials.DevId;
        public string AppId => securityHeader.Credentials.AppId;
        public string CertId => securityHeader.Credentials.AuthCert;
        public string ruName { get; }
        #endregion 公共属性
        #region 基础功能   
        private Core.Soap.CustomSecurityHeaderType securityHeader { get; } = new Core.Soap.CustomSecurityHeaderType() {  Credentials=new Core.Soap.UserIdPasswordType { } };
        [NonSerialized]
        private System.ServiceModel.ChannelFactory<Core.Soap.eBayAPIInterface> channelFactory = null;
        private void eBayAPIInstance()
        {
            var binding = new System.ServiceModel.BasicHttpsBinding()
            {
                //CloseTimeout = TimeSpan.FromMilliseconds(Timeout),
                MaxBufferPoolSize = int.MaxValue,
                MaxBufferSize = int.MaxValue,
                MaxReceivedMessageSize = int.MaxValue
            };
            var endpointAddress = new System.ServiceModel.EndpointAddress("https://api.ebay.com/wsapi");
            channelFactory = new System.ServiceModel.ChannelFactory<Core.Soap.eBayAPIInterface>(binding, endpointAddress);
            //var 报文 = new Core.Sdk.报文();
            channelFactory.Endpoint.EndpointBehaviors.Add(new Core.Sdk.ContextPropagationBehavior(报文));
        }
        private T handleRequest<T>(T abstractRequest) where T : Core.Soap.AbstractRequestType
        {
            abstractRequest.Version = this.Version;
            abstractRequest.MessageID = System.Guid.NewGuid().ToString();

            if (this.ErrorLanguage != Core.Soap.ErrorLanguageCodeType.CustomCode)
                abstractRequest.ErrorLanguage = this.ErrorLanguage.ToString();

            return abstractRequest;
        }
        private string getUrl(string apiName)
        {
            var url = String.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}?callname={1}&siteid={2}&client=netsoap",
                     "https://api.ebay.com/wsapi", apiName, Util.SiteUtility.GetSiteID(this.SiteCode).ToString(System.Globalization.CultureInfo.InvariantCulture));
            return url;
        }
        private Core.Soap.eBayAPIInterface GeteBayAPIClient(string url/*, double? timeout = null*/)
        {
            //if (timeout.HasValue)
            //    binding.CloseTimeout = TimeSpan.FromMilliseconds(timeout.Value);
            var endpointAddress = new System.ServiceModel.EndpointAddress(url);
            return channelFactory.CreateChannel(endpointAddress);
            //return Core.Sdk.eBayAPIInstance.Instance.GeteBayAPIClient(url);
        }
        #endregion 基础功能
        #region 授权相关
        /// <summary>
        /// 刷新ebay授权码
        /// </summary>
        /// <param name="eBayToken"></param>
        public void RefreshToken(string eBayToken)
        {
            this.eBayAuthToken = eBayToken;
        }
        //public ApiContext GetApiContext()
        //{
        //    var apiContext = new ApiContext();
        //    //apiContext.Version = System.Configuration.ConfigurationManager.AppSettings.Get(VERSION);
        //    //apiContext.Timeout = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings.Get(TIME_OUT));
        //    //apiContext.SoapApiServerUrl = System.Configuration.ConfigurationManager.AppSettings.Get(API_SERVER_URL);
        //    //apiContext.EPSServerUrl = System.Configuration.ConfigurationManager.AppSettings.Get(EPS_SERVER_URL);
        //    //apiContext.SignInUrl = System.Configuration.ConfigurationManager.AppSettings.Get(SIGNIN_URL);

        //    apiContext.ApiCredential = new ApiCredential
        //    {
        //        ApiAccount = new ApiAccount
        //        {
        //            Developer = this.DevId,
        //            Application = this.AppId,
        //            Certificate = this.CertId
        //        },
        //        eBayToken = ""
        //    };

        //    //apiContext.EnableMetrics = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings.Get(ENABLE_METRICS));


        //    //if (!string.IsNullOrEmpty(site))
        //    //{
        //    //    apiContext.Site = (SiteCodeType)Enum.Parse(typeof(SiteCodeType), site, true);
        //    //    ErrorLanguageCodeType lang = eBay.Service.Util.ErrorLanguageUtility.GetDefaultErrorLanguageCodeType(apiContext.Site);
        //    //    apiContext.ErrorLanguage = lang;
        //    //}
        //    //apiContext.ErrorLanguage = ErrorLanguageCodeType.zh_CN;
        //    //apiContext.RuleName = System.Configuration.ConfigurationManager.AppSettings["RuName"];// EBayPriceChanges.Config.RuName;
        //    apiContext.RuName = "";// System.Configuration.ConfigurationManager.AppSettings["RuName"];// EBayPriceChanges.Config.RuName;
        //    return apiContext;
        //}
        /// <summary>
        /// app发起授权请求网址
        /// </summary>
        /// <returns></returns>
        public Tuple<string, string> GetAuthUrl()
        {
            string sessionID = this.GetSessionID();
            var url = GetAuthUrl(this.ruName, sessionID);
            return Tuple.Create(url, sessionID);
        }
        /// <summary>
        /// 根据sessionID获取授权请求网址
        /// </summary>
        /// <param name="sessionID"></param>
        /// <returns></returns>
        public string GetAuthUrl(string sessionID)
        {
            return GetAuthUrl(this.ruName, sessionID);
        }
        /// <summary>
        /// 获取授权请求网址
        /// </summary>
        /// <param name="runame"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        private string GetAuthUrl(string runame, string code)
        {
            //https://signin.ebay.com/ws/eBayISAPI.dll?SignIn&UsingSSL=1&pUserId=&co_partnerId=2&siteid=0&ru=https%3A%2F%2Fscgi.ebay.com%3A443%2Fws%2FeBayISAPI.dll%3FSellerRegistrationEnterContactInfoShow%26usergoal%3D0%26SellerRegistrationEnterContactInfoShow%3D%26accountType%3D0&pp=pass&pageType=1586&i1=0
            string url = "https://signin.ebay.com/ws/eBayISAPI.dll?SignIn&RuName={0}&SessID={1}";
            url = string.Format(url, runame, code);
            return url;
        }
        /// <summary>
        /// 获取请求的sessionID
        /// </summary>
        /// <returns></returns>
        public string GetSessionID()
        {
            if (!string.IsNullOrWhiteSpace(this.ruName))
            {
                return this.GetSessionID(new Core.Soap.GetSessionIDRequestType { RuName = this.ruName })?.SessionID;
            }
            else { throw new Exception("RuName 不能为空"); }
        }
        /// <summary>
        /// 根据sessionID获取Token
        /// </summary>
        /// <param name="sessionID"></param>
        /// <returns></returns>
        public string GetToken(string sessionID)
        {
            return this.FetchToken(new Core.Soap.FetchTokenRequestType { SessionID = sessionID })?.eBayAuthToken;
        }
        ///// <summary>
        ///// 获取授权状态
        ///// </summary>
        ///// <returns></returns>
        //public TokenStatusType GetTokenStatus()
        //{
        //    ApiContext context = this.ApiContext;
        //    var call = new GetTokenStatusCall(this.ApiContext);
        //    return call.GetTokenStatus();
        //}
        /// <summary>
        /// 撤销授权
        /// </summary>
        /// <param name="unsubscribeNotification"></param>
        public void RevokeToken(bool unsubscribeNotification)
        {
            this.RevokeToken(new Core.Soap.RevokeTokenRequestType { UnsubscribeNotification = unsubscribeNotification });
        }
        /// <summary>
        /// 确认身份
        /// </summary>
        /// <param name="sessionID"></param>
        /// <returns></returns>
        public string ConfirmIdentity(string sessionID)
        {
            return this.ConfirmIdentity(new Core.Soap.ConfirmIdentityRequestType { SessionID = sessionID })?.UserID;
        }

        #endregion 授权相关



        /// <summary>
        /// 获取eBay在销售的商品
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="entriesPerPage"></param>
        /// <returns></returns>
        public Core.Soap.GetMyeBaySellingResponseType GetMyeBaySelling1(int pageNumber, int entriesPerPage)
        {
            var apicall = this.GetMyeBaySelling(new Core.Soap.GetMyeBaySellingRequestType
            {
                ActiveList = new Core.Soap.ItemListCustomizationType
                {
                    Pagination = new Core.Soap.PaginationType
                    {
                        EntriesPerPage = entriesPerPage,
                        PageNumber = pageNumber
                    }
                }
            });
            return apicall;
        }

        /// <summary>
        /// 根据eBay订单Id获取订单信息
        /// </summary>
        /// <param name="orderIDList"></param>
        /// <returns></returns>
        public System.Collections.Generic.List<Core.Soap.OrderType> GetOrders(params string[] orderIDList)
        {
            var orders = this.GetOrders(new Core.Soap.GetOrdersRequestType { OrderIDArray = new System.Collections.Generic.List<string>(orderIDList) });
            return orders.OrderArray;
        }
        /// <summary>
        /// 搜索eBay订单
        /// </summary>
        /// <param name="PageNumber"></param>
        /// <param name="EntriesPerPage"></param>
        /// <param name="CreateTimeFrom"></param>
        /// <param name="CreateTimeTo"></param>
        /// <param name="ModTimeFrom"></param>
        /// <param name="ModTimeTo"></param>
        /// <param name="IncludeFinalValueFee"></param>
        /// <param name="OrderRole"></param>
        /// <param name="OrderStatus"></param>
        /// <returns></returns>
        public Core.Soap.GetOrdersResponseType GetOrders(int PageNumber, int EntriesPerPage,
            System.DateTime? CreateTimeFrom = null,
            System.DateTime? CreateTimeTo = null,
            System.DateTime? ModTimeFrom = null,
            System.DateTime? ModTimeTo = null,
            bool IncludeFinalValueFee = true,
            eBay.Service.Core.Soap.TradingRoleCodeType OrderRole = Core.Soap.TradingRoleCodeType.Seller,
            eBay.Service.Core.Soap.OrderStatusCodeType OrderStatus = Core.Soap.OrderStatusCodeType.Completed
            )
        {
            //var apicall = new GetOrdersCall(this.ApiContext);
            //apicall.IncludeFinalValueFee = IncludeFinalValueFee;//指示是否在响应中包含所有事务对象最终价值费 (FVF)
            //apicall.DetailLevelList.Add(eBay.Service.Core.Soap.DetailLevelCodeType.ReturnAll);
            //apicall.Pagination = new eBay.Service.Core.Soap.PaginationType
            //{
            //    PageNumber = PageNumber,
            //    EntriesPerPage = EntriesPerPage
            //};
            //apicall.OrderRole = OrderRole;//筛选返回的订单是基于用户的角色。用户的角色是买方或卖方。
            //apicall.OrderStatus = OrderStatus;//使用该字段来检索处于特定状态的订单。
            ////apicall.SortingOrder = SortOrderCodeType.Descending;//指定此调用所返回的订单应如何排序
            //if (CreateTimeFrom.HasValue)
            //    apicall.CreateTimeFrom = CreateTimeFrom.Value;
            //if (CreateTimeTo.HasValue)
            //    apicall.CreateTimeTo = CreateTimeTo.Value;
            //if (ModTimeFrom.HasValue)
            //    apicall.ModTimeFrom = ModTimeFrom.Value;
            //if (ModTimeTo.HasValue)
            //    apicall.ModTimeTo = ModTimeTo.Value;
            //apicall.Execute();
            //return apicall;

            var apicall = this.GetOrders(new Core.Soap.GetOrdersRequestType
            {
                IncludeFinalValueFee = IncludeFinalValueFee,//指示是否在响应中包含所有事务对象最终价值费 (FVF)
                DetailLevel = new System.Collections.Generic.List<Core.Soap.DetailLevelCodeType> {
                    eBay.Service.Core.Soap.DetailLevelCodeType.ReturnAll
                },
                Pagination = new Core.Soap.PaginationType
                {
                    PageNumber = PageNumber,
                    EntriesPerPage = EntriesPerPage
                },
                OrderRole = OrderRole,//筛选返回的订单是基于用户的角色。用户的角色是买方或卖方。
                OrderStatus = OrderStatus,//使用该字段来检索处于特定状态的订单。
                SortingOrder = Core.Soap.SortOrderCodeType.Descending,//指定此调用所返回的订单应如何排序
                CreateTimeFrom = CreateTimeFrom,
                CreateTimeTo = CreateTimeTo,
                ModTimeFrom = ModTimeFrom,
                ModTimeTo = ModTimeTo
            });
            return apicall;
        }
        /// <summary>
        /// 完成订单，并上传发货跟踪号
        /// </summary>
        /// <param name="deliveryStatus"></param>
        /// <param name="shippingCarrierUsed"></param>
        /// <param name="shipmentTrackingNumber"></param>
        /// <param name="deliveryDate"></param>
        /// <param name="ItemID"></param>
        /// <param name="TransactionID"></param>
        /// <param name="Paid"></param>
        /// <param name="Shipped"></param>
        public void CompleteSale(Core.Soap.ShipmentDeliveryStatusCodeType deliveryStatus, string shippingCarrierUsed, string shipmentTrackingNumber, System.DateTime deliveryDate,
            string ItemID, string TransactionID, bool Paid, bool Shipped)
        {
            //var apicall = new eBay.Service.Call.CompleteSaleCall(this.ApiContext)
            //{
            //    Shipment = new ShipmentType
            //    {
            //        DeliveryStatus = deliveryStatus,
            //        //ShipmentTrackingDetails=new ShipmentTrackingDetailsTypeCollection(),
            //        ShippingCarrierUsed = shippingCarrierUsed,

            //        ShipmentTrackingNumber = shipmentTrackingNumber,
            //        DeliveryDate = deliveryDate,
            //    }
            //};
            //apicall.CompleteSale(ItemID, TransactionID, Paid, Shipped);
            var apicall = this.CompleteSale(new Core.Soap.CompleteSaleRequestType
            {
                Shipment = new Core.Soap.ShipmentType
                {
                    DeliveryStatus = deliveryStatus,
                    //ShipmentTrackingDetails=new System.Collections.Generic.List<Core.Soap.ShipmentTrackingDetailsType>(),
                    ShippingCarrierUsed = shippingCarrierUsed,
                    ShipmentTrackingNumber = shipmentTrackingNumber,
                    DeliveryDate = deliveryDate
                }
            });
        }
        public eBay.Service.Core.Soap.GeteBayDetailsResponseType GeteBayDetails(params Core.Soap.DetailNameCodeType[] items)
        {
            var apicall = this.GeteBayDetails(new Core.Soap.GeteBayDetailsRequestType { DetailName = new System.Collections.Generic.List<Core.Soap.DetailNameCodeType>(items) });
            return apicall;
        }
        /// <summary>
        /// 发送留言信息给客户
        /// </summary>
        /// <param name="ItemID"></param>
        /// <param name="MemberMessage"></param>
        public void AddMemberMessageAAQToPartner(string itemID, Core.Soap.MemberMessageType memberMessage)
        {
            var apicall = this.AddMemberMessageAAQToPartner(new Core.Soap.AddMemberMessageAAQToPartnerRequestType
            {
                ItemID = itemID,
                MemberMessage = memberMessage
            });
        }
        ///// <summary>
        ///// 上传图片到eBay服务器
        ///// </summary>
        ///// <param name="file"></param>
        ///// <param name="items"></param>
        ///// <returns></returns>
        //public eBay.Service.Core.Soap.UploadSiteHostedPicturesResponseType UpLoadSiteHostedPicture(string file, Core.Soap.PictureWatermarkCodeType[] items)
        //{
        //    eBay.Service.EPS.eBayPictureService eps = new eBay.Service.EPS.eBayPictureService(this.ApiContext);
        //    eBay.Service.Core.Soap.UploadSiteHostedPicturesRequestType request = new eBay.Service.Core.Soap.UploadSiteHostedPicturesRequestType
        //    {
        //        //图片水印控制
        //        PictureWatermark = new System.Collections.Generic.List<Core.Soap.PictureWatermarkCodeType>(items)
        //    };
        //    eBay.Service.Core.Soap.UploadSiteHostedPicturesResponseType response = eps.UpLoadSiteHostedPicture(request, file);
        //    return response;
        //}
        //public eBay.Service.Core.Soap.UploadSiteHostedPicturesResponseType UpLoadSiteHostedPicture(byte[] file, PictureWatermarkCodeType[] items)
        //{
        //    eBay.Service.Core.Soap.UploadSiteHostedPicturesRequestType request = new eBay.Service.Core.Soap.UploadSiteHostedPicturesRequestType
        //    {
        //        //图片水印控制
        //        PictureWatermark = new PictureWatermarkCodeTypeCollection(items)
        //    };
        //    eBay.Service.Core.Soap.UploadSiteHostedPicturesResponseType response = this.UpLoadSiteHostedPicture(request, file);
        //    return response;
        //}
        //public UploadSiteHostedPicturesResponseType UpLoadSiteHostedPicture(UploadSiteHostedPicturesRequestType request, byte[] fileName)
        //{
        //    UploadSiteHostedPicturesResponseType respObj = null;

        //    try
        //    {

        //        XmlDocument reqDoc = serializeToXmlDoc(request);

        //        fixEncoding(reqDoc);

        //        addApiCredentials(reqDoc);

        //        updateElementName(reqDoc, "UploadSiteHostedPicturesRequestType", "UploadSiteHostedPicturesRequest");

        //        string reqStr = reqDoc.OuterXml;

        //        string respStr = sendFile(fileName, reqStr);

        //        XmlDocument respDoc = new XmlDocument();
        //        respDoc.LoadXml(respStr);
        //        updateElementName(respDoc, "UploadSiteHostedPicturesResponse", "UploadSiteHostedPicturesResponseType");

        //        respStr = respDoc.OuterXml;

        //        respObj = (UploadSiteHostedPicturesResponseType)deserializeFromXml(respStr, typeof(UploadSiteHostedPicturesResponseType));

        //        if (respObj != null && respObj.Errors != null && respObj.Errors.Count > 0)
        //        {
        //            throw new ApiException(new ErrorTypeCollection(respObj.Errors));
        //        }

        //        return respObj;

        //    }
        //    catch (Exception ex)
        //    {
        //        ApiException mApiExcetpion = null;

        //        if (ex is ApiException)
        //        {
        //            mApiExcetpion = (ApiException)ex;
        //        }
        //        else
        //        {
        //            mApiExcetpion = new ApiException(ex.Message, ex);
        //        }
        //        MessageSeverity svr = mApiExcetpion.SeverityErrorCount > 0 ? MessageSeverity.Error : MessageSeverity.Warning;
        //        LogMessage("EPS Exception : " + mApiExcetpion.Message, MessageType.Exception, svr);

        //        if (mApiExcetpion.SeverityErrorCount > 0)
        //            throw mApiExcetpion;
        //    }

        //    return respObj;
        //}
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public Core.Soap.UserType GetUser(string userId = null, string itemID = null)
        {
            //var apicall = new GetUserCall(this.ApiContext);
            //if (!string.IsNullOrWhiteSpace(userId))
            //    apicall.UserID = userId;
            //if (!string.IsNullOrWhiteSpace(itemID))
            //    apicall.ItemID = itemID;
            //UserType user = apicall.GetUser();
            //return user;
            var apicall = this.GetUser(new Core.Soap.GetUserRequestType { UserID = userId, ItemID = itemID });
            return apicall.User;
        }
        #region 帖子更新

        /// <summary>
        /// 获取帖子
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Core.Soap.ItemType GetItem(string itemID)
        {
            var apicall = this.GetItem(new Core.Soap.GetItemRequestType { ItemID = itemID });
            return apicall.Item;
        }

        /// <summary>
        /// 更新帖子信息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public System.Collections.Generic.List<Core.Soap.FeeType> ReviseItem(Core.Soap.ItemType item)
        {
            try
            {
                //var apicall = new ReviseItemCall(this.ApiContext);
                //StringCollection deletedFields = new StringCollection();
                //apicall.DeletedFieldList = deletedFields;
                //apicall.ReviseItem(item, deletedFields, false);
                //FeeTypeCollection fees = apicall.FeeList;
                //return fees;

                var apicall = this.ReviseItem(new Core.Soap.ReviseItemRequestType { Item = item, DeletedField = new System.Collections.Generic.List<string> { }, VerifyOnly = false });
                return apicall.Fees;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
        //public Core.Soap.GetApiAccessRulesResponseType GetApiAccessRules1(Core.Soap.GetApiAccessRulesRequestType request)
        //{
        //    return GetApiAccessRulesAsync(request).Result;
        //}
        //public async System.Threading.Tasks.Task<Core.Soap.GetApiAccessRulesResponseType> GetApiAccessRulesAsync(Core.Soap.GetApiAccessRulesRequestType request)
        //{
        //    var apiName = "GetApiAccessRules";
        //    var url = getUrl(apiName, SiteCode);
        //    var client = Core.Sdk.eBayAPIInstance.Instance.GeteBayAPIClient(url);
        //    var requestInfo = new Core.Soap.GetApiAccessRulesRequest
        //    {
        //        GetApiAccessRulesRequestType = handleRequest(request),
        //        RequesterCredentials = this.securityHeader
        //    };
        //    var response = await client.GetApiAccessRulesAsync(requestInfo);
        //    return response.GetApiAccessRulesResponseType;
        //}
    }
}
