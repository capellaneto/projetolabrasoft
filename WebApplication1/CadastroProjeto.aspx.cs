using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class CadastroProjeto : System.Web.UI.Page
    {
        private void AtualizarGrid()
        {
            var listaProjetos = Repositorio.ListaProjetos;
            if (listaProjetos.Count > 0)
            {
                gridProjetos.DataSource = listaProjetos;
                gridProjetos.DataBind();
                lblAviso.Visible = false;
            }
            else
            {
                lblAviso.Visible = true;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                AtualizarGrid();
                var ListaCoordenadores = Repositorio.ListaCoordenadores;
                    
                Coordenadores.DataSource = ListaCoordenadores;
                Coordenadores.DataTextField = "Nome";
                Coordenadores.DataValueField = "CPF";
                Coordenadores.DataBind();

                Coordenadores.Items.Insert(0, new ListItem("Selecione", ""));

                var ListaBolsistas = Repositorio.ListaBolsistas;

                Bolsistas.DataSource = ListaBolsistas;
                Bolsistas.DataTextField = "Nome";
                Bolsistas.DataValueField = "CPF";
                Bolsistas.DataBind();

                Bolsistas.Items.Insert(0, new ListItem("Selecione", ""));
            }
        }
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitulo.Text) ||
            string.IsNullOrWhiteSpace(txtVerba.Text) ||
            string.IsNullOrWhiteSpace(txtArea.Text) ||
            Coordenadores.SelectedIndex <= 0)
            {
                lblMensagem.Text = "⚠️ Por favor, preencha todos os campos corretamente antes de salvar.";
                lblMensagem.CssClass = "alert alert-warning d-block";
                return;
            }

            if (Repositorio.ListaProjetos.Any(b => b.Titulo == txtTitulo.Text))
            {
                lblMensagem.Text = "⚠️ Este Projeto já foi cadastrado!";
                lblMensagem.CssClass = "alert alert-warning d-block";
                LimparCampos();
                AtualizarGrid();
                return; // Para a execução aqui
            }

            try
            {
                // Criando o objeto usando o novo Model
                Projeto novo = new Projeto();
                novo.Titulo = txtTitulo.Text;
                novo.Verba = float.Parse(txtVerba.Text);
                novo.Area = txtArea.Text;
                novo.Valor_Bolsa = float.Parse(txtValorBolsa.Text);

                // 2. ADICIONAR NA LISTA ESTÁTICA
                Repositorio.ListaProjetos.Add(novo);

                LimparCampos();
                lblMensagem.Text = "Coordenador salvo com sucesso!";
                lblMensagem.CssClass = "text-success";

                AtualizarGrid();
            }
            catch (Exception)
            {
                lblMensagem.Text = "Erro ao salvar coordenador.";
                lblMensagem.CssClass = "text-danger";
            }
        }

            private void LimparCampos()
        {
            txtTitulo.Text = "";
            txtVerba.Text = "";
            txtArea.Text = "";
            Coordenadores.SelectedIndex = 0;
            Bolsistas.SelectedIndex = 0;
            txtTitulo.Focus();
        }
    }
}