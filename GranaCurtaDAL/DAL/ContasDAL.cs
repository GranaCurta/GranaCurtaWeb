using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace GranaCurtaDAL.DAL
{
    public class ContasDAL
    {
        private string strConnStr;

        public ContasDAL(string strConnectionString)
        {
            this.strConnStr = strConnectionString;
        }

        public DataTable getContas()
        {
            StringBuilder strQuery = new StringBuilder();
            DataTable dtbRetorno = new DataTable();

            strQuery.AppendLine("SELECT");
            strQuery.AppendLine("	id_conta,");
            strQuery.AppendLine("	nm_conta,");
            strQuery.AppendLine("	c.id_tipo_conta,");
            strQuery.AppendLine("	nm_tipo_conta,");
            strQuery.AppendLine("	FORMAT(CAST(1000.00 AS NUMERIC(25, 2)), 'N', 'PT-BR') vl_saldo,");
            strQuery.AppendLine("	FORMAT(c.vl_limite_ce, 'N', 'PT-BR') vl_limite_ce");
            strQuery.AppendLine("FROM granacurta.tb_contas c (NOLOCK)");
            strQuery.AppendLine("INNER JOIN granacurta.tb_tipos_contas tc (NOLOCK)");
            strQuery.AppendLine("	ON C.id_tipo_conta = tc.id_tipo_conta");

            using (SqlConnection connection = new SqlConnection(this.strConnStr))
            {
                SqlCommand command = new SqlCommand(strQuery.ToString(), connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    dtbRetorno.Load(reader);

                    dtbRetorno.TableName = "Contas";

                }
                finally
                {
                    connection.Close();
                }
            }

            return dtbRetorno;
        }

        public DataTable getConta(int intId)
        {
            StringBuilder strQuery = new StringBuilder();
            DataTable dtbRetorno = new DataTable();

            strQuery.AppendLine("SELECT");
            strQuery.AppendLine("	id_conta,");
            strQuery.AppendLine("	nm_conta,");
            strQuery.AppendLine("	c.id_tipo_conta,");
            strQuery.AppendLine("	nm_tipo_conta,");
            strQuery.AppendLine("	FORMAT(CAST(1000.00 AS NUMERIC(25, 2)), 'N', 'PT-BR') vl_saldo,");
            strQuery.AppendLine("	FORMAT(c.vl_limite_ce, 'N', 'PT-BR') vl_limite_ce");
            strQuery.AppendLine("FROM granacurta.tb_contas c (NOLOCK)");
            strQuery.AppendLine("INNER JOIN granacurta.tb_tipos_contas tc (NOLOCK)");
            strQuery.AppendLine("	ON C.id_tipo_conta = tc.id_tipo_conta");
            strQuery.AppendLine("WHERE");
            strQuery.AppendLine("   id_conta = @id_conta");

            using (SqlConnection connection = new SqlConnection(this.strConnStr))
            {
                SqlCommand command = new SqlCommand(strQuery.ToString(), connection);

                try
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@id_conta", intId);

                    SqlDataReader reader = command.ExecuteReader();

                    dtbRetorno.Load(reader);

                    dtbRetorno.TableName = "Contas";

                }
                finally
                {
                    connection.Close();
                }
            }

            return dtbRetorno;
        }

        public int insertConta(string strConta, double dblVlrLimiteChequeEspecial, int intIdUsuario, int intIdTipoConta)
        {
            int intRetorno = -1;
            StringBuilder strQuery = new StringBuilder();

            try
            {
                strQuery.AppendLine("INSERT INTO granacurta.tb_contas");
                strQuery.AppendLine("(nm_conta, vl_limite_ce, id_usuario, id_tipo_conta, dt_criacao) VALUES ");
                strQuery.AppendLine("(@nm_conta, @vl_limite_ce, @id_usuario, @id_tipo_conta, GETDATE());");
                strQuery.AppendLine("SELECT CAST(scope_identity() AS int) id;");

                using (SqlConnection connection = new SqlConnection(this.strConnStr))
                {
                    SqlCommand command = new SqlCommand(strQuery.ToString(), connection);

                    command.Parameters.AddWithValue("@nm_conta", strConta);
                    command.Parameters.AddWithValue("@vl_limite_ce", dblVlrLimiteChequeEspecial);
                    command.Parameters.AddWithValue("@id_usuario", intIdUsuario);
                    command.Parameters.AddWithValue("@id_tipo_conta", intIdTipoConta);

                    connection.Open();

                    intRetorno = (int)command.ExecuteScalar();

                    connection.Close();

                }
            }
            catch (Exception)
            {
                intRetorno = -1;
            }

            return intRetorno;
        }

        public int updateConta(int intIdConta, string strConta, double dblVlrLimiteChequeEspecial, int intIdUsuario, int intIdTipoConta)
        {
            int intRetorno = -1;
            StringBuilder strQuery = new StringBuilder();

            try
            {
                strQuery.AppendLine("UPDATE C SET");
                strQuery.AppendLine("   nm_conta = @nm_conta,");
                strQuery.AppendLine("   vl_limite_ce = @vl_limite_ce,");
                strQuery.AppendLine("   id_tipo_conta = @id_tipo_conta,");
                strQuery.AppendLine("   dt_alteracao = GETDATE()");
                strQuery.AppendLine("FROM granacurta.tb_contas C");
                strQuery.AppendLine("WHERE");
                strQuery.AppendLine("   id_conta = @id_conta");
                strQuery.AppendLine("   AND id_usuario = @id_usuario");

                using (SqlConnection connection = new SqlConnection(this.strConnStr))
                {
                    SqlCommand command = new SqlCommand(strQuery.ToString(), connection);

                    command.Parameters.AddWithValue("@id_conta", intIdConta);
                    command.Parameters.AddWithValue("@nm_conta", strConta);
                    command.Parameters.AddWithValue("@vl_limite_ce", dblVlrLimiteChequeEspecial);
                    command.Parameters.AddWithValue("@id_usuario", intIdUsuario);
                    command.Parameters.AddWithValue("@id_tipo_conta", intIdTipoConta);

                    connection.Open();

                    //command.ExecuteScalar();
                    if (command.ExecuteNonQuery() > 0)
                    {
                        intRetorno = intIdConta;
                    }

                    connection.Close();

                }
            }
            catch (Exception)
            {
                intRetorno = -1;
            }

            return intRetorno;
        }

        public int deleteConta(int intIdUsuario, int intIdConta)
        {
            int intAffecRows = -1;
            StringBuilder strQuery = new StringBuilder();

            try
            {
                strQuery.AppendLine("DELETE FROM granacurta.tb_contas");
                strQuery.AppendLine("WHERE ");
                strQuery.AppendLine("   id_conta = @id_conta");
                strQuery.AppendLine("   AND id_usuario = @id_usuario");

                using (SqlConnection connection = new SqlConnection(this.strConnStr))
                {
                    SqlCommand command = new SqlCommand(strQuery.ToString(), connection);

                    command.Parameters.AddWithValue("@id_usuario", intIdUsuario);
                    command.Parameters.AddWithValue("@id_conta", intIdConta);

                    connection.Open();

                    intAffecRows = command.ExecuteNonQuery();

                    connection.Close();

                }
            }
            catch (Exception)
            {
                intAffecRows = -1;
            }

            return intAffecRows;
        }
    }
}
