using ExeResolvLinq.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ExeResolvLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            //PEGAR O CAMINHO DO ARQUIVO .TXT
            Console.Write("Digite o caminho completo do arquivo: ");
            string path = Console.ReadLine();

            List<Produto> list = new List<Produto>();

            //LER O ARQUIVO E ADICIONAR OS ITENS EM UMA LISTA
            using (StreamReader sr = File.OpenText(path))
            {
                while (!sr.EndOfStream)
                {
                    string[] campos = sr.ReadLine().Split(',');
                    string nome = campos[0];
                    double preco = double.Parse(campos[1],CultureInfo.InvariantCulture);
                    list.Add(new Produto(nome, preco));
                }
            }

            //CALCULAR PREÇO MÉDIO
            var avg = list.Select(p => p.Preco).DefaultIfEmpty(0.0).Average();
            Console.WriteLine("Média de preço = " + avg.ToString("F2", CultureInfo.InvariantCulture));

            //LISTA DE NOMES DE PRODUTOS EM ORDEM DESCRESCENTE COM VALOR ABAIXO DA MÉDIA
            var nomes = list.Where(p => p.Preco < avg).OrderByDescending(p => p.Nome).Select(p => p.Nome);
            foreach (var item in nomes)
            {
                Console.WriteLine(item);
            }
        }
    }
}
