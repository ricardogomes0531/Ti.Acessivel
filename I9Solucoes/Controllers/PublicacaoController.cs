using I9Solucoes.Models;
using I9Solucoes.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace I9Solucoes.Controllers
{
    public class PublicacaoController : Controller
    {
        // GET: Publicacao
        public ActionResult PegarPublicacao()
        {

            return View();
        }

        [HttpGet]
        public ActionResult Novo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar()
        {
            Erro erro = new Erro();
            try
            {
                var titulo = Request.Form["titulo"].ToString();
                var texto = Request.Form["texto"].ToString();
                var situacao = new PublicacaoRepository().Salvar(titulo, texto);
                if (situacao)
                {
                    erro.ExisteErro = false;
                    erro.Mensagem = "Publicação cadastrada com sucesso!";
                    erro.Detalhe = null;
                }
                else
                {
                    erro.ExisteErro = true;
                    erro.Mensagem = "Erro ao salvar a publicação.";
                    erro.Detalhe = null;
                }
            }
            catch(Exception ex)
            {
                erro.ExisteErro = true;
                erro.Detalhe = ex.Message;
                erro.Mensagem = "Ouve um erro ao salvar a publicação.";
            }

            return Json(erro, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EnviarPublicacao()
        {
            var publicacao = new PublicacaoRepository().PegarUltimaPublicacao();
            var pessoas = new PublicacaoRepository().PegarUsuariosSemPublicacao(publicacao.Id);
            if (pessoas.Count() > 0)
            {
                foreach (var p in pessoas)
                {
                    Mail.Enviar(p.Email, publicacao.Titulo, publicacao.Texto
                                                .Replace("[htmlInicio]", "<html><head><title>Ti Acessível</title></head><body>")
                        .Replace("[htmlFim]", "</body></html>")
                        .Replace("[br]", "<br>")
                        .Replace("[linkTiAcessivel]", "<a href='http://www.tiacessivel.com.br'>Clique aqui para ir para o Ti Acessível</a>")
                        .Replace("[linkWhatsapp]", "<a href='https://api.whatsapp.com/send?phone=5581995081051'>Fale comigo clicando aqui</a>")
                        );
                    new PublicacaoRepository().AtualizarStatusEnvioEmail(publicacao.Id, p.Id);
                }
            }
            return Json(publicacao, JsonRequestBehavior.AllowGet);
        }
    }
}