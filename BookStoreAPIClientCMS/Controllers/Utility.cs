using BookStoreAPIClientCMS.DataAPI;
using BookStoreAPIClientCMS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BookStoreAPIClientCMS.Controllers
{
    public class Utility : Controller
    {
        public static string GetMD5(string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bHash = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder sbHash = new StringBuilder();
            foreach (byte b in bHash)
            {
                sbHash.Append(String.Format("{0:x2}", b));
            }
            return sbHash.ToString();
        }
        public static Users authenToken()
        {
            if (System.Web.HttpContext.Current.Request.Cookies.Get("token") == null)
            {
                return null;
            }

            if (System.Web.HttpContext.Current.Session["User"] == null)
            {
                return null;
            }

            if (System.Web.HttpContext.Current.Session["User"] != null)
            {
                return (Users)System.Web.HttpContext.Current.Session["User"];
            }

            //string token = System.Web.HttpContext.Current.Request.Cookies.Get("token").Value;

            //RestClient rClient = new RestClient();
            //rClient.BaseUrl = ConfigAPI.BASE_URL;
            //rClient.EndPoint = ConfigAPI.API_GET_TOKEN + token;

            //ResponseMessage responseResult = rClient.RestRequestAll();
            //if (responseResult.Status == 0) return null;

            //Login_Tokens login_token = JsonConvert.DeserializeObject<Login_Tokens>(responseResult.Msg);
            //if (login_token != null)
            //{
            //    rClient.EndPoint = ConfigAPI.API_GET_USER_BY_ID + login_token.UserId;
            //    responseResult = rClient.RestRequestAll();
            //    if (responseResult.Status == 0) return null;

            //    System.Web.HttpContext.Current.Session["User"] = JsonConvert.DeserializeObject<Users>(responseResult.Msg);
            //    return JsonConvert.DeserializeObject<Users>(responseResult.Msg);
            //}

            return null;
        }
    }
}