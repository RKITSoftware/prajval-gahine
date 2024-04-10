namespace FileChanges.cs
{
    internal class Program
    {
        private string _folderPath;

        static void Main(string[] args)
        {

            if (args[0] == "-replace")
            {
                ReplaceContent rc = new ReplaceContent(
                        args[1],
                        args[2],
                        args[3],
                        args[4] == "y"
                    );

                rc.Replace();
            }
            else if (args[0] == "-appendNS")
            {
                UpdateNamespace objUpdateNamespace = new UpdateNamespace(args[1], args[2]);
                //UpdateNamespace objUpdateNamespace = new UpdateNamespace(
                //    @"F:\prajval-gahine\prajval-gahine\06 Advance CSharp\FirmAdvanceDemo\FirmAdvanceDemo\Models\POCO",
                //    "POCO");
                objUpdateNamespace.AppendNamespace();
            }

            else if (args[0] == "-replaceClassName")
            {
                ReplaceClassName rcn = new ReplaceClassName(args[1], args[2], args[3] == "y");
            }
        }
    }
}