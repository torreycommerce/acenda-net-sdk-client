using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcendaSDK.DTOs
{
    public class VariantDTO
    {

        public string product_id { get; set; }
        public string status { get; set; }
        public string name { get; set; }
        public string sku { get; set; }
        public string asin { get; set; }
        public string ean { get; set; }
        public string isbn { get; set; }
        public string has_stock { get; set; }
        public string cost { get; set; }
        public string barcode { get; set; }
        public string price { get; set; }
        public string compare_price { get; set; }
        public string popularity { get; set; }
        public string position { get; set; }
        public string images { get; set; }
        public string inventory_quantity { get; set; }
        public string inventory_minimum_quantity { get; set; }
        public string inventory_tracking { get; set; }
        public string inventory_policy { get; set; }
        public string inventory_shipping_estimate { get; set; }
        public string inventory_returnable { get; set; }
        public string require_shipping { get; set; }
        public string discountable { get; set; }
        public string taxable { get; set; }
        public string weight { get; set; }
        public string date_publish { get; set; }
        public string date_expire { get; set; }
       
    }
}
