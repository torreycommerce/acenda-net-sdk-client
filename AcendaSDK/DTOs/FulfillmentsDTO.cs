using System.Collections.Generic;

namespace AcendaSDK.DTOs
{
    public class FulfillmentsDTO
    {
        public string shipping_method { get; set; }
        public int? order_id { get; set; }
        public string status { get; set; }
        public string tracking_company { get; set; }
        public string tracking_number { get; set; }
        public List<string> tracking_numbers { get; set; }

        public string tracking_url { get; set; }
        public List<string> tracking_urls { get; set; }

        public List<FulfillmentItems> items { get; set; }
        public List<FulfillmentPackages> packages { get; set; }
    }

    public class FulfillmentItems
    {
        public int id { get; set; }
        public int quantity { get; set; }

    }
    public class FulfillmentPackages
    {
        public string height { get; set; }
        public string width { get; set; }
        public string depth { get; set; }
        public string weight { get; set; }
    }
}
