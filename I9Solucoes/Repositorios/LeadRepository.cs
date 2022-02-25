using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using I9Solucoes.Models;

namespace I9Solucoes.Repositorios
{
	public class LeadRepository : DataBase
	{
		private SqlConnection _conexao;
		public LeadRepository()
		{
			_conexao = Conectar();
		}

		public Lead PegarLead(string pagina)
		{
			Lead lead = new Lead();
			SqlCommand query = new SqlCommand("select * from dbo.configuracaolead where pagina=@pagina", _conexao);
			SqlParameter parametroLead = new SqlParameter()
			{
				ParameterName = "@pagina",
				Value = pagina,
SqlDbType=SqlDbType.VarChar
			};
			query.Parameters.Add(parametroLead);
			_conexao.Open();
			SqlDataReader dados = query.ExecuteReader();
			if (dados.Read())
			{
				lead.Pagina = dados.GetString(dados.GetOrdinal("pagina"));
				lead.Id = dados.GetInt32(dados.GetOrdinal("id"));
								lead.Titulo = dados.GetString(dados.GetOrdinal("titulo"));
				lead.Descricao = dados.GetString(dados.GetOrdinal("descricao"));
							}
			_conexao.Close();
			_conexao.Dispose();

			return lead;
		}

		public bool Inserir(string email)
		{
			bool retorno = false;
			SqlCommand query = new SqlCommand("insert into CapturaLead(email) values(@email)", _conexao);
			_conexao.Open();
			SqlParameter parametroEmail = new SqlParameter()
			{
				ParameterName = "@email",
				SqlDbType = SqlDbType.VarChar,
				Value = email
			};
			query.Parameters.Add(parametroEmail);
			if (query.ExecuteNonQuery() > 0)
				retorno = true;
			else
				retorno = false;
			_conexao.Close();
			_conexao.Dispose();

			return retorno;
		}

	}
}