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
            if (!IsPostBack)
            {
                Atualizar_Grid();
                LimparCampos();
            }
        }
        protected void Atualizar_Grid()
        {
            if(bolsistas.Count > 0)
            {
                gvBolsistas.DataSource = bolsistas;
                gvBolsistas.DataBind();
                gvBolsistas.Visible = true;
                LimparCampos();
                lblMensagem.Visible = false;
                btnFiltroFem.Visible = true;
                btnOrdemAlfabetica.Visible = true;
                btnFiltroMasc.Visible = true;
                btnOrigem.Visible = true;
            }
            else
            {
                gvBolsistas.Visible = false;
                btnFiltroFem.Visible = false;
                btnOrdemAlfabetica.Visible =false;
                btnFiltroMasc.Visible = false;
                btnOrigem .Visible = false;
            }
        }

        protected static List<Bolsista> bolsistas = new List<Bolsista>()
        {
            new Bolsista 
            {
                Nome = "Bruno Oliveira",
                CPF = "123.456.789-00",
                Matricula = "2024001",
                DataNascimento = new DateTime(2002, 5, 10),
                Sexo = "F"
            },
            new Bolsista
            {
                Nome = "Ana Silva",
                CPF = "987.654.321-00",
                Matricula = "2024002",
                DataNascimento = new DateTime(2001, 8, 22),
                Sexo = "M"
            },
            new Bolsista
            {
                Nome = "Carla Souza",
                CPF = "456.789.123-00",
                Matricula = "2024003",
                DataNascimento = new DateTime(2003, 1, 15),
                Sexo = "F"
            },
            new Bolsista
            {
                Nome = "Diego Santos",
                CPF = "321.654.987-00",
                Matricula = "2024004",
                DataNascimento = new DateTime(2000, 11, 3),
                Sexo = "M"
            },
            new Bolsista
            {
                Nome = "Eduarda Lima",
                CPF = "654.321.987-00",
                Matricula = "2024005",
                DataNascimento = new DateTime(2004, 7, 28),
                Sexo = "F"
            }
        };



        protected void btnFiltroMasc_Click(object sender, EventArgs e)
        {
            var masculino = bolsistas.Where(b => b.Sexo == "M").ToList();
            gvBolsistas.DataSource = masculino;
            gvBolsistas.DataBind();
        }

        protected void btnFiltroFem_Click(object sender, EventArgs e)
        {
            var feminino = bolsistas.Where(b => b.Sexo == "F").ToList();
            gvBolsistas.DataSource = feminino;
            gvBolsistas.DataBind();
        }

        protected void btnOrdemAlfabetica_Click(object sender, EventArgs e)
        {
            var alfabetico = bolsistas.OrderBy(p => p.Nome).ToList();
            gvBolsistas.DataSource = alfabetico;
            gvBolsistas.DataBind();
        }

        protected void btnOrigem_Click(object sender, EventArgs e)
        {
            //var origem = bolsistas.Where(b => b.Sexo == "M" || b.Sexo == "F" || b.Sexo == "O").ToList();
            //gvBolsistas.DataSource = origem;
            //gvBolsistas.DataBind();
            Atualizar_Grid();
        }


        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }
        protected void LimparCampos()
        {
            txtNome.Text = "";
            txtMatricula.Text = "";
            txtCPF.Text = "";
            txtDataNasc.Text = "";
            ddlSexo.SelectedValue = "";
        }
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtMatricula.Text) || string.IsNullOrWhiteSpace(txtCPF.Text) || string.IsNullOrWhiteSpace(txtDataNasc.Text) || ddlSexo.SelectedIndex <= 0) 
                {
                    lblMensagem.Text = "informações nao preenchidas";
                    lblMensagem.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                
                if (bolsistas.Any(b => b.CPF == txtCPF.Text))
                {
                    lblMensagem.Text = "CPF ja cadastrado";
                    lblMensagem.ForeColor = System.Drawing.Color.Red;
                    txtCPF.Text = "";
                    Atualizar_Grid();
                    lblMensagem.Visible = true;
                    return;
                }
                    // 1. Instanciar a classe
               
                Bolsista aluno = new Bolsista();

                // 2. Mapear a TELA para o OBJETO
                aluno.Nome = txtNome.Text;
                aluno.Matricula = txtMatricula.Text;
                aluno.CPF = txtCPF.Text;
                aluno.DataNascimento = DateTime.Parse(txtDataNasc.Text);
                aluno.Sexo = ddlSexo.SelectedValue;

                bolsistas.Add(aluno);
                Response.Redirect("CadastroBolsista.aspx");
                Atualizar_Grid();
                

                LimparCampos();
                // 3. Executar a lógica que você já criou na Semana 1
                string resumo = aluno.ObterResumo();
                int idade = aluno.CalcularIdade();

                // 4. Mostrar o resultado na tela
                lblMensagem.Text = $"Sucesso! {resumo}. Idade: {idade} anos.";
                lblMensagem.ForeColor = System.Drawing.Color.DarkGreen;
            }
            catch (Exception)
            {
                lblMensagem.Text = "Erro: Verifique se a data de nascimento foi preenchida.";
                lblMensagem.ForeColor = System.Drawing.Color.Red;
                lblMensagem.Visible = true;
            }
        }
    }
}
