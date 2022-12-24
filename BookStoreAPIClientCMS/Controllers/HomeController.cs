using BookStoreAPIClientCMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStoreAPIClientCMS.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            Users users = Utility.authenToken();
            if (users == null) return RedirectToAction("Login", "Users");
            return View();
        }
    }
}