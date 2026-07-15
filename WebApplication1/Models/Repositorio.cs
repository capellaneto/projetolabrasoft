using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Repositorio
    {
        public static List<Bolsista> ListaBolsistas = new List<Bolsista>
        {
            new Bolsista { Nome = "Alice Vieira", Matricula = "2024001", CPF = "111.222.333-01", Sexo = "F", DataNascimento = new DateTime(2005, 3, 12) },
            new Bolsista { Nome = "Bruno Henrique", Matricula = "2024002", CPF = "222.333.444-02", Sexo = "M", DataNascimento = new DateTime(2003, 7, 25) },
            new Bolsista { Nome = "Carla Dias", Matricula = "2024003", CPF = "333.444.555-03", Sexo = "F", DataNascimento = new DateTime(2004, 11, 30) },
            new Bolsista { Nome = "Daniel Augusto", Matricula = "2024004", CPF = "444.555.666-04", Sexo = "M", DataNascimento = new DateTime(2002, 1, 15) },
            new Bolsista { Nome = "Eduarda Lima", Matricula = "2024005", CPF = "555.666.777-05", Sexo = "F", DataNascimento = new DateTime(2006, 5, 20) },
            new Bolsista { Nome = "Felipe Neto", Matricula = "2024006", CPF = "666.777.888-06", Sexo = "O", DataNascimento = new DateTime(2001, 9, 10) },
            new Bolsista { Nome = "Gabriela Rocha", Matricula = "2024007", CPF = "777.888.999-07", Sexo = "F", DataNascimento = new DateTime(2005, 8, 05) },
            new Bolsista { Nome = "Hugo Souza", Matricula = "2024008", CPF = "888.999.000-08", Sexo = "M", DataNascimento = new DateTime(2003, 12, 12) },
            new Bolsista { Nome = "Isabela Martins", Matricula = "2024009", CPF = "999.000.111-09", Sexo = "O", DataNascimento = new DateTime(2004, 4, 18) },
            new Bolsista { Nome = "João Pedro", Matricula = "2024010", CPF = "000.111.222-10", Sexo = "M", DataNascimento = new DateTime(2002, 6, 22) }
        };
        public static List<Coordenador> ListaCoordenadores = new List<Coordenador>
        {
            new Coordenador { Nome = "Dr. Marcos Pontes", CPF = "123.123.123-11", Titulacao = "Doutor", AreaAtuacao = "Informática", Email = "marcos.pontes@ifba.edu.br" },
            new Coordenador { Nome = "Ma. Fernanda Costa", CPF = "234.234.234-22", Titulacao = "Mestre", AreaAtuacao = "Matemática", Email = "fernanda.costa@ifba.edu.br" },
            new Coordenador { Nome = "Dr. Sergio Moro", CPF = "345.345.345-33", Titulacao = "Pós-Doc", AreaAtuacao = "Direito", Email = "sergio.moro@ifba.edu.br" },
            new Coordenador { Nome = "Esp. Juliana Paes", CPF = "456.456.456-44", Titulacao = "Especialista", AreaAtuacao = "Administração", Email = "juliana.paes@ifba.edu.br" },
            new Coordenador { Nome = "Dr. Roberto Carlos", CPF = "567.567.567-55", Titulacao = "Doutor", AreaAtuacao = "Engenharia", Email = "roberto.carlos@ifba.edu.br" },
            new Coordenador { Nome = "Ma. Luciana Gimenez", CPF = "678.678.678-66", Titulacao = "Mestre", AreaAtuacao = "Saúde", Email = "luciana.gimenez@ifba.edu.br" },
            new Coordenador { Nome = "Dr. Enéas Carneiro", CPF = "789.789.789-77", Titulacao = "Doutor", AreaAtuacao = "Física", Email = "eneas@ifba.edu.br" },
            new Coordenador { Nome = "Esp. Cláudia Leitte", CPF = "890.890.890-88", Titulacao = "Especialista", AreaAtuacao = "Artes", Email = "claudia.leitte@ifba.edu.br" },
            new Coordenador { Nome = "Ma. Marina Silva", CPF = "901.901.901-99", Titulacao = "Mestre", AreaAtuacao = "Meio Ambiente", Email = "marina.silva@ifba.edu.br" },
            new Coordenador { Nome = "Dr. Silvio Santos", CPF = "012.012.012-00", Titulacao = "Doutor", AreaAtuacao = "Comunicação", Email = "silvio.santos@ifba.edu.br" }
        };
        public static List<Projeto> ListaProjetos = new List<Projeto>
        {

        };
    }
}