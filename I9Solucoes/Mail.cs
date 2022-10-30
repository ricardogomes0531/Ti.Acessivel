using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using I9Solucoes.Repositorios;

namespace I9Solucoes
{
	public static class Mail
	{
		public static void Enviar(string para, string assunto, string mensagem)
		{
			try
			{
				MailMessage mail = new MailMessage();
				mail.From = new MailAddress(ConfigurationManager.AppSettings["MailRemetente"].ToString(), "Portal Visão de DEV", System.Text.Encoding.UTF8);
				mail.Priority = MailPriority.Normal;
				mail.Subject = assunto;
				mail.SubjectEncoding = System.Text.Encoding.UTF8;
				mail.Body = mensagem;
				mail.BodyEncoding = System.Text.Encoding.UTF8;
				mail.To.Add(new MailAddress(para));
				mail.IsBodyHtml = true;
				SmtpClient smtp = new SmtpClient();
				smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["MailRemetente"].ToString(), ConfigurationManager.AppSettings["MailSenha"].ToString());
				//smtp.UseDefaultCredentials = false;
				smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["MailPorta"].ToString());
				smtp.Host = ConfigurationManager.AppSettings["MailSmtp"].ToString();
				smtp.EnableSsl = true;
						smtp.Send(mail);
			}
			catch (Exception ex)
			{
				new LogRepository().Inserir("Erro ao enviar email", ex.Message);
			}
		}
	}
}
