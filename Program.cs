using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

internal class Program
{
    private static async Task Main(string[] args)
    {
        ThreadPool.QueueUserWorkItem(async state =>
        {
            await CreateFile(1);
        });

        ThreadPool.QueueUserWorkItem(async state =>
        {
            await CreateFile(2);
        });

        await Task.Delay(1000); 

        ThreadPool.QueueUserWorkItem(async state =>
        {
            await ReadFile(1);
        });

        ThreadPool.QueueUserWorkItem(async state =>
        {
            await ReadFile(2);
        });

        await Task.Delay(1000);

        Console.WriteLine("Les deux fichiers sont bien lus.");
        Console.WriteLine("Continuation du traitement");
    }

    private static async Task CreateFile(int a)
    {
        Console.WriteLine($"Début de l'écriture du fichier {a}");
        using (StreamWriter sw = File.CreateText($@"..\Async\test{a}.txt"))
        {
            for (int i = 0; i < 900; i++)
            {
                await sw.WriteLineAsync($"Line {i}");
            }
        }
        Console.WriteLine($"Fin de l'écriture dans le fichier {a}");
    }

    private static async Task ReadFile(int a)
    {
        Console.WriteLine($"Début de lecture du fichier {a}");
        using (FileStream fsSource = new FileStream($@"..\Async\test{a}.txt", FileMode.Open, FileAccess.Read))
        {
            using (StreamReader reader = new StreamReader(fsSource, Encoding.UTF8))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    DeuxOuPas(line);
                }
            }
        }
        Console.WriteLine($"Fin de la lecture du fichier {a}");
    }

    private static void DeuxOuPas(string text)
    {
        foreach (char c in text)
        {
            if (c == '2')
            {
                Console.WriteLine(text);
            }
        }
    }
}
