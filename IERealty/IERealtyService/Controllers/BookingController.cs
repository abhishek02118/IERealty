using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using IERealtyDatabases;
using IERealtyDatabases.Databases;
using IERealtyDatabases.Databases.SqlServer;
using IERealtyDatabases.Models;

namespace IERealtyService.Controllers
{
    public class BookingController : ApiController
    {

        
        
        /*// GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }*/

        // GET api/<controller>
        public AppointmentModel Get()
        {
            using (var dbManager = DatabaseFactory.GetDatbase(DbType.SqlServer))
            {
                return dbManager.GetNewAppointmentDetails();
            }
        }

        // POST api/<controller>
        public void Post([FromBody]AppointmentModel bookAppointment)
        {
            using (var dbManager = DatabaseFactory.GetDatbase(DbType.SqlServer))
            {
                dbManager.BookAppointment(bookAppointment);
            }
            
        }

       /* // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }*/


    }
}