using System;
using System.Linq;
using System.Web;
using System.Web.UI;
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
            if (Repositorio.ListarUsuarios().Any(u =>
                u.Email.Equals(
                    txtEmailCadastro.Text.Trim(),
                    StringComparison.OrdinalIgnoreCase)))
            {
                lblMensagem.Text = "⚠️ Este Email já está cadastrado!";
                lblMensagem.CssClass = "alert alert-warning d-block";
                return;
            }

            Usuario usuario = new Usuario();

            usuario.Email = txtEmailCadastro.Text.Trim();
            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(
                txtSenhaCadastro.Text
            );

            Repositorio.SalvarUsuario(usuario);

            lblMensagem.Text = "✅ Cadastro realizado com sucesso!";
            lblMensagem.CssClass = "alert alert-success d-block";

            MultiView1.ActiveViewIndex = 0;
        }

        protected void btnLogar_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string senha = txtSenha.Text;

            Usuario usuario = Repositorio.ListarUsuarios()
                .FirstOrDefault(u =>
                    u.Email.Equals(
                        email,
                        StringComparison.OrdinalIgnoreCase));

            if (usuario == null)
            {
                lblMensagem.Text = "⚠️ Este Email não está cadastrado!";
                lblMensagem.CssClass = "alert alert-warning d-block";
                return;
            }

            if (!BCrypt.Net.BCrypt.Verify(senha, usuario.Senha))
            {
                lblMensagem.Text = "⚠️ Senha incorreta!";
                lblMensagem.CssClass = "alert alert-warning d-block";
                return;
            }

            string Cargo;

            if (usuario.Email.EndsWith(
                "@labrasoft.com",
                StringComparison.OrdinalIgnoreCase))
            {
                Cargo = "Admin";
            }
            else
            {
                Cargo = "Usuario";
            }

            // Gera o JWT
            string Token = TokenService.GerarToken(
                usuario.ID,
                usuario.Email,
                Cargo
            );

            // Cria o cookie com o MESMO nome usado pelo Site.master
            HttpCookie Cookie = new HttpCookie(
                "TokenJWT",
                Token
            );

            Cookie.HttpOnly = true;
            Cookie.Expires = DateTime.Now.AddDays(7);
            Cookie.Path = "/";

            Response.Cookies.Add(Cookie);

            // Mantém o usuário autenticado pela sessão
            Session["UsuarioID"] = usuario.ID;

            // IMPORTANTE:
            // NÃO chamar master.AtualizarMenu() aqui.
            //
            // O cookie está em Response.Cookies e ainda não
            // está disponível em Request.Cookies.
            //
            // O Site.master será executado novamente quando
            // BolsistaExemplo.aspx for carregada.

            Response.Redirect(
                "~/BolsistaExemplo.aspx",
                false
            );

            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
