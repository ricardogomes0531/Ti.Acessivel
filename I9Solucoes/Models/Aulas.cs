using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace I9Solucoes.Models
{
	public class Aulas
	{
		public int Id { get; set; }
		public int IdCurso { get; set; }
		public int IdModulo { get; set; }
		public string Nome { get; set; }
		public string ConteudoAula { get; set; }
		public string CaminhoArquivo { get; set; }
public int Frequencia { get; set; }
	}
}