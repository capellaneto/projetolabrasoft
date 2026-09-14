using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication1.Models;
using WebApplication1.Service;

namespace WebApplication1
{
    public class DespesaService
    {
        public async Task<bool> CadastrarDespesa(Despesa despesa)
        {
            DespesaRepositorio.SalvarDespesa(despesa);

            var projetos = ProjetoRepositorio.ListarProjeto();

            var projetoSelecionado = projetos
                .FirstOrDefault(p => p.Id == despesa.ProjetoID);

            if (projetoSelecionado == null)
            {
               throw new Exception ("Projeto não encontrado!");
            }

            var coordenador = CoordenadorRepositorio.ListarCoordenador()
                    .FirstOrDefault(c => c.Id == projetoSelecionado.IdCoordenador);


            if (coordenador == null)
            {
                throw new Exception("Coordenador do projeto não encontrado.");
            }

            EmailService emailService = new EmailService();

            bool emailEnviado = await emailService.EnviarNotificacaoDespesa(
                "labrasoft.ifba@gmail.com",
                coordenador.Nome,
                despesa.Descricao,
                despesa.Valor,
                despesa.Data,
                coordenador.Nome
            );

            return emailEnviado;
        }

        public List<Despesa> ListarDespesas()
        {
            return DespesaRepositorio.ListarDespesas();
        }

        public Despesa BuscarDespesa(int IdDespesa)
        {
            return DespesaRepositorio.ListarDespesas().FirstOrDefault(b => b.ID == IdDespesa);
        }

        public List<Despesa> ListarDespesasPorProjeto(int projetoID)
        {
            return DespesaRepositorio.ListarDespesas().Where(d => d.ProjetoID == projetoID).ToList();
        }
    }
}