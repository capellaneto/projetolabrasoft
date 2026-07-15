using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1
{
    public class Projeto
    {
        public string Titulo { get; set; }

        public float Verba { get; set; }

        public Coordenador coordenador { get; set; }

        public static List<Bolsista> ListaBolsistas = new List<Bolsista>();

        public float Valor_Bolsa {  get; set; }

        public string Area {  get; set; }
    }


}