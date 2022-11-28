using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace GestorEstoque
{
     class Program
    {
        static List<IEstoque> produtos = new List<IEstoque>();
        enum Menu { Lista = 1, Adicionar, Remover, Entrada, Saída, Zerar, Fechar}
        static void Main(string[] args)
        {
            Carregar();
            bool fecharPrograma = false;

            while (fecharPrograma != true)
            {
                Console.WriteLine("Escolha uma opção abaixo:\n");
                MostrarMenu();
                int opcaoInt = int.Parse(Console.ReadLine());
                while (opcaoInt < 1 || opcaoInt > 7)
                {
                    Console.Clear();
                    Console.WriteLine("Por favor, escolha apenas uma das opções abaixo:\n");
                    MostrarMenu();
                    opcaoInt = int.Parse(Console.ReadLine());
                }
                Menu opcao = (Menu)opcaoInt;

                switch (opcao)
                {
                    case Menu.Lista:
                        Listagem();
                        break;
                    case Menu.Adicionar:
                        Cadastro();
                        break;
                    case Menu.Remover:
                        Remover();
                        break;
                    case Menu.Entrada:
                        break;
                    case Menu.Saída:
                        break;
                    case Menu.Zerar:
                        ZerarListagem();
                        break;
                    case Menu.Fechar:
                        fecharPrograma = true;
                        break;
                }
            }
        }
        static void Listagem()
        {
            Console.Clear();
            int id = 0;
            foreach (IEstoque produto in produtos)
            {
                Console.WriteLine($"ID: {id}");
                produto.Exibir();
                id++;
            }
            Console.WriteLine("\nDigite ENTER para voltar ao menu.");
            Console.ReadLine();
            Console.Clear();
        }
        static void MostrarMenu()
        {
            Console.WriteLine("1- Listar produtos\n2- Adicionar produto\n3- Remover produto\n" +
                              "4- Entrada de produtos\n5- Saída de produtos\n6- Zerar listagem\n7- Fechar programa\n");
        }
        static void Cadastro()
        {
            Console.WriteLine("Escolha qual produto deseja cadastrar:");
            Console.WriteLine("1- Produto Físico\n2- Ebook\n3- Curso");
            int escolha = int.Parse(Console.ReadLine());
            switch (escolha)
            {
                case 1:
                    CadastroPF();
                    break;
                case 2:
                    CadastroEBook();
                    break;
                case 3:
                    CadastroCurso();
                    break;
            }
            Console.Clear();
        }
        static void CadastroPF()
        {
            Console.WriteLine("Cadastrando novo produto físico:");
            Console.Write("Nome: ");
            string nome = Console.ReadLine();
            Console.Write("Preço: ");
            float preco = float.Parse(Console.ReadLine());
            Console.Write("Valor do frete: ");
            float frete = float.Parse(Console.ReadLine());
            ProdutoFisico pf = new ProdutoFisico(nome, preco, frete);
            produtos.Add(pf);
            Salvar();
        }
        static void CadastroEBook()
        {
            Console.WriteLine("Cadastrando Ebook:");
            Console.Write("Nome: ");
            string nome = Console.ReadLine();
            Console.Write("Autor: ");
            string autor = Console.ReadLine();
            Console.Write("Preço: ");
            float preco = float.Parse(Console.ReadLine());
            Ebook ebook = new Ebook(nome, preco, autor);
            produtos.Add(ebook);
            Salvar();
        }
        static void CadastroCurso()
        {
            Console.WriteLine("Cadastrando Ebook:");
            Console.Write("Nome: ");
            string nome = Console.ReadLine();
            Console.Write("Autor: ");
            string autor = Console.ReadLine();
            Console.Write("Preço: ");
            float preco = float.Parse(Console.ReadLine());
            Curso curso = new Curso(nome, preco, autor);
            produtos.Add(curso);
            Salvar();
        }
        static void Remover()
        {
            Console.Clear();

            Console.WriteLine("Digite o ID que deseja remover:");
            int id = int.Parse(Console.ReadLine());

            while (id < 0 || id > produtos.Count - 1)
            {
                Console.WriteLine($"Por favor digite um ID válido entre 0 e {produtos.Count - 1}");
                id = int.Parse(Console.ReadLine());
            }

            Console.WriteLine();

            produtos[id].Exibir();
            Console.WriteLine("Tem certeza que deseja remover este produto da listagem?\n1- Sim\n2- Não");

            int confirmar = int.Parse(Console.ReadLine());
            if (confirmar == 1) 
            {
                produtos.RemoveAt(id);
                Console.WriteLine("Produto excluído com sucesso. Pressione ENTER para voltar ao menu.");
                Salvar();
            }
            else
            {
                Console.WriteLine("Pressione ENTER para voltar ao menu.");
            }

            Console.ReadLine();
            Console.Clear();
        }
        static void Salvar()
        {
            FileStream stream = new FileStream("produtos.dat", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            encoder.Serialize(stream, produtos);

            stream.Close();
        }
        static void Carregar()
        {
            FileStream stream = new FileStream("produtos.dat", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            try 
            {
                produtos = (List<IEstoque>)encoder.Deserialize(stream);
                if (produtos == null)
                {
                    produtos = new List<IEstoque>();
                }
            }
            catch
            {
                produtos = new List<IEstoque>();
            }
            stream.Close();
        }
        static void ZerarListagem()
        {
            Console.WriteLine("Você está prestes a excluir TODOS os dados salvos na lista de produtos.");
            Console.WriteLine("Tem absoluta certeza que deseja prosseguir?");
            Console.WriteLine("1- Sim\n2- Não");

            int exluirLista = int.Parse(Console.ReadLine());

            if (exluirLista == 1)
            {
                produtos.Clear();
                Salvar();
                Console.WriteLine("Dados salvos na lista apagados com sucesso!");
            }
            
            Console.WriteLine("Pressione ENTER para voltar ao menu.");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
