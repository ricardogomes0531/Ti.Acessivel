using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace I9Solucoes.Repositorios
{
	public abstract class DataBase
	{
		public SqlConnection Conectar()
		{
			var configuracaoConexao = ConfigurationManager.ConnectionStrings["I9Solucoes"].ToString();
			SqlConnection conexao = new SqlConnection(configuracaoConexao);
			return conexao;
		}
		
	}
}