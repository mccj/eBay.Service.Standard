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
using System.Linq;
using System.Reflection;
using System.Collections;
//using System.Web.Services.Protocols;
using System.Runtime.InteropServices;
using eBay.Service.Call;
using eBay.Service.Util;
using eBay.Service.Core.Soap;
using System.Collections.Generic;
//using System.Web.Services.Protocols;

#endregion

namespace eBay.Service.Core.Sdk
{
    public partial class ApiCall
    {

        internal void SendRequest2()
        {
            try
            {
                if (AbstractRequest == null)
                    throw new ApiException("RequestType reference not set to an instance of an object.", new System.ArgumentNullException());
                if (ApiContext == null)
                    throw new ApiException("ApiContext reference not set to an instance of an object.", new System.ArgumentNullException());
                if (ApiContext.ApiCredential == null)
                    throw new ApiException("Credentials reference in ApiContext object not set to an instance of an object.", new System.ArgumentNullException());



                string apiName = AbstractRequest.GetType().Name.Replace("RequestType", "");
                var requestName = Type.GetType("eBay.Service.Core.Soap." + apiName + "Request");


                if (this.ApiContext.EnableMetrics)
                {
                    mCallMetrics = this.ApiContext.CallMetricsTable.GetNewEntry(apiName);
                    mCallMetrics.ApiCallStarted = System.DateTime.Now;
                }


                string url = "";
                try
                {
                    if (ApiContext.SoapApiServerUrl != null && ApiContext.SoapApiServerUrl.Length > 0)
                        url = String.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}?callname={1}&siteid={2}&client=netsoap",
                            ApiContext.SoapApiServerUrl, apiName, SiteUtility.GetSiteID(Site).ToString(System.Globalization.CultureInfo.InvariantCulture));
                    else
                    {
                        url = "https://api.ebay.com/wsapi";
                        url = String.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}?callname={1}&siteid={2}&client=netsoap",
                            url, apiName, SiteUtility.GetSiteID(Site).ToString(System.Globalization.CultureInfo.InvariantCulture));
                    }



                }
                catch (Exception ex)
                {
                    throw new ApiException(ex.Message, ex);
                }

                LogMessage(url, MessageType.Information, MessageSeverity.Informational);






                //mEnableCompression//数据压缩
                //moAuthToken//moAuthToken授权//
                //mWebProxy//代理
                //mCallMetrics速度监控



                var eBayAPIInterfaceClient = eBayAPIInstance.Instance.GeteBayAPIClient(url, timeout: this.Timeout);
                //var 报文 = new 报文();
                //eBayAPIInterfaceClient.Endpoint.EndpointBehaviors.Add(new ContextPropagationBehavior(报文));
                //PropertyInfo pi;

                //pi = this.mServiceType.GetProperty("ApiLogManager");
                //if (pi == null)
                //    throw new SdkException("ApiLogManager was not found in InterfaceServiceType");
                //pi.SetValue(svcInst, this.mApiContext.ApiLogManager, null);

                //pi = this.mServiceType.GetProperty("EnableComression");
                //if (pi == null)
                //    throw new SdkException("EnableComression was not found in InterfaceServiceType");
                //pi.SetValue(svcInst, this.mEnableCompression, null);

                //Add oAuth 
                //if (pi == null)
                //    throw new SdkException("RequesterCredentials was not found in InterfaceServiceType");
                //pi.SetValue(svcInst, this., null);
                //if (string.IsNullOrEmpty(this.ApiContext.ApiCredential.oAuthToken))
                //{
                //    pi = this.mServiceType.GetProperty("RequesterCredentials");
                //    if (pi == null)
                //        throw new SdkException("RequesterCredentials was not found in InterfaceServiceType");
                //    pi.SetValue(svcInst, secHdr, null);
                //}

                //pi = this.mServiceType.GetProperty("WebProxy");
                //if (pi == null)
                //    throw new SdkException("WebProxy was not found in InterfaceServiceType");
                //pi.SetValue(svcInst, this.mApiContext.WebProxy, null);
                //if (this.mApiContext.WebProxy != null)
                //{
                //    LogMessage("Proxy Server is Set", MessageType.Information, MessageSeverity.Informational);
                //}

                //pi = this.mServiceType.GetProperty("CallMetricsEntry");
                //if (pi == null)
                //    throw new SdkException("CallMetricsEntry was not found in InterfaceServiceType");
                //if (this.ApiContext.EnableMetrics)
                //    pi.SetValue(svcInst, this.mCallMetrics, null);
                //else
                //    pi.SetValue(svcInst, null, null);

                //pi = this.mServiceType.GetProperty("OAuthToken");
                //if (!string.IsNullOrEmpty(this.ApiContext.ApiCredential.oAuthToken))
                //{
                //    this.mOAuth = this.ApiContext.ApiCredential.oAuthToken;
                //    pi.SetValue(svcInst, this.OAuth, null);
                //}




                ////svcCCTor.Timeout = Timeout;
                //this.mServiceType.GetProperty("Timeout").SetValue(svcInst, Timeout, null);

                AbstractRequest.Version = Version;

                if (!mDetailLevelOverride && AbstractRequest.DetailLevel == null)
                {
                    AbstractRequest.DetailLevel = mDetailLevelList;
                }

                //Added OutputSelector to base call JIRA-SDK-561
                AbstractRequest.OutputSelector = mOutputSelector;

                if (ApiContext.ErrorLanguage != ErrorLanguageCodeType.CustomCode)
                    AbstractRequest.ErrorLanguage = ApiContext.ErrorLanguage.ToString();

                //Populate the message
                AbstractRequest.MessageID = System.Guid.NewGuid().ToString();

                //Type methodtype = svcInst.GetType();
                //object[] reqparm = new object[] { AbstractRequest };

                CustomSecurityHeaderType secHdr = this.GetSecurityHeader();
                var request = System.Activator.CreateInstance(requestName);
                requestName.GetProperty("RequesterCredentials").SetValue(request, secHdr);
                requestName.GetProperty(apiName+"RequestType").SetValue(request, AbstractRequest);
                //, secHdr, this.AbstractRequest
                int retries = 0;
                int maxRetries = 0;
                bool doretry = false;
                CallRetry retry = null;
                if (mCallRetry != null)
                {
                    retry = mCallRetry;
                    maxRetries = retry.MaximumRetries;
                }
                else if (ApiContext.CallRetry != null)
                {
                    retry = ApiContext.CallRetry;
                    maxRetries = retry.MaximumRetries;
                }



                do
                {
                    Exception callException = null;
                    try
                    {
                        //        mResponse = null;
                        //        mApiException = null;

                        if (retries > 0)
                        {
                            LogMessage("Invoking Call Retry", MessageType.Information, MessageSeverity.Informational);
                            System.Threading.Thread.Sleep(retry.DelayTime);
                        }

                        if (BeforeRequest != null)
                            BeforeRequest(this, new BeforeRequestEventArgs(AbstractRequest));


                        //Invoke the Service
                        DateTime start = DateTime.Now;
                        //mResponse = (AbstractResponseType)methodtype.GetMethod(apiName).Invoke(svcInst, reqparm);

                        if (mCallMetrics != null)
                        {
                            mCallMetrics.NetworkSendStarted = DateTime.Now;
                            //request.Headers.Add("X-EBAY-API-METRICS", "true");
                        }
                        var response = eBayAPIInterfaceClient.GetType().GetMethod(apiName).Invoke(eBayAPIInterfaceClient, new[] { request });

                        mResponse = (AbstractResponseType)response.GetType().GetProperty(apiName + "ResponseType").GetValue(response);
                        if (mCallMetrics != null)
                        {
                            mCallMetrics.NetworkReceiveEnded = DateTime.Now;
                            //mCallMetrics.ServerProcessingTime = convertProcessingTime(response.Headers.Get("X-EBAY-API-PROCESS-TIME"));
                        }
                        //validate(response.StatusCode);
                        mResponseTime = DateTime.Now - start;

                        if (AfterRequest != null)
                            AfterRequest(this, new AfterRequestEventArgs(mResponse));

                        // Catch Token Expiration warning
                        if (mResponse != null && secHdr.HardExpirationWarning != null)
                        {
                            ApiContext.ApiCredential.TokenHardExpirationWarning(
                                System.Convert.ToDateTime(secHdr.HardExpirationWarning, System.Globalization.CultureInfo.CurrentUICulture));
                        }


                        if (mResponse != null && mResponse.Errors != null && mResponse.Errors.Count > 0)
                        {
                            throw new ApiException(new List<ErrorType>(mResponse.Errors));
                        }
                    }

                    catch (Exception ex)
                    {
                        // this catches soap faults 
                        if (ex.GetType() == typeof(TargetInvocationException))
                        {
                            // we never care about the outer exception
                            Exception iex = ex.InnerException;

                            //// Parse Soap Faults
                            //if (iex.GetType() == typeof(SoapException))
                            //{
                            //    ex = ApiException.FromSoapException((SoapException)iex);
                            //}
                            //else
                            if (iex.GetType() == typeof(InvalidOperationException))
                            {
                                // Go to innermost exception
                                while (iex.InnerException != null)
                                    iex = iex.InnerException;
                                ex = new ApiException(iex.Message, iex);
                            }
                            else if (iex.GetType() == typeof(HttpException))
                            {
                                HttpException httpEx = (HttpException)iex;
                                String str = "HTTP Error Code: " + httpEx.StatusCode;
                                ex = new ApiException(str, iex);
                            }
                            else
                            {
                                ex = new ApiException(iex.Message, iex);
                            }
                        }
                        callException = ex;

                        // log the message - override current switches - *if* (a) we wouldn't normally log it, and (b) 
                        // the exception matches the exception filter.

                        if (retry != null)
                            doretry = retry.ShouldRetry(ex);

                        if (!doretry || retries == maxRetries)
                        {
                            throw ex;
                        }
                        else
                        {
                            //string soapReq = 报文.发送.LastOrDefault();//  (string)this.mServiceType.GetProperty("SoapRequest").GetValue(svcInst, null);
                            //string soapResp = 报文.接收.LastOrDefault(); // (string)this.mServiceType.GetProperty("SoapResponse").GetValue(svcInst, null);

                            //LogMessagePayload(soapReq + "\r\n\r\n" + soapResp, MessageSeverity.Informational, ex);
                            MessageSeverity svr = ((ApiException)ex).SeverityErrorCount > 0 ? MessageSeverity.Error : MessageSeverity.Warning;
                            LogMessage(ex.Message, MessageType.Exception, svr);







                        }
                    }

                    finally
                    {
                        //string soapReq = 报文.发送.LastOrDefault();// (string)this.mServiceType.GetProperty("SoapRequest").GetValue(svcInst, null);
                        //string soapResp = 报文.接收.LastOrDefault(); //(string)this.mServiceType.GetProperty("SoapResponse").GetValue(svcInst, null);

                        //if (!doretry || retries == maxRetries)
                        //    LogMessagePayload(soapReq + "\r\n\r\n" + soapResp, MessageSeverity.Informational, callException);

                        if (mResponse != null && mResponse.Timestamp.HasValue)
                            ApiContext.CallUpdate(mResponse.Timestamp.Value);
                        else
                            ApiContext.CallUpdate(new DateTime(0));

                        //mSoapRequest = soapReq;
                        //mSoapResponse = soapResp;
                        retries++;




                        //var ssss = System.Xml.Linq.XElement.Parse(soapResp);
                        //var sdfsdfdsf = ssss.Elements().LastOrDefault().FirstNode.ToString();

                        //var ser = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(eBay.Service.Core.Soap.GetApiAccessRulesResponseType), "GetApiAccessRulesResponse");
                        //using (var ms = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(sdfsdfdsf)))
                        //{
                        //    var jsonObject = ser.ReadObject(ms);
                        //}
                    }

                } while (doretry && retries <= maxRetries);
            }

            catch (Exception ex)
            {
                ApiException aex = ex as ApiException;

                if (aex != null)
                {
                    mApiException = aex;
                }
                else
                {
                    mApiException = new ApiException(ex.Message, ex);
                }
                MessageSeverity svr = mApiException.SeverityErrorCount > 0 ? MessageSeverity.Error : MessageSeverity.Warning;
                LogMessage(mApiException.Message, MessageType.Exception, svr);

                if (mApiException.SeverityErrorCount > 0)
                    throw mApiException;

            }
            finally
            {
                if (this.ApiContext.EnableMetrics)
                    mCallMetrics.ApiCallEnded = DateTime.Now;
            }
        }

    }
}
