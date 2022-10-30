using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace I9Solucoes.Models
{
    public class UsuarioAtividade
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdModuloBloqueado { get; set; }
public string Resposta { get; set; }
        public string ComentarioProfessor { get; set; }
        public int? Nota { get; set; }
        public DateTime DataEnvio { get; set; }
        public string SnConcluida { get; set; }

        
    }
}