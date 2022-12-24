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
using System.Web.Mvc;
using BookStoreADOAPI.DataAPI;
using BookStoreADOAPI.Models;
using Newtonsoft.Json;

namespace BookStoreADOAPI.Controllers
{
    public class UsersController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Users
        public IQueryable<Users> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
        public IHttpActionResult GetUsers(int id)
        {
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return Json(new ResponseMessage(0, "Not Found"));
            }
            return Json(new ResponseMessage(1, Utility.ToJSON(users)));
        }

        // GET: api/Users?usr=&pwd=
        public JsonResult<ResponseMessage> GetUserExist(string usr, string pwd)
        {
            Users users = db.Users.Where(p => p.Username == usr && p.Password == pwd).SingleOrDefault();
            if (users == null)
            {
                return Json(new ResponseMessage(0, "Not Found"));
            }

            return Json(new ResponseMessage(1, Utility.ToJSON(users)));
        }

        //// PUT: api/Users/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutUsers(int id, Users users)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

            //    if (id != users.Id)
            //    {
            //        return BadRequest();
            //    }

            //    db.Entry(users).State = EntityState.Modified;

            //    try
            //    {
            //        db.SaveChanges();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!UsersExists(id))
            //        {
            //            return null;
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }

            //    return StatusCode(HttpStatusCode.NoContent);
            //}

            //// POST: api/Users
            //[ResponseType(typeof(Users))]
            //public IHttpActionResult PostUsers(Users users)
            //{
            //    if (!ModelState.IsValid)
            //    {
            //        return BadRequest(ModelState);
            //    }

            //    db.Users.Add(users);
            //    db.SaveChanges();

            //    return CreatedAtRoute("DefaultApi", new { id = users.Id }, users);
            //}

            //// DELETE: api/Users/5
            //[ResponseType(typeof(Users))]
            //public IHttpActionResult DeleteUsers(int id)
            //{
            //    Users users = db.Users.Find(id);
            //    if (users == null)
            //    {
            //        return null;
            //    }

            //    db.Users.Remove(users);
            //    db.SaveChanges();

            //    return Ok(users);
            //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsersExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}