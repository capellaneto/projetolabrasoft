using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;


namespace WebApplication1.Service
{
    public class EmailService
    {
        public async Task<bool> EnviarNotificacaoDespesa(
            string emailDestino,
            string nomeUsuario,
            string descricao,
            decimal valor,
            DateTime dataDespesa,
            string coordenador)
        {
            try
            {
                string caminhoJson = HttpContext.Current.Server.MapPath(
                    "~/App_Data/google_secret.json");

                string caminhoTemplate = HttpContext.Current.Server.MapPath(
                    "~/Web/TemplateEmail.html");

                string corpoHtml = File.ReadAllText(caminhoTemplate);


                corpoHtml = corpoHtml.Replace("{{NOME_USUARIO}}", nomeUsuario);

                corpoHtml = corpoHtml.Replace("{{DESCRICAO}}", descricao);

                corpoHtml = corpoHtml.Replace(
                    "{{VALOR}}",
                    valor.ToString("N2"));

                corpoHtml = corpoHtml.Replace(
                    "{{DATA_DESPESA}}",
                    dataDespesa.ToString("dd/MM/yyyy"));
                
                corpoHtml = corpoHtml.Replace("{{COORDENADOR}}", coordenador);


                UserCredential credenciais;

                using (var stream = new FileStream(
                    caminhoJson,
                    FileMode.Open,
                    FileAccess.Read))
                {
                    string pastaToken = HttpContext.Current.Server.MapPath(
                        "~/App_Data/Tokens");

                    credenciais = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.FromStream(stream).Secrets,
                        new[] { GmailService.Scope.GmailSend },
                        "user",
                        CancellationToken.None,
                        new FileDataStore(pastaToken, true)
                    );
                }


                var service = new GmailService(
                    new BaseClientService.Initializer
                    {
                        HttpClientInitializer = credenciais,
                        ApplicationName = "LabraSoft"
                    });


                var msgRaw = MontarMensagemRaw(
                    emailDestino,
                    "Nova Despesa Registrada",
                    corpoHtml);

                await service.Users.Messages.Send(
                    msgRaw,
                    "me"
                ).ExecuteAsync();


                return true;
            }
            catch (Exception ex)
            {
                string erroDetalhado = ex.Message;

                return false;
            }
        }


        private Google.Apis.Gmail.v1.Data.Message MontarMensagemRaw(
            string para,
            string assunto,
            string corpoHtml)
        {
            string conteudoMime =
                $"To: {para}\r\n" +
                $"Subject: {assunto}\r\n" +
                "Content-Type: text/html; charset=utf-8\r\n\r\n" +
                $"{corpoHtml}";


            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(
                conteudoMime);


            string base64 = Convert.ToBase64String(bytes)
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", "");


            return new Google.Apis.Gmail.v1.Data.Message
            {
                Raw = base64
            };
        }
    }
}
