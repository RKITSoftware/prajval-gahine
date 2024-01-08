using System;
using System.IO;

namespace CSharpBasicApp
{
    /// <summary>
    /// MyClassFileOperations demonstrate FileOperations in c#
    /// </summary>
    internal class MyClassFilOperations
    {
        static void Main(string[] args)
        {
            string rootPath = @"D:\rootFolder";

            string[] dirs = Directory.GetDirectories(rootPath, "*", SearchOption.AllDirectories);

            foreach (string dir in dirs)
            {
                Console.WriteLine(dir);
            }
            Console.WriteLine();

            var files = Directory.GetFiles(rootPath, "*.*", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                //Console.WriteLine(file);
                //Console.WriteLine(Path.GetFileName(file));
                //Console.WriteLine(Path.GetFileNameWithoutExtension(file));
                //Console.WriteLine(Path.GetDirectoryName(file));
                
                // extracting file info'
                var info = new FileInfo(file);
                Console.WriteLine($"{Path.GetFileName(file)}: {info.Length} bytes");

            }

            // verify whether the directory exits
            string newPath = @"D:\rootFolder\SubFolder3";
            bool isDirectoryExits = Directory.Exists(newPath);
            if (isDirectoryExits)
            {
                Console.WriteLine("Directory Exits");
            }
            else
            {
                Console.WriteLine("Directory Doestnot exits");
                Directory.CreateDirectory(newPath);
            }
            Console.WriteLine();

            // copying files
            string[] files1 = Directory.GetFiles(rootPath);
            string destinationFolder = @"D:\rootFolder\SubFolder1\NestedFolder";
            foreach(string file in files1)
            {
                Console.WriteLine(file);
                File.Copy(file, $"{destinationFolder}{ Path.GetFileName(file)}", true);
                // File.Move(file, $"{destinationFolder}{ Path.GetFileName(file)}");
            }

            ReadingFromFile();
        }
        #region Public Methods
        /// <summary>
        /// ReadingFromFile method reads the lines of file
        /// </summary>
        public static void ReadingFromFile()
        {
            string filePath = @"D:\rootFolder\file1.txt";
            List<string> lines = File.ReadAllLines(filePath).ToList();
            foreach(string line in lines)
            {
                Console.WriteLine(line);
            }

            lines.Add("Have a Nice Day");
            File.WriteAllLines(filePath, lines);
        }

        /// <summary>
        /// ReadingFileUsingStream demonstrate reading a file using stream
        /// </summary>
        public static void ReadingFileUsingStream()
        {
            StreamReader sr = new StreamReader(@"C:\Users\prajv\source\repos\CSharpBasicApp\Data.txt");
            Console.WriteLine(sr.ReadToEnd());
            sr.Close();
        }
        #endregion
    }
}
