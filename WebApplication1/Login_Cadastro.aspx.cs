using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.Models;

namespace WebApplication1
{
    public partial class Login_Cadastro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnCadastro_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }

        protected void btnVoltarLogin_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (Repositorio.ListarUsuarios().Any(u =>u.Email.Equals(txtEmailCadastro.Text.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                lblMensagem.Text = "⚠️ Este Email já está cadastrado!";
                lblMensagem.CssClass = "alert alert-warning d-block";
                return;
            }


            Usuario usuario = new Usuario();

            usuario.Email = txtEmailCadastro.Text;
            usuario.Senha = txtSenhaCadastro.Text;

            Repositorio.SalvarUsuario(usuario);

            MultiView1.ActiveViewIndex = 0;
        }

        protected void btnLogar_Click(Object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string senha = txtSenha.Text;

            Usuario usuario = Repositorio.ListarUsuarios()
                .FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            if(usuario == null)
            {
                lblMensagem.Text = "⚠️ Este Email não está cadastrado!";
                lblMensagem.CssClass = "alert alert-warning d-block";
                return;
            }
            
            if (usuario.Senha != senha)
            {
                lblMensagem.Text = "⚠️ Senha incorreta!";
                lblMensagem.CssClass = "alert alert-warning d-block";
                return;
            }
            lblMensagem.Text = "✅ Login realizado com sucesso!";
            lblMensagem.CssClass = "alert alert-success d-block";

            Session["UsuarioID"] = usuario.ID;
           

            Site master = (Site)this.Master;
            master.AtualizarMenu();
        }
    }
}