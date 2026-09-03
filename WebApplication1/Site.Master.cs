using System;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace WebApplication1
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AtualizarMenu();
        }

        public void AtualizarMenu()
        {
            // Descobre qual página está sendo acessada
            string pagina = VirtualPathUtility.GetFileName(
                Request.AppRelativeCurrentExecutionFilePath
            );

            // A página de login NÃO deve exigir autenticação
            if (pagina.Equals("Login_Cadastro.aspx",
                StringComparison.OrdinalIgnoreCase))
            {
                menuNavegacao.Visible = false;
                return;
            }

            // Para as demais páginas, precisa existir sessão
            if (Session["UsuarioID"] == null)
            {
                menuNavegacao.Visible = false;

                Response.Redirect(
                    "~/Login_Cadastro.aspx",
                    false
                );

                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            // Procura o cookie correto
            HttpCookie Cookie = Request.Cookies["TokenJWT"];

            if (Cookie == null || string.IsNullOrEmpty(Cookie.Value))
            {
                menuNavegacao.Visible = false;

                Response.Redirect(
                    "~/Login_Cadastro.aspx",
                    false
                );

                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            // Valida o JWT
            var Usuario = TokenService.ValidarToken(Cookie.Value);

            if (Usuario == null)
            {
                menuNavegacao.Visible = false;

                Response.Redirect(
                    "~/Login_Cadastro.aspx",
                    false
                );

                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            // Obtém o e-mail do JWT
            string email = Usuario.Claims
                .FirstOrDefault(c => c.Type == "Email")
                ?.Value;

            if (string.IsNullOrEmpty(email))
            {
                menuNavegacao.Visible = false;

                Response.Redirect(
                    "~/Login_Cadastro.aspx",
                    false
                );

                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            // Usuário autenticado
            menuNavegacao.Visible = true;

            // Esconde opções administrativas
            if (!email.EndsWith(
                    "@labrasoft.com",
                    StringComparison.OrdinalIgnoreCase))
            {
                lnkCoordenadores.Visible = false;
                lnkProjetos.Visible = false;
                lnkDespesas.Visible = false;
            }
        }
    }
}
