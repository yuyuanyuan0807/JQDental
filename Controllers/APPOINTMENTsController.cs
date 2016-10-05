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
    public class APPOINTMENTsController : ApiController
    {
        private jqdentaldbEntities db = new jqdentaldbEntities();

        // GET: api/APPOINTMENTs
        public IQueryable<Appointment> GetAPPOINTMENTsForToday()
        {
            List<Appointment> AppointmentList = new List<Appointment>();
            Appointment appointment;
            foreach (APPOINTMENT a in db.APPOINTMENTs)
            {
                if (a.Appointment_Date.Value.Date == DateTime.Now.Date)
                {
                    appointment = new Appointment();
                    appointment.Appointment_Time = a.Appointment_Time;
                    appointment.Dentist = new Dentist();
                    appointment.Dentist_ID = a.Dentist_ID;
                    appointment.Dentist.FirstName = a.DENTIST.Dentist_First_Name;
                    appointment.Dentist.LastName = a.DENTIST.Dentist_Last_Name;
                    appointment.Dentist.MiddleName = a.DENTIST.Dentist_Middle_Name;
                    appointment.ScheduledServiceList = new List<string>();
                    appointment.Appointment_Date = a.Appointment_Date;
                    appointment.Appointment_Time = a.Appointment_Time;
                    foreach (SCHEDULED_SERVICE ss in a.SCHEDULED_SERVICE)
                    {
                        appointment.ScheduledServiceList.Add(ss.SERVICE.Service_Description);
                    }
                    appointment.Patient = new Patient();
                    appointment.Patient.Patient_First_Name = a.PATIENT.Patient_First_Name;
                    appointment.Patient.Patient_Last_Name = a.PATIENT.Patient_Last_Name;
                    AppointmentList.Add(appointment);
                }
            }

            return AppointmentList.OrderBy(a => a.Dentist_ID).AsQueryable();
        }

        // Return patients for an account ID.
        public IQueryable<Patient> GetAPPOINTMENTPatients(int accountID)
        {
            List<Patient> patientList = new List<Patient>();
            Patient patient;
            var query = from p in db.PATIENTs where p.Account_ID == accountID select p;

            foreach (var pt in query)
            {
                patient = new Patient();
                patient.Account_ID = accountID;
                patient.Patient_ID = pt.Patient_ID;
                patient.Patient_First_Name = pt.Patient_First_Name;
                patient.Patient_Last_Name = pt.Patient_Last_Name;
                patientList.Add(patient);
            }
            return patientList.AsQueryable();
        }

        // Return services for appointments
        public IQueryable<Service> GetAPPOINTMENTServices()
        {
            List<Service> serviceList = new List<Service>();
            Service service;

            foreach (var s in db.SERVICEs)
            {
                service = new Service();
                service.ServiceID = s.Service_ID;
                service.ServiceName = s.Service_Description;
                service.cost = s.Service_Cost.Value;
                serviceList.Add(service);
            }
            return serviceList.AsQueryable();
        }


        // GET: api/APPOINTMENTsByUser
        public IQueryable<Appointment> GetAPPOINTMENTsByPatient(int accountID)
        {
            List<Appointment> appointmentList = new List<Appointment>();
            Appointment appointment;
            var queryPatient = from p in db.PATIENTs where p.Account_ID == accountID select p;

            foreach (var pt in queryPatient)
            {

                var query = from p in db.APPOINTMENTs where p.Patient_ID == pt.Patient_ID select p;
                foreach (var a in query)
                {
                    appointment = new Appointment();
                    appointment.Appointment_ID = a.Appointment_ID;
                    appointment.Appointment_Date = a.Appointment_Date;
                    appointment.Appointment_Time = a.Appointment_Time;
                    appointment.Dentist_ID = a.Dentist_ID;
                    appointment.Patient_ID = a.Patient_ID;
                    appointment.Dentist = new Dentist();
                    appointment.Dentist.FirstName = a.DENTIST.Dentist_First_Name;
                    appointment.Dentist.LastName = a.DENTIST.Dentist_Last_Name;
                    appointment.Dentist.MiddleName = a.DENTIST.Dentist_Middle_Name;
                    appointment.ScheduledServiceList = new List<string>();
                    appointment.Patient = new Patient();
                    appointment.Patient.Patient_First_Name = a.PATIENT.Patient_First_Name;
                    appointment.Patient.Patient_Last_Name = a.PATIENT.Patient_Last_Name;


                    //appointment.Scheduled_ServiceID = a.SCHEDULED_SERVICE.s;
                    appointmentList.Add(appointment);
                }
            }

            return appointmentList.AsQueryable();
        }

        // GET: api/APPOINTMENTs/5
        public async Task<IHttpActionResult> GetAPPOINTMENT(int id)
        {
            APPOINTMENT aPPOINTMENT = await db.APPOINTMENTs.FindAsync(id);
            if (aPPOINTMENT == null)
            {
                return NotFound();
            }

            return Ok(aPPOINTMENT);
        }

        // PUT: api/APPOINTMENTs/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAPPOINTMENT(Appointment appointment)
        {
            APPOINTMENT aPPOINTMENT = new APPOINTMENT();

            aPPOINTMENT.Appointment_Date = appointment.Appointment_Date;
            aPPOINTMENT.Appointment_Time = appointment.Appointment_Time;
            aPPOINTMENT.Dentist_ID = appointment.Dentist_ID;
            aPPOINTMENT.Patient_ID = appointment.Patient_ID;
            db.APPOINTMENTs.Add(aPPOINTMENT);

            db.SaveChanges();

            foreach (var scheduledService in appointment.ScheduledServiceIDs)
            {

                SCHEDULED_SERVICE sCHEDSERVICE = new SCHEDULED_SERVICE();
                sCHEDSERVICE.Appointment_ID = aPPOINTMENT.Appointment_ID;
                sCHEDSERVICE.Scheduled_Service_ID = scheduledService;
                db.SCHEDULED_SERVICE.Add(sCHEDSERVICE);
            }

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/APPOINTMENTs
        [ResponseType(typeof(APPOINTMENT))]
        public async Task<IHttpActionResult> PostAPPOINTMENT([FromBody] Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var appointmentloc = db.APPOINTMENTs.First(p => p.Appointment_ID == appointment.Appointment_ID);
            if (appointmentloc != null)
            {
                appointmentloc.Appointment_Date = appointment.Appointment_Date;
            }
            await db.SaveChangesAsync();
            return Ok(appointment);

        }


        // DELETE: api/APPOINTMENTs/5
        [ResponseType(typeof(APPOINTMENT))]
        public async Task<IHttpActionResult> DeleteAPPOINTMENT(int id)
        {
            var appointment = db.APPOINTMENTs.First(p => p.Appointment_ID == id);
            if (appointment == null)
            {
                return NotFound();
            }

            db.APPOINTMENTs.Remove(appointment);
            await db.SaveChangesAsync();

            return Ok(appointment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool APPOINTMENTExists(int id)
        {
            return db.APPOINTMENTs.Count(e => e.Appointment_ID == id) > 0;
        }
    }
}