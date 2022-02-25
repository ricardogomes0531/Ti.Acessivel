using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace I9Solucoes.Models
{
	public class Erro
	{
		public string Mensagem { get; set; }
		public string Detalhe { get; set; }
		public bool ExisteErro { get; set; }
	}
}