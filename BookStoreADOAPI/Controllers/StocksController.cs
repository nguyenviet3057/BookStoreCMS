using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using BookStoreADOAPI.DataAPI;
using BookStoreADOAPI.Models;
using Newtonsoft.Json;

namespace BookStoreADOAPI.Controllers
{
    public class StocksController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Stocks
        public JsonResult<ResponseMessage> GetStocks()
        {
            return Json(new ResponseMessage(1, JsonConvert.SerializeObject(db.Stocks)));
        }

        // GET: api/Stocks/5
        [ResponseType(typeof(Stocks))]
        public JsonResult<ResponseMessage> GetStocks(string id)
        {
            Stocks stocks = db.Stocks.Find(id);
            if (stocks == null)
            {
                return Json(new ResponseMessage(0, "Not Found"));
            }

            return Json(new ResponseMessage(1, Utility.ToJSON(stocks)));
        }

        // PUT: api/Stocks/5
        [ResponseType(typeof(void))]
        public JsonResult<ResponseMessage> PutStocks(string id, Stocks stocks)
        {
            if (!ModelState.IsValid)
            {
                return Json(new ResponseMessage(0, "Bad Request"));
            }

            if (id != stocks.STOCK_ID)
            {
                return Json(new ResponseMessage(0, "Bad Request"));
            }

            db.Entry(stocks).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StocksExists(id))
                {
                    return Json(new ResponseMessage(0, "Not Found"));
                }
                else
                {
                    throw;
                }
            }

            return Json(new ResponseMessage(1, "Success"));
        }

        // POST: api/Stocks
        [ResponseType(typeof(Stocks))]
        public JsonResult<ResponseMessage> PostStocks(Stocks stocks)
        {
            if (!ModelState.IsValid)
            {
                return Json(new ResponseMessage(0, "Bad Request"));
            }

            db.Stocks.Add(stocks);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (StocksExists(stocks.STOCK_ID))
                {
                    return Json(new ResponseMessage(0, "Conflict"));
                }
                else
                {
                    throw;
                }
            }

            return Json(new ResponseMessage(1, "Success")); ;
        }

        // DELETE: api/Stocks/5
        [ResponseType(typeof(Stocks))]
        public JsonResult<ResponseMessage> DeleteStocks(string id)
        {
            Stocks stocks = db.Stocks.Find(id);
            if (stocks == null)
            {
                return Json(new ResponseMessage(0, "Not Found"));
            }

            db.Stocks.Remove(stocks);
            db.SaveChanges();

            return Json(new ResponseMessage(1, "Delete Success"));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StocksExists(string id)
        {
            return db.Stocks.Count(e => e.STOCK_ID == id) > 0;
        }
    }
}