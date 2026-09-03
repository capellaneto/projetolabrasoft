using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using static WebApplication1.Models.Repositorio;

namespace WebApplication1
{
    public class BolsistaRepositorio : BaseRepository 
    {
        public static List<Bolsista> ListaBolsistas = new List<Bolsista>();

        public static void SalvarBolsista(Bolsista bolsista)
        {
            string conexao = ConfigurationManager
                .ConnectionStrings["Conexao"]
                .ConnectionString;


            using (SqlConnection conn = new SqlConnection(conexao))
            {
                conn.Open();

                string sql = @"INSERT INTO Bolsista
                              (Nome, Matricula, CPF, Sexo, DataNascimento)
                              VALUES
                              (@Nome, @Matricula, @CPF, @Sexo, @DataNascimento)";


                SqlCommand cmd = new SqlCommand(sql, conn);


                cmd.Parameters.AddWithValue("@Nome", bolsista.Nome);
                cmd.Parameters.AddWithValue("@Matricula", bolsista.Matricula);
                cmd.Parameters.AddWithValue("@CPF", bolsista.CPF);
                cmd.Parameters.AddWithValue("@Sexo", bolsista.Sexo);
                cmd.Parameters.AddWithValue("@DataNascimento", bolsista.DataNascimento);


                cmd.ExecuteNonQuery();
            }
        }

        public static List<Bolsista> ListarBolsistas()
        {
            List<Bolsista> lista = new List<Bolsista>();

            using (SqlConnection conn = Conexao.CriarConexao())
            {
                conn.Open();

                string sql = "SELECT * FROM Bolsista";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Bolsista b = new Bolsista();

                    b.Id = Convert.ToInt32(reader["Id"]);
                    b.Nome = reader["Nome"].ToString();
                    b.Matricula = reader["Matricula"].ToString();
                    b.CPF = reader["CPF"].ToString();
                    b.Sexo = reader["Sexo"].ToString();
                    b.DataNascimento = Convert.ToDateTime(reader["DataNascimento"]);

                    lista.Add(b);
                }
            }
            return lista;
        }

        public static List<BolsistaGridDTO> ListarBolsistasNaGrid()
        {
            List<BolsistaGridDTO> lista = new List<BolsistaGridDTO>();

            using (SqlConnection conn = Conexao.CriarConexao())
            {
                conn.Open();

                string sql = "SELECT Id, Nome, Matricula, Sexo, DataNascimento FROM Bolsista";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    BolsistaGridDTO b = new BolsistaGridDTO();

                    b.Id = Convert.ToInt32(reader["Id"]);
                    b.Nome = reader["Nome"].ToString();
                    b.Matricula = reader["Matricula"].ToString();
                    b.Sexo = reader["Sexo"].ToString();
                    b.DataNascimento = Convert.ToDateTime(reader["DataNascimento"]);

                    lista.Add(b);
                }
            }
            return lista;
        }
    }
}