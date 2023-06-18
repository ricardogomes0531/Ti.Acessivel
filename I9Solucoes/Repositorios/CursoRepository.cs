using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using I9Solucoes.Models;

namespace I9Solucoes.Repositorios
{
    public class CursoRepository : DataBase
    {
        private SqlConnection _conexao;
        public CursoRepository()
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
            if (query.ExecuteNonQuery() > 0)
                retorno = true;
            else
                retorno = false;
            _conexao.Close();
            _conexao.Dispose();
            return retorno;
        }

        public List<Curso> Listar()
        {
            List<Curso> retorno = new List<Curso>();
            SqlCommand query = new SqlCommand("select * from curso where Ativo=1", _conexao);
            _conexao.Open();
            SqlDataReader dados = query.ExecuteReader();
            while (dados.Read())
            {
                Curso curso = new Curso()
                {
                    AceitaMatricula = dados.GetBoolean(dados.GetOrdinal("AceitaMatricula")),
                    Ativo = dados.GetBoolean(dados.GetOrdinal("Ativo")),
                    DataCadastro = dados.GetDateTime(dados.GetOrdinal("DataCadastro")),
                    DataInicio = dados.GetDateTime(dados.GetOrdinal("DataInicio")),
                    Descricao = dados.GetString(dados.GetOrdinal("Descricao")),
                    Id = dados.GetInt32(dados.GetOrdinal("Id")),
                    Nome = dados.GetString(dados.GetOrdinal("Nome")),
                    TempoPrevistoDuracao = dados.GetInt32(dados.GetOrdinal("TempoPrevistoDuracao")),
                    ValorMonetario = dados.GetDecimal(dados.GetOrdinal("ValorMonetario")),
                    Explicacao = dados.GetString(dados.GetOrdinal("Explicacao"))
                };
                retorno.Add(curso);
            }
            _conexao.Close();
            _conexao.Dispose();

            return retorno;
        }

        public Curso Buscar(int id)
        {
            Curso retorno = new Curso();
            SqlCommand query = new SqlCommand("select * from curso where Id=@id", _conexao);
            _conexao.Open();
            SqlParameter idParametro = new SqlParameter()
            {
                ParameterName = "@id",
                SqlDbType = SqlDbType.Int,
                Value = id
            };
            query.Parameters.Add(idParametro);
            SqlDataReader dados = query.ExecuteReader();
            if (dados.Read())
            {
                retorno.AceitaMatricula = dados.GetBoolean(dados.GetOrdinal("AceitaMatricula"));
                retorno.DataInicio = dados.GetDateTime(dados.GetOrdinal("DataInicio"));
                retorno.Descricao = dados.GetString(dados.GetOrdinal("Descricao"));
                retorno.Explicacao = dados.GetString(dados.GetOrdinal("Explicacao"));
                retorno.Id = dados.GetInt32(dados.GetOrdinal("Id"));
                retorno.Nome = dados.GetString(dados.GetOrdinal("Nome"));
                retorno.TempoPrevistoDuracao = dados.GetInt32(dados.GetOrdinal("TempoPrevistoDuracao"));
                retorno.ValorMonetario = dados.GetDecimal(dados.GetOrdinal("ValorMonetario"));
                retorno.DataCadastro = dados.GetDateTime(dados.GetOrdinal("DataCadastro"));
                retorno.Ativo = dados.GetBoolean(dados.GetOrdinal("Ativo"));
            }
            _conexao.Close();
            _conexao.Dispose();

            return retorno;
        }

        public List<CursoAluno> ListarCursosDoAluno(string email)
        {
            List<CursoAluno> cursos = new List<CursoAluno>();
            SqlCommand query = new SqlCommand("select c.Nome, ac.SnLiberado, ac.DataCadastro, ac.IdCurso, ac.DataInicio, ac.Datafim, t.linkpagamento, t.valor from curso c, aluno_curso ac, usuario u, tempo_cobranca_curso t where c.id=ac.IdCurso and ac.IdAluno=u.id and t.idcurso=ac.idcurso and t.id=ac.idtempoassinatura and u.email=@email", _conexao);
            _conexao.Open();
            SqlParameter parametroEmail = new SqlParameter()
            {
                ParameterName = "@email",
                SqlDbType = SqlDbType.VarChar,
                Value = email
            };
            query.Parameters.Add(parametroEmail);
            SqlDataReader dados = query.ExecuteReader();
            while (dados.Read())
            {
                CursoAluno curso = new CursoAluno()
                {
                    DataCadastro = dados.GetDateTime(dados.GetOrdinal("DataCadastro")),
                    IdCurso = dados.GetInt32(dados.GetOrdinal("IdCurso")),
                    Liberado = dados.GetString(dados.GetOrdinal("SnLiberado")),
                    NomeCurso = dados.GetString(dados.GetOrdinal("Nome")),
                    DataFim = dados.GetDateTime(dados.GetOrdinal("DataFim")),
                    DataInicio = dados.GetDateTime(dados.GetOrdinal("DataInicio")),
                    LinkPagamento = dados.GetString(dados.GetOrdinal("LinkPagamento")),
                    Valor = dados.GetDecimal(dados.GetOrdinal("Valor"))
                };
                cursos.Add(curso);
            };
            _conexao.Close();
            _conexao.Dispose();

            return cursos;
        }

        public List<Modulos> ListarModulosDoCurso(int idCurso)
        {
            List<Modulos> modulos = new List<Modulos>();
            SqlCommand query = new SqlCommand("select Id, Nome, IdCurso from dbo.modulo_curso where IdCurso=@idCurso", _conexao);
            _conexao.Open();
            SqlParameter parametroIdCurso = new SqlParameter()
            {
                ParameterName = "@idCurso",
                SqlDbType = SqlDbType.Int,
                Value = idCurso
            };
            query.Parameters.Add(parametroIdCurso);
            SqlDataReader dados = query.ExecuteReader();
            while (dados.Read())
            {
                Modulos modulo = new Modulos()
                {
                    Id = dados.GetInt32(dados.GetOrdinal("Id")),
                    Nome = dados.GetString(dados.GetOrdinal("Nome")),
                    IdCurso = dados.GetInt32(dados.GetOrdinal("IdCurso"))
                };
                modulos.Add(modulo);
            };
            _conexao.Close();
            _conexao.Dispose();

            return modulos;
        }

        public List<Aulas> ListarAulasDoModulo(int idCurso, int idModulo, int idAluno)
        {
            List<Aulas> aulas = new List<Aulas>();
            SqlCommand query = new SqlCommand("select ac.Id, ac.IdCurso, ac.IdModulo, ac.Nome, ac.ConteudoAula, ac.CaminhoArquivo, (select count(*) from aluno_frequencia f where f.idcurso=ac.idcurso and f.idmodulo=ac.idmodulo and f.idaula=ac.id and f.idaluno=@idAluno) as frequencia, (select count(*) from AtividadeCurso at where at.IdCurso=ac.IdCurso and at.IdModuloBloqueado=ac.IdModulo)  as TotalAtividades, (select count(*) from UsuarioAtividadeCurso ua where ua.IdUsuario=@idAluno and ua.IdModuloBloqueado=ac.IdModulo and ua.SnConcluida='S') as TotalAtividadeRealizada from aula_modulo_curso ac where ac.IdCurso=@idCurso and ac.IdModulo=@idModulo", _conexao);
            _conexao.Open();
            SqlParameter parametroIdCurso = new SqlParameter()
            {
                ParameterName = "@idCurso",
                SqlDbType = SqlDbType.Int,
                Value = idCurso
            };
            SqlParameter parametroIdModulo = new SqlParameter()
            {
                ParameterName = "@idModulo",
                SqlDbType = SqlDbType.Int,
                Value = idModulo
            };
            SqlParameter parametroIdAluno = new SqlParameter()
            {
                ParameterName = "@idAluno",
                SqlDbType = SqlDbType.Int,
                Value = idAluno
            };

            query.Parameters.Add(parametroIdCurso);
            query.Parameters.Add(parametroIdModulo);
            query.Parameters.Add(parametroIdAluno);
            SqlDataReader dados = query.ExecuteReader();
            while (dados.Read())
            {
                Aulas aula = new Aulas()
                {
                    CaminhoArquivo = dados.IsDBNull(dados.GetOrdinal("CaminhoArquivo")) ? null : dados.GetString(dados.GetOrdinal("CaminhoArquivo")),
                    ConteudoAula = dados.IsDBNull(dados.GetOrdinal("ConteudoAula")) ? null : dados.GetString(dados.GetOrdinal("ConteudoAula")),
                    Id = dados.GetInt32(dados.GetOrdinal("Id")),
                    IdCurso = dados.GetInt32(dados.GetOrdinal("IdCurso")),
                    IdModulo = dados.GetInt32(dados.GetOrdinal("IdModulo")),
                    Nome = dados.GetString(dados.GetOrdinal("Nome")),
                    Frequencia = dados.GetInt32(dados.GetOrdinal("frequencia")),
                    TotalAtividades = dados.GetInt32(dados.GetOrdinal("TotalAtividades")),
                    TotalAtividadeRealizada = dados.GetInt32(dados.GetOrdinal("TotalAtividadeRealizada"))
                };
                aulas.Add(aula);
            };
            _conexao.Close();
            _conexao.Dispose();

            return aulas;
        }

        public string MostrarTituloDaAula(int idCurso, int idModulo, int idAula)
        {
            string tituloAula = string.Empty;
            SqlCommand query = new SqlCommand("select Nome from dbo.aula_modulo_curso where IdCurso=@idCurso and IdModulo=@idModulo and Id=@idAula", _conexao);
            _conexao.Open();
            SqlParameter parametroIdCurso = new SqlParameter()
            {
                ParameterName = "@idCurso",
                SqlDbType = SqlDbType.Int,
                Value = idCurso
            };

            SqlParameter parametroIdModulo = new SqlParameter()
            {
                ParameterName = "@idModulo",
                SqlDbType = SqlDbType.Int,
                Value = idModulo
            };

            SqlParameter parametroIdAula = new SqlParameter()
            {
                ParameterName = "@idAula",
                SqlDbType = SqlDbType.Int,
                Value = idAula
            };

            query.Parameters.Add(parametroIdCurso);
            query.Parameters.Add(parametroIdModulo);
            query.Parameters.Add(parametroIdAula);

            SqlDataReader dado = query.ExecuteReader();
            if (dado.Read())
                tituloAula = dado.GetString(dado.GetOrdinal("Nome"));
            _conexao.Close();
            _conexao.Dispose();

            return tituloAula;
        }

        public string MostrarConteudoDaAula(int idCurso, int idModulo, int idAula)
        {
            string conteudoAula = string.Empty;
            SqlCommand query = new SqlCommand("select ConteudoAula from dbo.aula_modulo_curso where IdCurso=@idCurso and IdModulo=@idModulo and Id=@idAula", _conexao);
            _conexao.Open();
            SqlParameter parametroIdCurso = new SqlParameter()
            {
                ParameterName = "@idCurso",
                SqlDbType = SqlDbType.Int,
                Value = idCurso
            };

            SqlParameter parametroIdModulo = new SqlParameter()
            {
                ParameterName = "@idModulo",
                SqlDbType = SqlDbType.Int,
                Value = idModulo
            };

            SqlParameter parametroIdAula = new SqlParameter()
            {
                ParameterName = "@idAula",
                SqlDbType = SqlDbType.Int,
                Value = idAula
            };

            query.Parameters.Add(parametroIdCurso);
            query.Parameters.Add(parametroIdModulo);
            query.Parameters.Add(parametroIdAula);

            SqlDataReader dado = query.ExecuteReader();
            if (dado.Read())
                conteudoAula = dado.GetString(dado.GetOrdinal("ConteudoAula"));
            _conexao.Close();
            _conexao.Dispose();

            return conteudoAula;
        }

        public string PegarCaminhoDoArquivoMp3(int idCurso, int idModulo, int idAula)
        {
            string caminhoArquivo = string.Empty;
            SqlCommand query = new SqlCommand("select CaminhoArquivo from dbo.aula_modulo_curso where IdCurso=@idCurso and IdModulo=@idModulo and Id=@idAula", _conexao);
            _conexao.Open();
            SqlParameter parametroIdCurso = new SqlParameter()
            {
                ParameterName = "@idCurso",
                SqlDbType = SqlDbType.Int,
                Value = idCurso
            };

            SqlParameter parametroIdModulo = new SqlParameter()
            {
                ParameterName = "@idModulo",
                SqlDbType = SqlDbType.Int,
                Value = idModulo
            };

            SqlParameter parametroIdAula = new SqlParameter()
            {
                ParameterName = "@idAula",
                SqlDbType = SqlDbType.Int,
                Value = idAula
            };

            query.Parameters.Add(parametroIdCurso);
            query.Parameters.Add(parametroIdModulo);
            query.Parameters.Add(parametroIdAula);

            SqlDataReader dado = query.ExecuteReader();
            if (dado.Read())
                caminhoArquivo = dado.GetString(dado.GetOrdinal("CaminhoArquivo"));
            _conexao.Close();
            _conexao.Dispose();

            return caminhoArquivo;
        }

        public bool InserirAlunoNoCurso(int idCurso, int idAluno, string snLiberado, DateTime dataFim)
        {
            bool resultado = false;
            SqlCommand comando = new SqlCommand("insert into aluno_curso(idcurso, idaluno, snliberado, datacadastro, datafim) values(@idCurso, @idAluno, @snLiberado, @dataCadastro, @dataFim)", _conexao);
            _conexao.Open();
            SqlParameter parametroIdCurso = new SqlParameter()
            {
                ParameterName = "@idCurso",
                SqlDbType = SqlDbType.Int,
                Value = idCurso
            };
            SqlParameter parametroIdAluno = new SqlParameter()
            {
                ParameterName = "@idAluno",
                SqlDbType = SqlDbType.Int,
                Value = idAluno
            };
            SqlParameter parametroSnLiberado = new SqlParameter()
            {
                ParameterName = "@snLiberado",
                SqlDbType = SqlDbType.VarChar,
                Value = "n"
            };
            SqlParameter parametroDataCadastro = new SqlParameter()
            {
                ParameterName = "@dataCadastro",
                SqlDbType = SqlDbType.Date,
                Value = DateTime.Now
            };
            SqlParameter parametroDataFim = new SqlParameter()
            {
                ParameterName = "@dataFim",
                SqlDbType = SqlDbType.DateTime,
                Value = DateTime.Now
            };

            comando.Parameters.Add(parametroIdCurso);
            comando.Parameters.Add(parametroIdAluno);
            comando.Parameters.Add(parametroSnLiberado);
            comando.Parameters.Add(parametroDataCadastro);
            comando.Parameters.Add(parametroDataFim);
            if (comando.ExecuteNonQuery() > 0)
                resultado = true;
            _conexao.Close();
            _conexao.Dispose();

            return resultado;
        }

        public List<TempoCobrancaCurso> BuscarTempoCobranca(int idCurso)
        {
            List<TempoCobrancaCurso> tempoCobranca = new List<TempoCobrancaCurso>();
            SqlCommand comando = new SqlCommand("select * from dbo.tempo_cobranca_curso where idCurso=@idCurso", _conexao);
            _conexao.Open();
            SqlParameter parametroIdCurso = new SqlParameter()
            {
                ParameterName = "@idCurso",
                SqlDbType = SqlDbType.Int,
                Value = idCurso
            };
            comando.Parameters.Add(parametroIdCurso);
            SqlDataReader dado = comando.ExecuteReader();
            while (dado.Read())
            {
                TempoCobrancaCurso tempo = new TempoCobrancaCurso()
                {
                    Id = dado.GetInt32(dado.GetOrdinal("id")),
                    IdCurso = dado.GetInt32(dado.GetOrdinal("idCurso")),
                    Tempo = dado.GetString(dado.GetOrdinal("tempo")),
                    Valor = dado.GetDecimal(dado.GetOrdinal("valor"))
                };
                tempoCobranca.Add(tempo);
            }
            _conexao.Close();
            _conexao.Dispose();

            return tempoCobranca;
        }

        public bool InserirAlunoNoCurso(int idCurso, int idAluno, DateTime dataFim, DateTime dataInicio, int idTempoAssinatura, int? idDemonstracao=null)
        {
            bool alunoInserido = false;
            SqlCommand query = new SqlCommand("insert into aluno_curso(idcurso, idaluno, snliberado, datacadastro, datafim, datainicio, idtempoassinatura, idDemonstracao) values(@idCurso, @idAluno,@snLiberado, @dataCadastro, @dataFim, @dataInicio, @idTempoAssinatura, @idDemonstracao)", _conexao);
            _conexao.Open();

            SqlParameter parametroIdCurso = new SqlParameter()
            {
                ParameterName = "@idCurso",
                SqlDbType = SqlDbType.Int,
                Value = idCurso
            };

            SqlParameter parametroIdDemonstracao = new SqlParameter()
            {
                ParameterName = "@idDemonstracao",
                SqlDbType = SqlDbType.Int,
                            };
            if (idDemonstracao.HasValue)
            {
                parametroIdDemonstracao.Value = idDemonstracao.Value;
                    }
            else
            {
                parametroIdDemonstracao.Value = Convert.DBNull; 
            }
            SqlParameter parametroIdAluno = new SqlParameter()
            {
                ParameterName = "@idAluno",
                SqlDbType = SqlDbType.Int,
                Value = idAluno
            };

            SqlParameter parametroSnLiberado = new SqlParameter()
            {
                ParameterName = "@snLiberado",
                SqlDbType = SqlDbType.Char,
                Value = "n"
            };

            SqlParameter parametroDataCadastro = new SqlParameter()
            {
                ParameterName = "@dataCadastro",
                SqlDbType = SqlDbType.Date,
                Value = DateTime.Now
            };

            SqlParameter parametroDataFim = new SqlParameter()
            {
                ParameterName = "@dataFim",
                SqlDbType = SqlDbType.Date,
                Value = dataFim
            };
            SqlParameter parametroDataInicio = new SqlParameter()
            {
                ParameterName = "@dataInicio",
                SqlDbType = SqlDbType.Date,
                Value = dataInicio
            };
            SqlParameter parametroIdTempoAssinatura = new SqlParameter()
            {
                ParameterName = "@idTempoAssinatura",
                SqlDbType = SqlDbType.Int,
                Value = idTempoAssinatura
            };

            query.Parameters.Add(parametroIdCurso);
            query.Parameters.Add(parametroIdAluno);
            query.Parameters.Add(parametroSnLiberado);
            query.Parameters.Add(parametroDataCadastro);
            query.Parameters.Add(parametroDataFim);
            query.Parameters.Add(parametroDataInicio);
            query.Parameters.Add(parametroIdTempoAssinatura);
            query.Parameters.Add(parametroIdDemonstracao);
            if (query.ExecuteNonQuery() > 0)
                alunoInserido = true;

            _conexao.Close();
            _conexao.Dispose();

            return alunoInserido;
        }

        public int BuscarTempoDoCurso(int idCobranca, int idCurso)
        {
            int totalTempoCobranca = 0;
            SqlCommand comando = new SqlCommand("select tempo from dbo.tempo_cobranca_curso where idCurso=@idCurso and id=@idCobranca", _conexao);
            _conexao.Open();
            SqlParameter parametroIdCurso = new SqlParameter()
            {
                ParameterName = "@idCurso",
                SqlDbType = SqlDbType.Int,
                Value = idCurso
            };

            SqlParameter parametroIdCobranca = new SqlParameter()
            {
                ParameterName = "@idCobranca",
                SqlDbType = SqlDbType.Int,
                Value = idCobranca
            };

            comando.Parameters.Add(parametroIdCurso);
            comando.Parameters.Add(parametroIdCobranca);

            SqlDataReader dado = comando.ExecuteReader();
            if (dado.Read())
            {
                totalTempoCobranca = Convert.ToInt32(dado.GetString(dado.GetOrdinal("tempo")));
            }
            _conexao.Close();
            _conexao.Dispose();

            return totalTempoCobranca;
        }

        public bool ChecarSeAlunoJaEstarInscritoNoCurso(int idAluno, int idCurso)
        {
            bool alunoInscrito = false;
            SqlCommand query = new SqlCommand("select count(*) as total from dbo.aluno_curso where idCurso=@idCurso and idAluno=@idAluno", _conexao);
            _conexao.Open();

            SqlParameter parametroIdAluno = new SqlParameter()
            {
                ParameterName = "@idAluno",
                SqlDbType = SqlDbType.Int,
                Value = idAluno
            };

            SqlParameter parametroIdCurso = new SqlParameter()
            {
                ParameterName = "@idCurso",
                SqlDbType = SqlDbType.Int,
                Value = idCurso
            };
            query.Parameters.Add(parametroIdAluno);
            query.Parameters.Add(parametroIdCurso);
            SqlDataReader dados = query.ExecuteReader();
            if (dados.Read())
            {
                if (dados.GetInt32(dados.GetOrdinal("total")) > 0)
                    alunoInscrito = true;
            }
            _conexao.Close();
            _conexao.Dispose();

            return alunoInscrito;
        }

        public bool InserirFrequencia(int idCurso, int idModulo, int idAula, int idAluno)
        {
            bool resultado = false;
            SqlCommand comando = new SqlCommand("insert into aluno_frequencia(idcurso,idmodulo,idaula, idaluno, datacadastro) values(@idCurso, @idModulo, @idAula, @idAluno, @dataCadastro)", _conexao);
            _conexao.Open();
            SqlParameter parametroIdCurso = new SqlParameter()
            {
                ParameterName = "@idCurso",
                SqlDbType = SqlDbType.Int,
                Value = idCurso
            };

            SqlParameter parametroIdModulo = new SqlParameter()
            {
                ParameterName = "@idModulo",
                SqlDbType = SqlDbType.Int,
                Value = idModulo
            };

            SqlParameter parametroIdAula = new SqlParameter()
            {
                ParameterName = "@idAula",
                SqlDbType = SqlDbType.Int,
                Value = idAula
            };

            SqlParameter parametroIdAluno = new SqlParameter()
            {
                ParameterName = "@idAluno",
                SqlDbType = SqlDbType.Int,
                Value = idAluno
            };
            SqlParameter parametroDataCadastro = new SqlParameter()
            {
                ParameterName = "@dataCadastro",
                SqlDbType = SqlDbType.Date,
                Value = DateTime.Now
            };
            comando.Parameters.Add(parametroIdCurso);
            comando.Parameters.Add(parametroIdModulo);
            comando.Parameters.Add(parametroIdAula);
            comando.Parameters.Add(parametroIdAluno);
            comando.Parameters.Add(parametroDataCadastro);
            if (comando.ExecuteNonQuery() > 0)
                resultado = true;
            _conexao.Close();
            _conexao.Dispose();

            return resultado;
        }

        public bool RemoverFrequencia(int idCurso, int idModulo, int idAula, int idAluno)
        {
            bool resultado = false;
            SqlCommand comando = new SqlCommand("delete from aluno_frequencia where idcurso=@idCurso and idmodulo=@idModulo and idaula=@idAula and idaluno=@idAluno", _conexao);
            _conexao.Open();
            SqlParameter parametroIdCurso = new SqlParameter()
            {
                ParameterName = "@idCurso",
                SqlDbType = SqlDbType.Int,
                Value = idCurso
            };

            SqlParameter parametroIdModulo = new SqlParameter()
            {
                ParameterName = "@idModulo",
                SqlDbType = SqlDbType.Int,
                Value = idModulo
            };

            SqlParameter parametroIdAula = new SqlParameter()
            {
                ParameterName = "@idAula",
                SqlDbType = SqlDbType.Int,
                Value = idAula
            };

            SqlParameter parametroIdAluno = new SqlParameter()
            {
                ParameterName = "@idAluno",
                SqlDbType = SqlDbType.Int,
                Value = idAluno
            };
            comando.Parameters.Add(parametroIdCurso);
            comando.Parameters.Add(parametroIdModulo);
            comando.Parameters.Add(parametroIdAula);
            comando.Parameters.Add(parametroIdAluno);
            if (comando.ExecuteNonQuery() > 0)
                resultado = true;
            _conexao.Close();
            _conexao.Dispose();

            return resultado;
        }

        public bool LiberarCursoParaAluno(int idCurso, int idAluno)
        {
            bool resultado = false;
            SqlCommand comando = new SqlCommand("update dbo.aluno_curso set snliberado='s' where idcurso=@idcurso and idaluno=@idaluno", _conexao);
            _conexao.Open();
            SqlParameter parametroIdCurso = new SqlParameter()
            {
                ParameterName = "@idCurso",
                SqlDbType = SqlDbType.Int,
                Value = idCurso
            };
            SqlParameter parametroIdAluno = new SqlParameter()
            {
                ParameterName = "@idAluno",
                SqlDbType = SqlDbType.Int,
                Value = idAluno
            };
            comando.Parameters.Add(parametroIdCurso);
            comando.Parameters.Add(parametroIdAluno);
            if (comando.ExecuteNonQuery() > 0)
                resultado = true;
            _conexao.Close();
            _conexao.Dispose();


            return resultado;
        }

        public bool AtivarCurso(int idCurso, int idAluno)
        {
            bool resultado = false;
            SqlCommand query = new SqlCommand("update dbo.aluno_curso set snliberado=@liberado where idaluno=@idAluno and idcurso=@idCurso", _conexao);
            _conexao.Open();
            SqlParameter parametroLiberado = new SqlParameter()
            {
                ParameterName = "@liberado",
                SqlDbType = SqlDbType.VarChar,
                Value = "s"
            };
            SqlParameter parametroIdAluno = new SqlParameter()
            {
                ParameterName = "@idAluno",
                SqlDbType = SqlDbType.Int,
                Value = idAluno
            };
            SqlParameter parametroIdCurso = new SqlParameter()
            {
                ParameterName = "@idCurso",
                SqlDbType = SqlDbType.Int,
                Value = idCurso
            };
            query.Parameters.Add(parametroIdAluno);
            query.Parameters.Add(parametroIdCurso);
            query.Parameters.Add(parametroLiberado);
            if (query.ExecuteNonQuery() > 0)
                resultado = true;
            _conexao.Close();
            _conexao.Dispose();

            return resultado;
        }

        public AtividadeCurso GetAtividadeOrigem(int idCurso, int idModulo)
        {
            AtividadeCurso atividade = new AtividadeCurso();
            SqlCommand query = new SqlCommand("select * from dbo.atividadecurso where idcurso=@idcurso and idmodulo=@idmodulo", _conexao);
            _conexao.Open();
            SqlParameter parametroIdModulo = new SqlParameter
            {
                ParameterName = "idModulo",
                SqlDbType = SqlDbType.Int,
                Value = idModulo
            };
            SqlParameter parametroIdCurso = new SqlParameter
            {
                ParameterName = "idCurso",
                SqlDbType = SqlDbType.Int,
                Value = idCurso
            };

            query.Parameters.Add(parametroIdCurso);
            query.Parameters.Add(parametroIdModulo);

            SqlDataReader dados = query.ExecuteReader();
            if (dados.Read())
            {
                atividade.IdAtividade = dados.GetInt32(dados.GetOrdinal("idatividade"));
                atividade.Titulo = dados.GetString(dados.GetOrdinal("titulo"));
                atividade.Descricao = dados.GetString(dados.GetOrdinal("descricao"));
                atividade.IdCurso = dados.GetInt32(dados.GetOrdinal("idcurso"));
                atividade.IdModuloBloqueado = dados.GetInt32(dados.GetOrdinal("idmodulobloqueado"));
            }
            _conexao.Close();
            _conexao.Dispose();

            return atividade;
        }

        public AtividadeCurso GetAtividadeDestino(int idCurso, int idModulo)
        {
            AtividadeCurso atividade = new AtividadeCurso();
            SqlCommand query = new SqlCommand("select * from dbo.atividadecurso where idcurso=@idcurso and idmodulobloqueado=@idmodulo", _conexao);
            _conexao.Open();
            SqlParameter parametroIdModulo = new SqlParameter
            {
                ParameterName = "idModulo",
                SqlDbType = SqlDbType.Int,
                Value = idModulo
            };
            SqlParameter parametroIdCurso = new SqlParameter
            {
                ParameterName = "idCurso",
                SqlDbType = SqlDbType.Int,
                Value = idCurso
            };

            query.Parameters.Add(parametroIdCurso);
            query.Parameters.Add(parametroIdModulo);

            SqlDataReader dados = query.ExecuteReader();
            if (dados.Read())
            {
                atividade.IdAtividade = dados.GetInt32(dados.GetOrdinal("idatividade"));
                atividade.Titulo = dados.GetString(dados.GetOrdinal("titulo"));
                atividade.Descricao = dados.GetString(dados.GetOrdinal("descricao"));
                atividade.IdCurso = dados.GetInt32(dados.GetOrdinal("idcurso"));
                atividade.IdModuloBloqueado = dados.GetInt32(dados.GetOrdinal("idmodulobloqueado"));
            }
            _conexao.Close();
            _conexao.Dispose();

            return atividade;
        }

        public UsuarioAtividade GetAtividadeUsuario(int idCurso, int idModuloBloqueado, int idUsuario, int idAtividade)
        {
            UsuarioAtividade atividade = new UsuarioAtividade();
            SqlCommand query = new SqlCommand("select * from UsuarioAtividadeCurso where idcurso=@idCurso and idmodulobloqueado=@idModuloBloqueado and idusuario=@idUsuario and idatividade=@idAtividade", _conexao);
            _conexao.Open();
            SqlParameter parametroIdModuloBloqueado = new SqlParameter
            {
                ParameterName = "idModuloBloqueado",
                SqlDbType = SqlDbType.Int,
                Value = idModuloBloqueado
            };
            SqlParameter parametroIdCurso = new SqlParameter
            {
                ParameterName = "idCurso",
                SqlDbType = SqlDbType.Int,
                Value = idCurso
            };
            SqlParameter parametroIdUsuario = new SqlParameter
            {
                ParameterName = "idUsuario",
                SqlDbType = SqlDbType.Int,
                Value = idUsuario
            };

            SqlParameter parametroIdAtividade = new SqlParameter
            {
                ParameterName = "idAtividade",
                SqlDbType = SqlDbType.Int,
                Value = idAtividade
            };

            query.Parameters.Add(parametroIdCurso);
            query.Parameters.Add(parametroIdModuloBloqueado);
            query.Parameters.Add(parametroIdUsuario);
            query.Parameters.Add(parametroIdAtividade);
            SqlDataReader dados = query.ExecuteReader();
            if (dados.Read())
            {
                atividade.Id = dados.GetInt32(dados.GetOrdinal("id"));
                atividade.IdModuloBloqueado = dados.GetInt32(dados.GetOrdinal("idmodulobloqueado"));
                atividade.IdUsuario = dados.GetInt32(dados.GetOrdinal("idusuario"));
                atividade.ComentarioProfessor = dados.IsDBNull(dados.GetOrdinal("ComentarioProfessor")) ? null : dados.GetString(dados.GetOrdinal("ComentarioProfessor"));
                                                atividade.Resposta = dados.IsDBNull(dados.GetOrdinal("Resposta")) ? null : dados.GetString(dados.GetOrdinal("Resposta"));
                atividade.SnConcluida = dados.IsDBNull(dados.GetOrdinal("SnConcluida")) ? null : dados.GetString(dados.GetOrdinal("SnConcluida"));
                if (dados.IsDBNull(dados.GetOrdinal("nota")))
                    atividade.Nota = null;
                else
                    atividade.Nota = dados.GetInt32(dados.GetOrdinal("nota"));
            }
            _conexao.Close();
            _conexao.Dispose();

            return atividade;
        }

        public List<AtividadeCurso> ListarAtividades(int idCurso, int idModulo)
        {
            List<AtividadeCurso> atividades = new List<AtividadeCurso>();
            SqlCommand query = new SqlCommand("select * from AtividadeCurso where idCurso=@idCurso and idModulo=@idModulo", _conexao);
            _conexao.Open();
            SqlParameter parametroIdCurso = new SqlParameter
            {
                SqlDbType = SqlDbType.Int,
                ParameterName = "idCurso",
                Value = idCurso
            };

            SqlParameter parametroIdModulo = new SqlParameter
            {
                SqlDbType = SqlDbType.Int,
                ParameterName = "idModulo",
                Value = idModulo
            };
            query.Parameters.Add(parametroIdCurso);
            query.Parameters.Add(parametroIdModulo);

            SqlDataReader dados = query.ExecuteReader();
            while (dados.Read())
            {
                AtividadeCurso atividade = new AtividadeCurso
                {
                    IdAtividade = dados.GetInt32(dados.GetOrdinal("idatividade")),
                    Descricao = dados.GetString(dados.GetOrdinal("descricao")),
                    IdCurso = dados.GetInt32(dados.GetOrdinal("idcurso")),
                    IdModulo = dados.GetInt32(dados.GetOrdinal("idmodulo")),
                    Titulo = dados.GetString(dados.GetOrdinal("titulo"))
                };
                atividades.Add(atividade);
            }
            _conexao.Close();
            _conexao.Dispose();

            return atividades;
        }

        public AtividadeCurso GetAtividade(int idAtividade)
        {
            AtividadeCurso atividade = new AtividadeCurso();
            SqlCommand query = new SqlCommand("select * from AtividadeCurso where idAtividade=@idAtividade", _conexao);
            _conexao.Open();
            SqlParameter parametroIdAtividade = new SqlParameter
            {
                SqlDbType = SqlDbType.Int,
                ParameterName = "idAtividade",
                Value = idAtividade
            };

            query.Parameters.Add(parametroIdAtividade);

            SqlDataReader dados = query.ExecuteReader();
            if (dados.Read())
            {
                atividade.IdAtividade = dados.GetInt32(dados.GetOrdinal("idatividade"));
                atividade.Descricao = dados.GetString(dados.GetOrdinal("descricao"));
                atividade.IdCurso = dados.GetInt32(dados.GetOrdinal("idcurso"));
                atividade.IdModulo = dados.GetInt32(dados.GetOrdinal("idmodulo"));
                atividade.Titulo = dados.GetString(dados.GetOrdinal("titulo"));
                atividade.IdModuloBloqueado = dados.GetInt32(dados.GetOrdinal("IdModuloBloqueado"));
            }
            _conexao.Close();
            _conexao.Dispose();

            return atividade;
        }

        public int SalvarAtividade(int idAluno, int idCurso, int idModuloBloqueado, string resposta, int idAtividade)
        {
            int idUsuarioAtividade = 0;
            SqlCommand query = new SqlCommand("if (not exists(select * from UsuarioAtividadeCurso where idcurso=@idCurso and idModuloBloqueado=@idModuloBloqueado and idUsuario=@idAluno and IdAtividade=@idAtividade and SnConcluida='N')) begin insert into UsuarioAtividadeCurso(IdUsuario, IdModuloBloqueado, Resposta, ComentarioProfessor, SnConcluida, Nota, DataEnvio, IdCurso, IdAtividade) values(@idAluno, @idModuloBloqueado, @resposta, null, 'N', null, Getdate(), @idCurso, @idAtividade) select scope_identity() end else begin update UsuarioAtividadeCurso set resposta=@resposta, DataEnvio=getdate() where IdCurso=@idCurso and IdModuloBloqueado=@idModuloBloqueado and IdUsuario=@idAluno and SnConcluida='N' and IdAtividade=@idAtividade select id from UsuarioAtividadeCurso where IdAtividade=@idAtividade end", _conexao);
            _conexao.Open();
            SqlParameter parametroIdAluno = new SqlParameter
            {
                ParameterName = "idAluno",
                SqlDbType = SqlDbType.Int,
                Value = idAluno
            };

            SqlParameter parametroIdCurso = new SqlParameter
            {
                ParameterName = "idCurso",
                SqlDbType = SqlDbType.Int,
                Value = idCurso
            };
            SqlParameter parametroIdModuloBloqueado = new SqlParameter
            {
                ParameterName = "idModuloBloqueado",
                SqlDbType = SqlDbType.Int,
                Value = idModuloBloqueado
            };
            SqlParameter parametroResposta = new SqlParameter
            {
                ParameterName = "resposta",
                SqlDbType = SqlDbType.VarChar,
                Value = resposta
            };

            SqlParameter parametroIdAtividade = new SqlParameter
            {
                ParameterName = "idAtividade",
                SqlDbType = SqlDbType.Int,
                Value = idAtividade
            };

            query.Parameters.Add(parametroIdCurso);
            query.Parameters.Add(parametroIdModuloBloqueado);
            query.Parameters.Add(parametroResposta);
            query.Parameters.Add(parametroIdAluno);
            query.Parameters.Add(parametroIdAtividade);

            idUsuarioAtividade = Convert.ToInt32(query.ExecuteScalar());

            _conexao.Close();
            _conexao.Dispose();

            return idUsuarioAtividade;
        }

        public DetalheAtividade BuscarDetalhesAtividade(int id)
        {
                        DetalheAtividade atividade = new DetalheAtividade();
            SqlCommand query = new SqlCommand("select u.nome, u.email, ac.idAtividade as idAtividade, ac.descricao, uac.resposta, uac.id, uac.snconcluida from usuarioatividadecurso uac, atividadecurso ac, usuario u where uac.idAtividade=ac.idAtividade and u.id=uac.idUsuario and uac.id=@id", _conexao);
            _conexao.Open();
            SqlParameter idParametro = new SqlParameter()
            {
                ParameterName = "@id",
                SqlDbType = SqlDbType.Int,
                Value = id
            };
            query.Parameters.Add(idParametro);
            SqlDataReader dados = query.ExecuteReader();
            if (dados.Read())
            {
                atividade.Descricao = dados.GetString(dados.GetOrdinal("descricao"));
                atividade.Resposta = dados.GetString(dados.GetOrdinal("resposta"));
                atividade.Usuario = dados.GetString(dados.GetOrdinal("nome"));
                atividade.Id = dados.GetInt32(dados.GetOrdinal("id"));
                atividade.IdAtividade = dados.GetInt32(dados.GetOrdinal("idAtividade"));
                atividade.Concluida = dados.GetString(dados.GetOrdinal("snconcluida"));
                atividade.EmailUsuario = dados.GetString(dados.GetOrdinal("email"));
            }
            _conexao.Close();
            _conexao.Dispose();

            return atividade;
        }

        public bool SalvarCorrecaoAtividade(int idAtividadeCurso, string comentarioProfessor, string snConcluida, int nota)
        {
            bool resultado = false;
            SqlCommand comando = new SqlCommand("update usuarioatividadecurso set snconcluida=@snConcluida, comentarioprofessor=@comentarioProfessor, nota=@nota where id=@idUsuarioAtividade", _conexao);
            _conexao.Open();
            SqlParameter parametroIdUsuarioAtividade = new SqlParameter()
            {
                ParameterName = "@idUsuarioAtividade",
                SqlDbType = SqlDbType.Int,
                Value = idAtividadeCurso
            };
            SqlParameter parametroComentarioProfessor = new SqlParameter()
            {
                ParameterName = "@comentarioProfessor",
                SqlDbType = SqlDbType.VarChar,
                Value = comentarioProfessor
            };
            SqlParameter parametroSnConcluida = new SqlParameter()
            {
                ParameterName = "@snConcluida",
                SqlDbType = SqlDbType.VarChar,
                                Value = snConcluida
            };
            SqlParameter parametroNota = new SqlParameter()
            {
                ParameterName = "@nota",
                SqlDbType = SqlDbType.Int,
                Value = nota
            };

            comando.Parameters.Add(parametroIdUsuarioAtividade);
            comando.Parameters.Add(parametroComentarioProfessor);
            comando.Parameters.Add(parametroSnConcluida);
            comando.Parameters.Add(parametroNota);
            if (comando.ExecuteNonQuery() > 0)
                resultado = true;
            _conexao.Close();
            _conexao.Dispose();


            return resultado;
        }

public List<DadosLiberacaoAluno> ListarAlunosSemLiberacao()
        {
            List<DadosLiberacaoAluno> alunos = new List<DadosLiberacaoAluno>();
            SqlCommand query = new SqlCommand("select u.nome as Nome, ac.DataCadastro as DataCadastro, c.nome as NomeCurso, c.id as IdCurso, ac.id as Id, t.id as IdAssinatura from curso c, aluno_curso ac, usuario u, tempo_cobranca_curso t where c.id=ac.IdCurso and ac.IdAluno=u.id and t.idcurso=ac.idcurso and t.id=ac.idtempoassinatura and ac.snliberado='n'", _conexao);
            _conexao.Open();
            SqlDataReader dados = query.ExecuteReader();
            while(dados.Read())
            {
                DadosLiberacaoAluno aluno = new DadosLiberacaoAluno()
                {
                                         IdCurso = dados.GetInt32(dados.GetOrdinal("IdCurso")),
                    Nome = dados.GetString(dados.GetOrdinal("Nome")),
                    NomeCurso = dados.GetString(dados.GetOrdinal("NomeCurso")),
                     Id = dados.GetInt32(dados.GetOrdinal("Id")),
                      IdAssinatura = dados.GetInt32(dados.GetOrdinal("IdAssinatura"))
                };
                if (dados.IsDBNull(dados.GetOrdinal("DataCadastro")))
                {
                    aluno.DataCadastro = null;
                }
                else
                {
                    aluno.DataCadastro = dados.GetDateTime(dados.GetOrdinal("DataCadastro"));
                }
                alunos.Add(aluno);
            }
            return alunos;
        }

        public bool LiberarCursoAlunoInscrito(int id, int idAssinatura, int idCurso, DateTime dataFim)
        {
            bool resultado = false;
            SqlCommand comando = new SqlCommand("update aluno_curso set snliberado='s', datainicio=getdate(), datafim=@dataFim where id=@id", _conexao);
            _conexao.Open();
            SqlParameter parametroId = new SqlParameter()
            {
                ParameterName = "@id",
                SqlDbType = SqlDbType.Int,
                Value = id
            };
            SqlParameter parametroDataFim = new SqlParameter()
            {
                ParameterName = "@dataFim",
                SqlDbType = SqlDbType.Date,
                Value = dataFim
            };

            comando.Parameters.Add(parametroId);
            comando.Parameters.Add(parametroDataFim);
            if (comando.ExecuteNonQuery() > 0)
                resultado = true;
            _conexao.Close();
            _conexao.Dispose();


            return resultado;
        }


    }
}
