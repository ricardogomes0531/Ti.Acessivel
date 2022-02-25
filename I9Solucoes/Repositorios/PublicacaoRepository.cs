using I9Solucoes.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace I9Solucoes.Repositorios
{
    public class PublicacaoRepository : DataBase
    {
		private SqlConnection _conexao;

		public PublicacaoRepository()
		{
			_conexao = Conectar();
		}

		public List<Usuario> PegarUsuariosSemPublicacao(int idPublicacao)
        {
			List<Usuario> usuarios = new List<Usuario>();
			SqlCommand query = new SqlCommand("select * from usuario where id not in((select idusuario from usuario_publicacao where idpublicacao=@idPublicacao))", _conexao);
			_conexao.Open();
			SqlParameter parametroIdPublicacao = new SqlParameter()
			{
				SqlDbType = SqlDbType.Int,
				ParameterName = "idPublicacao",
				Value = idPublicacao
			};
			query.Parameters.Add(parametroIdPublicacao);
			SqlDataReader dados = query.ExecuteReader();
			while(dados.Read())
			{
				Usuario usuario = new Usuario();
				usuario.Email = dados.GetString(dados.GetOrdinal("email"));
				usuario.Id = dados.GetInt32(dados.GetOrdinal("id"));
				usuario.Nome = dados.GetString(dados.GetOrdinal("nome"));
				usuarios.Add(usuario);
            }
			return usuarios;
        }

		public bool Salvar(string titulo, string texto)
        {
			bool retorno = false;
			SqlCommand query = new SqlCommand("insert into publicacao(titulo, texto, datacadastro) values(@titulo, @texto, @dataCadastro)", _conexao);
			_conexao.Open();
			SqlParameter parametroTitulo = new SqlParameter()
			{
				SqlDbType = SqlDbType.VarChar,
				ParameterName = "titulo",
				Value = titulo
			};
			SqlParameter parametroTexto = new SqlParameter()
			{
				SqlDbType = SqlDbType.VarChar,
				ParameterName = "texto",
				Value = texto
			};
			SqlParameter parametroData = new SqlParameter()
			{
				ParameterName = "dataCadastro",
				SqlDbType = SqlDbType.Date,
				Value = DateTime.Now
			};
			query.Parameters.Add(parametroTitulo);
			query.Parameters.Add(parametroTexto);
			query.Parameters.Add(parametroData);
			if (query.ExecuteNonQuery() > 0)
				retorno = true;

			return retorno;
		}

		public Publicacao PegarUltimaPublicacao()
		{
			Publicacao publicacao = new Publicacao();
			SqlCommand query = new SqlCommand("select top 1 * from publicacao order by id desc", _conexao);
			_conexao.Open();
			SqlDataReader dados = query.ExecuteReader();
			if (dados.Read())
			{
				publicacao.DataCadastro = dados.GetDateTime(dados.GetOrdinal("datacadastro"));
				publicacao.Id = dados.GetInt32(dados.GetOrdinal("id"));
				publicacao.Texto = dados.GetString(dados.GetOrdinal("texto"));
				publicacao.Titulo = dados.GetString(dados.GetOrdinal("titulo"));
			}
			return publicacao;

		}

		public bool AtualizarStatusEnvioEmail(int idPublicacao, int idUsuario)
		{
			bool retorno = false;
			SqlCommand query = new SqlCommand("insert into usuario_publicacao(idpublicacao, idusuario, dataenvio) values(@idPublicacao, @idUsuario, @dataCadastro)", _conexao);
			_conexao.Open();
			SqlParameter parametroIdPublicacao = new SqlParameter()
			{
				SqlDbType = SqlDbType.Int,
				ParameterName = "idPublicacao",
				Value = idPublicacao
			};
			SqlParameter parametroIdUsuario = new SqlParameter()
			{
				SqlDbType = SqlDbType.Int,
				ParameterName = "idUsuario",
				Value = idUsuario
			};
			SqlParameter parametroData = new SqlParameter()
			{
				ParameterName = "dataCadastro",
				SqlDbType = SqlDbType.Date,
				Value = DateTime.Now
			};
			query.Parameters.Add(parametroIdPublicacao);
			query.Parameters.Add(parametroIdUsuario);
			query.Parameters.Add(parametroData);
			if (query.ExecuteNonQuery() > 0)
				retorno = true;

			return retorno;
		}

	}
}