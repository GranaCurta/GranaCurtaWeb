using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace GranaCurtaDAL.DAL
{
    public class TiposContasDAL
    {
        private string strConnStr;

        public TiposContasDAL(string strConnectionString)
        {
            this.strConnStr = strConnectionString;
        }

        public DataTable getTiposContas()
        {
            StringBuilder strQuery = new StringBuilder();
            DataTable dtbRetorno = new DataTable();

            strQuery.AppendLine("SELECT");
            strQuery.AppendLine("	id_tipo_conta,");
            strQuery.AppendLine("	nm_tipo_conta");
            strQuery.AppendLine("FROM granacurta.tb_tipos_contas tc (NOLOCK)");

            using (SqlConnection connection = new SqlConnection(this.strConnStr))
            {
                SqlCommand command = new SqlCommand(strQuery.ToString(), connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    dtbRetorno.Load(reader);

                    dtbRetorno.TableName = "TiposContas";

                }
                finally
                {
                    connection.Close();
                }
            }

            return dtbRetorno;
        }

        public DataTable getTiposContas(int intId)
        {
            StringBuilder strQuery = new StringBuilder();
            DataTable dtbRetorno = new DataTable();

            strQuery.AppendLine("SELECT");
            strQuery.AppendLine("	id_tipo_conta,");
            strQuery.AppendLine("	nm_tipo_conta");
            strQuery.AppendLine("FROM granacurta.tb_tipos_contas tc (NOLOCK)");
            strQuery.AppendLine("WHERE");
            strQuery.AppendLine("   id_tipo_conta = @id_tipo_conta");

            using (SqlConnection connection = new SqlConnection(this.strConnStr))
            {
                SqlCommand command = new SqlCommand(strQuery.ToString(), connection);

                try
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@id_tipo_conta", intId);

                    SqlDataReader reader = command.ExecuteReader();

                    dtbRetorno.Load(reader);

                    dtbRetorno.TableName = "TiposContas";

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
