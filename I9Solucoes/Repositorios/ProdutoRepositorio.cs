using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using I9Solucoes.Models;

namespace I9Solucoes.Repositorios
{
	public class ProdutoRepository : DataBase
	{
		private SqlConnection _conexao;
		public ProdutoRepository()
		{
			_conexao = Conectar();
		}

		public bool Inserir(int idFornecedor, string usuarioCadastro, decimal custo, decimal custoVenda, string localizacao, string marca, DateTime? dataValidade = null, int? estoqueMinimo = null, int? prazoEntregaFornecedor = null)
		{
			bool retorno = false;
			SqlCommand query = new SqlCommand("insert into Produto(IdFornecedor,DataCadastro,DataAlteracao,UsuarioCadastro,UsuarioAlteracao,EstoqueMinimo,DataValidade,Custo,CustoVenda,PrazoEntregaFornecedor,Localizacao,Marca) values(@idFornecedor,@dataCadastro,@dataAlteracao,@usuarioCadastro,@usuarioAlteracao,@estoqueMinimo,@dataValidade,@custo,@custoVenda,@prazoEntregaFornecedor,@localizacao,@marca)", _conexao);
			_conexao.Open();
			SqlParameter parametroIdFornecedor = new SqlParameter()
			{
				ParameterName = "@idFornecedor",
				SqlDbType = SqlDbType.Int,
				Value = idFornecedor
			};
			SqlParameter parametroDataCadastro = new SqlParameter()
			{
				ParameterName = "@dataCadastro",
				SqlDbType = SqlDbType.DateTime,
				Value = DateTime.Now
			};

			SqlParameter parametroDataAlteracao = new SqlParameter()
			{
				ParameterName = "@dataAlteracao",
				SqlDbType = SqlDbType.DateTime,
				Value = Convert.DBNull
			};

			SqlParameter parametroUsuarioCadastro = new SqlParameter()
			{
				ParameterName = "@usuarioCadastro",
				SqlDbType = SqlDbType.VarChar,
				Value = usuarioCadastro
			};

			SqlParameter parametroUsuarioAlteracao = new SqlParameter()
			{
				ParameterName = "@usuarioAlteracao",
				SqlDbType = SqlDbType.VarChar,
				Value = Convert.DBNull
			};

			SqlParameter parametroEstoqueMinimo = new SqlParameter()
			{
				ParameterName = "@estoqueMinimo",
				SqlDbType = SqlDbType.Int,
				Value = estoqueMinimo
			};

			SqlParameter parametroDataValidade = new SqlParameter()
			{
				ParameterName = "@dataValidade",
				SqlDbType = SqlDbType.DateTime,
				Value = dataValidade
			};

			SqlParameter parametroCusto = new SqlParameter()
			{
				ParameterName = "@custo",
				SqlDbType = SqlDbType.Decimal,
				Value = custo
			};

			SqlParameter parametroCustoVenda = new SqlParameter()
			{
				ParameterName = "@custoVenda",
				SqlDbType = SqlDbType.Decimal,
				Value = custoVenda
			};

			SqlParameter parametroPrazoEntrega = new SqlParameter()
			{
				ParameterName = "@prazoEntregaFornecedor",
				SqlDbType = SqlDbType.Int,
				Value = prazoEntregaFornecedor
			};

			SqlParameter parametroLocalizacao = new SqlParameter()
			{
				ParameterName = "@localizacao",
				SqlDbType = SqlDbType.VarChar,
				Value = localizacao
			};

			SqlParameter parametroMarca = new SqlParameter()
			{
				ParameterName = "@marca",
				SqlDbType = SqlDbType.VarChar,
				Value = marca
			};

			query.Parameters.Add(parametroCusto);
			query.Parameters.Add(parametroCustoVenda);
			query.Parameters.Add(parametroDataAlteracao);
			query.Parameters.Add(parametroDataCadastro);
			query.Parameters.Add(parametroDataValidade);
			query.Parameters.Add(parametroEstoqueMinimo);
			query.Parameters.Add(parametroIdFornecedor);
			query.Parameters.Add(parametroLocalizacao);
			query.Parameters.Add(parametroMarca);
			query.Parameters.Add(parametroPrazoEntrega);
			query.Parameters.Add(parametroUsuarioAlteracao);
			query.Parameters.Add(parametroUsuarioCadastro);

			if (query.ExecuteNonQuery() > 0)
				retorno = true;
			else
				retorno = false;

			return retorno;
		}
	}
}
		