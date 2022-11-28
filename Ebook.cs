using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEstoque
{
    [System.Serializable]
    class Ebook : Produto, IEstoque
    {
        public string autor;
        private int vendas;
        public Ebook(string nome, float preco, string autor)
        {
            this.nome = nome;
            this.preco = preco;
            this.autor = autor;            
        }

        public void AdicionarEntrada()
        {
            
        }

        public void AdicionarSaída()
        {
           
        }

        public void Exibir()
        {
            Console.WriteLine("Tipo: Ebook");
            Console.WriteLine($"Nome: {nome}");
            Console.WriteLine($"Autor: {autor}");
            Console.WriteLine($"Preço: {preco}");
            Console.WriteLine($"Vendas em todo período: {vendas}\n");
        }
    }
}
