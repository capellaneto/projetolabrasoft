using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1
{
    public class CoordenadorService
    {
        public bool CadastrarCoordenador(Coordenador coordenador)
        {
            if (CoordenadorRepositorio.ListaCoordenadores.Any(b => b.CPF == coordenador.CPF))
            {
                return false;
            }

            else
            {
                CoordenadorRepositorio.SalvarCoordenador(coordenador);
                return true;
            }
        }

        public Coordenador BuscarCoordenador(int id)
        {
            return CoordenadorRepositorio.ListarCoordenador().FirstOrDefault(b => b.Id == id);
        }

        public List<CoordenadorGridDTO> FiltrarNomeTitulacao(string busca)
        {
            var listaCoordenadores = CoordenadorRepositorio.ListarCoordenadorNaGrid();

            return listaCoordenadores.Where(c => c.Nome.ToLower().Contains(busca.ToLower()) || c.Titulacao.ToLower().Contains(busca.ToLower())).ToList();
        }

        public List<CoordenadorGridDTO> ListarCoordenadores()
        {
            return CoordenadorRepositorio.ListarCoordenadorNaGrid();
        }

        public void ExcluirCoordenador(int id)
        {
            CoordenadorRepositorio.ExcluirCoordenador(id);
        }

        public void AtualizarEmail(int id,  string NovoEmail)
        {
            CoordenadorRepositorio.AtualizarEmailCoordenador(id, NovoEmail);
        }
    }
}