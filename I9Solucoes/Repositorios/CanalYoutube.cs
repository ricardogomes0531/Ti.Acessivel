using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using I9Solucoes.Models;

namespace I9Solucoes.Repositorios
{
	public class CanalYoutubeRepository : DataBase
	{
		private SqlConnection _conexao;
		
		public CanalYoutubeRepository()


			{
			_conexao = Conectar();
		}

		public List<CanalYoutube> ListarVideos()
		{
			List<CanalYoutube> canal = new List<CanalYoutube>();
			SqlCommand query = new SqlCommand("select * from dbo.canal order by id desc", _conexao);
			_conexao.Open();
			SqlDataReader dados = query.ExecuteReader();
			while (dados.Read()){
				CanalYoutube youtube = new CanalYoutube()
				{
					Descricao = dados.GetString(dados.GetOrdinal("descricao")),
					Id = dados.GetInt32(dados.GetOrdinal("id")),
					Link = dados.GetString(dados.GetOrdinal("link")),
					Titulo = dados.GetString(dados.GetOrdinal("titulo"))
				};
				canal.Add(youtube);
			}
			_conexao.Close();
			_conexao.Dispose();

			return canal;
		}

	}
}