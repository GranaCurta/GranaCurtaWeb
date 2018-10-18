using GranaCurtaDAL.DAL;
using GranaCurtaWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GranaCurtaWeb.Controllers
{
    public class MovimentacoesController : ApiController
    {
        // GET: api/Movimentacoes
        public DataTable Get()
        {
            DataTable dtbRetorno = null;

            try
            {
                int intIdUsuario = TokenManager.GetTokenSID(Request.Headers.Authorization.Parameter);

                MovimentacoesDAL dal = new MovimentacoesDAL(System.Configuration.ConfigurationManager.ConnectionStrings["DB_GranaCurta"].ConnectionString);

                dtbRetorno = dal.GetMovimentacoes(intIdUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtbRetorno;
        }

        // GET: api/Movimentacoes/{id}
        public DataTable Get(int id)
        {
            DataTable dtbRetorno = null;

            try
            {
                int intIdUsuario = TokenManager.GetTokenSID(Request.Headers.Authorization.Parameter);

                MovimentacoesDAL dal = new MovimentacoesDAL(System.Configuration.ConfigurationManager.ConnectionStrings["DB_GranaCurta"].ConnectionString);

                dtbRetorno = dal.GetMovimentacao(intIdUsuario, id);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtbRetorno;
        }

        // GET: api/Movimentacoes
        [Route("~/api/Movimentacoes/data/{data_inicio}")]
        public IEnumerable<string> GetMovByData(string data_inicio)
        {
            return new string[] { "GetMovByData", data_inicio };
        }

        // GET: api/Movimentacoes
        [Route("~/api/Movimentacoes/data/{data_inicio}/{data_fim}")]
        public IEnumerable<string> GetMovByDataPeriodo(string data_inicio, string data_fim)
        {
            return new string[] { "GetMovByDataPeriodo", data_inicio, data_fim };
        }

        // POST: api/Movimentacoes
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Movimentacoes/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Movimentacoes/5
        public void Delete(int id)
        {
        }
    }
}
