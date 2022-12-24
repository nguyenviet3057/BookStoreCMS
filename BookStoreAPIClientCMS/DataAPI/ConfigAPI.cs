using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreAPIClientCMS.DataAPI
{
    public class ConfigAPI
    {
        public static string BASE_URL = "https://localhost:44309/";
        public static string API_GET_USER_BY_ID = "api/Users/"; //+ id
        public static string API_GET_USER_USR = "api/Users?usr="; //+ username
        public static string API_GET_USER_PWD = "&pwd="; //+ username
        public static string API_GET_TOKEN = "api/Login_Tokens?token="; //+ token
        public static string API_GET_TOKEN_BY_ID = "api/Login_Tokens/"; //+ id
        public static string API_INSERT_TOKEN = "api/Login_Tokens"; //using object
        public static string API_GET_STOCKS = "api/Stocks";
        public static string API_INSERT_STOCK = "api/Stocks";
        public static string API_GET_STOCK_BY_ID = "api/Stocks/";
        public static string API_UPDATE_STOCK = "api/Stocks";
        public static string API_DELETE_STOCK = "api/Stocks/";
    }
}