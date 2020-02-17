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

        ServiceFactory serviceFactory = new ServiceFactory("de68b3a16c43a9cc50eba78f2118bb13@acenda.com", "9be679e98cb861c1c50ff9498b7f1f1f", "netclient");
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
          //401
          BaseDTO customerDeleteResult = customer.Delete("3191672");
        }
        catch (Exception ex)
        {

        }

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
                            id = 74,
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

          var orderGetFUlfillmentsResult = order.GetFulfillments("2562611", "86");
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
