using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static WebApplication1.Models.Repositorio;

namespace WebApplication1
{
    public class DespesaRepositorio : BaseRepository
    {
        public static int SalvarDespesa(Despesa despesa)
        {
            string conexao = ConfigurationManager
                .ConnectionStrings["Conexao"]
                .ConnectionString;


            using (SqlConnection conn = new SqlConnection(conexao))
            {
                conn.Open();

                string sql = @"INSERT INTO Despesas
                              (Descricao, Valor, DataDespesa, Categoria, ProjetoID)
                              VALUES
                              (@Descricao,  @Valor, @DataDespesa, @Categoria, @ProjetoID);


                              SELECT SCOPE_IDENTITY();";


                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Descricao", despesa.Descricao);
                cmd.Parameters.AddWithValue("@Valor", despesa.Valor);
                cmd.Parameters.AddWithValue("@DataDespesa", despesa.Data);
                cmd.Parameters.AddWithValue("@Categoria", despesa.Categoria);
                cmd.Parameters.AddWithValue("@ProjetoID", despesa.ProjetoID);

                int idGerado = Convert.ToInt32(cmd.ExecuteScalar());

                return idGerado; //testar função
            }
        }

        public static List<Despesa> ListarDespesas()
        {
            List<Despesa> lista = new List<Despesa>();

            using (SqlConnection conn = Conexao.CriarConexao())
            {
                conn.Open();

                string sql = "SELECT * FROM Despesas";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Despesa b = new Despesa();

                    b.Descricao = reader["Descricao"].ToString();
                    b.Valor = Convert.ToDecimal(reader["Valor"]);
                    b.Categoria = reader["Categoria"].ToString();
                    b.ProjetoID = Convert.ToInt32(reader["ProjetoID"]);
                    b.Data = Convert.ToDateTime(reader["DataDespesa"]);

                    lista.Add(b);
                }
            }
            return lista;
        }
    }
}