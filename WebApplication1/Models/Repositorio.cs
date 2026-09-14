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












        

        
    }
}