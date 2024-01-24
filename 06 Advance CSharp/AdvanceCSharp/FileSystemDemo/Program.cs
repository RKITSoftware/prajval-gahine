using System.Runtime;
using System.Text;

namespace FileSystemDemo
{
    internal class Program
    {
        static void CopyImage(string orignalFilePath, string copyingFilePath)
        {
            // create stream instance of original image file
            Stream stm = File.Open(orignalFilePath, FileMode.Open);
            int count = (int)stm.Length;

            // create binary reader instance on the above file stream instance
            BinaryReader br = new BinaryReader(stm, Encoding.UTF8);
            byte[] imgbytes = br.ReadBytes(count);

            // create stream instance of copying file with file mode create new
            Stream stm2 = File.Open(copyingFilePath, FileMode.Create);

            // write to the copying file using binary writer
            BinaryWriter bw = new BinaryWriter(stm2, Encoding.UTF8);

            bw.Write(imgbytes);
        }

        static void GetText(string textFilePath)
        {
            Stream stm = File.Open(textFilePath, FileMode.Open);
            int count = (int)stm.Length;
            using (BinaryReader br = new BinaryReader(stm, Encoding.UTF8))
            {
                //char[] chars = br.ReadChars(11);
                while (count > 0)
                {
                    Console.Write(br.ReadChar());
                    count--;
                }
            }
        }

        static void DatFileWriteRead(string datFilePath)
        {
            FileInfo fi = new FileInfo(datFilePath);
            using(BinaryWriter bw = new BinaryWriter(fi.OpenWrite()))
            {
                int intgr = 7;
                string str = "hello world";
                bw.Write(intgr);
                bw.Write(str);
            }

            using(BinaryReader br = new BinaryReader(fi.OpenRead()))
            {
                Console.WriteLine("\n\nDat file reading");
                Console.WriteLine(br.ReadInt32());
                Console.WriteLine(br.ReadString());
                Console.WriteLine("Dat file reading complete");
            }
        }
        static void Main(string[] args)
        {
            // drive info class
            DriveInfo[] lstDi = DriveInfo.GetDrives();
            foreach(DriveInfo di in lstDi){
                Console.WriteLine(di.Name);
            }
            DriveInfo drive = new DriveInfo("C:\\");

            Console.WriteLine("Drive name: " + drive.Name);
            Console.WriteLine("Drive total space (GB): " + drive.TotalSize / (1024 * 1024 * 1024));
            Console.WriteLine("Drive total free space (GB): " + drive.TotalFreeSpace / (1024 * 1024 * 1024));
            Console.WriteLine("Drive format: " + drive.DriveFormat);
            Console.WriteLine("Drive volumn label: " + drive.VolumeLabel);
            Console.WriteLine("Drive root dir: " + drive.RootDirectory);
            Console.WriteLine("Drive ready state: " + drive.IsReady + "\n\n");

            // directoryinfo class
            DirectoryInfo dirInfo = new DirectoryInfo(@"F:\prajval-gahine\fileSystemDemoTest");
            Console.WriteLine("Directory full name: " + dirInfo.FullName);
            Console.WriteLine("Directory root: " + dirInfo.Root);
            Console.WriteLine("Directory attributes: " + dirInfo.Attributes);
            Console.WriteLine("Directory creation time: " + dirInfo.CreationTime);
            Console.WriteLine("Directory name: " + dirInfo.Name);
            Console.WriteLine("Directory parent: " + dirInfo.Parent?.Name);
            Console.WriteLine("Directory exits: " + dirInfo.Exists + "\n\n");
            //dirInfo.GetFiles();

            // creates the specified directory with path if directory not exists
            //dirInfo.Create();

            // extending current directory path
            dirInfo.CreateSubdirectory(@"subdirectroy1\subdirectroy11");
            //dirInfo.Delete(@"subdirectory1\subsirectory11", false, true);
            //dirInfo.Delete(true);

            // reading and writing to a file
            string text = File.ReadAllText(dirInfo.FullName + @"\text.txt");
            Console.WriteLine("file content: " + text);

            // writing to file
            //File.WriteAllText(dirInfo.FullName + @"\text.txt", "hello world 2.0");

            GetText(dirInfo.FullName + @"\text.txt");

            // binary reader and writer
            // image file
            CopyImage(dirInfo.FullName + @"\subdirectroy1\image1.jpg", dirInfo.FullName + @"\subdirectroy1\subdirectroy11\image1.jpg");

            // binary reader and writer dat file
            DatFileWriteRead(dirInfo.FullName + @"\subdirectroy1\bFile.dat");
        }
    }
}