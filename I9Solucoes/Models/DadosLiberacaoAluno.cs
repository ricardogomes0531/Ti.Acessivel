using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace I9Solucoes.Models
{
    public class DadosLiberacaoAluno
    {
        public string Nome { get; set; }
                public string NomeCurso { get; set; }
        public int IdCurso { get; set; }
        public DateTime? DataCadastro { get; set; }
        public int Id { get; set; }
        public int IdAssinatura { get; set; }
    }
}