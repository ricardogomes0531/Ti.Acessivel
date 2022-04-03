use I9Solucoes;
	IF OBJECT_ID('dbo.usuario') is null
create table Usuario(Id int identity primary key, Nome varchar(50) not null, DataNascimento date not null, Cpf varchar(14) not null, Sexo varchar(8) not null, Email varchar(50) not null, Celular varchar(11) not null, Whatsapp varchar(3) not null, senha varchar(10))
	IF OBJECT_ID('dbo.curso') is null
create table curso(Id int identity primary key, Nome varchar(50) not null, DataCadastro date not null, TempoPrevistoDuracao int not null, Descricao varchar(100) not null, Explicacao varchar(1000) not null, Ativo bit not null, AceitaMatricula bit not null, DataInicio date, ValorMonetario decimal not null)
--insere curso
insert into curso (nome, datacadastro, tempoprevistoduracao, descricao, explicacao, ativo, aceitamatricula, datainicio, valormonetario)
values('Lógica de programação para Deficientes Visuais', getdate(), 3, 'Curso prático de lógica de programação com php', 'Curso onde a 	pessoa cega aprenderá sem todo aquele sofrimento, os conceitos com prática aplicada.', 1, 1, '01-11-2020', '300.00')

	IF OBJECT_ID('dbo.aluno_curso') is null
create table aluno_curso(Id int identity primary key, IdCurso int, IdAluno int, SnLiberado varchar(1), DataCadastro datetime, DataFim date, DataInicio date, IdTempoAssinatura int)
insert into aluno_curso(idcurso, idaluno, snliberado, datacadastro, datafim) values(1,2,'s',getdate(), '01-02-2021');

	IF OBJECT_ID('dbo.modulo_curso') is null
create table modulo_curso(Id int identity primary key, IdCurso int, Nome varchar(50))
insert into modulo_curso(idcurso, nome) values((select c.id from dbo.curso c where c.nome='lógica de programação para deficientes visuais'), 'introdução')

insert into modulo_curso(idcurso, nome) values(1, 'Estruturas condicionais')

	IF OBJECT_ID('dbo.aula_modulo_curso') is null
create table aula_modulo_curso(Id int identity primary key, IdCurso int, IdModulo int, Nome varchar(50), ConteudoAula varchar(max), CaminhoArquivo varchar(50))
insert into aula_modulo_curso(idcurso, idmodulo, Nome, conteudoaula, caminhoarquivo) values(1, 1, 'Aula 01', 'PHP é uma linguagem de programação interpretada.', '')
insert into aula_modulo_curso(idcurso, idmodulo, Nome, conteudoaula, caminhoarquivo) values((select c.id from dbo.curso c where c.nome='lógica de programação para deficientes visuais'), 6, 'Aula 01', null, 'audio.mp3')


	IF OBJECT_ID('dbo.tempo_cobranca_curso') is null
create table tempo_cobranca_curso(Id int identity primary key, idCurso int, tempo char(3), valor decimal, LinkPagamento varchar(500))
insert into dbo.tempo_cobranca_curso(idCurso, tempo, valor) values(1, '3', '100.00')
insert into dbo.tempo_cobranca_curso(idCurso, tempo, valor) values(1, '6', '500.00')

	IF OBJECT_ID('dbo.aluno_frequencia') is null
create table aluno_frequencia(Id int identity primary key, IdCurso int, IdModulo int, IdAula int, IdAluno int, DataCadastro datetime)

	IF OBJECT_ID('dbo.canal') is null
create table canal(Id int identity primary key, Titulo varchar(100), Descricao varchar(100), Link Varchar(100))
insert into dbo.canal(titulo, descricao, link) values('Criando Primeira Aplicação no Padrão MVC Utilizando ASP.Net','','https://www.youtube.com/watch?v=E0B1-sj5m7U&t=1474s')
insert into dbo.canal(titulo, descricao, link) values('Criando Aplicação Olá Mundo com ASP.Net MVC','','https://www.youtube.com/watch?v=rKiwq8P0fW0&t=625s')
insert into dbo.canal(titulo, descricao, link) values('Trabalhando com Classes no ASP.Net Utilizando C#','','https://www.youtube.com/watch?v=fSfBbTUSnco&t=2s')
insert into dbo.canal(titulo, descricao, link) values('Trabalhando com Requisições em API de Forma Acessível Utilizando o CURL','','https://www.youtube.com/watch?v=1wbnp4A8_CY&t=790s')
insert into dbo.canal(titulo, descricao, link) values('Instalando o Linux no Windows com Acessibilidade','','https://www.youtube.com/watch?v=qpJ5Jg-zUws&t=18s')
insert into dbo.canal(titulo, descricao, link) values('Instalando o Banco de Dados MySQL no Linux com Acessibilidade','','https://www.youtube.com/watch?v=nHH9Q2fZeUg&t=759s')
insert into dbo.canal(titulo, descricao, link) values('Restrição com Constraint no MySQL','','https://www.youtube.com/watch?v=qkSxwUHg1kc&t=577s')
insert into dbo.canal(titulo, descricao, link) values('Entenda Tudo Sobre Normalização em Banco de Dados, para que Você Nunca Mais Tenha Dúvida','','https://www.youtube.com/watch?v=d8EYwgneoOI&t=945s')
	IF OBJECT_ID('dbo.ConfiguracaoLead') is null
create table ConfiguracaoLead(Id int identity primary key, Titulo varchar(50) not null, Pagina varchar(50) not null, Descricao varchar(500) not null)
insert into configuracaolead(titulo, pagina, descricao) values('Ebook de MongoDB', 'mongodb', 'Este é um livro digital do banco de dados não relacional MongoDB')

	IF OBJECT_ID('dbo.CapturaLead') is null
create table CapturaLead(Id int identity primary key, email varchar(50) not null);

	IF OBJECT_ID('dbo.log') is null
create table log(Id int identity primary key, Erro varchar(max), Detalhe varchar(max))

	IF OBJECT_ID('dbo.AtividadeCurso') is null
create table AtividadeCurso(IdAtividade int identity primary key, IdCurso int not null, IdModulo int not null, IdModuloBloqueado int not null, Titulo varchar(150) not null, Descricao varchar(max))

	IF OBJECT_ID('dbo.UsuarioAtividadeCurso') is null
create table UsuarioAtividadeCurso(Id int identity primary key, IdUsuario int not null, IdModuloBloqueado int not null)

alter table UsuarioAtividadeCurso add Resposta varchar(max), ComentarioProfessor varchar(max), SnConcluida char(1), Nota int, DataEnvio date
alter table UsuarioAtividadeCurso add IdCurso int not null