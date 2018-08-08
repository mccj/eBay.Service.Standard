//public class eeeee
//{
//    public static void eeeefff()
//    {
//        var address = new System.ServiceModel.EndpointAddress("https://api.ebay.com/wsapi?callname=GetItem&siteid=0&client=netsoap");
//        //var binding = new System.ServiceModel.WSHttpBinding();
//        var binding = new System.ServiceModel.BasicHttpsBinding()
//        {
//            //CloseTimeout = TimeSpan.FromMilliseconds(Timeout),
//            MaxBufferPoolSize = int.MaxValue,
//            MaxBufferSize = int.MaxValue,
//            MaxReceivedMessageSize = int.MaxValue
//        };
//        using (var factory = new System.ServiceModel.ChannelFactory<eBayAPIInterface>(binding, address))
//        {
//            var channel = factory.CreateChannel();

//            var ccc = new CustomSecurityHeaderType
//            {
//                eBayAuthToken = "AgAAAA**AQAAAA**aAAAAA**xzEWWw**nY+sHZ2PrBmdj6wVnY+sEZ2PrA2dj6AEmISkD5iAog+dj6x9nY+seQ**vNsAAA**AAMAAA**ifIc+dCZ67RP8tXIdKOVSHiYCc5rQOnHSe8BCou0I6ZLmoNJDCwYTAHcNVw8RT8gCVrzdgzanA3tY1P3dTb1fBS5sJ4gfLuZxwTWdM7LiqqxCFl2ie1kxbHzlfTxhOWM4nvNpx74zbQUYwBs0jkkJ4Rm573bOPYmIBZQ1CvKcZ53UAxWUNx/xuFCGzYSn5Vn9SyiIVmdmATg7nJCuDB6Q1QlhzEOgDL9MiJLCT9DrEZwqQ2p83/Vn7npXSKT1al3WjGKkjv4FVkKjiYPP60ZYN35YNq2k4jkE016YnNonBEDbtwIrKCl2ZlrKMKGzpt3lnzklrnnQDXSYAyCWWMTTvkN2vjSmIWLJ6boFj5qbCEwYVQPczabXuthZ+Nj9MbXrhzUYWJoLJrsj3KB4+uKi/x8jyo+QnUC/rF+UbIsSC1sJsytr7xAo+8uEKHb7LF2mT3McXxk7V41C/tkwefXIWiRkJni3/8Lt8dQcjG3ZWP+OgDSHsjlpe2SxAla/2xWpCbowxK/XV6ZKY82rnbIZzxq9EQkR34ovnoEm9aGJDtl4fS2d/BrP//p4uyw8Be+Um5mb9IQXeycTr8T+9HYSVEXspfgvf80AhftjoARBEidTOba3vLJPe1E3vGRhk6wnK6a3JBKV0LYEJQ/PUz3GI78JSivKhHccwCImz0HT0Wt575MwugYoZMJ3+YDpZQirPg6rinx3MkCwYwdiyjapGaVlAeQOkIQXBpiRSctRfi9uUuQGWwlbsNNFKpRE3ye"
//            };
//            //var ssssss = channel.GetApiAccessRules(new GetApiAccessRulesRequest
//            //{
//            //    GetApiAccessRulesRequestType = new GetApiAccessRulesRequestType
//            //    {
//            //        Version = "1065",
//            //        MessageID = System.Guid.NewGuid().ToString(),
//            //    },
//            //    RequesterCredentials = ccc
//            //});


//            var rules11 = channel.GetItem(new GetItemRequest
//            {
//                GetItemRequestType = new GetItemRequestType
//                {
//                    ItemID = "323269291886",
//                    Version="1065",
//                    MessageID = System.Guid.NewGuid().ToString()
//                },
//                RequesterCredentials = ccc
//            });
//        }

//        ////nettcpbinding 绑定方式
//        //public static object ExecuteMethod<T>(string pUrl, string pMethodName, params object[] pParams)
//        //{
//        //    EndpointAddress address = new EndpointAddress(pUrl);
//        //    Binding bindinginstance = null;
//        //    NetTcpBinding ws = new NetTcpBinding();
//        //    ws.MaxReceivedMessageSize = 20971520;
//        //    ws.Security.Mode = SecurityMode.None;
//        //    bindinginstance = ws;
//        //    using (ChannelFactory<T> channel = new ChannelFactory<T>(bindinginstance, address))
//        //    {
//        //        T instance = channel.CreateChannel();
//        //        using (instance as IDisposable)
//        //        {
//        //            try
//        //            {
//        //                Type type = typeof(T);
//        //                MethodInfo mi = type.GetMethod(pMethodName);
//        //                return mi.Invoke(instance, pParams);
//        //            }
//        //            catch (TimeoutException)
//        //            {
//        //                (instance as ICommunicationObject).Abort();
//        //                throw;
//        //            }
//        //            catch (CommunicationException)
//        //            {
//        //                (instance as ICommunicationObject).Abort();
//        //                throw;
//        //            }
//        //            catch (Exception vErr)
//        //            {
//        //                (instance as ICommunicationObject).Abort();
//        //                throw;
//        //            }
//        //        }
//        //    }
//        //}

//    }
//}