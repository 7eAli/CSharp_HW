using System.Collections;
using System.IO;
using System.Text;

namespace _8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                Console.WriteLine("Введите буквенное обозначение диска");
                string drive = Console.ReadLine()!.ToUpper() + @":\";
                if (Path.Exists(drive))
                {
                    string extension = '.' + args[0];
                    string files = FindSpecifiedFilesWithText(drive, extension, args[1]);
                    Console.WriteLine(files);                    
                }
            }
            else
            {
                Console.WriteLine("Введите буквенное обозначение диска");
                string drive = Console.ReadLine()!.ToUpper() + @":\";
                if (Path.Exists(drive))
                {
                    string files = FindSpecifiedFilesWithText(drive, ".txt", "asd");
                    Console.WriteLine(files);
                }
                
            }
            //foreach (string file in Directory.GetFiles(Directory.GetCurrentDirectory()))
            //{
            //    Console.WriteLine(Path.GetExtension(file));
            //}
            //Console.WriteLine(Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()));
        }
       

        static string FindSpecifiedFilesWithText(string driveName, string extension, string text)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (string file in Directory.GetFiles(driveName))
                {
                    
                    if (Path.GetExtension(file) == extension)
                    {
                        using (StreamReader sr = new StreamReader(file))
                        {                            
                            if (sr.ReadToEnd().Contains(text))
                            {
                                stringBuilder.Append(file);
                                stringBuilder.Append('\n');
                            }
                        }
                    }
                }
                foreach (string directory in Directory.GetDirectories(driveName))
                    stringBuilder.Append(FindSpecifiedFilesWithText(directory, extension, text));

                return stringBuilder.ToString();
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
