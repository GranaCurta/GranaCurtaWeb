using GranaCurtaDAL.DAL;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GranaCurtaWeb
{
    public class CartoesController : ApiController
    {
        // GET api/<controller>
        public DataTable Get()
        {
            CartoesDAL dal = new CartoesDAL(System.Configuration.ConfigurationManager.ConnectionStrings["DB_GranaCurta"].ConnectionString);

            DataTable dtbRetorno = dal.getCartoes();

            return dtbRetorno;
        }

        // GET api/<controller>/5
        public DataTable Get(int id)
        {
            CartoesDAL dal = new CartoesDAL(System.Configuration.ConfigurationManager.ConnectionStrings["DB_GranaCurta"].ConnectionString);

            DataTable dtbRetorno = dal.getCartao(id);

            return dtbRetorno;
        }

        // POST api/<controller>
        public int Post([FromBody]JObject value)
        {
            int intRetorno = -1;

            try
            {
                string strCartao = (string)value["nm_cartao"];
                double dblLimite = (double)value["vl_limite"];
                string strBandeira = (string)value["nm_bandeira"];
                string str4UltDig = (string)value["nm_4_ult_dig"];
                int intVencimentoDia = (int)value["vl_vencimento_dia"];
                //int intMelhorDia = (int)value["vl_melhor_dia"];
                int intMelhorDia = -1;
                int intIdUsuario = 1;

                CartoesDAL dal = new CartoesDAL(System.Configuration.ConfigurationManager.ConnectionStrings["DB_GranaCurta"].ConnectionString);

                intRetorno = dal.insertCartao(strCartao, dblLimite, strBandeira, str4UltDig, intVencimentoDia, intMelhorDia, intIdUsuario);
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
                string strCartao = (string)value["nm_cartao"];
                double dblLimite = (double)value["vl_limite"];
                string strBandeira = (string)value["nm_bandeira"];
                string str4UltDig = (string)value["nm_4_ult_dig"];
                int intVencimentoDia = (int)value["vl_vencimento_dia"];
                //int intMelhorDia = (int)value["vl_melhor_dia"];
                int intMelhorDia = -1;
                int intIdUsuario = 1;

                CartoesDAL dal = new CartoesDAL(System.Configuration.ConfigurationManager.ConnectionStrings["DB_GranaCurta"].ConnectionString);

                intRetorno = dal.updateCartao(id, strCartao, dblLimite, strBandeira, str4UltDig, intVencimentoDia, intMelhorDia, intIdUsuario);
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
                CartoesDAL dal = new CartoesDAL(System.Configuration.ConfigurationManager.ConnectionStrings["DB_GranaCurta"].ConnectionString);

                int intIdUsuario = 1;

                int intAffecRows = dal.deleteCartao(intIdUsuario, id);

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