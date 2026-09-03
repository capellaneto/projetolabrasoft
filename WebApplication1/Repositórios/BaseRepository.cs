using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public abstract class BaseRepository
    {
        protected readonly string conexao;
        
        public BaseRepository()
        {
            conexao = ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString;

            if(string.IsNullOrEmpty(conexao))
            {
                throw new Exception("String de Conexão não foi encontrada!");
            }
        }
    }
}