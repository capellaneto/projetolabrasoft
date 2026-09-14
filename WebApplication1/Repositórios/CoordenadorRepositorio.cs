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
    public class CoordenadorRepositorio : BaseRepository
    {
        public static List<Coordenador> ListaCoordenadores = new List<Coordenador>();

        public static void SalvarCoordenador(Coordenador coordenador)
        {
            string conexao = ConfigurationManager
                .ConnectionStrings["Conexao"]
                .ConnectionString;


            using (SqlConnection conn = new SqlConnection(conexao))
            {
                conn.Open();

                string sql = @"INSERT INTO Coordenador
                              (Nome, CPF, Titulacao, AreaAtuacao, Email)
                              VALUES
                              (@Nome,  @CPF, @Titulacao, @AreaAtuacao, @Email)";


                SqlCommand cmd = new SqlCommand(sql, conn);


                cmd.Parameters.AddWithValue("@Nome", coordenador.Nome);
                cmd.Parameters.AddWithValue("@CPF", coordenador.CPF);
                cmd.Parameters.AddWithValue("@Titulacao", coordenador.Titulacao);
                cmd.Parameters.AddWithValue("@AreaAtuacao", coordenador.AreaAtuacao);
                cmd.Parameters.AddWithValue("@Email", coordenador.Email);


                cmd.ExecuteNonQuery();
            }
        }

        public static List<Coordenador> ListarCoordenador()
        {
            List<Coordenador> lista = new List<Coordenador>();

            using (SqlConnection conn = Conexao.CriarConexao())
            {
                conn.Open();

                string sql = "SELECT * FROM Coordenador";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Coordenador b = new Coordenador();

                    b.Id = Convert.ToInt32(reader["Id"]);
                    b.Nome = reader["Nome"].ToString();
                    b.CPF = reader["CPF"].ToString();
                    b.Titulacao = reader["Titulacao"].ToString();
                    b.AreaAtuacao = reader["AreaAtuacao"].ToString();
                    b.Email = reader["Email"].ToString();

                    lista.Add(b);
                }
            }
            return lista;
        }

        public static List<CoordenadorGridDTO> ListarCoordenadorNaGrid()
        {
            List<CoordenadorGridDTO> lista = new List<CoordenadorGridDTO>();

            using (SqlConnection conn = Conexao.CriarConexao())
            {
                conn.Open();

                string sql = "SELECT Id, Nome, Titulacao, Email FROM Coordenador";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    CoordenadorGridDTO b = new CoordenadorGridDTO();

                    b.Id = Convert.ToInt32(reader["Id"]);
                    b.Nome = reader["Nome"].ToString();
                    b.Titulacao = reader["Titulacao"].ToString();
                    b.Email = reader["Email"].ToString();

                    lista.Add(b);
                }
            }
            return lista;
        }

        public static void AtualizarEmailCoordenador(int id, string NovoEmail)
        {
            string conexao = ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString;


            using (SqlConnection conn = new SqlConnection(conexao))
            {
                conn.Open();

                string sql = @"UPDATE Coordenador
                               SET Email = @Email
                               WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Email", NovoEmail);
                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
            }
        }

        public static void ExcluirCoordenador(int IdCoordenador)
        {
            string conexao = ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(conexao))
            {
                conn.Open();

                string sql = "DELETE FROM Coordenador WHERE ID = @ID";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ID", IdCoordenador);

                cmd.ExecuteNonQuery();


            }
        }
    }
}