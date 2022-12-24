using BookStoreAPIClientCMS.DataAPI;
using BookStoreAPIClientCMS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace BookStoreAPIClientCMS.Controllers
{
    public class UsersController : Controller
    {
        public ActionResult Login()
        {
            HttpCookie token = Request.Cookies.Get("token");
            if (System.Web.HttpContext.Current.Session["User"] == null || token == null || token.Value == "")
            {
                return View();
            }
            RestClient rClient = new RestClient();
            rClient.BaseUrl = ConfigAPI.BASE_URL;
            rClient.EndPoint = ConfigAPI.API_GET_TOKEN + token.Value;

            ResponseMessage responseResult = rClient.RestRequestAll();
            if (responseResult.Status == 0) return View();
            Login_Tokens login_token = JsonConvert.DeserializeObject<Login_Tokens>(responseResult.Msg);

            rClient.EndPoint = ConfigAPI.API_GET_USER_BY_ID + login_token.UserId;
            responseResult = rClient.RestRequestAll();
            if (responseResult.Status == 0) return View();
            Users users = JsonConvert.DeserializeObject<Users>(responseResult.Msg);

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            try
            {
                if (form["Username"] == null)
                {
                    return View("Login");
                }
                string Username = form["Username"];
                string Password = form["Password"];

                RestClient rClient = new RestClient();
                rClient.BaseUrl = ConfigAPI.BASE_URL;
                rClient.EndPoint = ConfigAPI.API_GET_USER_USR + Username + ConfigAPI.API_GET_USER_PWD + Password;

                ResponseMessage responseResult = rClient.RestRequestAll();
                if (responseResult.Status == 0) return View("Login");

                Users users = JsonConvert.DeserializeObject<Users>(responseResult.Msg);
                if (users == null)
                {
                    return View("Login");
                }
                System.Web.HttpContext.Current.Session["User"] = users;
                string token = Utility.GetMD5(users.Username + DateTime.Now + users.Id);

                HttpCookie UserCookie = new HttpCookie("token");
                UserCookie.Value = token;
                UserCookie.Expires = DateTime.Now.AddMinutes(10);
                Response.Cookies.Add(UserCookie);

                Login_Tokens login_token = new Login_Tokens();
                login_token.UserId = users.Id;
                login_token.Token = token;

                rClient.EndPoint = ConfigAPI.API_INSERT_TOKEN;

                responseResult = rClient.InsertData(login_token);

                if (responseResult.Status == 1)
                {
                    return RedirectToAction("Index", "Home");
                }
                return View();
            }
            catch
            {
                return HttpNotFound();
            }
        }

        public ActionResult Logout()
        {
            try
            {
                HttpCookie token = Request.Cookies.Get("token");
                if (token == null || token.Value == "")
                {
                    return RedirectToAction("Login", "Users");
                }

                RestClient rClient = new RestClient();
                rClient.BaseUrl = ConfigAPI.BASE_URL;
                rClient.EndPoint = ConfigAPI.API_GET_TOKEN + token.Value;

                Response.Cookies["token"].Expires = DateTime.Now.AddDays(-1); //Delete token

                ResponseMessage responseResult = rClient.RestRequestAll();
                Login_Tokens login_token = JsonConvert.DeserializeObject<Login_Tokens>(responseResult.Msg);

                rClient.EndPoint = ConfigAPI.API_GET_TOKEN_BY_ID + login_token.Id;

                responseResult = rClient.DeleteData();

                if (responseResult.Status == 1)
                {
                    System.Web.HttpContext.Current.Session["User"] = null;
                    return RedirectToAction("Login", "Users");
                }
                return Content("<h1>Gặp lỗi! Ấn F5</h1>");
            }
            catch
            {
                return HttpNotFound();
            }
        }

        public ActionResult Index()
        {
            if (Utility.authenToken() == null) return RedirectToAction("Login", "Users");
            return View();
        }
    }
}