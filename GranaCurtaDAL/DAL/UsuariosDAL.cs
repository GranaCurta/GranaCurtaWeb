using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranaCurtaDAL.DAL
{
    public class UsuariosDAL
    {
        private string strConnStr;

        public UsuariosDAL(string strConnectionString)
        {
            this.strConnStr = strConnectionString;
        }

        public DataTable getLogin(string strNomeEMail, string strSenha)
        {
            StringBuilder strQuery = new StringBuilder();
            DataTable dtbRetorno = new DataTable();

            strQuery.AppendLine("SELECT");
            strQuery.AppendLine("	id_usuario,");
            strQuery.AppendLine("	nm_usuario,");
            strQuery.AppendLine("	nm_email");
            strQuery.AppendLine("FROM granacurta.tb_usuarios (NOLOCK)");
            strQuery.AppendLine("WHERE");
            strQuery.AppendLine("	nm_email = @nm_email");
            strQuery.AppendLine("	AND pass = @pass");

            using (SqlConnection connection = new SqlConnection(this.strConnStr))
            {
                SqlCommand command = new SqlCommand(strQuery.ToString(), connection);

                try
                {
                    command.Parameters.AddWithValue("@nm_email", strNomeEMail);
                    command.Parameters.AddWithValue("@pass", strSenha);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    dtbRetorno.Load(reader);

                    dtbRetorno.TableName = "Usuario";
                }
                catch (Exception)
                {

                }
                finally
                {
                    connection.Close();
                }
            }

            return dtbRetorno;
        }
    }
}
