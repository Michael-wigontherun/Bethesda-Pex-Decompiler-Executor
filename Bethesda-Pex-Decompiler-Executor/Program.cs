using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Bethesda_Pex_Decompiler_Executor
{
    public static class Program
    {
        public static string AssemblerFolder;
        public static List<string> scriptNameList = new List<string>();

        public static void Main(string[] args)
        {
            if (FoldersCheck())
            {
                Console.WriteLine("Hello World!!");
                ProcessDirectory(AssemblerFolder + @"\Scripts");
                foreach (string scriptName in scriptNameList)
                {
                    DecompileScript(scriptName);
                    MoveToOutput(scriptName);
                }
                ExtraActions();
            }
            else
            {
                Console.WriteLine("Press enter to exit.");
                Console.ReadLine();
            }
        }
        
        public static void ExtraActions()
        {
            string actionNumber = "";
            do
            {
                Console.WriteLine("\n\n\nFor extra actions type the number without \" or \"Exit\" or \"999\" to Exit program. " +
                    "\n      Any other input will be ignored and looped until exit code is inputted.");
                Console.WriteLine("Type \"1\" to open all decompiled files using Notpad.");
                Console.WriteLine("Type \"2\" to scan for the ability to eslify.");
                actionNumber = Console.ReadLine();
                switch (actionNumber)
                {
                    case "1" : 
                        OpenDecompiledFiles();
                        break;
                    case "2":
                        EslifyCheck();
                        break;
                    default:
                        break;
                }

            } while (actionNumber != "Exit" && actionNumber != "999");
        }

        public static void EslifyCheck()
        {
            List<string> decompiledScriptNameList = new List<string>();
            string[] fileEntries = Directory.GetFiles(AssemblerFolder + @"\Output");
            foreach (string fileName in fileEntries)
            {
                if (Path.GetExtension(fileName).ToLower().Equals(".pas"))
                {
                    decompiledScriptNameList.Add(fileName);
                }
            }

            foreach (string fileName in decompiledScriptNameList)
            {
                Console.WriteLine($"Scanning {Path.GetFileNameWithoutExtension(fileName)}");
                StreamReader file = new StreamReader(fileName);
                string s;
                while ((s = file.ReadLine()) != null)
                {
                    if (s.Contains("GetFormFromFile"))
                    {
                        Console.WriteLine(s);
                    }
                }
                file.Close();
                Console.WriteLine($"Scanning for {Path.GetFileNameWithoutExtension(fileName)} complete");
            }
            Console.WriteLine("If any files outputted a line from the decompiled scripts check if they contain the plugin name for the mod");
        }

        public static void OpenDecompiledFiles()
        {
            string[] fileEntries = Directory.GetFiles(AssemblerFolder + @"\Output");
            foreach (string fileName in fileEntries)
            {
                try
                {
                    Process myProcess = new Process();
                    myProcess.StartInfo.FileName = "Notepad.Exe";
                    myProcess.StartInfo.Arguments = fileName;
                    myProcess.Start();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        public static void MoveToOutput(string scriptName)
        {
            string orgFilePath = AssemblerFolder + $"\\Scripts\\{scriptName}.disassemble.pas";
            string newFilePath = AssemblerFolder + $"\\Output\\{scriptName}.disassemble.pas";
            try
            {
                Console.WriteLine("\"" + orgFilePath + "\" found.");
                File.Move(orgFilePath, newFilePath, true);
                Console.WriteLine("move to \"" + newFilePath + "\"");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        public static void DecompileScript(string scriptName)
        {
            Process Assembler = new Process();
            Assembler.StartInfo.FileName = AssemblerFolder + @"\PapyrusAssembler.exe";
            Assembler.StartInfo.Arguments = $"\"{scriptName}\" -D";
            Assembler.StartInfo.WorkingDirectory = AssemblerFolder + @"\Scripts";
            Assembler.EnableRaisingEvents = true;
            Assembler.Start();
            Assembler.WaitForExit();
        }

        public static void ProcessDirectory(string targetDirectory)
        {
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                ProcessFile(fileName);
            }
        }

        public static void ProcessFile(string path)
        {
            if (Path.GetExtension(path).ToLower().Equals(".pex"))
            {
                scriptNameList.Add(Path.GetFileNameWithoutExtension(path));
            }
            
        }

        public static bool FoldersCheck()
        {
            AssemblerFolder = Path.GetDirectoryName(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName));
            if (File.Exists(AssemblerFolder + @"\PapyrusAssembler.exe"))
            {
                Console.WriteLine(AssemblerFolder + @"\PapyrusAssembler.exe");
                Console.WriteLine("Exists.");
            }
            else
            {
                Console.WriteLine($"Decompiler not installed in correct folder should be something like \"{@"Skyrim Special Edition\Papyrus Compiler\Decompiler"}\"");
                return false;
            }

            if (Directory.Exists(AssemblerFolder + @"\Scripts"))
            {
                Console.WriteLine("Script Folder: " + AssemblerFolder + @"\Scripts");
                Console.WriteLine("Exists.");
            }
            else
            {
                Console.WriteLine("Script Folder: " + AssemblerFolder + @"\Scripts");
                Directory.CreateDirectory(AssemblerFolder + @"\Scripts");
                Console.WriteLine("Created.");
            }

            if (Directory.EnumerateFileSystemEntries(AssemblerFolder + @"\Scripts").Any())
            {
                Console.WriteLine("Script Folder: " + AssemblerFolder + @"\Scripts");
                Console.WriteLine("Has scripts to decompile");
            }
            else
            {
                Console.WriteLine("Script Folder: " + AssemblerFolder + @"\Scripts");
                Console.WriteLine("Has no scripts to decompile, Closing...");
                return false;
            }

            if (Directory.Exists(AssemblerFolder + @"\Output"))
            {
                Console.WriteLine("Output Folder: " + AssemblerFolder + @"\Output");
                Console.WriteLine("Exists.");
            }
            else
            {
                Console.WriteLine("Output Folder: " + AssemblerFolder + @"\Output");
                Directory.CreateDirectory(AssemblerFolder + @"\Output");
                Console.WriteLine("Created.");
            }
            
            return true;
        }
    }
}