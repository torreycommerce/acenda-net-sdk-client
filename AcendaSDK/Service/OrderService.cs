using AcendaSDK.DTOs;
using System;
using System.Net;
using System.Net.Http;

namespace AcendaSDK.Service
{
    public class OrderService : IService
    {
        private static object lockObject = new object();
        private static OrderService orderService;
        private AuthInfo _authInfo = new AuthInfo();
        private AuthParameters _authParameters = new AuthParameters();
        private OrderService(AuthInfo authInfo, AuthParameters authParameters)
        {
            _authParameters = authParameters;
            _authInfo = authInfo;
        }

        /// <summary>
        /// OrderListDTO
        /// </summary>
        /// <typeparam name="T">OrderListDTO</typeparam>
        /// <returns></returns>
        public T GetAll<T>() where T : new()
        {
            Response response = new Response();
            T orderDTO = new T();
            var token = AuthorizationService.Authorize(_authParameters.ClientId, _authParameters.ClientSecret, _authParameters.StoreName);
            var url = HelperFunctions.CreateUrlFromParts(_authParameters.StoreName, Constants.apiOrder, "", token.access_token);
            var result = HelperFunctions.HttpGet(url).GetAwaiter().GetResult();
            if (result.IsSuccessStatusCode)
            {
                response.HttpStatusCode = result.StatusCode;
                response.Result = result.Content.ReadAsAsync<OrderListDTO>().Result;
                if (response.Result != null)
                {
                    orderDTO = (T)response.Result;
                }
                else
                {
                    return default(T);
                }
            }
            else
            {
                response.HttpStatusCode = result.StatusCode;
                return orderDTO;
            }


            return orderDTO;
        }
        /// <summary>
        /// OrderDTO
        /// </summary>
        /// <typeparam name="T">OrderDTO</typeparam>
        /// <param name="id"></param>
        /// <returns></returns>

        public T GetById<T>(string id) where T : new()
        {
            Response response = new Response();
            T orderDTO = new T();
            var token = AuthorizationService.Authorize(_authParameters.ClientId, _authParameters.ClientSecret, _authParameters.StoreName);
            var url = HelperFunctions.CreateUrlFromParts(_authParameters.StoreName, Constants.apiOrder, id, token.access_token);
            var result = HelperFunctions.HttpGet(url).GetAwaiter().GetResult();
            if (result.IsSuccessStatusCode)
            {
                response.HttpStatusCode = result.StatusCode;
                response.Result = result.Content.ReadAsAsync<T>().Result;
                if (response.Result != null)
                {
                    orderDTO = (T)response.Result;
                }
                else
                {
                    return default(T);
                }
            }
            else
            {
                response.HttpStatusCode = result.StatusCode;
                return orderDTO;
            }


            return orderDTO;

        }


        public static OrderService Instance(AuthInfo authInfo, string clientId, string clientSecret, string storeName)
        {
            if (orderService == null)
            {
                lock (lockObject)
                {
                    if (orderService == null)
                    {
                        orderService = new OrderService(authInfo, new AuthParameters()
                        {
                            ClientId = clientId,
                            ClientSecret = clientSecret,
                            StoreName = storeName
                        });
                    }
                }
            }

            return orderService;
        }

        public BaseDTO Create(object data)
        {
            Response response = new Response();
            var type = new CreateOrderDTO();

            if (data.GetType() == type.GetType())
            {


                var token = AuthorizationService.Authorize(_authParameters.ClientId, _authParameters.ClientSecret, _authParameters.StoreName);
                var url = HelperFunctions.CreateUrlFromParts(_authParameters.StoreName, Constants.apiOrder,string.Empty, token.access_token);
                var result = HelperFunctions.HttpPost(url, data).GetAwaiter().GetResult();
                if (result != null)
                {

                    return result;

                }
                else
                {
                    return new BaseDTO()
                    {
                        code = (int)HttpStatusCode.BadRequest,


                    };
                }

            }
            else
            {
                throw new System.Exception("Not suppoerted type of parameter");
            }
        }

        public BaseDTO CreateFulfillments(string orderId ,FulfillmentsDTO fulfillments)
        {
            Response response = new Response();
        

            if (fulfillments != null && !string.IsNullOrEmpty(orderId))
            {


                var token = AuthorizationService.Authorize(_authParameters.ClientId, _authParameters.ClientSecret, _authParameters.StoreName);
                var url = HelperFunctions.CreateUrlFromParts(_authParameters.StoreName, Constants.apiOrder, orderId + "/fulfillments", token.access_token);
                var result = HelperFunctions.HttpPost(url, fulfillments).GetAwaiter().GetResult();
                if (result != null)
                {

                    return result;

                }
                else
                {
                    return new BaseDTO()
                    {
                        code = (int)HttpStatusCode.BadRequest,


                    };
                }

            }
            else
            {
                throw new System.Exception("Null parameters");
            }


        }

        public OrderGetFulfillmentsDTO GetFulfillments(string orderid, string fulfillmentsId)
        {
            Response response = new Response();
            OrderGetFulfillmentsDTO orderDTO = new OrderGetFulfillmentsDTO();
            var token = AuthorizationService.Authorize(_authParameters.ClientId, _authParameters.ClientSecret, _authParameters.StoreName);
            var url = HelperFunctions.CreateUrlFromParts(_authParameters.StoreName, Constants.apiOrder, orderid + "/fulfillments/?id= " + fulfillmentsId,  token.access_token);
            var result = HelperFunctions.HttpGet(url).GetAwaiter().GetResult();
            if (result.IsSuccessStatusCode)
            {
                response.HttpStatusCode = result.StatusCode;
                response.Result = result.Content.ReadAsAsync<OrderGetFulfillmentsDTO>().Result;
                if (response.Result != null)
                {
                    orderDTO = (OrderGetFulfillmentsDTO)response.Result;
                }
                else
                {
                    return default(OrderGetFulfillmentsDTO);
                }
            }
            else
            {
                response.HttpStatusCode = result.StatusCode;
                return orderDTO;
            }


            return orderDTO;
        }
        public BaseDTO Update(string id, object data)
        {
            throw new NotImplementedException();
        }

        public BaseDTO Delete(string id)
        {
            throw new NotImplementedException();
        }
    }


}



