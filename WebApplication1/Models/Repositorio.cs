using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1.Models
{
    public class Repositorio
    {
        public static List<Bolsista> ListaBolsistas = new List<Bolsista>();


        public static List<Coordenador> ListaCoordenadores = new List<Coordenador>();

        public static List<Projeto> ListaProjetos = new List<Projeto>
        {

        };

        public class Conexao
        {
            public static SqlConnection CriarConexao()
            {
                string conexao = ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString;

                return new SqlConnection(conexao);
            }
        }
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

                while(reader.Read())
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
                    b.Verba = Convert.ToSingle(reader["VerbaAprovada"]);
                    b.IdCoordenador = Convert.ToInt32(reader["CoordenadorID"]);
                    b.Valor_Bolsa = Convert.ToSingle(reader["ValorBolsaIndividual"]);
                    b.Area = reader["AreaConhecimento"].ToString();

                    lista.Add(b);
                }
            }
            return lista;
        }


        public static void SalvarVinculoBolsistaProjeto (int idProjeto, int idBolsista)
        {
            string conexao = ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString;


            using (SqlConnection conn = new SqlConnection(conexao))
            {
                conn.Open();


                string sql = @"INSERT INTO ProjetoBolsista
                       (ProjetoID, BolsistaID, DataVinculo)

                       VALUES

                       (@IdProjeto, @IdBolsista, @DataVinculo)";


                SqlCommand cmd = new SqlCommand(sql, conn);


                cmd.Parameters.AddWithValue("@ProjetoID", idProjeto);
                cmd.Parameters.AddWithValue("@BolsistaID", idBolsista);
                cmd.Parameters.AddWithValue("@DataVinculo", DateTime.Now);


                cmd.ExecuteNonQuery();
            }
        }

    }
}