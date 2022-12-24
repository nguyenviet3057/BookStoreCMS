using BookStoreAPIClientCMS.DataAPI;
using BookStoreAPIClientCMS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStoreAPIClientCMS.Controllers
{
    public class StocksController : Controller
    {
        // GET: Stocks
        public ActionResult Index()
        {
            try
            {
                Users users = Utility.authenToken();
                if (users == null) return RedirectToAction("Login", "Users");

                RestClient rClient = new RestClient();
                rClient.BaseUrl = ConfigAPI.BASE_URL;
                rClient.EndPoint = ConfigAPI.API_GET_STOCKS;

                ResponseMessage responseResult = rClient.RestRequestAll();
                if (responseResult.Status == 0) return Content(responseResult.Msg);
                List<Stocks> bookList = new List<Stocks>();
                bookList = JsonConvert.DeserializeObject<List<Stocks>>(responseResult.Msg);
                
                return View(bookList);
            }
            catch
            {
                return HttpNotFound();
            }
        }

        // GET: Stocks/Details/5
        public ActionResult Details(string id)
        {
            try
            {
                Users users = Utility.authenToken();
                if (users == null) return RedirectToAction("Login", "Users");
                Stocks book = new Stocks();

                RestClient rClient = new RestClient();
                rClient.BaseUrl = ConfigAPI.BASE_URL;
                rClient.EndPoint = ConfigAPI.API_GET_STOCK_BY_ID + id;

                ResponseMessage responseResult = rClient.RestRequestAll();
                if (responseResult.Status == 0) return HttpNotFound();
                book = JsonConvert.DeserializeObject<Stocks>(responseResult.Msg);

                return View(book);
            }
            catch
            {
                return HttpNotFound();
            }
        }

        // GET: Stocks/Create
        public ActionResult Create()
        {
            Users users = Utility.authenToken();
            if (users == null) return RedirectToAction("Login", "Users");

            return View();
        }

        // POST: Stocks/Create
        [HttpPost]
        public ActionResult Create(Stocks obj)
        {
            try
            {
                // TODO: Add insert logic here
                Users users = Utility.authenToken();
                if (users == null) return RedirectToAction("Login", "Users");

                RestClient rClient = new RestClient();
                rClient.BaseUrl = ConfigAPI.BASE_URL;                
                rClient.EndPoint = ConfigAPI.API_INSERT_STOCK;

                ResponseMessage responseResult = rClient.InsertData(obj);
                if (responseResult.Status == 0) return View();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Stocks/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                Users users = Utility.authenToken();
                if (users == null) return RedirectToAction("Login", "Users");
                Stocks book = new Stocks();

                RestClient rClient = new RestClient();
                rClient.BaseUrl = ConfigAPI.BASE_URL;
                rClient.EndPoint = ConfigAPI.API_GET_STOCK_BY_ID + id;

                ResponseMessage responseResult = rClient.RestRequestAll();
                if (responseResult.Status == 0) return HttpNotFound();
                book = JsonConvert.DeserializeObject<Stocks>(responseResult.Msg);

                return View(book);
            }
            catch
            {
                return HttpNotFound();
            }
        }

        // POST: Stocks/Edit/5
        [HttpPost]
        public ActionResult Edit(Stocks obj)
        {
            try
            {
                // TODO: Add update logic here
                Users users = Utility.authenToken();
                if (users == null) return RedirectToAction("Login", "Users");

                RestClient rClient = new RestClient();
                rClient.BaseUrl = ConfigAPI.BASE_URL;
                rClient.EndPoint = ConfigAPI.API_UPDATE_STOCK;

                ResponseMessage responseResult = rClient.UpdateData(obj);
                if (responseResult.Status == 0) return View();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Stocks/Delete/5
        public ActionResult Delete(string id)
        {
            Users users = Utility.authenToken();
            if (users == null) return RedirectToAction("Login", "Users");
            Stocks book = new Stocks();

            RestClient rClient = new RestClient();
            rClient.BaseUrl = ConfigAPI.BASE_URL;
            rClient.EndPoint = ConfigAPI.API_GET_STOCK_BY_ID + id;

            ResponseMessage responseResult = rClient.RestRequestAll();
            if (responseResult.Status == 0) return HttpNotFound();
            book = JsonConvert.DeserializeObject<Stocks>(responseResult.Msg);

            return View(book);
        }

        // POST: Stocks/Delete/5
        [HttpPost]
        public ActionResult Delete(Stocks obj)
        {
            try
            {
                // TODO: Add delete logic here
                Users users = Utility.authenToken();
                if (users == null) return RedirectToAction("Login", "Users");

                RestClient rClient = new RestClient();
                rClient.BaseUrl = ConfigAPI.BASE_URL;
                rClient.EndPoint = ConfigAPI.API_DELETE_STOCK + obj.STOCK_ID;

                ResponseMessage responseResult = rClient.DeleteData();
                if (responseResult.Status == 0) return View();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
