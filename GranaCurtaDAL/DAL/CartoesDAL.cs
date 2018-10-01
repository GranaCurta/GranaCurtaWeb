using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace GranaCurtaDAL.DAL
{
    public class CartoesDAL
    {
        private string strConnStr;

        public CartoesDAL(string strConnectionString)
        {
            this.strConnStr = strConnectionString;
        }

        public DataTable getCartoes(int intIdUsuario)
        {
            StringBuilder strQuery = new StringBuilder();
            DataTable dtbRetorno = new DataTable();

            strQuery.AppendLine("SELECT");
            strQuery.AppendLine("	id_cartao,");
            strQuery.AppendLine("	nm_cartao,");
            strQuery.AppendLine("	FORMAT(vl_limite, 'N', 'PT-BR') vl_limite,");
            strQuery.AppendLine("	nm_bandeira,");
            strQuery.AppendLine("	ISNULL(nm_4_ult_dig, '') nm_4_ult_dig,");
            strQuery.AppendLine("	ISNULL(CAST(vl_vencimento_dia AS VARCHAR), '') vl_vencimento_dia,");
            strQuery.AppendLine("	id_usuario,");
            strQuery.AppendLine("	dt_criacao,");
            strQuery.AppendLine("	dt_alteracao");
            strQuery.AppendLine("FROM granacurta.tb_cartoes (NOLOCK)");
            strQuery.AppendLine("WHERE");
            strQuery.AppendLine("   id_usuario = @id_usuario");

            using (SqlConnection connection = new SqlConnection(this.strConnStr))
            {
                SqlCommand command = new SqlCommand(strQuery.ToString(), connection);

                try
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@id_usuario", intIdUsuario);

                    SqlDataReader reader = command.ExecuteReader();

                    dtbRetorno.Load(reader);

                    dtbRetorno.TableName = "Cartoes";

                }
                finally
                {
                    connection.Close();
                }
            }

            return dtbRetorno;
        }

        public DataTable getCartao(int intId, int intIdUsuario)
        {
            StringBuilder strQuery = new StringBuilder();
            DataTable dtbRetorno = new DataTable();

            strQuery.AppendLine("SELECT");
            strQuery.AppendLine("	id_cartao,");
            strQuery.AppendLine("	nm_cartao,");
            strQuery.AppendLine("	FORMAT(vl_limite, 'N', 'PT-BR') vl_limite,");
            strQuery.AppendLine("	nm_bandeira,");
            strQuery.AppendLine("	nm_4_ult_dig,");
            strQuery.AppendLine("	ISNULL(CAST(vl_vencimento_dia AS VARCHAR), '') vl_vencimento_dia,");
            strQuery.AppendLine("	id_usuario,");
            strQuery.AppendLine("	dt_criacao,");
            strQuery.AppendLine("	dt_alteracao");
            strQuery.AppendLine("FROM granacurta.tb_cartoes (NOLOCK)");
            strQuery.AppendLine("WHERE");
            strQuery.AppendLine("   id_cartao = @id_cartao");
            strQuery.AppendLine("   AND id_usuario = @id_usuario");

            using (SqlConnection connection = new SqlConnection(this.strConnStr))
            {
                SqlCommand command = new SqlCommand(strQuery.ToString(), connection);

                try
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@id_cartao", intId);
                    command.Parameters.AddWithValue("@id_usuario", intIdUsuario);

                    SqlDataReader reader = command.ExecuteReader();

                    dtbRetorno.Load(reader);

                    dtbRetorno.TableName = "Cartoes";

                }
                finally
                {
                    connection.Close();
                }
            }

            return dtbRetorno;
        }

        public int insertCartao(string strCartao, double dblLimite, string strBandeira, string str4UltDig, int intVencimentoDia, int intMelhorDia, int intIdUsuario)
        {
            int intRetorno = -1;
            StringBuilder strQuery = new StringBuilder();

            try
            {
                strQuery.AppendLine("INSERT INTO granacurta.tb_cartoes");
                strQuery.AppendLine("(nm_cartao, vl_limite, nm_bandeira, nm_4_ult_dig, vl_vencimento_dia, vl_melhor_dia, id_usuario, dt_criacao) VALUES");
                strQuery.AppendLine("(@nm_cartao, @vl_limite, @nm_bandeira, @nm_4_ult_dig, @vl_vencimento_dia, @vl_melhor_dia, @id_usuario, GETDATE())");
                strQuery.AppendLine("SELECT CAST(scope_identity() AS int) id;");

                using (SqlConnection connection = new SqlConnection(this.strConnStr))
                {
                    SqlCommand command = new SqlCommand(strQuery.ToString(), connection);

                    command.Parameters.AddWithValue("@nm_cartao", strCartao);
                    command.Parameters.AddWithValue("@vl_limite", dblLimite);
                    command.Parameters.AddWithValue("@nm_bandeira", strBandeira);
                    command.Parameters.AddWithValue("@nm_4_ult_dig", str4UltDig);
                    command.Parameters.AddWithValue("@vl_vencimento_dia", intVencimentoDia);
                    command.Parameters.AddWithValue("@vl_melhor_dia", intMelhorDia);
                    command.Parameters.AddWithValue("@id_usuario", intIdUsuario);

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

        public int updateCartao(int intIdCartao, string strCartao, double dblLimite, string strBandeira, string str4UltDig, int intVencimentoDia, int intMelhorDia, int intIdUsuario)
        {
            int intRetorno = -1;
            StringBuilder strQuery = new StringBuilder();

            try
            {
                strQuery.AppendLine("UPDATE C SET");
                strQuery.AppendLine("	nm_cartao = @nm_cartao,");
                strQuery.AppendLine("	vl_limite = @vl_limite,");
                strQuery.AppendLine("	nm_bandeira = @nm_bandeira,");
                strQuery.AppendLine("	nm_4_ult_dig = @nm_4_ult_dig,");
                strQuery.AppendLine("	vl_vencimento_dia = @vl_vencimento_dia,");
                strQuery.AppendLine("	vl_melhor_dia = @vl_melhor_dia,");
                strQuery.AppendLine("	dt_alteracao = GETDATE()");
                strQuery.AppendLine("FROM granacurta.tb_cartoes C");
                strQuery.AppendLine("WHERE");
                strQuery.AppendLine("	C.id_cartao = @id_cartao");
                strQuery.AppendLine("	and c.id_usuario = @id_usuario");

                using (SqlConnection connection = new SqlConnection(this.strConnStr))
                {
                    SqlCommand command = new SqlCommand(strQuery.ToString(), connection);

                    command.Parameters.AddWithValue("@nm_cartao", strCartao);
                    command.Parameters.AddWithValue("@vl_limite", dblLimite);
                    command.Parameters.AddWithValue("@nm_bandeira", strBandeira);
                    command.Parameters.AddWithValue("@nm_4_ult_dig", str4UltDig);
                    command.Parameters.AddWithValue("@vl_vencimento_dia", intVencimentoDia);
                    command.Parameters.AddWithValue("@vl_melhor_dia", intMelhorDia);
                    command.Parameters.AddWithValue("@id_cartao", intIdCartao);
                    command.Parameters.AddWithValue("@id_usuario", intIdUsuario);

                    connection.Open();

                    if (command.ExecuteNonQuery() > 0)
                    {
                        intRetorno = intIdCartao;
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

        public int deleteCartao(int intIdUsuario, int intIdCartao)
        {
            int intAffecRows = -1;
            StringBuilder strQuery = new StringBuilder();

            try
            {
                strQuery.AppendLine("DELETE FROM granacurta.tb_cartoes");
                strQuery.AppendLine("WHERE ");
                strQuery.AppendLine("   id_cartao = @id_cartao");
                strQuery.AppendLine("   AND id_usuario = @id_usuario");

                using (SqlConnection connection = new SqlConnection(this.strConnStr))
                {
                    SqlCommand command = new SqlCommand(strQuery.ToString(), connection);

                    command.Parameters.AddWithValue("@id_usuario", intIdUsuario);
                    command.Parameters.AddWithValue("@id_cartao", intIdCartao);

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
