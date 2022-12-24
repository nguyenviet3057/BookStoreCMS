using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreAPIClientCMS.Models
{
    public class Suppliers
    {
        public string Supplier_ID { get; set; }
        public string Stock_ID { get; set; }
        public string Supplier_name { get; set; }
        public string Supplier_City { get; set; }
        public string Supplier_Street { get; set; }
        public string Supplier_PCode { get; set; }
        public string Supplier_email { get; set; }
        public string Supplier_phone { get; set; }
    }
}