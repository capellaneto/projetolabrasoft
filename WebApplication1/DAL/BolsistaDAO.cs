

namespace WebApplication1.DAL
{
    public class BolsistaDAO
    {
        public static void Salvar(Bolsista bolsista)
        {
            using (SqlConnection conn = Conexao.CriarConexao())
            {
                conn.Open();
            }
        }
    }
}