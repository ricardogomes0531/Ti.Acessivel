using System;
using System.Collections.Generic;
using System.Linq;

namespace I9Solucoes.Models
{
	public class Curso
	{
		public int Id { get; set; }
		public string Nome { get; set; }
		public DateTime DataCadastro { get; set; }
		public int TempoPrevistoDuracao { get; set; }
		public string Descricao { get; set; }
		public string Explicacao { get; set; }
		public bool Ativo { get; set; }
		public bool AceitaMatricula { get; set; }
		public DateTime DataInicio { get; set; }
		public decimal ValorMonetario { get; set; }
	}
}