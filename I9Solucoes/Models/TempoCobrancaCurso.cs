using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace I9Solucoes.Models
{
	public class TempoCobrancaCurso
	{
		public int Id { get; set; }

		public int IdCurso { get; set; }
		public string Tempo { get; set; }
		public decimal Valor { get; set; }
	}
}