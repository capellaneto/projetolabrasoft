using System;

namespace WebApplication1.Models
{
    public class Bolsista
    {
        // EXERCÍCIO POO:
        // Com base no formulário que vocês criaram, definam as propriedades abaixo.
        // Lembrem-se de usar 'public', o tipo de dado (string, int, etc) e o { get; set; }

        public string Nome { get; set; }
        public string cpf { get; set; }
        // TODO: Criar a propriedade para o CPF
        public string matricula { get; set; }
        // TODO: Criar a propriedade para a Matrícula
        public DateTime data_nacimento { get; set; }
        // TODO: Criar a propriedade para a Data de Nascimento
        public char sexo { get; set; }
        // TODO: Criar a propriedade para o Sexo
        public string resumir_bolsista()
        {
            return $"nome: {Nome}, matricula: {matricula}";
        }
        // TODO: Criar método com o resumo das informações contendo nome e matrícula
        public int idade_bolsista()
        {
            int idade = DateTime.Now.Year - data_nacimento.Year;
            return idade;
        }

        //TODO: Criar método que calcúla a idade do bolsista      

    }
}
