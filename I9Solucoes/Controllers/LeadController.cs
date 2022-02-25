using I9Solucoes.Models;
using I9Solucoes.Repositorios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace I9Solucoes.Controllers
{
    public class LeadController : Controller
    {
        // GET: LeadController
        public ActionResult Index(string pagina)
        {
            Lead lead = new LeadRepository().PegarLead(pagina);
            ViewBag.titulo = lead.Titulo;
            ViewBag.pagina = lead.Pagina;
            ViewBag.descricao = lead.Descricao;
                        return View();
        }

        [HttpPost]
        public ActionResult SalvarEEnviar()
        {
            LeadRepository captura = new LeadRepository();
            bool resultado = false;
            Erro erro = new Erro();
            try
            {
                resultado = captura.Inserir(Request.Form["email"].ToString());
                if (resultado)
                {
                    erro.Mensagem = "O e-mail com o conteúdo foi enviado para você. Não esqueça de verificar na pasta de spam caso não o encontre na caixa de entrada.";
                    erro.Detalhe = null;
                    erro.ExisteErro = false;
                    /*
                                            Mail.Enviar(Request.Form["email"].ToString(), "TI Acessível - Envio do seu Presente", ConfigurationManager.AppSettings["MailMensagemBemVindo"].ToString());
                                            Mail.Enviar(ConfigurationManager.AppSettings["MailEmailAdministrador"].ToString(), "Cadastro de Novo Aluno no Site Visão de DEV", "Um novo aluno(a) se cadastrou no site Visão de DEV. O e-mail é " + Request.Form["email"].ToString() + " o nome é " + Request.Form["nome"].ToString() + " o celular cadastrado é: " + Request.Form["celular"].ToString() + ", é whatsapp: " + Request.Form["whatsapp"].ToString());
                    */
                }
                else
                {
                    erro.Mensagem = "Erro ao realizar o cadastro e envio do e-mail.";
                    erro.Detalhe = null;
                    erro.ExisteErro = true;
                    Mail.Enviar(ConfigurationManager.AppSettings["MailEmailAdministrador"].ToString(), "Erro no Cadastro de Novo Aluno no Site Visão de DEV", "Um novo aluno(a) obteve um erro ao se cadastrar no site Visão de DEV. O e-mail é " + Request.Form["email"].ToString() + " o nome é " + Request.Form["nome"].ToString() + " o celular cadastrado é: " + Request.Form["celular"].ToString() + ", é whatsapp: " + Request.Form["whatsapp"].ToString() + " a data de nascimento é: " + Request.Form["dataNascimento"].ToString());
                            }
                }
                        catch (Exception ex)
            {
                erro.Mensagem = "Erro ao realizar cadastro e envio do e-mail de lead" + ex.Message;
                erro.Detalhe = ex.Message;
                erro.ExisteErro = true;
                    Mail.Enviar(ConfigurationManager.AppSettings["MailEmailAdministrador"].ToString(),"Erro no Cadastro de Novo Aluno no Site Visão de DEV",erro.Detalhe);
            }
                           return Json(erro, JsonRequestBehavior.AllowGet);
}
    }
}