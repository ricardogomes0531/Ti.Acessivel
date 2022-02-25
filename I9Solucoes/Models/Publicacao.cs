using System;

namespace I9Solucoes.Models
{
    public class Publicacao
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public DateTime DataCadastro { get; set; }

    }
}