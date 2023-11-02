
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

        public ObservableCollection<VisualRegisters> DataItems { get; set; }
        //public List<Register> DataItems { get; set; }

        public class DataItem
        {
  
        }
        #endregion

        public MainPage()
        {
            // var memBank = this.memoryBank; //memBank.Clear();
            registerBank = VisualRegisters.initRegisters();
            DataItems = new ObservableCollection<VisualRegisters>(registerBank);

          //  var registers = this.registerBank; // current registers and their initial values
           // dataGridView4.DataSource = memBank;
            //DataItems.DataSource = registers;

            InitializeComponent();
            // Sample data for testing
           // DataItems = registers;
            BindingContext = this; // Set the BindingContext
        }

        #region //MENU
        // File Commands
        private void OpenCommand(object sender, EventArgs e) { }
        private void NewFileCommand(object sender, EventArgs e) { }
        private void ExitCommand(object sender, EventArgs e) { }
        private void SaveCommand(object sender, EventArgs e) { }
        private void EditCommand(object sender, EventArgs e) { }
        // Appearance Editors
        private void DarkMode(object sender, EventArgs e) { }
        private void LightMode(object sender, EventArgs e) { }  
        // Debug Controls
        private void RunCommand(object sender, EventArgs e) 
        {
            int i = 0;
            i++;
        } 
        private void DebugCommand(object sender, EventArgs e) { }
        private void PauseCommand(object sender, EventArgs e) { }
        #endregion

        #region //Text
        //Text Fillers
        private void OnEditorTextChanged(object sender, EventArgs e) { }
        private void OnEditorCompleted(object sender, EventArgs e) { }
        #endregion
    }
}