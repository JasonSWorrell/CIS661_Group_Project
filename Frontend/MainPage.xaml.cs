using System.Collections.ObjectModel;

namespace Frontend
{
    public partial class MainPage : ContentPage
    {
        #region //TempVar
        string calcTotal;
        int num1;
        int num2;
        string option;
        int result;
        string[] stringInstruction;
        int[] binaryInstruction;

        public List<VisualMemBank> memoryBank { get; set; }
        public List<VisualRegisters> registerBank { get; set; }
        public string[] Instructions { get; set; }
        public ObservableCollection<VisualRegisters> DataItems { get; set; }
        //public List<Register> DataItems { get; set; }
        public Op_Code Program_Instance;
        public FileHandler FileMan;

        public class DataItem
        {
            
        }
        #endregion

        public MainPage()
        {
            //var memBank = this.memoryBank; 
            //memBank.Clear();
            Program_Instance = new Op_Code();
            registerBank = VisualRegisters.initRegisters(Program_Instance) ;
            DataItems = new ObservableCollection<VisualRegisters>(registerBank);
            FileMan = new FileHandler();

           var registers = this.registerBank; // current registers and their initial values
           //dataGridView4.DataSource = memBank;
           //DataItems.DataSource = registers;

            InitializeComponent();
            // Sample data for testing
            //DataItems = registers;
            BindingContext = this; // Set the BindingContext
        }

        #region //MENU
        // File Commands
        private void OpenCommand(object sender, EventArgs e) 
        {            
            FileMan.OpenFile();
        }
        private void NewFileCommand(object sender, EventArgs e) 
        {
         
        }
        public void ExitCommand(object sender, EventArgs e) 
        {
            Application.Current.Quit();
        }
        private void SaveCommand(object sender, EventArgs e) 
        {
            FileMan.SaveFile(Instructions);
        }
        private void EditCommand(object sender, EventArgs e) 
        {
            
        }
        // Appearance Editors
        private void DarkMode(object sender, EventArgs e) 
        {
            
        }
        private void LightMode(object sender, EventArgs e) 
        {
        
        }  
        // Debug Controls
        private void RunCommand(object sender, EventArgs e) 
        {
            Program_Instance.Perform_The_Op_Code(Instructions, Instructions.Count() - 1);
            Instructions = FileMan.InstructionList;
        } 
        private void DebugCommand(object sender, EventArgs e) 
        {
        
        }
        private void PauseCommand(object sender, EventArgs e) 
        {
        
        }
        #endregion

        #region //Text
        //Text Fillers
        private void OnEditorTextChanged(object sender, EventArgs e) 
        {
        
        }
        private void OnEditorCompleted(object sender, EventArgs e) 
        {
        
        }
        #endregion
    }
}