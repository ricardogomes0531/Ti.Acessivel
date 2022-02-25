using I9Solucoes.Models;
using I9Solucoes.Repositorios;
using System;
using System.Configuration;
using System.Web.Mvc;

namespace I9Solucoes.Controllers
{
    public class CadastroController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Salvar()
        {
            CadastroRepository cadastro = new CadastroRepository();
            bool resultado = false;
            int idAluno = 0;
            Erro erro = new Erro();
            try
            {
                idAluno = new UsuarioRepository().PesquisarIdDoAlunoPeloEmail(Request.Form["email"].ToString());
                if (idAluno > 0)
                {
                    erro.Mensagem = "O e-mail informado já está em uso no sistema.";
                    new LogRepository().Inserir(erro.Mensagem, Request.Form["email"].ToString());
                    erro.Detalhe = null;
                    erro.ExisteErro = true;
                }
                else
                {
                    resultado = cadastro.Inserir(Request.Form["nome"].ToString(), Convert.ToDateTime(Request.Form["dataNascimento"].ToString()), Request.Form["cpf"].ToString(), Request.Form["sexo"].ToString(), Request.Form["email"].ToString(), Request.Form["celular"].ToString(), Request.Form["whatsapp"].ToString(), Request.Form["senha"].ToString());
                    if (resultado)
                    {
                        erro.Mensagem = "Cadastro realizado com sucesso";
                        erro.Detalhe = null;
                        erro.ExisteErro = false;
                                                                        Mail.Enviar(Request.Form["email"].ToString(), "Cadastro no Portal Visão de DEV", ConfigurationManager.AppSettings["MailMensagemBemVindo"].ToString());
                                                Mail.Enviar(ConfigurationManager.AppSettings["MailEmailAdministrador"].ToString(), "Cadastro de Novo Aluno no Site Visão de DEV", "Um novo aluno(a) se cadastrou no site Visão de DEV. O e-mail é " + Request.Form["email"].ToString() + " o nome é " + Request.Form["nome"].ToString() + " o celular cadastrado é: " + Request.Form["celular"].ToString() + ", é whatsapp: " + Request.Form["whatsapp"].ToString());
                   }
                    else
                    {
                        erro.Mensagem = "Erro ao realizar o cadastro.";
                        erro.Detalhe = null;
                        erro.ExisteErro = true;
                        new LogRepository().Inserir("Erro no Cadastro de Novo Aluno no Site", "Um novo aluno(a) obteve um erro ao se cadastrar no site Visão de DEV. O e-mail é " + Request.Form["email"].ToString() + " o nome é " + Request.Form["nome"].ToString() + " o celular cadastrado é: " + Request.Form["celular"].ToString() + ", é whatsapp: " + Request.Form["whatsapp"].ToString() + " a data de nascimento é: " + Request.Form["dataNascimento"].ToString());
                        Mail.Enviar(ConfigurationManager.AppSettings["MailEmailAdministrador"].ToString(), "Erro no Cadastro de Novo Aluno no Site Visão de DEV", "Um novo aluno(a) obteve um erro ao se cadastrar no site Visão de DEV. O e-mail é " + Request.Form["email"].ToString() + " o nome é " + Request.Form["nome"].ToString() + " o celular cadastrado é: " + Request.Form["celular"].ToString() + ", é whatsapp: " + Request.Form["whatsapp"].ToString() + " a data de nascimento é: " + Request.Form["dataNascimento"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                erro.Mensagem = "Erro ao realizar cadastro " + ex.Message;
                erro.Detalhe = ex.Message;
                erro.ExisteErro = true;
                                Mail.Enviar(ConfigurationManager.AppSettings["MailEmailAdministrador"].ToString(), "Erro no Cadastro de Novo Aluno no Site Visão de DEV", erro.Detalhe);
                new LogRepository().Inserir(ex.Message, ex.InnerException.Message);
            }
            return Json(erro, JsonRequestBehavior.AllowGet);
        }
    }
}