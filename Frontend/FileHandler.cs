using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Frontend
{
    public class FileHandler : INotifyPropertyChanged
    {
        private List<string> instructionList;
        private string cacheDir;
        private string filePath;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string CacheDir { get; }
        public string FilePath { get; }
        public List<string> InstructionList {  get { return instructionList; } }    
        public FileHandler() 
        {
            instructionList = new List<string> { };
            //cacheDir = FileSystem.Current.CacheDirectory;
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
            if (file != null)
            {
                
                if (file.FileName.EndsWith("txt", StringComparison.OrdinalIgnoreCase))
                {
                    filePath = file.FullPath;
                    reader = new StreamReader(filePath);
                    instruction = "";
                    
                    while (instruction != null)
                    {
                        instruction = reader.ReadLine();
                        instructionList.Add(instruction);
                    }
                    reader.Close();                    
                }
            }
            //await DisplayAlert("Success", "File uploaded", "OK");
        }
        public List<string> initInstructions()
        {
            List<string> instructs = new List<string>();
            if(instructionList != null)
            {
                foreach (string Instruction in instructionList)
                {
                    instructs.Add(Instruction);
                }
                return instructs;
            }
            else
            {
                instructs.Add("Open a file or start code here.");
                return instructs;
            }
            
        }
    }
}
