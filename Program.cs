using System;
using System.IO;
using System.Text;

internal class Program
{
    private async static Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        await createFile();
        Console.WriteLine("Continuation du traitement");
    }



    private async static Task createFile()
    {

        Console.WriteLine("Début de la lecture du fichier");
        using (FileStream fs = File.Create(@"C:\Users\Nathan-Efficom\Desktop\cours\algo\Async\test.txt", 1024)) { }
        using (StreamWriter sw = File.CreateText(@"C:\Users\Nathan-Efficom\Desktop\cours\algo\Async\test.txt"))
        {
            for (int i = 0; i < 9990000; i++)
            {
                await sw.WriteLineAsync($"Line {i}");
            }
        }
        Console.WriteLine("Fin de l'écriture dans le fichier");
    }


}

