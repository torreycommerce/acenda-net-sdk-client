using AcendaSDK.DTOs;
using AcendaSDK.Service;
using System;
using System.Collections.Generic;

namespace ClientAcenda
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                //Fill in below fields with client id, client secret and your store name
                ServiceFactory serviceFactory = new ServiceFactory("3ed034cf215262b0d9c0a124e80158e6@acenda.com", "8f79d7cb1ee6f32af0fbf0359f1122c2", "botstore");
                ProductService product = (ProductService)serviceFactory.GetService(AcendaSDK.Enums.ServiceType.Product);
                OrderService order = (OrderService)serviceFactory.GetService(AcendaSDK.Enums.ServiceType.Order);
                InventoryService inventory = (InventoryService)serviceFactory.GetService(AcendaSDK.Enums.ServiceType.Inventory);
                CustomerService customer = (CustomerService)serviceFactory.GetService(AcendaSDK.Enums.ServiceType.Customer);

                // Get Customer By Id       
                CustomerDTO customerDTO = customer.GetById<CustomerDTO>("3191680");

                //Get All Customers
                CustomerListDTO customerListDTO = customer.GetAll<CustomerListDTO>();

                //Update Customer

                BaseDTO customerUpdateResult = customer.Update("3191680", new CreateUpdateCustomerDTO()
                {
                    //first_name = "Alex",
                    //last_name = "Vivian",
                    //email = "alex@acenda.com",
                    phone_number = "555-555-5555"
                });

                //Delete a customer by Id
                //BaseDTO customerDeleteResult = customer.Delete("3191677");

                //Define the api endpoint of the generic service (ex "customer")
                GenericService generic = (GenericService)serviceFactory.GetService(AcendaSDK.Enums.ServiceType.Generic, "customer");

                //Create a customer using generic service
                BaseDTO genericCreateResult = generic.Create(new CreateUpdateCustomerDTO()
                {
                    first_name = "Bob",
                    last_name = "Smith",
                    email = "bob33@acenda.com",
                    phone_number = "123-123-1234"
                });

                //Get List of customers by using Generic Service

                CustomerListDTO customerListFromGenericDTO = generic.GetAll<CustomerListDTO>();
                //Get List of all Products
                ProductListDTO productDTOs = product.GetAll<ProductListDTO>();

                //Get Product By Id
                ProductDTO productDTO = product.GetById<ProductDTO>("2");
                GenericService variant = (GenericService)serviceFactory.GetService(AcendaSDK.Enums.ServiceType.Generic, "variant");

                GenericResponseDTO response = variant.GetAllPaginated<GenericResponseDTO>(2, 100,"{'status':'active'}");

                //Get Product Variant By Id
                ProductVariantsDTO productVariants = product.GetVariants("2");

                //Get List of Orders
                OrderListDTO orderListDTO = order.GetAll<OrderListDTO>();

                //Get Order By Id
                OrderDTO orderDTO = order.GetById<OrderDTO>("2562634");


                //Create a customer using customer service
                BaseDTO customerCreateResult = customer.Create(new CreateUpdateCustomerDTO()
                {
                    first_name = "Bob",
                    last_name = "Smith",
                    email = "bob19@acenda.com",
                    phone_number = "123-123-1234"
                });


                //Update inventory of a variant by Id (5 is variantId)
                BaseDTO inventoryUpdateResult = inventory.Update("5", new VariantDTO() { inventory_quantity = "200" });

                BillingAddress billingAddress = new BillingAddress()
                {
                    first_name = "bob",
                    last_name = "smith",
                    phone_number = "123-123-1234",
                    street_line1 = "123 Test ln. ",

                    city = "San Diego",
                    state = "CA",
                    zip = "92101",
                    country = "US"
                };
                ShippingAddress shippingAddress = new ShippingAddress()
                {
                    first_name = "bob",
                    last_name = "smith",
                    phone_number = "123-123-1234",
                    street_line1 = "123 Test ln. ",

                    city = "San Diego",
                    state = "CA",
                    zip = "92101",
                    country = "US"
                };
                List<CreateOrderItem> creatOrderItem = new List<CreateOrderItem>();
                creatOrderItem.Add(new CreateOrderItem()
                {
                    quantity = 1,
                    product_id = 2


                });

                CreateOrderDTO createOrderDTO = new CreateOrderDTO()
                {
                    email = "cob@acenda.com",

                    billing_address = billingAddress,


                    shipping_address = shippingAddress,


                    items = creatOrderItem,



                };

                //Create order
                BaseDTO orderCreateResult = order.Create(createOrderDTO);

                try
                {
                    //Create fulfillment for order Id  2562599         
                    BaseDTO result = order.CreateFulfillments("2562599", new FulfillmentsDTO()
                    {
                        tracking_numbers = new List<string>() { "1Z999AA10123456784" },
                        tracking_urls = new List<string>() { "https://www.ups.com/track?loc=en_US&tracknum=1Z999AA10123456784/trackdetails" },
                        tracking_company = "UPS",
                        shipping_method = "Ground",
                        status = "success",
                        items = new List<FulfillmentItems>()
                    {
                        new FulfillmentItems()
                        {
                            id = 74,//item id inside order object
                            quantity  =1,
                        }
                    }

                    });
                }
                catch (Exception ex)
                {

                }

                try
                {
                    //Get Fulfillment details of given OrderId and FulfillmentId
                    var orderGetFulfillmentsResult = order.GetFulfillments("2562611", "86");
                }
                catch (Exception ex)
                {

                }

                Console.ReadKey();
            }


            catch (Exception ex)
            {

                var e = ex;
            }
            Console.ReadKey();
        }
    }
}
