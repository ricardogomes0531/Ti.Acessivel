using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using I9Solucoes.Models;

namespace I9Solucoes.Repositorios
{
	public class CadastroRepository : DataBase
	{
		private SqlConnection _conexao;
		public CadastroRepository()
		{
			_conexao = Conectar();
		}

		public bool Inserir(string nome, DateTime dataNascimento, string cpf, string sexo, string email, string celular, string whatsapp, string senha)
		{
			bool retorno = false;
			SqlCommand query = new SqlCommand("insert into Usuario(Nome, DataNascimento, Cpf, Sexo, Email, Celular, WhatSapp, senha, snemailenviado, datacadastro) values(@nome, @dataNascimento, @cpf, @sexo, @email, @celular, @whatsapp, @senha, null, getdate())", _conexao);
			_conexao.Open();
			SqlParameter parametroNome = new SqlParameter()
			{
				ParameterName = "@nome",
				SqlDbType = SqlDbType.VarChar,
				Value = nome
			};
			SqlParameter parametroDataNascimento = new SqlParameter()
			{
				ParameterName = "@dataNascimento",
				SqlDbType = SqlDbType.DateTime,
				Value = dataNascimento
			};
			SqlParameter parametroCpf = new SqlParameter()
			{
				ParameterName = "@cpf",
				SqlDbType = SqlDbType.VarChar,
				Value = cpf
			};
			SqlParameter parametroSexo = new SqlParameter()
			{
				ParameterName = "@sexo",
				SqlDbType = SqlDbType.VarChar,
				Value = sexo
			};
			SqlParameter parametroEmail = new SqlParameter()
			{
				ParameterName = "@email",
				SqlDbType = SqlDbType.VarChar,
				Value = email
			};
			SqlParameter parametroCelular = new SqlParameter()
			{
				ParameterName = "@celular",
				SqlDbType = SqlDbType.VarChar,
				Value = celular
			};
			SqlParameter parametroWhatsapp = new SqlParameter()
			{
				ParameterName = "@whatsapp",
				SqlDbType = SqlDbType.VarChar,
				Value = whatsapp
			};
			SqlParameter parametroSenha = new SqlParameter()
			{
				ParameterName = "@senha",
				SqlDbType = SqlDbType.VarChar,
				Value = senha
			};
			query.Parameters.Add(parametroNome);
			query.Parameters.Add(parametroDataNascimento);
			query.Parameters.Add(parametroCpf);
			query.Parameters.Add(parametroSexo);
			query.Parameters.Add(parametroEmail);
			query.Parameters.Add(parametroCelular);
			query.Parameters.Add(parametroWhatsapp);
			query.Parameters.Add(parametroSenha);
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
