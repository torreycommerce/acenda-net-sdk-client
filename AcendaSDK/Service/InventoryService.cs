using AcendaSDK.DTOs;
using System.Net;

namespace AcendaSDK.Service
{
    public class InventoryService : IService
    {
        private static object lockObject = new object();
        private static InventoryService inventoryService;
        private AuthInfo _authInfo = new AuthInfo();
        private AuthParameters _authParameters = new AuthParameters();
        private InventoryService(AuthInfo authInfo, AuthParameters authParameters)
        {
            _authParameters = authParameters;
            _authInfo = authInfo;
        }
        public T GetAll<T>() where T : new()
        {
            throw new System.NotImplementedException();
        }

        public T GetById<T>(string id) where T : new()
        {
            throw new System.NotImplementedException();
        }

      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data">VariantDTO</param>
        /// <returns></returns>
        public BaseDTO Update(string id, object data)
        {

            Response response = new Response();
            var type = new VariantDTO();

            if (data.GetType() == type.GetType())
            {


                var token = AuthorizationService.Authorize(_authParameters.ClientId, _authParameters.ClientSecret, _authParameters.StoreName);
                var url = HelperFunctions.CreateUrlFromParts(_authParameters.StoreName, Constants.apiVariant, id, token.access_token);
                var result = HelperFunctions.HttpPut(url, data).GetAwaiter().GetResult();
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
        public static InventoryService Instance(AuthInfo authInfo, string clientId, string clientSecret, string storeName)
        {
            if (inventoryService == null)
            {
                lock (lockObject)
                {
                    if (inventoryService == null)
                    {
                        inventoryService = new InventoryService(authInfo, new AuthParameters()
                        {
                            ClientId = clientId,
                            ClientSecret = clientSecret,
                            StoreName = storeName
                        });
                    }
                }
            }

            return inventoryService;
        }

        public BaseDTO Create(object data)
        {
            throw new System.NotImplementedException();
        }

        public BaseDTO Delete(string id)
        {
            throw new System.NotImplementedException();
        }
    }


}
