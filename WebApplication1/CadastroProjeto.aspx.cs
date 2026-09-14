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

            ProjetoService projetoservice = new ProjetoService();


            var listaProjetos = projetoservice.ListarProjeto();
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
            if (!IsPostBack)
            {
                AtualizarGrid();

                CarregarCoordenadores();

                CarregarBolsistas();

                CarregarBolsistasDisponiveis();
            }
        }

        protected void Atualizar_Bolsistas(int IDProjeto)
        {

            ProjetoService projetoservice = new ProjetoService();


            Projeto projeto = projetoservice.BuscarProjeto(IDProjeto);

            rptBolsistas.DataSource = projeto.ListaBolsistasProjeto;
            rptBolsistas.DataBind();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            ProjetoService projetoservice = new ProjetoService();


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
            
            if (projetoservice.ListarProjeto().Any(b => b.Titulo == txtTitulo.Text))
            {
                lblMensagem.Text = "⚠️ Este Projeto já foi cadastrado!";
                lblMensagem.CssClass = "alert alert-warning d-block";
                LimparCampos();
                AtualizarGrid();
                return; // Para a execução aqui
            }

            if (projetoservice.ListarProjetosCompletos().Any(p => p.IdCoordenador == Convert.ToInt32(Coordenadores.SelectedValue)))
            {
                lblMensagem.Text = "⚠️ Este Coordenador já está cadastrado em outro projeto!";
                lblMensagem.CssClass = "alert alert-warning d-block";
                LimparCampos();
                AtualizarGrid();
                return;
            }

            foreach (ListItem item in ddlBolsistas.Items)
            {
                if (item.Selected && !string.IsNullOrEmpty(item.Value))
                {
                    bool ListaBolsistas = projetoservice.ListarProjetosCompletos().Any(p =>
                    p.ListaBolsistasProjeto.Any(b => b.Id == Convert.ToInt32(item.Value)));

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
                Projeto novo = new Projeto();
                novo.Titulo = txtTitulo.Text;
                novo.Verba = decimal.Parse(txtVerba.Text);
                novo.Area = txtArea.Text;
                novo.Valor_Bolsa = decimal.Parse(txtValorBolsa.Text);
                novo.IdCoordenador = Convert.ToInt32(Coordenadores.SelectedValue);

                BolsistaService bolsistaservice = new BolsistaService();

                foreach (ListItem item in ddlBolsistas.Items)
                {
                    if (item.Selected && !string.IsNullOrEmpty(item.Value))
                    {

                        Bolsista bolsista = bolsistaservice.BuscarBolsista(Convert.ToInt32(item.Value));

                        if (bolsista != null)
                        {
                            novo.ListaBolsistasProjeto.Add(bolsista);
                        }
                    }
                }

                bool sucesso = projetoservice.CadastrarProjeto(novo);

                if (sucesso)
                {
                    LimparCampos();

                    lblMensagem.Text = "Projeto salvo com sucesso!"; //verificar mensagem depois
                    lblMensagem.CssClass = "text-success";

                    AtualizarGrid();
                }
                else
                {
                    lblMensagem.Text = "⚠️ Projeto já cadastrado ou coordenador já vinculado a outro projeto.";
                    lblMensagem.CssClass = "text-warning";
                }
            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Erro ao cadastrar projeto: " + ex.Message;
                lblMensagem.CssClass = "text-danger";
            }
        }
        protected void gridProjetos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ProjetoService projetoservice = new ProjetoService();
            CoordenadorService coordenadorservice = new CoordenadorService();
            DespesaService despesaservice = new DespesaService();

            if (e.CommandName == "MostrarDetalhes")
            {
                int indice = Convert.ToInt32(e.CommandArgument);

                Projeto projeto = projetoservice.ListarProjetosCompletos()[indice];

                ViewState["ProjetoID"] = projeto.Id;

                projeto.coordenador = coordenadorservice.BuscarCoordenador(projeto.IdCoordenador);

                pnlDetalhes.Visible = true;

                CarregarBolsistasDisponiveis();

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

                List<Despesa> despesasProjeto = despesaservice.ListarDespesasPorProjeto(projeto.Id);

                GridDespesas.DataSource = despesasProjeto;
                GridDespesas.DataBind();

            }
            
        }

        private void CarregarCoordenadores() //opções de coordenadores só atualiza se rodar o programa dnv
        {
            ProjetoService projetoservice = new ProjetoService();
            CoordenadorService coordenadorservice = new CoordenadorService();


            List<CoordenadorGridDTO> coordenadores = coordenadorservice.ListarCoordenadores();
            List<Projeto> projetos = projetoservice.ListarProjetosCompletos();

            List<CoordenadorGridDTO> CoordenadoresDisponiveis = new List<CoordenadorGridDTO>();

            foreach (CoordenadorGridDTO coordenador in coordenadores)
            {
                bool EstaEmProjeto = false;

                foreach (Projeto projeto in projetos)
                {
                    if (projeto.IdCoordenador == coordenador.Id)
                    {
                        EstaEmProjeto = true;
                    }
                }
                if(!EstaEmProjeto)
                {
                    CoordenadoresDisponiveis.Add(coordenador);
                }
            }

            Coordenadores.DataSource = CoordenadoresDisponiveis;
            Coordenadores. DataTextField = "Nome";
            Coordenadores.DataValueField = "Id";
            Coordenadores.DataBind();

            Coordenadores.Items.Insert(0, new ListItem("Selecione", ""));
        }

        private void CarregarBolsistas() //opções de bolsistas só atualiza se rodar o programa dnv
        {

            BolsistaService bolsistaservice = new BolsistaService();
            ProjetoService projetoservice = new ProjetoService();


            List<BolsistaGridDTO> bolsistas = bolsistaservice.ListarBolsistas();
            List<Projeto> projetos = projetoservice.ListarProjetosCompletos();

            List<BolsistaGridDTO> ListaDisponiveis = new List<BolsistaGridDTO>();

            foreach (BolsistaGridDTO bolsista in bolsistas)
            {
                bool EstaEmProjeto = false;

                foreach (Projeto projeto in projetos)
                {
                    foreach (Bolsista bolsistaProjeto in projeto.ListaBolsistasProjeto)
                    {

                        if (bolsistaProjeto.Id == bolsista.Id)
                        {
                            EstaEmProjeto = true;
                        }
                    }
                }
                if (!EstaEmProjeto)
                {
                    ListaDisponiveis.Add(bolsista);
                }
            }

            ddlBolsistas.DataSource = ListaDisponiveis;
            ddlBolsistas.DataTextField = "Nome";
            ddlBolsistas.DataValueField = "Id";
            ddlBolsistas.DataBind();

            ddlBolsistas.Items.Insert(0, new ListItem("Selecione", ""));
        }


        private void AdicionarBolsistaProjeto()
        {
            int projetoID = Convert.ToInt32(ViewState["ProjetoID"]);

            foreach(ListItem item in lstBolsistasDisponiveis.Items)
            {
                if(item.Selected)
                {
                    int bolsistaID = Convert.ToInt32(item.Value);

                    ProjetoRepositorio.SalvarVinculoBolsistaProjeto(projetoID, bolsistaID);
                }
            }
            Atualizar_Bolsistas(projetoID);
            CarregarBolsistasDisponiveis();
            CarregarBolsistas();
        }


        protected void btnAdicionarBolsista_Click(object sender, EventArgs e)
        {
            try
            {
                AdicionarBolsistaProjeto();

                lblMensagem.Text = "Bolsista adicionado com sucesso!";
                lblMensagem.CssClass = "alert alert-success d-block";
            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Não foi posivel cadastrar o bolsista!";
                lblMensagem.CssClass = "alert alert-danger d-block";
            }
        }


        private void CarregarBolsistasDisponiveis()
        {
            List<Bolsista> bolsistas = BolsistaRepositorio.ListarBolsistas();
            List<Projeto> projetos = ProjetoRepositorio.ListarProjeto();

            List<Bolsista> BolsistasDisponiveis = new List<Bolsista>();

            foreach (Bolsista bolsista in bolsistas)
            {
                bool EstaEmProjeto = false;

                foreach (Projeto projeto in projetos)
                {
                    foreach (Bolsista bolsistaProjeto in projeto.ListaBolsistasProjeto)
                    {

                        if (bolsistaProjeto.Id == bolsista.Id)
                        {
                            EstaEmProjeto = true;
                        }
                    }
                }
                if (!EstaEmProjeto)
                {
                    BolsistasDisponiveis.Add(bolsista);
                }
            }

            lstBolsistasDisponiveis.DataSource = BolsistasDisponiveis;
            lstBolsistasDisponiveis.DataTextField = "Nome";
            lstBolsistasDisponiveis.DataValueField = "Id";
            lstBolsistasDisponiveis.DataBind();


        }


        protected void rptBolsistas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
                int idBolsista = Convert.ToInt32(e.CommandArgument);
                int ProjetoID = Convert.ToInt32(ViewState["ProjetoID"]);

                try
                {
                    ProjetoRepositorio.ExcluirBolsistaProjeto(ProjetoID, idBolsista);

                    lblMensagem.Text = "Bolsista excluido com sucesso";
                    lblMensagem.Visible = true;
                    Atualizar_Bolsistas(ProjetoID);

                    CarregarBolsistas();
                    CarregarBolsistasDisponiveis();

            }
                catch (Exception ex)
                {
                    lblMensagem.Text = "Não foi possivel excluir este bolsista, erro: " + ex;
                    lblMensagem.Visible = true;
                }
            }

        protected void btnFecharDetalhes_Click(object sender, EventArgs e)
        {
            pnlDetalhes.Visible = false;
        }



private void LimparCampos()
        {
            txtTitulo.Text = "";
            txtVerba.Text = "";
            txtValorBolsa.Text = "";
            txtArea.Text = "";
            Coordenadores.SelectedIndex = 0;
            ddlBolsistas.SelectedIndex = 0;
            txtTitulo.Focus();
        }


    }
}