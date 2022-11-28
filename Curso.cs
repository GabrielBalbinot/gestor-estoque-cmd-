using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEstoque
{
    [System.Serializable]
    class Curso : Produto, IEstoque
    {
        public string autor;
        private int vagas;
        public Curso(string nome, float preco, string autor)
        {
            this.nome = nome;
            this.preco = preco;
            this.autor = autor;
        }

        public void AdicionarEntrada()
        {
            Console.WriteLine($"Adicionar vagas disponívieis para o curso {nome}.");
            Console.WriteLine("Digite a quantidade de vagas para dar entrada: ");
            int entrada = int.Parse(Console.ReadLine());
            vagas += entrada;
            Console.WriteLine("Entrada registrada.");
            Console.ReadLine();
        }

        public void AdicionarSaida()
        {
            Console.WriteLine($"Adicionar matrículas no curso {nome}.");
            Console.WriteLine("Digite a quantidade de matrículas para registrar: ");
            int entrada = int.Parse(Console.ReadLine());
            vagas -= entrada;
            Console.WriteLine("Matrícula(s) registrada(s).");
            Console.ReadLine();
        }

        public void Exibir()
        {
            Console.WriteLine("Tipo: Curso");
            Console.WriteLine($"Nome: {autor}");
            Console.WriteLine($"Autor: {autor}");
            Console.WriteLine($"Preço: {preco}");
            Console.WriteLine($"Vagas disponíveis: {vagas}\n");
        }
    }
}
