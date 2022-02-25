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
	public class FornecedorRepository : DataBase
	{
		private SqlConnection _conexao;
		public FornecedorRepository()
		{
			_conexao = Conectar();
		}

		public bool Inserir(string nome, string usuarioCadastro, string endereco, string cep, string uf, string cidade, string numero, string complemento)
		{
			bool retorno = false;
			SqlCommand query = new SqlCommand("insert into Fornecedor(Nome, DataCadastro, DataAlteracao, UsuarioCadastro, UsuarioAlteracao, Endereco, Cep, Uf, Cidade, Numero, Complemento) values(@Nome, @DataCadastro, @DataAlteracao, @usuarioCadastro, @usuarioAlteracao, @Endereco, @Cep, @Uf, @Cidade, @Numero, @Complemento)", _conexao);
			_conexao.Open();
			SqlParameter parametroNome = new SqlParameter()
			{
				ParameterName = "@nome",
				SqlDbType = SqlDbType.VarChar,
				Value = nome
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

			SqlParameter parametroEndereco = new SqlParameter()
			{
				ParameterName = "@endereco",
				SqlDbType = SqlDbType.VarChar,
				Value = endereco
			};

			SqlParameter parametroCep = new SqlParameter()
			{
				ParameterName = "@cep",
				SqlDbType = SqlDbType.VarChar,
				Value = cep
			};
						
			SqlParameter parametroUf = new SqlParameter()
			{
				ParameterName = "@uf",
				SqlDbType = SqlDbType.VarChar,
				Value = uf
			};

			SqlParameter parametroCidade = new SqlParameter()
			{
				ParameterName = "@cidade",
				SqlDbType = SqlDbType.VarChar,
				Value = cidade
			};

			SqlParameter parametroNumero = new SqlParameter()
			{
				ParameterName = "@numero",
				SqlDbType = SqlDbType.VarChar,
				Value = numero
			};

			SqlParameter parametroComplemento = new SqlParameter()
			{
				ParameterName = "@complemento",
				SqlDbType = SqlDbType.VarChar,
				Value = complemento
			};

			query.Parameters.Add(parametroNome);
			query.Parameters.Add(parametroDataCadastro);
			query.Parameters.Add(parametroDataAlteracao);
			query.Parameters.Add(parametroUsuarioCadastro);
			query.Parameters.Add(parametroUsuarioAlteracao);
			query.Parameters.Add(parametroEndereco);
			query.Parameters.Add(parametroCep);
			query.Parameters.Add(parametroUf);
			query.Parameters.Add(parametroNumero);
			query.Parameters.Add(parametroComplemento);
			query.Parameters.Add(parametroCidade);
			if (query.ExecuteNonQuery()>0)
				retorno = true;
			else
				retorno = false;

			return retorno;
		}

		public List<Fornecedor> Listar()
		{
			List<Fornecedor> retorno = new List<Fornecedor>();
			SqlCommand query = new SqlCommand("select * from fornecedor", _conexao);
			_conexao.Open();
			SqlDataReader dados = query.ExecuteReader();
			while (dados.Read())
			{
				Fornecedor fornecedor = new Fornecedor()
				{
					Cep = dados.GetString(dados.GetOrdinal("cep")),
					Cidade = dados.GetString(dados.GetOrdinal("cidade")),
					Complemento = dados.GetString(dados.GetOrdinal("complemento")),
					DataAlteracao = DateTime.Now,
					DataCadastro = dados.GetDateTime(dados.GetOrdinal("datacadastro")),
					Endereco = dados.GetString(dados.GetOrdinal("endereco")),
					Id = dados.GetInt32(dados.GetOrdinal("id")),
					UsuarioAlteracao ="ricardo",
					UsuarioCadastro = dados.GetString(dados.GetOrdinal("usuariocadastro")),
					Nome = dados.GetString(dados.GetOrdinal("nome")),
					Numero = dados.GetString(dados.GetOrdinal("numero")),
					Uf = dados.GetString(dados.GetOrdinal("uf"))
				};
				retorno.Add(fornecedor);
			}
			return retorno;
		}
	}
}