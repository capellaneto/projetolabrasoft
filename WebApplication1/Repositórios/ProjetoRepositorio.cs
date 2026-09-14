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
    public class ProjetoRepositorio : BaseRepository
    {
        public static List<Projeto> ListaProjetos = new List<Projeto>
        {

        };

        public static int SalvarProjeto(Projeto projeto)
        {
            string conexao = ConfigurationManager
                .ConnectionStrings["Conexao"]
                .ConnectionString;


            using (SqlConnection conn = new SqlConnection(conexao))
            {
                conn.Open();

                string sql = @"INSERT INTO Projeto
                              (Titulo, VerbaAprovada, CoordenadorID, ValorBolsaIndividual, AreaConhecimento)
                              VALUES
                              (@Titulo,  @Verba, @IdCoordenador, @Valor_Bolsa, @Area);


                              SELECT SCOPE_IDENTITY();";


                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Titulo", projeto.Titulo);
                cmd.Parameters.AddWithValue("@Verba", projeto.Verba);
                cmd.Parameters.AddWithValue("@IdCoordenador", projeto.IdCoordenador);
                cmd.Parameters.AddWithValue("@Valor_Bolsa", projeto.Valor_Bolsa);
                cmd.Parameters.AddWithValue("@Area", projeto.Area);

                int idGerado = Convert.ToInt32(cmd.ExecuteScalar());

                return idGerado;
            }
        }

        public static List<Projeto> ListarProjeto()
        {
            List<Projeto> lista = new List<Projeto>();

            using (SqlConnection conn = Conexao.CriarConexao())
            {
                conn.Open();

                string sql = "SELECT * FROM Projeto";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Projeto b = new Projeto();

                    b.Id = Convert.ToInt32(reader["ID"]);
                    b.Titulo = reader["Titulo"].ToString();
                    b.Verba = Convert.ToDecimal(reader["VerbaAprovada"]);
                    b.IdCoordenador = Convert.ToInt32(reader["CoordenadorID"]);
                    b.Valor_Bolsa = Convert.ToDecimal(reader["ValorBolsaIndividual"]);
                    b.Area = reader["AreaConhecimento"].ToString();

                    b.ListaBolsistasProjeto = ListarBolsistasProjeto(b.Id);

                    lista.Add(b);
                }
            }
            return lista;
        }

        public static List<ProjetoGridDTO> ListarProjetoNaGrid()
        {
            List<ProjetoGridDTO> lista = new List<ProjetoGridDTO>();

            using (SqlConnection conn = Conexao.CriarConexao())
            {
                conn.Open();

                string sql = @"
                            SELECT p.ID, p.Titulo, p.VerbaAprovada, c.Nome AS Coordenador
                            FROM Projeto p
                            INNER JOIN Coordenador c
                                ON p.CoordenadorID = c.ID";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ProjetoGridDTO b = new ProjetoGridDTO();

                    b.Id = Convert.ToInt32(reader["ID"]);
                    b.Titulo = reader["Titulo"].ToString();
                    b.Verba = Convert.ToDecimal(reader["VerbaAprovada"]);
                    b.Coordenador = reader["Coordenador"].ToString();

                    lista.Add(b);
                }
            }

            return lista;
        }

        public static void ExcluirProjeto(int IdProjeto)
        {
            string conexao = ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(conexao))
            {
                conn.Open();

                string sql = "DELETE FROM Projeto WHERE ID = @ID";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ID", IdProjeto);

                cmd.ExecuteNonQuery();
            }
        }

        public static void ExcluirBolsistaProjeto(int IdProjeto, int IdBolsista)
        {
            string conexao = ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(conexao))
            {
                conn.Open();

                string sql = "DELETE FROM ProjetoBolsista WHERE ProjetoID = @ProjetoID AND BolsistaID = @BolsistaID";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ProjetoID", IdProjeto);
                cmd.Parameters.AddWithValue("@BolsistaID", IdBolsista);

                cmd.ExecuteNonQuery();
            }
        }

        public static void SalvarVinculoBolsistaProjeto(int idProjeto, int idBolsista)
        {
            string conexao = ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString;


            using (SqlConnection conn = new SqlConnection(conexao))
            {
                conn.Open();


                string sql = @"INSERT INTO ProjetoBolsista
                       (ProjetoID, BolsistaID, DataVinculo)

                       VALUES

                       (@idProjeto, @idBolsista, @DataVinculo)";


                SqlCommand cmd = new SqlCommand(sql, conn);


                cmd.Parameters.AddWithValue("@idProjeto", idProjeto);
                cmd.Parameters.AddWithValue("@idBolsista", idBolsista);
                cmd.Parameters.AddWithValue("@DataVinculo", DateTime.Now);


                cmd.ExecuteNonQuery();
            }
        }

        public static List<Bolsista> ListarBolsistasProjeto(int idProjeto)
        {
            List<Bolsista> lista = new List<Bolsista>();

            using (SqlConnection conn = Conexao.CriarConexao())
            {
                conn.Open();

                string sql = @"
                      SELECT b.*
                      FROM Bolsista b
                      INNER JOIN ProjetoBolsista pb
                           ON b.Id = pb.BolsistaID
                      WHERE pb.ProjetoID = @ProjetoID";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ProjetoID", idProjeto);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Bolsista b = new Bolsista();

                    b.Id = Convert.ToInt32(reader["Id"]);
                    b.Nome = reader["Nome"].ToString();
                    b.CPF = reader["CPF"].ToString();
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