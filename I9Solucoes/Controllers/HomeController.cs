using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using I9Solucoes.Repositorios;
using I9Solucoes.Models;
using I9Solucoes.Filtro;
using System.Configuration;

namespace I9Solucoes.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
						ViewBag.totalVisitantesOnline = System.Web.HttpContext.Current.Application["totalVisitantesOnline"];
						return View();
		}

		[HttpPost]
		public ActionResult Logar(string email, string senha)
		{
            var resultado = new UsuarioRepository().Autenticar(email, senha);
            try
            {
                                if (resultado)
                {
                    HttpCookie cookieLogin = new HttpCookie("login");
                    //cookieLogin.Secure = true;
                    cookieLogin.Value = email;
                    Response.Cookies.Add(cookieLogin);
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            			return Json(resultado,JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public ActionResult Cursos()
		{
			 List<Curso> cursos = new List<Curso>();
			try
			{
				cursos = new CursoRepository().Listar();
							}
			catch (Exception ex)
			{
				return Json(ex.Message, JsonRequestBehavior.AllowGet);
			}
			return Json(cursos,JsonRequestBehavior.AllowGet);
		}

		[PermissoesFilters]
		public ActionResult MeusCursos()
		{
			List<CursoAluno> cursos = new List<CursoAluno>();
			HttpCookie cookieLogin = Request.Cookies["login"];
					cursos = new CursoRepository().ListarCursosDoAluno(cookieLogin.Value.ToString());
			ViewBag.envioComprovante = ConfigurationManager.AppSettings["NumeroWhatsappEnvioComprovantePagamento"].ToString();
			return View(cursos);
		}

		public ActionResult Login()
		{
			return View();
		}

		public ActionResult DadosLogin()
		{
			HttpCookie cookieLogin = Request.Cookies["login"];
			PerfilUsuario usuario = new UsuarioRepository().PegarDadosDoUsuario(cookieLogin.Value.ToString());
			return View(usuario);
		}

		public ActionResult Sair()
		{
			HttpCookie cookieLogin = new HttpCookie("login");
			//cookieLogin.Secure = true;
			cookieLogin.Value = string.Empty;
			Response.Cookies.Add(cookieLogin);
return RedirectToAction("Index");
		}

		public ActionResult RedefinirSenha()
		{
			return View();
		}

		[HttpPost]
		public ActionResult RecuperarSenha()
		{
			var email = Request.Form["email"].ToString();
			string senha = new UsuarioRepository().PesquisarSenhaDoAlunoPeloEmail(email);
			if (!string.IsNullOrEmpty(senha))
			{
				var mensagem = $"Prezado(a) aluno(a), você solicitou sua senha ao portal. Seu e-mail foi devidamente localizado, por isso estamos disponibilizando para você sua senha de acesso. A senha é: {senha}";
				Mail.Enviar(email, "Recuperação de Senha", mensagem);
			}
			return View();
		}

		public ActionResult Sobre()
		{
			return View();
		}

		public ActionResult ListarVideos()
		{
			List<CanalYoutube> canal = new CanalYoutubeRepository().ListarVideos();
			return View(canal);
		}

		public ActionResult NovoAluno()
        {
			var alunos = new UsuarioRepository().GetUsuarioSemCurso();
			if (alunos.Count > 0)
			{
				foreach (var aluno in alunos)
				{
					var textoEmail = ConfigurationManager.AppSettings["alunoNovo"].ToString().Replace("[name]", aluno.Nome)
						.Replace("[htmlInicio]", "<html><head><title>Ti Acessível</title></head><body>")
						.Replace("[htmlFim]", "</body></html>")
						.Replace("[br]", "<br>")
						.Replace("[linkTiAcessivel]", "<a href='http://www.tiacessivel.com.br'>Clique aqui para ir para o Ti Acessível</a>")
						.Replace("[linkWhatsapp]", "<a href='https://api.whatsapp.com/send?phone=5581995081051'>Fale comigo clicando aqui</a>");

					Mail.Enviar(aluno.Email, "Bem-vindo ao TiAcessivel", textoEmail);
					new UsuarioRepository().AlterarStatusParaEnviado(aluno.Id);
				}
			}

			return Json(alunos, JsonRequestBehavior.AllowGet);
        }
	}
}