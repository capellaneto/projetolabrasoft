using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class CadastroCoordenador : System.Web.UI.Page
    {
        // Lista estática para manter os dados em memória durante a execução
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AtualizarGrid();
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) ||
            string.IsNullOrWhiteSpace(txtCPF.Text) ||
            string.IsNullOrWhiteSpace(txtArea.Text) ||
            string.IsNullOrWhiteSpace(txtEmail.Text) ||
            ddlTitulacao.SelectedIndex <= 0)
            {
                lblMensagem.Text = "⚠️ Por favor, preencha todos os campos corretamente antes de salvar.";
                lblMensagem.CssClass = "alert alert-warning d-block";
                return;
            }

            try
            {
                // Criando o objeto usando o novo Model
                Coordenador novo = new Coordenador();
                novo.Nome = txtNome.Text;
                novo.CPF = txtCPF.Text;
                novo.Titulacao = ddlTitulacao.SelectedValue;
                novo.AreaAtuacao = txtArea.Text;
                novo.Email = txtEmail.Text;

                CoordenadorService service = new CoordenadorService();

                bool cadastrado = service.CadastrarCoordenador(novo);

                if (!cadastrado)
                {
                    lblMensagem.Text = "Coordenador cadastrado com sucesso!";
                    lblMensagem.CssClass = "alert alert-success d-block";
                }

                LimparCampos();
                AtualizarGrid();
            }
            catch (Exception)
            {
                lblMensagem.Text = "Erro ao salvar coordenador.";
                lblMensagem.CssClass = "text-danger";
            }
        }

        protected void btnFiltrarNomeTitulacao_Click(object sender, EventArgs e)
        {
            var busca = txtFiltro.Text.Trim();

            CoordenadorService service = new CoordenadorService();

            var coordenadores = service.FiltrarNomeTitulacao(busca);

            if (coordenadores.Count > 0)
            {
                // Tem resultado: mostra o grid e esconde o aviso
                gridCoordenadores.DataSource = coordenadores;
                gridCoordenadores.DataBind();

                gridCoordenadores.Visible = true;
                lblAviso.Visible = false;
            }
            else
            {
                // Não encontrou nada: limpa o grid e mostra o aviso
                gridCoordenadores.DataSource = null;
                gridCoordenadores.DataBind();

                gridCoordenadores.Visible = false; // Opcional: esconde o cabeçalho do grid
                lblAviso.Text = $"Nenhum coordenador encontrado para: '{txtFiltro.Text}'";
                lblAviso.Visible = true;
            }
        }

        private void AtualizarGrid()
        {
            CoordenadorService service = new CoordenadorService();

            var listaCoordenadores = service.ListarCoordenadores();

            if (listaCoordenadores.Count > 0)
            {
                gridCoordenadores.DataSource = listaCoordenadores;
                gridCoordenadores.DataBind();
                lblAviso.Visible = false;
            }
            else
            {
                lblAviso.Visible = true;
            }
        }



        protected void gridCoordenadores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow linha = (GridViewRow)((Control)e.CommandSource).NamingContainer;

            Panel painel = (Panel)linha.FindControl("pnlAtualizarEmail");

            CoordenadorService service = new CoordenadorService();

            if (e.CommandName == "Excluir Coordenador")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                try
                {
                    service.ExcluirCoordenador(id);

                    lblMensagem.Text = "Coordenador excluido com sucesso!";
                    lblMensagem.Visible = true;
                    AtualizarGrid();
                }

                catch (Exception ex)
                {
                    lblMensagem.Text = "Não foi possivel excluir este coordenador, erro: " +ex;
                    lblMensagem.Visible = true;
                }
            }
            
            else if (e.CommandName == "Atualizar")
            {
                painel.Visible = true;

                Button btnAtualizar = (Button)linha.FindControl("btnAtualizar");
                btnAtualizar.Visible = false;
            }

            else if (e.CommandName == "Cancelar")
            {
                painel.Visible = false;
                
                Button btnAtualizar = (Button)linha.FindControl("btnAtualizar");
                btnAtualizar.Visible = true;
            }

            else if (e.CommandName == "Confirmar")
            {
                TextBox txtNovoEmail = (TextBox)linha.FindControl("txtNovoEmail");

                string NovoEmail = txtNovoEmail.Text.Trim();

                if (string.IsNullOrWhiteSpace(NovoEmail))
                {
                    lblMensagem.Text = "⚠️ Digite um novo e-mail.";
                    lblMensagem.CssClass = "text-danger";
                    return;
                }

                int id = Convert.ToInt32(e.CommandArgument);

                try
                {
                    service.AtualizarEmail(id, NovoEmail);

                    lblMensagem.Text = "E-mail atualizado com sucesso!";
                    lblMensagem.CssClass = "text-success";

                    AtualizarGrid();
                }
                catch (Exception)
                {
                    lblMensagem.Text = "Erro ao atualizar o e-mail.";
                    lblMensagem.CssClass = "text-danger";
                }
            }
        }

        private void LimparCampos()
        {
            txtNome.Text = "";
            txtCPF.Text = "";
            txtArea.Text = "";
            txtEmail.Text = "";
            ddlTitulacao.SelectedIndex = 0;
            txtNome.Focus();
        }        
    }
}