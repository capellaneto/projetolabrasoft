using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models; // Garante que o C# ache sua classe

namespace WebApplication1
{
    public partial class CadastroBolsista : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Na primeira vez que a página carrega, podemos querer exibir a lista 
            if (!IsPostBack)
            {
                AtualizarGrid();
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) ||
            string.IsNullOrWhiteSpace(txtMatricula.Text) ||
            string.IsNullOrWhiteSpace(txtCPF.Text) ||
            string.IsNullOrWhiteSpace(txtDataNasc.Text) ||
            ddlSexo.SelectedIndex <= 0)
            {
                lblMensagem.Text = "⚠️ Por favor, preencha todos os campos corretamente antes de salvar.";
                lblMensagem.CssClass = "alert alert-warning d-block";
                return;
            }

            try
            {
                Bolsista novo = new Bolsista();

                novo.Nome = txtNome.Text;
                novo.Matricula = txtMatricula.Text;
                novo.CPF = txtCPF.Text;
                novo.Sexo = ddlSexo.SelectedValue;
                novo.DataNascimento = DateTime.Parse(txtDataNasc.Text);

                BolsistaService service = new BolsistaService();

                bool cadastrado = service.CadastrarBolsista(novo);

                if(!cadastrado)
                {
                    lblMensagem.Text = "Bolsista cadastrado com sucesso!";
                    lblMensagem.CssClass = "alert alert-success d-block";
                }

                LimparCampos();
                AtualizarGrid();
                return;
            }
            catch (Exception)
            {
                lblMensagem.Text = "Erro ao cadastrar. Verifique os dados.";
                lblMensagem.CssClass = "alert alert-danger d-block";
            }
        }
        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();

            lblMensagem.Text = "";
            lblMensagem.CssClass = "";
        }


        private void LimparCampos()
        {
            txtNome.Text = "";
            txtMatricula.Text = "";
            txtCPF.Text = "";
            txtDataNasc.Text = "";
            ddlSexo.SelectedIndex = 0;
            txtNome.Focus();
        }

        private void AtualizarGrid()
        {
            BolsistaService service = new BolsistaService();

            var listaBolsistas = service.ListarBolsistas();

            if (listaBolsistas.Count > 0)
            {
                gridBolsistas.DataSource = listaBolsistas;
                gridBolsistas.DataBind();

                lblAvisoGrid.Visible = false;
                gridBolsistas.Visible = true;

                // MOSTRAR OS FILTROS
                pnlFiltros.Visible = true;
            }
            else
            {
                lblAvisoGrid.Visible = true;
                gridBolsistas.Visible = false;

                // ESCONDER OS FILTROS
                pnlFiltros.Visible = false;
            }
        }


        protected void btnFiltrarHomens_Click(object sender, EventArgs e)
        {
            BolsistaService service = new BolsistaService();
            var resultado = service.ListarHomens();

            gridBolsistas.DataSource = resultado;
            gridBolsistas.DataBind();

            lblMensagem.Text = $"Exibindo {resultado.Count} Homens encontrados.";
            lblMensagem.CssClass = "alert alert-info d-block";
        }

        protected void btnFiltrarMulheres_Click(object sender, EventArgs e)
        {
            BolsistaService service = new BolsistaService();
            var resultado = service.ListarMulheres();

            gridBolsistas.DataSource = resultado;
            gridBolsistas.DataBind();

            lblMensagem.Text = $"Exibindo {resultado.Count} mulheres encontradas.";
            lblMensagem.CssClass = "alert alert-info d-block";
        }


        protected void btnOrdemAlfabetica_Click(object sender, EventArgs e)
        {
            BolsistaService service = new BolsistaService();
            var resultado = service.ListarOrdemAlfabetica();

            gridBolsistas.DataSource = resultado;
            gridBolsistas.DataBind();

            lblMensagem.Text = "Lista organizada por ordem alfabética.";
            lblMensagem.CssClass = "alert alert-secondary d-block";
        }


        protected void btnVerTodos_Click(object sender, EventArgs e)
        {
            AtualizarGrid();
            lblMensagem.Text = "Lista completa.";
            lblMensagem.CssClass = "alert alert-light d-block border";
        }
    }
}
