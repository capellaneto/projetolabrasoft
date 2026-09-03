using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static WebApplication1.Models.Repositorio;

namespace WebApplication1.Models
{
    public class Repositorio
    {
        

        

        

        public class Conexao
        {
            public static SqlConnection CriarConexao()
            {
                string conexao = ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString;

                return new SqlConnection(conexao);
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
                cmd.Parameters.AddWithValue("@Data", despesa.Data);
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
                    b.Data = Convert.ToDateTime(reader["Data"]);

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

        public static void ExcluirCoordenador  (int IdCoordenador)
        {
            string conexao = ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(conexao))
            {
                conn.Open();

                string sql = "DELETE FROM Coordenador WHERE ID = @ID";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@ID", IdCoordenador);

                cmd.ExecuteNonQuery ();
                               
                             
            }
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

                cmd.ExecuteNonQuery(); //atividade extra, finalizar
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

        public static int SalvarUsuario(Usuario usuario)
        {
            string conexao = ConfigurationManager
                .ConnectionStrings["Conexao"]
                .ConnectionString;


            using (SqlConnection conn = new SqlConnection(conexao))
            {
                conn.Open();

                string sql = @"INSERT INTO Usuario
                              (Email, Senha)
                              VALUES
                              (@Email, @Senha);


                              SELECT SCOPE_IDENTITY();";


                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                cmd.Parameters.AddWithValue("@Senha", usuario.Senha);

                int idUsuario = Convert.ToInt32(cmd.ExecuteScalar());

                return idUsuario;
            }
        }

        public static List<Usuario> ListarUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();

            using (SqlConnection conn = Conexao.CriarConexao())
            {
                conn.Open();

                string sql = "SELECT * FROM Usuario";

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Usuario b = new Usuario();

                    b.ID = Convert.ToInt32(reader["ID"]);
                    b.Email = reader["Email"].ToString();
                    b.Senha = reader["Senha"].ToString();

                    lista.Add(b);
                }
            }
            return lista;
        }
    }
}