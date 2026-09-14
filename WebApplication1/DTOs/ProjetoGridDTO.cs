using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1
{
    public class ProjetoGridDTO
    {
        public int Id { get; set; } //Data no sql e DTO do Projeto

        public string Titulo { get; set; }

       public string Coordenador { get; set; }

        public decimal Verba { get; set; }
    }
}