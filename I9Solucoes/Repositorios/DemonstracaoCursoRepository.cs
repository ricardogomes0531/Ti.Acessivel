using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using I9Solucoes.Models;

namespace I9Solucoes.Repositorios
{
    public class DemonstracaoCursoRepository : DataBase
    {
        private SqlConnection _conexao;
        public DemonstracaoCursoRepository()
        {
            _conexao = Conectar();
        }


        public DemonstracaoCursoModel Buscar(int idCurso)
        {
            DemonstracaoCursoModel demonstracao = new DemonstracaoCursoModel();
            SqlCommand query = new SqlCommand("select * from Demonstracao where idCurso=@idCurso and snativo='s'", _conexao);
            _conexao.Open();
            SqlParameter idCursoParametro = new SqlParameter()
            {
                ParameterName = "@idCurso",
                SqlDbType = SqlDbType.Int,
                Value = idCurso
            };
            query.Parameters.Add(idCursoParametro);
            SqlDataReader dados = query.ExecuteReader();
            if (dados.Read())
            {
                demonstracao = new DemonstracaoCursoModel()
                {
                    Id = dados.GetInt32(dados.GetOrdinal("Id")),
                    Ativo = dados.GetString(dados.GetOrdinal("SnAtivo")),
                    Codigo = dados.GetString(dados.GetOrdinal("Codigo")),
                    IdCurso = dados.GetInt32(dados.GetOrdinal("IdCurso")),
                    QuantidadeDias = dados.GetInt32(dados.GetOrdinal("QuantidadeDias")),
                    Nome = dados.GetString(dados.GetOrdinal("Nome"))
                };
            }
            _conexao.Close();
            _conexao.Dispose();
            
        
            return demonstracao;
        }

    }
}