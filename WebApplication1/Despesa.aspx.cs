using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class Despesa1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarProjetos();
            }
        }

        private void CarregarProjetos()
        {
            ddlProjeto.DataSource = Repositorio.ListarProjeto();

            ddlProjeto.DataTextField = "Titulo";
           
            ddlProjeto.DataValueField = "Id";

            ddlProjeto.DataBind();

            ddlProjeto.Items.Insert(0, new ListItem("Selecione um projeto", ""));
        }


        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescricao.Text) ||
            string.IsNullOrWhiteSpace(txtValor.Text) || 
            string.IsNullOrWhiteSpace(txtData.Text) || 
            string.IsNullOrWhiteSpace(txtCategoria.Text))
            {
                lblMensagem.Text = "⚠️ Por favor, preencha todos os campos corretamente antes de salvar.";
                lblMensagem.CssClass = "alert alert-warning d-block";
                return;
            }

            try
            {
                Despesa novo = new Despesa();
                novo.Descricao = txtDescricao.Text;
                novo.Valor = decimal.Parse(txtValor.Text);
                novo.Categoria = txtCategoria.Text;
                novo.Data = DateTime.Parse(txtData.Text);

                lblMensagem.Text = "Despesa cadastrada com sucesso.";
                lblMensagem.CssClass = "alert alert-success d-block";
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
        }


        private void LimparCampos()
        {
            txtDescricao.Text = "";
            txtValor.Text = "";
            txtCategoria.Text = "";
            txtData.Text = "";
            ddlProjeto.SelectedIndex = 0;
        }
    }
}