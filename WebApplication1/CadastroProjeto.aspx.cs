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
                //caso der erro passar codigo abaixo para atualizar grid
                var ListaCoordenadores = Repositorio.ListaCoordenadores;
                    
                Coordenadores.DataSource = ListaCoordenadores;
                Coordenadores.DataTextField = "Nome";
                Coordenadores.DataValueField = "CPF";
                Coordenadores.DataBind();

                Coordenadores.Items.Insert(0, new ListItem("Selecione", ""));

                var ListaBolsistas = Repositorio.ListaBolsistas;

                ListaTodosBolsistas.DataSource = ListaBolsistas;
                ListaTodosBolsistas.DataTextField = "Nome";
                ListaTodosBolsistas.DataValueField = "CPF";
                ListaTodosBolsistas.DataBind();

                ListaTodosBolsistas.Items.Insert(0, new ListItem("Selecione", ""));
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

           //if(Repositorio) terminar if que bloqueia o cadastro de coordenadores e bolsistas ja cadastrados em outros projetos
            
            if (Repositorio.ListaProjetos.Any(b => b.Titulo == txtTitulo.Text))
            {
                lblMensagem.Text = "⚠️ Este Projeto já foi cadastrado!";
                lblMensagem.CssClass = "alert alert-warning d-block";
                LimparCampos();
                AtualizarGrid();
                return; // Para a execução aqui
            }

            if (Repositorio.ListaProjetos.Any(p => p.coordenador.CPF == Coordenadores.SelectedValue))
            {
                lblMensagem.Text = "⚠️ Este Coordenador já está cadastrado em outro projeto!";
                lblMensagem.CssClass = "alert alert-warning d-block";
                LimparCampos();
                AtualizarGrid();
                return;
            }

            foreach (ListItem item in ListaTodosBolsistas.Items)
            {
                if (item.Selected)
                {
                    bool ListaBolsistas = Repositorio.ListaProjetos.Any(p =>
                    p.ListaBolsistasProjeto.Any(b => b.CPF == item.Value));

                    if (ListaBolsistas)
                    {
                        lblMensagem.Text = "⚠️ Este Bolsista já está cadastrado em outro projeto!";
                        lblMensagem.CssClass = "alert alert-warning d-block";
                        LimparCampos();
                        AtualizarGrid();
                        return;
                    }
                }
            }

                try
            {
                // Criando o objeto usando o novo Model
                Projeto novo = new Projeto();
                novo.Titulo = txtTitulo.Text;
                novo.Verba = float.Parse(txtVerba.Text);
                novo.Area = txtArea.Text;
                novo.Valor_Bolsa = float.Parse(txtValorBolsa.Text);
                novo.coordenador = Repositorio.ListaCoordenadores.FirstOrDefault(c => c.CPF == Coordenadores.SelectedValue);


               
                foreach (ListItem item in ListaTodosBolsistas.Items)
                {
                    if (item.Selected)
                    {

                        Bolsista bolsista = Repositorio.ListaBolsistas.FirstOrDefault(b => b.CPF == (item.Value));

                       if (bolsista != null)
                        {
                            novo.ListaBolsistasProjeto.Add(bolsista); 
                        }
                    }
                }

                    // 2. ADICIONAR NA LISTA ESTÁTICA
                    Repositorio.ListaProjetos.Add(novo);

                LimparCampos();
                lblMensagem.Text = "Projeto salvo com sucesso!";
                lblMensagem.CssClass = "text-success";

                AtualizarGrid();
            }
            catch (Exception)
            {
                lblMensagem.Text = "Erro ao salvar Projeto.";
                lblMensagem.CssClass = "text-danger";
            }
        }
        protected void gridProjetos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "MostrarDetalhes")
            {
                int indice = Convert.ToInt32(e.CommandArgument);

                Projeto projeto = Repositorio.ListaProjetos[indice];
                //relação de indice da tabela pode não convergir com a posição do objeto na lista

                pnlDetalhes.Visible = true;

                lblTitulo.Text = "Titulo: " + projeto.Titulo;
                lblArea.Text = "Area: " + projeto.Area;
                lblVerba.Text = "Verba: " + projeto.Verba;
                lblValorBolsa.Text = "Valor da Bolsa: " + projeto.Valor_Bolsa;
                lblCoordenador.Text = "Coordenador: " + projeto.coordenador.Nome;



                rptBolsistas.DataSource = projeto.ListaBolsistasProjeto;
                rptBolsistas.DataBind();


                if (projeto.ListaBolsistasProjeto.Count == 0)
                {
                    lblSemBolsista.Visible = true;
                }
                else
                {
                    lblSemBolsista.Visible = false;
                }

            }
        }





private void LimparCampos()
        {
            txtTitulo.Text = "";
            txtVerba.Text = "";
            txtValorBolsa.Text = "";
            txtArea.Text = "";
            Coordenadores.SelectedIndex = 0;
            ListaTodosBolsistas.SelectedIndex = 0;
            txtTitulo.Focus();
        }


    }
}