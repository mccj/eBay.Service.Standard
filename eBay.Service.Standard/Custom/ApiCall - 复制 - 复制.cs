//#region Copyright
////	Copyright (c) 2013 eBay, Inc.
////	
////	This program is licensed under the terms of the eBay Common Development and
////	Distribution License (CDDL) Version 1.0 (the "License") and any subsequent  
////	version thereof released by eBay.  The then-current version of the License can be 
////	found at http://www.opensource.org/licenses/cddl1.php and in the eBaySDKLicense 
////	file that is under the eBay SDK ../docs directory
//#endregion

//#region Namespaces
//using System;
//using System.Reflection;
//using System.Collections;
////using System.Web.Services.Protocols;
//using System.Runtime.InteropServices;
//using eBay.Service.Call;
//using eBay.Service.Util;
//using eBay.Service.Core.Soap;
//using System.Collections.Generic;
//using System.ServiceModel.Description;
//using System.ServiceModel.Channels;
//using System.ServiceModel.Dispatcher;
//using System.ServiceModel;
////using System.Web.Services.Protocols;

//#endregion

//namespace eBay.Service.Core.Soap
//{
//    [System.CodeDom.Compiler.GeneratedCodeAttribute("CodeGen05", "1.0.6708.30800")]
//    [System.SerializableAttribute()]
//    [System.Diagnostics.DebuggerStepThroughAttribute()]
//    [System.ComponentModel.DesignerCategoryAttribute("code")]
//    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:ebay:apis:eBLBaseComponents")]
//    public partial class GetApiAccessRulesResponseType : AbstractResponseType
//    {
//        private ApiAccessRuleTypeCollection mApiAccessRule;
//        /// <summary>
//        /// 
//        /// </summary>
//        [System.Xml.Serialization.XmlElementAttribute("ApiAccessRule")]
//        public ApiAccessRuleTypeCollection ApiAccessRule
//        {
//            get
//            {
//                return this.mApiAccessRule;
//            }
//            set
//            {
//                this.mApiAccessRule = value;
//            }
//        }
//    }

//    public sealed class ApiAccessRuleTypeCollection : System.Collections.CollectionBase
//    {
//        /// <summary>
//        /// 
//        /// </summary>
//        public ApiAccessRuleTypeCollection()
//        {
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="items"></param>
//        public ApiAccessRuleTypeCollection(ApiAccessRuleType[] items)
//        {
//            this.AddRange(items);
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="items"></param>
//        public ApiAccessRuleTypeCollection(ApiAccessRuleTypeCollection items)
//        {
//            this.AddRange(items);
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        public ApiAccessRuleType this[int index]
//        {
//            get
//            {
//                return ((ApiAccessRuleType)(this.InnerList[index]));
//            }
//            set
//            {
//                this.InnerList[index] = value;
//            }
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        public bool IsFixedSize
//        {
//            get
//            {
//                return this.InnerList.IsFixedSize;
//            }
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        public bool IsReadOnly
//        {
//            get
//            {
//                return this.InnerList.IsReadOnly;
//            }
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        public bool IsSynchronized
//        {
//            get
//            {
//                return this.InnerList.IsSynchronized;
//            }
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        public object SyncRoot
//        {
//            get
//            {
//                return this.InnerList.SyncRoot;
//            }
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="item"></param>
//        /// <returns></returns>
//        public int Add(ApiAccessRuleType item)
//        {
//            return this.InnerList.Add(item);
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="items"></param>
//        public void AddRange(ApiAccessRuleType[] items)
//        {
//            this.InnerList.AddRange(items);
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="items"></param>
//        public void AddRange(ApiAccessRuleTypeCollection items)
//        {
//            this.InnerList.AddRange(items);
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="item"></param>
//        /// <returns></returns>
//        public bool Contains(ApiAccessRuleType item)
//        {
//            return this.InnerList.Contains(item);
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="items"></param>
//        /// <param name="index"></param>
//        public void CopyTo(ApiAccessRuleType[] items, int index)
//        {
//            this.InnerList.CopyTo(items, index);
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="item"></param>
//        /// <returns></returns>
//        public int IndexOf(ApiAccessRuleType item)
//        {
//            return this.InnerList.IndexOf(item);
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="index"></param>
//        /// <param name="item"></param>
//        public void Insert(int index, ApiAccessRuleType item)
//        {
//            this.InnerList.Insert(index, item);
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="index"></param>
//        /// <returns></returns>
//        public ApiAccessRuleType ItemAt(int index)
//        {
//            return ((ApiAccessRuleType)(this.InnerList[index]));
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="item"></param>
//        public void Remove(ApiAccessRuleType item)
//        {
//            this.InnerList.Remove(item);
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <returns></returns>
//        public ApiAccessRuleType[] ToArray()
//        {
//            return ((ApiAccessRuleType[])(this.InnerList.ToArray(typeof(ApiAccessRuleType))));
//        }
//    }

//}
