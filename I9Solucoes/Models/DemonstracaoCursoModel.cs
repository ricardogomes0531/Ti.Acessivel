using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace I9Solucoes.Models
{
    public class DemonstracaoCursoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int IdCurso { get; set; }
        public string Codigo { get; set; }
        public int QuantidadeDias { get; set; }
        public string Ativo { get; set; }

    }
}