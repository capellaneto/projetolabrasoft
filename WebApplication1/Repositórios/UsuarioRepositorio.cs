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
    public class UsuarioRepositorio : BaseRepository
    {
        public static int SalvarUsuario(Usuario usuario)
        {
            string conexao = ConfigurationManager
                .ConnectionStrings["Conexao"]
                .ConnectionString;


            using (SqlConnection conn = new SqlConnection(conexao))
            {
                conn.Open();

                string sql = @"INSERT INTO Usuario
                              (Email, Senha, Nome)
                              VALUES
                              (@Email, @Senha, @Nome);


                              SELECT SCOPE_IDENTITY();";


                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Email", usuario.Email);
                cmd.Parameters.AddWithValue("@Senha", usuario.Senha);
                cmd.Parameters.AddWithValue("@Nome", usuario.Nome);

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