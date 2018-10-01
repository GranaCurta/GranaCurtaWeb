using GranaCurtaDAL.DAL;
using GranaCurtaWeb.Models;
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
            DataTable dtbRetorno = null;

            try
            {
                int intIdUsuario = TokenManager.GetTokenSID(Request.Headers.Authorization.Parameter);

                CartoesDAL dal = new CartoesDAL(System.Configuration.ConfigurationManager.ConnectionStrings["DB_GranaCurta"].ConnectionString);

                dtbRetorno = dal.getCartoes(intIdUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtbRetorno;
        }

        // GET api/<controller>/5
        public DataTable Get(int id)
        {
            DataTable dtbRetorno = null;

            try
            {
                int intIdUsuario = TokenManager.GetTokenSID(Request.Headers.Authorization.Parameter);

                CartoesDAL dal = new CartoesDAL(System.Configuration.ConfigurationManager.ConnectionStrings["DB_GranaCurta"].ConnectionString);

                dtbRetorno = dal.getCartao(id, intIdUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }

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
                int intIdUsuario = TokenManager.GetTokenSID(Request.Headers.Authorization.Parameter);

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
                int intIdUsuario = TokenManager.GetTokenSID(Request.Headers.Authorization.Parameter);

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
                int intIdUsuario = TokenManager.GetTokenSID(Request.Headers.Authorization.Parameter);

                CartoesDAL dal = new CartoesDAL(System.Configuration.ConfigurationManager.ConnectionStrings["DB_GranaCurta"].ConnectionString);

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