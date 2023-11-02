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
    internal class FileHandler
    {
        List<string> InstructionList;
        string cacheDir;
        string filePath;
        public FileHandler() 
        {
            cacheDir = FileSystem.Current.CacheDirectory;
            InstructionList = new List<string>();
            filePath = "";
        }
        public void SaveFile(List<string> Instructions) 
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
            if (file == null)
            {
                if (file.FileName.EndsWith("txt", StringComparison.OrdinalIgnoreCase))
                {
                    file.OpenReadAsync().Wait();    
                    reader = new StreamReader(file.FullPath);
                    instruction = "Start";
                    while (instruction != null)
                    {
                        instruction = reader.ReadLine();
                        InstructionList.Add(instruction);
                    }
                    reader.Close();
                }
            }            
        }
    }
}
