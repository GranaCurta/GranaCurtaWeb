using GranaCurtaDAL.DAL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GranaCurtaWeb.Controllers
{
    public class ContasController : ApiController
    {
        // GET api/<controller>
        public DataTable Get()
        {
            ContasDAL dal = new ContasDAL(System.Configuration.ConfigurationManager.ConnectionStrings["DB_GranaCurta"].ConnectionString);

            DataTable dtbRetorno = dal.getContas();

            return dtbRetorno;
        }

        // GET api/<controller>/5
        public DataTable Get(int id)
        {
            ContasDAL dal = new ContasDAL(System.Configuration.ConfigurationManager.ConnectionStrings["DB_GranaCurta"].ConnectionString);

            DataTable dtbRetorno = dal.getConta(id);

            return dtbRetorno;
        }

        // POST api/<controller>
        public int Post([FromBody]JObject value)
        {
            int intRetorno = -1;

            try
            {
                string strConta = (string)value["nm_conta"];
                double dblVlLimiteCE = (double)value["vl_limite_ce"];
                int intIdTipoConta = (int)value["id_tipo_conta"];
                int intIdUsuario = 1;

                ContasDAL dal = new ContasDAL(System.Configuration.ConfigurationManager.ConnectionStrings["DB_GranaCurta"].ConnectionString);

                intRetorno = dal.insertConta(strConta, dblVlLimiteCE, intIdUsuario, intIdTipoConta);
            }
            catch (Exception)
            {
                intRetorno = -1;
            }

            return intRetorno;
        }

        // PUT api/<controller>/5
        public int Put(int id, [FromBody]JObject value)
        {
            int intRetorno = -1;

            try
            {
                string strConta = (string)value["nm_conta"];
                double dblVlrLimiteChequeEspecial = (double)value["vl_limite_ce"];
                int intIdTipoConta = (int)value["id_tipo_conta"];
                int intIdUsuario = 1;

                ContasDAL dal = new ContasDAL(System.Configuration.ConfigurationManager.ConnectionStrings["DB_GranaCurta"].ConnectionString);

                intRetorno = dal.updateConta(id, strConta, dblVlrLimiteChequeEspecial, intIdUsuario, intIdTipoConta);
            }
            catch (Exception)
            {
                intRetorno = -1;
            }

            return intRetorno;
        }

        // DELETE api/<controller>/5
        public int Delete(int id)
        {
            int intRetorno = -1;

            try
            {
                ContasDAL dal = new ContasDAL(System.Configuration.ConfigurationManager.ConnectionStrings["DB_GranaCurta"].ConnectionString);

                int intIdUsuario = 1;

                int intAffecRows = dal.deleteConta(intIdUsuario, id);

                if (intAffecRows > 0)
                {
                    intRetorno = id;
                }
            }
            catch (Exception)
            {
                intRetorno = -1;
            }

            return intRetorno;
        }
    }
}