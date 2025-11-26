using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        string sourceDirectory = @"Z:\bence";

        
        string[] formatumok = { ".txt", ".pdf", ".xlsx", ".docx" };

        
        long meret = 1024 * 200;

        
        foreach (string a in formatumok)
        {
            string mmeret = a.TrimStart('.');

            Directory.CreateDirectory(Path.Combine(sourceDirectory, mmeret, "small"));
            Directory.CreateDirectory(Path.Combine(sourceDirectory, mmeret, "large"));
        }

        
        var fileok = Directory.GetFiles(sourceDirectory);

        foreach (var file in fileok)
        {
            string kiv = Path.GetExtension(file).ToLower();

            
            if (!formatumok.Contains(kiv))
                continue;

            long size = new FileInfo(file).Length;
            string a = kiv.TrimStart('.');

            
            string celmappa = size < meret ? "small" : "large";

            MoveFile(file, sourceDirectory, Path.Combine(a, celmappa));
        }

        Console.WriteLine("Fájlok sikeresen szétválogatva méret és típus szerint.");
    }
 
         static void MoveFile(string file, string root, string folder)
         {
                string celm = Path.Combine(root, folder);
                string celf = Path.Combine(celm, Path.GetFileName(file));

                try
                {
                    if (File.Exists(celf))
                        File.Delete(celf);  

                    File.Move(file, celf);

                    Console.WriteLine($"Áthelyezve: {file} -> {celf}");
                }
                catch (Exception ki)
                {
                    Console.WriteLine($"Hiba történt: {ki.Message}");
                }
         }

    }

