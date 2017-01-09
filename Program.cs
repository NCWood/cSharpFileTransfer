using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyFileTransfer
{

    class Program
    {
        
        public static void fTransfer(string srcFolder, string dstFolder)
        {      
            DirectoryInfo dirInfoSrc = new DirectoryInfo(srcFolder);
            DirectoryInfo dirInfoDst = new DirectoryInfo(dstFolder);

            CheckFile(dirInfoSrc, dirInfoDst);
        }
        
        //Method checks files within source folder for creation/modification time/date & copies to destination folder
        public static void CheckFile(DirectoryInfo source, DirectoryInfo destination)
        {

            Directory.CreateDirectory(destination.FullName);

            //Set for time 24 hours prior to...
            DateTime aDayOld = DateTime.Now.AddHours(-24);
            //...current time/transfer request time
            DateTime tTime = DateTime.Now;
            Console.WriteLine("The following files have been copied to the Destination Folder:\n");
         
            foreach (FileInfo fInfo in source.GetFiles("*.txt"))
            {
                var created = fInfo.CreationTime;
                var modified = fInfo.LastWriteTime;
                
                if (created > aDayOld || modified > aDayOld)
                {
                    Console.WriteLine(@"Copying: {0}\{1}", destination.FullName, fInfo.Name);
                    Console.WriteLine("Copied: {0}", tTime);
                    Console.WriteLine("Created: {0}", created);
                    Console.WriteLine("Modified: {0}", modified);
                    Console.WriteLine("-----------------------------\n");
                    //Copies files meeting criteria (cre8/mod within 24 hrs)
                    fInfo.CopyTo(Path.Combine(destination.FullName, fInfo.Name), true);
                }
            
            }
            
            /*foreach (DirectoryInfo srcSubDir in source.GetDirectories())
            {
                DirectoryInfo dstSubDir = destination.CreateSubdirectory(srcSubDir.Name);
                CheckFile(srcSubDir, dstSubDir);
            }*/

        }
                

        static void Main(string[] args)
        {
            string srcFolder = @"C:\\Users\\nicho\\Desktop\\cSharpSrc\\";
            string dstFolder = @"C:\\Users\\nicho\\Desktop\\cSharpDst\\";

            fTransfer(srcFolder, dstFolder);
                      
        }
    }
}
