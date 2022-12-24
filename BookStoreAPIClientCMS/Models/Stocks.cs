using BookStoreAPIClientCMS.DataAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreAPIClientCMS.Models
{
    public class Stocks
    {
        public string STOCK_ID { get; set; }
        public string isbn { get; set; }
        public string Author_name { get; set; }
        public string Title_name { get; set; }
        public string Publisher_name { get; set; }
        public string Published_year { get; set; }
        public string Supplier_ID { get; set; }
        public string Supplier_name { get; set; }
        public string Retail_price { get; set; }
    }
}