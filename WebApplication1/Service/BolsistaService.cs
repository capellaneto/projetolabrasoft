using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1
{
    public class BolsistaService
    {
        public bool CadastrarBolsista(Bolsista bolsista)
        {
            if (BolsistaRepositorio.ListaBolsistas.Any(b => b.CPF == bolsista.CPF))
            {
                return false;
            }

            else
            {
                BolsistaRepositorio.SalvarBolsista(bolsista);
                return true;
            }
        }

        public Bolsista BuscarBolsista(int id)
        {
            return BolsistaRepositorio.ListarBolsistas().FirstOrDefault(b => b.Id == id);
        }

        public List<BolsistaGridDTO> ListarBolsistas()
        {
            return BolsistaRepositorio.ListarBolsistasNaGrid();
        }

        public List<BolsistaGridDTO> ListarHomens()
        {
            var listaBolsistas = BolsistaRepositorio.ListarBolsistasNaGrid();

            return listaBolsistas.Where(x => x.Sexo == "M").ToList();
        }

        public List<BolsistaGridDTO> ListarMulheres()
        {
            var listaBolsistas = BolsistaRepositorio.ListarBolsistasNaGrid();

            return listaBolsistas.Where(x => x.Sexo == "F").ToList();
        }

        public List<BolsistaGridDTO> ListarOrdemAlfabetica()
        {
            var listaBolsistas = BolsistaRepositorio.ListarBolsistasNaGrid();

            return listaBolsistas.OrderBy(x => x.Nome).ToList();
        }
    }
}