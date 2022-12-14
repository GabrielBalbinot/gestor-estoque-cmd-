using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEstoque
{
    [System.Serializable]
    class ProdutoFisico : Produto, IEstoque
    {
        public float frete;
        private int estoque;
        public ProdutoFisico(string nome, float preco, float frete)
        {
            this.nome = nome;
            this.preco = preco;
            this.frete = frete;            
        }

        public void AdicionarEntrada()
        {
            Console.WriteLine($"Adicionar entrada no estoque para o produto {nome}.");
            Console.WriteLine("Digite a quantidade para dar entrada: ");
            int entrada = int.Parse(Console.ReadLine());
            estoque += entrada;
            Console.WriteLine("Entrada registrada.");
            Console.ReadLine();
        }

        public void AdicionarSaida()
        {
            Console.WriteLine($"Adicionar saída de estoque para o produto {nome}.");
            Console.WriteLine("Digite a quantidade de saídas para registrar: ");
            int entrada = int.Parse(Console.ReadLine());
            estoque -= entrada;
            Console.WriteLine("Saída registrada.");
            Console.ReadLine();
        }

        public void Exibir()
        {
            Console.WriteLine("Tipo: Produto físico");
            Console.WriteLine($"Nome: {nome}");
            Console.WriteLine($"Preço: {preco}");
            Console.WriteLine($"Frete: {frete}");
            Console.WriteLine($"Quantidade em estoque: {estoque} \n");
        }
    }
}
