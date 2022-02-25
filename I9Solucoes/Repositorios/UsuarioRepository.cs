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
	public class UsuarioRepository : DataBase
	{
		private SqlConnection _conexao;
		public UsuarioRepository()
		{
			_conexao = Conectar();
		}

		public bool Autenticar(string usuario, string senha)
		{
			bool retorno = false;
						SqlCommand query = new SqlCommand("select * from dbo.usuario where email=@email and senha=@senha", _conexao);
			_conexao.Open();
			SqlParameter parametroEmail = new SqlParameter();
			parametroEmail.ParameterName = "@email";
			parametroEmail.SqlDbType = SqlDbType.VarChar;
			parametroEmail.Value = usuario;
			SqlParameter parametroSenha = new SqlParameter();
			parametroSenha.ParameterName = "@senha";
			parametroSenha.SqlDbType = SqlDbType.VarChar;
			parametroSenha.Value = senha;
			query.Parameters.Add(parametroEmail);
			query.Parameters.Add(parametroSenha);
						SqlDataReader dados = query.ExecuteReader();
			if (dados.Read())
				if (dados.GetString(dados.GetOrdinal("email")) == usuario && dados.GetString(dados.GetOrdinal("senha")) == senha)
					retorno = true;

				else
					retorno = false;
			_conexao.Close();
			_conexao.Dispose();

			return retorno;
		}

		public int PesquisarIdDoAlunoPeloEmail(string email)
		{
			int idAluno = 0;
			SqlCommand query = new SqlCommand("select * from dbo.usuario where email=@email", _conexao);
			_conexao.Open();
			SqlParameter parametroEmail = new SqlParameter();
			parametroEmail.ParameterName = "@email";
			parametroEmail.SqlDbType = SqlDbType.VarChar;
			parametroEmail.Value = email;

			query.Parameters.Add(parametroEmail);

			SqlDataReader dados = query.ExecuteReader();
			if (dados.Read())
				idAluno = dados.GetInt32(dados.GetOrdinal("id"));
			_conexao.Close();
			_conexao.Dispose();

			return idAluno;
		}

		public PerfilUsuario PegarDadosDoUsuario(string email)
		{
			PerfilUsuario usuario = new PerfilUsuario();
			SqlCommand query = new SqlCommand("select u.nome, u.email, (select count(idCurso) from dbo.aluno_curso ac where ac.idAluno=u.id) as totalCursos from dbo.usuario u where email=@email", _conexao);
			_conexao.Open();
			SqlParameter parametroEmail = new SqlParameter();
			parametroEmail.ParameterName = "@email";
			parametroEmail.SqlDbType = SqlDbType.VarChar;
			parametroEmail.Value = email;

			query.Parameters.Add(parametroEmail);

			SqlDataReader dados = query.ExecuteReader();
			if (dados.Read())
			{
				usuario.Email = dados.GetString(dados.GetOrdinal("email"));
				usuario.Nome = dados.GetString(dados.GetOrdinal("nome"));
				usuario.TotalDeCursos = dados.GetInt32(dados.GetOrdinal("totalCursos"));
			}
			_conexao.Close();
			_conexao.Dispose();

			return usuario;
		}

				public string PesquisarSenhaDoAlunoPeloEmail(string email)
		{
			string senha = string.Empty;
			SqlCommand query = new SqlCommand("select senha from dbo.usuario where email=@email", _conexao);
			_conexao.Open();
			SqlParameter parametroEmail = new SqlParameter();
			parametroEmail.ParameterName = "@email";
			parametroEmail.SqlDbType = SqlDbType.VarChar;
			parametroEmail.Value = email;

			query.Parameters.Add(parametroEmail);

			SqlDataReader dados = query.ExecuteReader();
			if (dados.Read())
				senha = dados.GetString(dados.GetOrdinal("senha"));
			_conexao.Close();
			_conexao.Dispose();

			return senha;
		}

		public List<Usuario> GetUsuarioSemCurso()
		{
			List<Usuario> usuarios = new List<Usuario>();
			SqlCommand query = new SqlCommand("select * from Usuario where SnEmailEnviado is null and datediff(day,datacadastro,getdate())>=5 and id not in ((select IdAluno from aluno_curso))", _conexao);
			_conexao.Open();
			SqlDataReader dados = query.ExecuteReader();
			while (dados.Read())
			{
				Usuario usuario = new Usuario()
				{
					Email = dados.GetString(dados.GetOrdinal("Email")),
					Id = dados.GetInt32(dados.GetOrdinal("Id")),
					Nome = dados.GetString(dados.GetOrdinal("Nome"))
				};
				usuarios.Add(usuario);
			}
			_conexao.Close();
			_conexao.Dispose();
			return usuarios;
		}

		public void AlterarStatusParaEnviado(int id)
        {
			SqlCommand query = new SqlCommand("update usuario set SnEmailEnviado='s' where id=@id", _conexao);
			_conexao.Open();
			SqlParameter parametroId = new SqlParameter()
			{
				ParameterName = "id",
				SqlDbType = SqlDbType.Int,
				Value = id
			};
			query.Parameters.Add(parametroId);
			query.ExecuteNonQuery();
			_conexao.Close();
			_conexao.Dispose();
        }
        
		
	}
}