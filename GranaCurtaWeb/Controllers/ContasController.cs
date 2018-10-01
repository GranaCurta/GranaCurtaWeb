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

namespace GranaCurtaWeb.Controllers
{
    public class ContasController : ApiController
    {
        /// <summary>
        /// Retorna todas as contas de um determinado usuário
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable Get()
        {
            DataTable dtbRetorno = null;

            try
            {
                int intIdUsuario = TokenManager.GetTokenSID(Request.Headers.Authorization.Parameter);

                ContasDAL dal = new ContasDAL(System.Configuration.ConfigurationManager.ConnectionStrings["DB_GranaCurta"].ConnectionString);

                dtbRetorno = dal.GetContas(intIdUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtbRetorno;
        }

        /// <summary>
        /// Retorna uma conta de um usuário
        /// </summary>
        /// <param name="id">id da conta</param>
        /// <returns>DataTable</returns>
        public DataTable Get(int id)
        {
            DataTable dtbRetorno = null;

            try
            {
                int intIdConta = id;
                int intIdUsuario = TokenManager.GetTokenSID(Request.Headers.Authorization.Parameter);

                ContasDAL dal = new ContasDAL(System.Configuration.ConfigurationManager.ConnectionStrings["DB_GranaCurta"].ConnectionString);

                dtbRetorno = dal.GetConta(intIdConta, intIdUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtbRetorno;
        }

        /// <summary>
        /// Insere uma conta de um usuário
        /// </summary>
        /// <param name="value">JSON com os parametros:
        /// nm_conta
        /// vl_limite_ce
        /// id_tipo_conta
        /// </param>
        /// <returns></returns>
        public int Post([FromBody]JObject value)
        {
            int intRetorno = -1;

            try
            {
                string strConta = (string)value["nm_conta"];
                double dblVlLimiteCE = (double)value["vl_limite_ce"];
                int intIdTipoConta = (int)value["id_tipo_conta"];
                int intIdUsuario = TokenManager.GetTokenSID(Request.Headers.Authorization.Parameter);

                ContasDAL dal = new ContasDAL(System.Configuration.ConfigurationManager.ConnectionStrings["DB_GranaCurta"].ConnectionString);

                intRetorno = dal.InsertConta(strConta, dblVlLimiteCE, intIdUsuario, intIdTipoConta);
            }
            catch (Exception)
            {
                intRetorno = -1;
            }

            return intRetorno;
        }

        /// <summary>
        /// Atualiza uma conta de um usuário
        /// </summary>
        /// <param name="value">JSON com os parametros:
        /// id_conta
        /// nm_conta
        /// vl_limite_ce
        /// id_tipo_conta
        /// </param>
        /// <returns></returns>
        public int Put([FromBody]JObject value)
        {
            int intRetorno = -1;

            try
            {
                int intIdConta = (int)value["id_conta"];
                string strConta = (string)value["nm_conta"];
                double dblVlrLimiteChequeEspecial = (double)value["vl_limite_ce"];
                int intIdTipoConta = (int)value["id_tipo_conta"];
                int intIdUsuario = TokenManager.GetTokenSID(Request.Headers.Authorization.Parameter);

                ContasDAL dal = new ContasDAL(System.Configuration.ConfigurationManager.ConnectionStrings["DB_GranaCurta"].ConnectionString);

                intRetorno = dal.UpdateConta(intIdConta, strConta, dblVlrLimiteChequeEspecial, intIdUsuario, intIdTipoConta);
            }
            catch (Exception)
            {
                intRetorno = -1;
            }

            return intRetorno;
        }

        /// <summary>
        /// Exclui uma conta de um usuário
        /// </summary>
        /// <param name="id">Id da conta</param>
        /// <returns></returns>
        public int Delete(int id)
        {
            int intRetorno = -1;

            try
            {
                int intIdConta = id;
                int intIdUsuario = TokenManager.GetTokenSID(Request.Headers.Authorization.Parameter);

                ContasDAL dal = new ContasDAL(System.Configuration.ConfigurationManager.ConnectionStrings["DB_GranaCurta"].ConnectionString);

                int intAffecRows = dal.DeleteConta(intIdUsuario, intIdConta);

                if (intAffecRows > 0)
                {
                    intRetorno = intIdConta;
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