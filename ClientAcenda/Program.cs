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
        ServiceFactory serviceFactory = new ServiceFactory("client_id", "client_secret", "store_name");
        var product = (ProductService)serviceFactory.GetService(AcendaSDK.Enums.ServiceType.Product);
        var order = (OrderService)serviceFactory.GetService(AcendaSDK.Enums.ServiceType.Order);
        var inventory = (InventoryService)serviceFactory.GetService(AcendaSDK.Enums.ServiceType.Inventory);
        var customer = (CustomerService)serviceFactory.GetService(AcendaSDK.Enums.ServiceType.Customer);


        ProductListDTO productDTOs = product.GetAll<ProductListDTO>();
        ProductDTO productDTO = product.GetById<ProductDTO>("2");
        ProductVariantsDTO productVariants = product.GetVariants("2");
        OrderListDTO orderListDTO = order.GetAll<OrderListDTO>();
        OrderDTO orderDTO = order.GetById<OrderDTO>("2562611");

        try
        {
          BaseDTO customerCreateResult = customer.Create(new CreateCustomerDTO()
          {
            first_name = "Bob",
            last_name = "Smith",
            email = "bob@acenda.com",
            phone_number = "123-123-1234"
          });
        }
        catch (Exception ex)
        {
    
        }
        try
        {
            //3191672 customer id
            BaseDTO customerDeleteResult = customer.Delete("3191672");
        }
        catch (Exception ex)
        {

        }
         //5 is variantId
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
          email = "bob@acenda.com",

          billing_address = billingAddress,


          shipping_address = shippingAddress,


          items = creatOrderItem,



        };
        BaseDTO orderCreateResult = order.Create(createOrderDTO);

        try
        {
          //2562599 sample order id             
          var result = order.CreateFulfillments("2562599", new FulfillmentsDTO()
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
            //orderid,fulfillmentId
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
