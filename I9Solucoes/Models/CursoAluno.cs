using System;

namespace I9Solucoes.Models
{
	public class CursoAluno
	{
		public int IdCurso { get; set; }
		public string NomeCurso { get; set; }
		public DateTime DataCadastro { get; set; }
				public string Liberado { get; set; }
		public DateTime DataInicio { get; set; }
		public DateTime DataFim { get; set; }
		public string LinkPagamento { get; set; }
		public decimal Valor { get; set; }

	}
}