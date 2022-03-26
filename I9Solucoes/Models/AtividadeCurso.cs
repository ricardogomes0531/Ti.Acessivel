using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace I9Solucoes.Models
{
    public class AtividadeCurso
    {
        public int IdAtividade { get; set; }
        public int IdCurso { get; set; }
        public int IdModulo { get; set; }
        public int IdModuloBloqueado { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }


    }
}