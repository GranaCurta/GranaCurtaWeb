using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;

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

        public int insert(string strData, string strDataReferencia, string strDescricao, int? intNumeroParcela, int? intQtdParcelas, double dblValor, int intIdUsuario, int intIdCategoria, int? intIdCartao, int? intIdConta)
        {
            int intRetorno = -1;
            StringBuilder strQuery = new StringBuilder();

            try
            {
                strQuery.AppendLine("INSERT INTO granacurta.tb_movimentacoes");
                strQuery.AppendLine("(dt_movimentacao, dt_referencia, ds_movimentacao, nu_parcela, qt_parcelas, vl_movimentacao, id_usuario, id_categoria, id_cartao, id_conta, dt_criacao) VALUES");
                strQuery.AppendLine("(@dt_movimentacao, @dt_referencia, @ds_movimentacao, @nu_parcela, @qt_parcelas, @vl_movimentacao, @id_usuario, @id_categoria, @id_cartao, @id_conta, GETDATE())");
                strQuery.AppendLine("SELECT CAST(scope_identity() AS int) id;");

                using (SqlConnection connection = new SqlConnection(this.strConnStr))
                {
                    SqlCommand command = new SqlCommand(strQuery.ToString(), connection);

                    command.Parameters.AddWithValue("@dt_movimentacao", strData);
                    command.Parameters.AddWithValue("@dt_referencia", strDataReferencia);
                    command.Parameters.AddWithValue("@ds_movimentacao", strDescricao);
                    command.Parameters.AddWithValue("@nu_parcela", intNumeroParcela);
                    command.Parameters.AddWithValue("@qt_parcelas", intQtdParcelas);
                    command.Parameters.AddWithValue("@vl_movimentacao", dblValor);
                    command.Parameters.AddWithValue("@id_usuario", intIdUsuario);
                    command.Parameters.AddWithValue("@id_categoria", intIdCategoria);
                    command.Parameters.AddWithValue("@id_cartao", intIdCartao);
                    command.Parameters.AddWithValue("@id_conta", intIdConta);

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

        public int update(int intIdMovimentacao, string strData, string strDataReferencia, string strDescricao, int? intNumeroParcela, int? intQtdParcelas, double dblValor, int intIdUsuario, int intIdCategoria, int? intIdCartao, int? intIdConta)
        {
            int intRetorno = -1;
            StringBuilder strQuery = new StringBuilder();

            try
            {
                strQuery.AppendLine("UPDATE M SET");
                strQuery.AppendLine("	dt_movimentacao = @dt_movimentacao,");
                strQuery.AppendLine("	dt_referencia = @dt_referencia,");
                strQuery.AppendLine("	ds_movimentacao = @ds_movimentacao,");
                strQuery.AppendLine("	nu_parcela = @nu_parcela,");
                strQuery.AppendLine("	qt_parcelas = @qt_parcelas,");
                strQuery.AppendLine("	vl_movimentacao = @vl_movimentacao,");
                strQuery.AppendLine("	id_categoria = @id_categoria,");
                strQuery.AppendLine("	id_cartao = @id_cartao,");
                strQuery.AppendLine("	id_conta = @id_conta,");
                strQuery.AppendLine("	dt_alteracao = GETDATE()");
                strQuery.AppendLine("FROM granacurta.tb_movimentacoes M");
                strQuery.AppendLine("WHERE");
                strQuery.AppendLine("	M.id_movimentacao = @id_movimentacao");
                strQuery.AppendLine("	AND M.id_usuario = @id_usuario");

                using (SqlConnection connection = new SqlConnection(this.strConnStr))
                {
                    SqlCommand command = new SqlCommand(strQuery.ToString(), connection);

                    command.Parameters.AddWithValue("@dt_movimentacao", strData);
                    command.Parameters.AddWithValue("@dt_referencia", strDataReferencia);
                    command.Parameters.AddWithValue("@ds_movimentacao", strDescricao);
                    command.Parameters.AddWithValue("@nu_parcela", intNumeroParcela);
                    command.Parameters.AddWithValue("@qt_parcelas", intQtdParcelas);
                    command.Parameters.AddWithValue("@vl_movimentacao", dblValor);
                    command.Parameters.AddWithValue("@id_categoria", intIdCategoria);
                    command.Parameters.AddWithValue("@id_cartao", intIdCartao);
                    command.Parameters.AddWithValue("@id_conta", intIdConta);
                    command.Parameters.AddWithValue("@id_movimentacao", intIdMovimentacao);
                    command.Parameters.AddWithValue("@id_usuario", intIdUsuario);

                    connection.Open();

                    if (command.ExecuteNonQuery() > 0)
                    {
                        intRetorno = intIdMovimentacao;
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

        public int delete(int intIdUsuario, int intIdMovimentacao)
        {
            int intAffecRows = -1;
            StringBuilder strQuery = new StringBuilder();

            try
            {
                strQuery.AppendLine("DELETE FROM granacurta.tb_movimentacoes");
                strQuery.AppendLine("WHERE ");
                strQuery.AppendLine("   id_movimentacao = @id_movimentacao");
                strQuery.AppendLine("   AND id_usuario = @id_usuario");

                using (SqlConnection connection = new SqlConnection(this.strConnStr))
                {
                    SqlCommand command = new SqlCommand(strQuery.ToString(), connection);

                    command.Parameters.AddWithValue("@id_usuario", intIdUsuario);
                    command.Parameters.AddWithValue("@id_movimentacao", intIdMovimentacao);

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

        public int delete(int intIdUsuario, List<int> lstMovimentacoes)
        {
            int intAffecRows = -1;
            StringBuilder strQuery = new StringBuilder();
            StringBuilder strIdMovimentacoes = new StringBuilder();

            try
            {
                foreach (int item in lstMovimentacoes)
                {
                    strIdMovimentacoes.Append(item.ToString() + ",");
                }

                strQuery.AppendLine("DELETE FROM granacurta.tb_movimentacoes");
                strQuery.AppendLine("WHERE ");
                strQuery.AppendLine("   id_movimentacao IN( @id_movimentacoes )");
                strQuery.AppendLine("   AND id_usuario = @id_usuario");

                using (SqlConnection connection = new SqlConnection(this.strConnStr))
                {
                    SqlCommand command = new SqlCommand(strQuery.ToString(), connection);

                    command.Parameters.AddWithValue("@id_usuario", intIdUsuario);
                    //command.Parameters.AddWithValue("@id_movimentacao", intIdMovimentacao);
                    command.Parameters.AddWithValue("@id_movimentacoes", strIdMovimentacoes.ToString().TrimEnd(','));

                    connection.Open();

                    //intAffecRows = command.ExecuteNonQuery();

                    connection.Close();

                }
            }
            catch (Exception)
            {
                intAffecRows = -1;
            }

            return intAffecRows;
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
