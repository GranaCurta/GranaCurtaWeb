using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using GranaCurtaDAL.DAL;
using GranaCurtaWeb.Models;
using Newtonsoft.Json.Linq;

namespace GranaCurtaWeb.Controllers
{
    public class LoginController : ApiController
    {
        //[Route("api/login/{email}/{senha}")]
        public HttpResponseMessage Post([FromBody]JObject values)
        {
            UsuariosDAL dal = new UsuariosDAL(System.Configuration.ConfigurationManager.ConnectionStrings["DB_GranaCurta"].ConnectionString);

            StringBuilder stbEmail = new StringBuilder();
            StringBuilder stbSenha = new StringBuilder();

            bool blnHasValues = false;

            if (values != null)
            {
                if (values.ContainsKey("email") && values.ContainsKey("senha"))
                {
                    blnHasValues = true;
                    stbEmail.Append((string)values["email"]);
                    stbSenha.Append((string)values["senha"]);
                }
            }
            
            if(!blnHasValues)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "E-mail ou senha inválidos.");
            }

            DataTable objUsuario = dal.getLogin(
                stbEmail.ToString(),
                stbSenha.ToString()
            );

            if (objUsuario.TableName.Contains("Usuario"))
            {
                foreach (DataRow row in objUsuario.Rows)
                {
                    if (row["nm_email"].ToString().Equals(stbEmail.ToString()))
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, TokenManager.GenerateToken(stbEmail.ToString()));
                    }
                }
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "E-mail ou senha inválidos.");
        }
    }
}
