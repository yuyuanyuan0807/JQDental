using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Team6JQDental;
using Team6JQDental.Models;

namespace Team6JQDental.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DENTISTsController : ApiController
    {
        private jqdentaldbEntities db = new jqdentaldbEntities();

        // GET: api/DENTISTs
        public IQueryable<Dentist> GetDENTISTs()
        {
            List<Dentist> DentistCollection = new List<Dentist>();
            Dentist dentist;
            foreach(DENTIST d in db.DENTISTs){
                dentist = new Dentist();
                dentist.FirstName = d.Dentist_First_Name;
                dentist.LastName = d.Dentist_Last_Name;
                dentist.MiddleName = d.Dentist_Middle_Name;
                dentist.JoinDate =  d.Dentist_Join_Date.Value.Date.ToString();
                dentist.DentistID = d.Dentist_ID;
                DentistCollection.Add(dentist);
            }

            return DentistCollection.AsQueryable();
        }

        // GET: api/DENTISTs/5
        [ResponseType(typeof(Dentist))]
        public async Task<IHttpActionResult> GetDENTIST(int id)
        {
            DENTIST d = await db.DENTISTs.FindAsync(id);
            Dentist dentist = new Dentist();

            if (d == null)
            {
                return NotFound();
            }
            else
            {
                dentist.FirstName = d.Dentist_First_Name;
                dentist.LastName = d.Dentist_Last_Name;
                dentist.MiddleName = d.Dentist_Middle_Name;
                dentist.JoinDate = d.Dentist_Join_Date.Value.Date.ToString();
                dentist.DentistID = d.Dentist_ID;
            }
           

            return Ok(dentist);
        }

        // PUT: api/DENTISTs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDENTIST(int id, DENTIST dENTIST)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dENTIST.Dentist_ID)
            {
                return BadRequest();
            }

            db.Entry(dENTIST).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DENTISTExists(id))
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

        // POST: api/DENTISTs
        [ResponseType(typeof(Dentist))]
        public async Task<IHttpActionResult> PostDENTIST(Dentist dentist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DENTIST dbDentist = new DENTIST();
            dbDentist.Dentist_First_Name = dentist.FirstName;
            dbDentist.Dentist_ID = dentist.DentistID;
            dbDentist.Dentist_Join_Date = DateTime.Parse(dentist.JoinDate);
            dbDentist.Dentist_Middle_Name = dentist.MiddleName;
            dbDentist.Dentist_Last_Name = dentist.LastName;

            db.DENTISTs.Add(dbDentist);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = dbDentist.Dentist_ID }, dbDentist);
        }

        // DELETE: api/DENTISTs/5
        // Commented out for now.
        [ResponseType(typeof(DENTIST))]
        public async Task<IHttpActionResult> DeleteDENTIST(int id)
        {
            DENTIST dENTIST = await db.DENTISTs.FindAsync(id);
            //if (dENTIST == null)
            //{
            //    return NotFound();
            //}

            //db.DENTISTs.Remove(dENTIST);
            //await db.SaveChangesAsync();

            return Ok(dENTIST);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DENTISTExists(int id)
        {
            return db.DENTISTs.Count(e => e.Dentist_ID == id) > 0;
        }
    }
}