using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using I9Solucoes.Models;

namespace I9Solucoes.Repositorios
{
    public class LogRepository : DataBase
    {
        private SqlConnection _conexao;
        public LogRepository()
        {
            _conexao = Conectar();
        }

        public bool Inserir(string erro, string detalhe)
        {
            bool retorno = false;
            SqlCommand query = new SqlCommand("insert into log(Erro, detalhe) values(@erro, @detalhe)", _conexao);
            _conexao.Open();
            SqlParameter parametroErro = new SqlParameter()
            {
                ParameterName = "@erro",
                SqlDbType = SqlDbType.VarChar,
                Value = erro
            };
            SqlParameter parametroDetalhe = new SqlParameter()
            {
                ParameterName = "@detalhe",
                SqlDbType = SqlDbType.VarChar,
                Value = detalhe
            };

            query.Parameters.Add(parametroErro);
            query.Parameters.Add(parametroDetalhe);
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
