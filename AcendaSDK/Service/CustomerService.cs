using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AcendaSDK.DTOs;

namespace AcendaSDK.Service
{
    public class CustomerService : IService
    {
        private static object lockObject = new object();
        private static CustomerService customerService;
        private AuthInfo _authInfo = new AuthInfo();
        private AuthParameters _authParameters = new AuthParameters();
        private CustomerService(AuthInfo authInfo, AuthParameters authParameters)
        {
            _authParameters = authParameters;
            _authInfo = authInfo;
        }
        public BaseDTO Create(object data)
        {

            Response response = new Response();
            var type = new CreateCustomerDTO();

            if (data.GetType() == type.GetType())
            {


                var token = AuthorizationService.Authorize(_authParameters.ClientId, _authParameters.ClientSecret, _authParameters.StoreName);
                var url = HelperFunctions.CreateUrlFromParts(_authParameters.StoreName, Constants.apiCustomer, string.Empty, token.access_token);
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

        public BaseDTO Delete(string id)
        {
            Response response = new Response();
            BaseDTO baseDTO = new BaseDTO();
            var token = AuthorizationService.Authorize(_authParameters.ClientId, _authParameters.ClientSecret, _authParameters.StoreName);
            var url = HelperFunctions.CreateUrlFromParts(_authParameters.StoreName, Constants.apiCustomer, "?id=" + id, token.access_token);
            var result = HelperFunctions.HttpDelete(url).GetAwaiter().GetResult();
            if (result.IsSuccessStatusCode)
            {
                response.HttpStatusCode = result.StatusCode;
                response.Result = result.Content.ReadAsAsync<BaseDTO>().Result;
                if (response.Result != null)
                {
                    baseDTO = (BaseDTO)response.Result;
                }
                else
                {
                    return default(BaseDTO);
                }
            }
            else
            {

                baseDTO.status = result.StatusCode.ToString();
                baseDTO.code = (int)result.StatusCode;
                return baseDTO;
            }


            return baseDTO;
        }

        public T GetAll<T>() where T : new()
        {
            throw new NotImplementedException();
        }

        public T GetById<T>(string id) where T : new()
        {
            throw new NotImplementedException();
        }

        public BaseDTO Update(string id, object data)
        {
            throw new NotImplementedException();
        }
        public static CustomerService Instance(AuthInfo authInfo, string clientId, string clientSecret, string storeName)
        {
            if (customerService == null)
            {
                lock (lockObject)
                {
                    if (customerService == null)
                    {
                        customerService = new CustomerService(authInfo, new AuthParameters()
                        {
                            ClientId = clientId,
                            ClientSecret = clientSecret,
                            StoreName = storeName
                        });
                    }
                }
            }

            return customerService;
        }
    }
}
