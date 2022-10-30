using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace I9Solucoes.Models
{
    public class DetalheAtividade
    {
        public int Id { get; set; }
        public int IdAtividade { get; set; }
        public string Usuario { get; set; }
        public string EmailUsuario { get; set; }
        public string Descricao { get; set; }
        public string Resposta { get; set; }
        public string Concluida { get; set; }
    }
}