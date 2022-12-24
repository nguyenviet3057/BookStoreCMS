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
using BookStoreADOAPI.Models;

namespace BookStoreADOAPI.Controllers
{
    public class SuppliersController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/Suppliers
        public IQueryable<Suppliers> GetSuppliers()
        {
            return db.Suppliers;
        }

        // GET: api/Suppliers/5
        [ResponseType(typeof(Suppliers))]
        public IHttpActionResult GetSuppliers(string id)
        {
            Suppliers suppliers = db.Suppliers.Find(id);
            if (suppliers == null)
            {
                return NotFound();
            }

            return Ok(suppliers);
        }

        // PUT: api/Suppliers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSuppliers(string id, Suppliers suppliers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != suppliers.Supplier_ID)
            {
                return BadRequest();
            }

            db.Entry(suppliers).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuppliersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Suppliers
        [ResponseType(typeof(Suppliers))]
        public IHttpActionResult PostSuppliers(Suppliers suppliers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Suppliers.Add(suppliers);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (SuppliersExists(suppliers.Supplier_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = suppliers.Supplier_ID }, suppliers);
        }

        // DELETE: api/Suppliers/5
        [ResponseType(typeof(Suppliers))]
        public IHttpActionResult DeleteSuppliers(string id)
        {
            Suppliers suppliers = db.Suppliers.Find(id);
            if (suppliers == null)
            {
                return NotFound();
            }

            db.Suppliers.Remove(suppliers);
            db.SaveChanges();

            return Ok(suppliers);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SuppliersExists(string id)
        {
            return db.Suppliers.Count(e => e.Supplier_ID == id) > 0;
        }
    }
}