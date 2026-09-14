using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1
{
    public class ProjetoService
    {
        public bool CadastrarProjeto(Projeto projeto)
        {
            if (ProjetoRepositorio.ListarProjeto().Any(b => b.Titulo == projeto.Titulo))
            {
                return false;
            }

            if(ProjetoRepositorio.ListarProjeto().Any(b => b.IdCoordenador == projeto.IdCoordenador))
            {
                return false;
            }


            int idProjeto = ProjetoRepositorio.SalvarProjeto(projeto);

            foreach (Bolsista bolsista in projeto.ListaBolsistasProjeto)
            {
                ProjetoRepositorio.SalvarVinculoBolsistaProjeto(idProjeto, bolsista.Id);
            }

            return true;
        }

        public List<Projeto> ListarProjetosCompletos()
        {
            return ProjetoRepositorio.ListarProjeto();
        }

        public List<ProjetoGridDTO> ListarProjeto()
        {
            return ProjetoRepositorio.ListarProjetoNaGrid();
        }

        public Projeto BuscarProjeto(int IDProjeto)
        {
            return ProjetoRepositorio.ListarProjeto().FirstOrDefault(b => b.Id == IDProjeto);
        }
    }
}