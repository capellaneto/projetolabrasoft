using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Models;
using WebApplication1.Service;

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

            ddlProjetos.DataSource = Repositorio.ListarProjeto();

            ddlProjetos.DataTextField = "Titulo";

            ddlProjetos.DataValueField = "Id";

            ddlProjetos.DataBind();

            ddlProjetos.Items.Insert(0, new ListItem("Selecione um projeto", ""));
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlCategoria.SelectedValue == "Outro")
            {
                txtOutraCategoria.Visible = true;
            }
            else
            {
                txtOutraCategoria.Visible = false;
            }
        }

        protected async void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescricao.Text) ||
            string.IsNullOrWhiteSpace(txtValor.Text) ||
            string.IsNullOrWhiteSpace(txtData.Text) ||  
            ddlCategoria.SelectedValue == "" ||
                ddlProjetos.SelectedIndex <= 0)
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
                novo.Data = DateTime.Parse(txtData.Text);
                novo.ProjetoID = Convert.ToInt32(ddlProjetos.SelectedValue);
                if (ddlCategoria.SelectedValue == "Outro")
                {
                    novo.Categoria = txtOutraCategoria.Text;
                }
                else
                {
                    novo.Categoria = ddlCategoria.SelectedValue;
                }

                Repositorio.SalvarDespesa(novo);

                var projetos = Repositorio.ListarProjeto();

                var projetoSelecionado = projetos
                    .FirstOrDefault(p => p.Id == novo.ProjetoID);


                if (projetoSelecionado == null)
                {
                    lblMensagem.Text = "Projeto não encontrado.";
                    lblMensagem.CssClass = "alert alert-danger d-block";
                    return;
                }

                var coordenador = Repositorio.ListarCoordenador()
                    .FirstOrDefault(c => c.Id == projetoSelecionado.IdCoordenador);


                if (coordenador == null)
                {
                    lblMensagem.Text = "Coordenador do projeto não encontrado.";
                    lblMensagem.CssClass = "alert alert-danger d-block";
                    return;
                }



                    EmailService emailService = new EmailService();

                    bool emailEnviado = await emailService.EnviarNotificacaoDespesa(
                        "labrasoft.ifba@gmail.com",
                        coordenador.Nome,
                        novo.Descricao,
                        novo.Valor,
                        novo.Data,
                        coordenador.Nome
                    );

                    if (emailEnviado)
                    {
                        lblMensagem.Text = "Despesa cadastrada e e-mail enviado com sucesso.";
                        lblMensagem.CssClass = "alert alert-success d-block";
                    }
                    else
                    {
                        lblMensagem.Text = "Despesa cadastrada, mas o e-mail não pôde ser enviado.";
                        lblMensagem.CssClass = "alert alert-warning d-block";
                    }


                

                LimparCampos();
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
            ddlCategoria.SelectedValue = "";
            txtData.Text = "";
            txtOutraCategoria.Text = "";
            txtOutraCategoria.Visible = false;

            if (ddlProjetos.Items.Count > 0)
                ddlProjetos.SelectedIndex = 0;
        }
    }
}