using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GranaCurtaDAL.DAL
{
    public class MovimentacoesDAL
    {
        private string strConnStr;

        public MovimentacoesDAL(string strConnectionString)
        {
            this.strConnStr = strConnectionString;
        }

        public DataTable GetMovimentacoes(int intIdUsuario)
        {
            StringBuilder strQuery = this.GetQuerySelect();
            DataTable dtbRetorno = new DataTable();

            strQuery.AppendLine("WHERE");
            strQuery.AppendLine("   m.id_usuario = @id_usuario");

            using (SqlConnection connection = new SqlConnection(this.strConnStr))
            {
                SqlCommand command = new SqlCommand(strQuery.ToString(), connection);

                try
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@id_usuario", intIdUsuario);

                    SqlDataReader reader = command.ExecuteReader();

                    dtbRetorno.Load(reader);

                    dtbRetorno.TableName = "Movimentacoes";

                }
                finally
                {
                    connection.Close();
                }
            }

            return dtbRetorno;
        }

        public DataTable GetMovimentacao(int intIdUsuario, int intIdMovimentacao)
        {
            StringBuilder strQuery = this.GetQuerySelect();
            DataTable dtbRetorno = new DataTable();

            strQuery.AppendLine("WHERE");
            strQuery.AppendLine("   m.id_movimentacao = @id_movimentacao");
            strQuery.AppendLine("   AND m.id_usuario = @id_usuario");

            using (SqlConnection connection = new SqlConnection(this.strConnStr))
            {
                SqlCommand command = new SqlCommand(strQuery.ToString(), connection);

                try
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@id_usuario", intIdUsuario);
                    command.Parameters.AddWithValue("@id_movimentacao", intIdMovimentacao);

                    SqlDataReader reader = command.ExecuteReader();

                    dtbRetorno.Load(reader);

                    dtbRetorno.TableName = "Movimentacoes";

                }
                finally
                {
                    connection.Close();
                }
            }

            return dtbRetorno;
        }

        private StringBuilder GetQuerySelect()
        {
            StringBuilder strQuery = new StringBuilder();

            strQuery.AppendLine("SELECT");
            strQuery.AppendLine("	m.id_movimentacao,");
            strQuery.AppendLine("	m.id_categoria,");
            strQuery.AppendLine("    m.id_cartao,");
            strQuery.AppendLine("    m.id_conta,");
            strQuery.AppendLine("	FORMAT(m.dt_movimentacao, 'd', 'PT-BR') dt_movimentacao_f,");
            strQuery.AppendLine("	FORMAT(m.dt_referencia, 'MM/yyyy', 'PT-BR') dt_referencia_f,");
            strQuery.AppendLine("	CASE WHEN m.id_cartao IS NULL");
            strQuery.AppendLine("		THEN m.ds_movimentacao");
            strQuery.AppendLine("		ELSE m.ds_movimentacao + ' ' + CAST(M.nu_parcela AS varchar) + '/' + CAST(M.qt_parcelas AS varchar)");
            strQuery.AppendLine("	END ds_movimentacao_c,");
            strQuery.AppendLine("	isnull(c.nm_categoria, '') nm_categoria,");
            strQuery.AppendLine("	CASE WHEN m.id_conta IS NULL");
            strQuery.AppendLine("		THEN ''");
            strQuery.AppendLine("		ELSE co.nm_conta + '/' + tp_co.nm_tipo_conta");
            strQuery.AppendLine("	END nm_conta_c,");
            strQuery.AppendLine("	CASE WHEN m.id_cartao IS NULL");
            strQuery.AppendLine("		THEN ''");
            strQuery.AppendLine("		ELSE ca.nm_cartao + '/' + ca.nm_bandeira + ' (' + ca.nm_4_ult_dig + ')'");
            strQuery.AppendLine("	END nm_cartao_c,");
            strQuery.AppendLine("	m.id_cartao,");
            strQuery.AppendLine("	m.id_conta,");
            strQuery.AppendLine("	FORMAT(m.vl_movimentacao, 'N', 'PT-BR') vl_movimentacao_f");
            strQuery.AppendLine("FROM granacurta.tb_movimentacoes m (NOLOCK)");
            strQuery.AppendLine("INNER JOIN granacurta.tb_categorias c (NOLOCK)");
            strQuery.AppendLine("	ON m.id_categoria = c.id_categoria");
            strQuery.AppendLine("LEFT JOIN granacurta.tb_cartoes ca (NOLOCK)");
            strQuery.AppendLine("	ON m.id_cartao = ca.id_cartao");
            strQuery.AppendLine("LEFT JOIN granacurta.tb_contas co (NOLOCK)");
            strQuery.AppendLine("	ON m.id_conta = co.id_conta");
            strQuery.AppendLine("LEFT JOIN granacurta.tb_tipos_contas tp_co (NOLOCK)");
            strQuery.AppendLine("	ON tp_co.id_tipo_conta = co.id_tipo_conta");

            return strQuery;
        }
    }
}
