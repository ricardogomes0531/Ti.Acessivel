using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace I9Solucoes.Models
{
	public class Fornecedor
	{
		public int Id { get; set; }
		public string Nome { get; set; }
		public DateTime DataCadastro { get; set; }
		public DateTime? DataAlteracao { get; set; }
		public string UsuarioCadastro { get; set; }
		public string UsuarioAlteracao { get; set; }
		public string Endereco { get; set; }
		public string Cep { get; set; }
				public string Uf { get; set; }
		public string Cidade { get; set; }
		public string Numero { get; set; }
		public string Complemento { get; set; }
	}
}
