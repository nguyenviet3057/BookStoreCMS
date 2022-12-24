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
    public class Login_TokensController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Login_Tokens
        public IQueryable<Login_Tokens> GetLogin_Tokens()
        {
            return db.Login_Tokens;
        }

        //// GET: api/Login_Tokens/5
        //[ResponseType(typeof(Login_Tokens))]
        //public JsonResult<ResponseMessage> GetLogin_Tokens(int id)
        //{
        //    Login_Tokens login_Tokens = db.Login_Tokens.Find(id);
        //    if (login_Tokens == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(login_Tokens);
        //}

        // GET: api/Login_Tokens/5
        [ResponseType(typeof(Login_Tokens))]
        public JsonResult<ResponseMessage> GetLogin_TokensByToken(string token)
        {
            Login_Tokens login_Tokens = db.Login_Tokens.Where(p => p.Token == token).FirstOrDefault();
            if (login_Tokens == null)
            {
                return Json(new ResponseMessage(0, "Not Found"));
            }

            return Json(new ResponseMessage(1, Utility.ToJSON(login_Tokens)));
        }

        // PUT: api/Login_Tokens/5
        [ResponseType(typeof(void))]
        public JsonResult<ResponseMessage> PutLogin_Tokens(int id, Login_Tokens login_Tokens)
        {
            if (!ModelState.IsValid)
            {
                return Json(new ResponseMessage(0, "Failed"));
            }

            if (id != login_Tokens.Id)
            {
                return Json(new ResponseMessage(0, "Failed"));
            }

            db.Entry(login_Tokens).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Login_TokensExists(id))
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

        // POST: api/Login_Tokens
        [ResponseType(typeof(Login_Tokens))]
        public JsonResult<ResponseMessage> PostLogin_Tokens(Login_Tokens login_Tokens)
        {
            if (!ModelState.IsValid)
            {
                return Json(new ResponseMessage(0, "Add Login_Tokens Success"));
            }

            db.Login_Tokens.Add(login_Tokens);
            db.SaveChanges();

            return Json(new ResponseMessage(1, "Add Login_Tokens Success"));
        }

        // DELETE: api/Login_Tokens/5
        [ResponseType(typeof(Login_Tokens))]
        public JsonResult<ResponseMessage> DeleteLogin_Tokens(int id)
        {
            Login_Tokens login_Tokens = db.Login_Tokens.Find(id);
            if (login_Tokens == null)
            {
                return Json(new ResponseMessage(0, "Not Found"));
            }

            db.Login_Tokens.Remove(login_Tokens);
            db.SaveChanges();

            return Json(new ResponseMessage(1, "Delete Login_Tokens Success"));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Login_TokensExists(int id)
        {
            return db.Login_Tokens.Count(e => e.Id == id) > 0;
        }
    }
}