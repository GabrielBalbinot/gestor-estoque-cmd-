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
                        Console.Clear();
                        Listagem();
                        break;
                    case Menu.Adicionar:
                        Console.Clear();
                        Cadastro();
                        break;
                    case Menu.Remover:
                        Console.Clear();
                        Remover();
                        break;
                    case Menu.Entrada:
                        Console.Clear();
                        Entrada();
                        break;
                    case Menu.Saída:
                        Console.Clear();
                        Saida();
                        break;
                    case Menu.Zerar:
                        Console.Clear();
                        ZerarListagem();
                        break;
                    case Menu.Fechar:
                        fecharPrograma = true;
                        break;
                }
                Console.Clear();
            }
        }
        static void Listagem()
        {
            int id = 0;
            foreach (IEstoque produto in produtos)
            {
                Console.WriteLine($"ID: {id}");
                produto.Exibir();
                id++;
            }
            Console.ReadLine();
        }
        static void MostrarMenu()
        {
            Console.WriteLine("1- Listar produtos\n2- Adicionar produto\n3- Remover produto\n" +
                              "4- Entrada de produtos\n5- Saída de produtos\n6- Zerar listagem\n7- Fechar programa\n");
        }
        static void Entrada()
        {
            Listagem();
            Console.WriteLine("Digite o ID que deseja dar entrada:");            
            int id = int.Parse(Console.ReadLine());

            while (id < 0 || id > produtos.Count - 1)
            {
                Console.WriteLine($"Por favor digite um ID válido:");
                id = int.Parse(Console.ReadLine());
            }

            produtos[id].AdicionarEntrada();
            Salvar();

        }
        static void Saida()
        {
            Listagem();
            Console.WriteLine("Digite o ID que deseja dar baixa:");
            int id = int.Parse(Console.ReadLine());

            while (id < 0 || id > produtos.Count - 1)
            {
                Console.WriteLine($"Por favor digite um ID válido:");
                id = int.Parse(Console.ReadLine());
            }

            produtos[id].AdicionarSaida();
            Salvar();
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
            Listagem();
            Console.WriteLine("\nDigite o ID que deseja remover:");
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
            else
            {
                Console.WriteLine("Lista permanece inalterada.");
            }
            Console.ReadLine();
        }
    }
}
