using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WebApplication1.Models;

namespace WebApplication1
{
    public class Projeto
    {
        public int Id { get; set; }

        public int IdCoordenador { get; set; }
        public string Titulo { get; set; }

        public decimal Verba { get; set; }

        public Coordenador coordenador { get; set; }
        
        public List<Bolsista> ListaBolsistasProjeto { get; set; } = new List<Bolsista>(); 
        //se a mesma lista for mostrada para todos os projetos, é por conta do static

        public decimal Valor_Bolsa {  get; set; }

        public string Area {  get; set; }
    }


}