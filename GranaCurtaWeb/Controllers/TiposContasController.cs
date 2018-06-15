using GranaCurtaDAL.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GranaCurtaWeb.Controllers
{
    public class TiposContasController : ApiController
    {
        // GET api/<controller>
        public DataTable Get()
        {
            TiposContasDAL dal = new TiposContasDAL(System.Configuration.ConfigurationManager.ConnectionStrings["DB_GranaCurta"].ConnectionString);

            DataTable dtbRetorno = dal.getTiposContas();

            return dtbRetorno;
        }

        // GET api/<controller>/5
        public DataTable Get(int id)
        {
            TiposContasDAL dal = new TiposContasDAL(System.Configuration.ConfigurationManager.ConnectionStrings["DB_GranaCurta"].ConnectionString);

            DataTable dtbRetorno = dal.getTiposContas(id);

            return dtbRetorno;
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
        }
    }
}