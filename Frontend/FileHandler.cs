using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Frontend
{
    public class FileHandler
    {
        private string[] instructionList;
        private string cacheDir;
        private string filePath;

        public string CacheDir { get; }
        public string FilePath { get; }
        public string[] InstructionList {  get { return instructionList; } }    
        public FileHandler() 
        {
            cacheDir = FileSystem.Current.CacheDirectory;
            filePath = "";
        }
        public void SaveFile(string[] Instructions) 
        {
            StreamWriter writer = new StreamWriter(filePath);
            foreach(string Instruction in Instructions) 
            {
                writer.WriteLine(Instruction);
            }
            writer.Close();
        }
        public async void OpenFile() 
        {
            string instruction;
            StreamReader reader;
            var file = await FilePicker.Default.PickAsync();
            if (file != null)
            {
                if (file.FileName.EndsWith("txt", StringComparison.OrdinalIgnoreCase))
                {
                    file.OpenReadAsync().Wait();    
                    reader = new StreamReader(file.FullPath);
                    instruction = reader.ReadLine();
                    instructionList[0] = reader.ReadLine();
                    while (instruction != null)
                    {
                        instruction = reader.ReadLine();
                        instructionList.Append(instruction);
                    }
                    reader.Close();
                }
            }            
        }
    }
}
