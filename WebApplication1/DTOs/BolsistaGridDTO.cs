using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class BolsistaGridDTO
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Sexo { get; set; }

        public string Matricula { get; set; }

        public DateTime DataNascimento { get; set; }
    }
}