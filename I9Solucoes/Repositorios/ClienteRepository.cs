using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace I9Solucoes.Repositorios
{
	public class ClienteRepository:DataBase
	{
		private SqlConnection _conexao;
		public ClienteRepository()
		{
			_conexao = Conectar();
		}
				
	}
}