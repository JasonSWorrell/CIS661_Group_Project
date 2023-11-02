
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
        private void ExitCommand(object sender, EventArgs e) { }
        private void SaveCommand(object sender, EventArgs e) { }
        private void EditCommand(object sender, EventArgs e) { }
        // Appearance Editors
        private void DarkMode(object sender, EventArgs e) { }
        private void LightMode(object sender, EventArgs e) { }  
        // Debug Controls
        private void RunCommand(object sender, EventArgs e) { } 
        private void DebugCommand(object sender, EventArgs e) { }
        #endregion

        #region //Text
        //Text Fillers
        private void OnEditorTextChanged(object sender, EventArgs e) { }
        private void OnEditorCompleted(object sender, EventArgs e) { }
        #endregion

        #region //Action
         void onRun(object sender, EventArgs e)
                {
                    //BinaryTranslation.Text = Program.Text;
                    // txtDisplayTotal.Text = codeBox.Text;
                    Program.Unfocus();  // Force update of binding
                    String[] fullCode = Program.Text.Replace("\r", "\n").Split('\n'); // This is throwing an error, might just need to set it to an object instance. Though it looks like it might be.

                    BinaryTranslation.Text = "";

                    foreach (String line in fullCode)
                    {
                        if (line != "")
                        {
                            stringInstruction = line.Replace(", ", " ").Split(new char[] { ',', ' ', });
                            binaryInstruction = generateBinaryInstructions.GetBinary(stringInstruction);

                            int op = binaryInstruction[0];
                            int dest = binaryInstruction[1];
                            int r1 = binaryInstruction[2];
                            int r2 = binaryInstruction[3];
                            VisualRegisters Item1 = DataItems[dest];
                            VisualRegisters Item2 = DataItems[r1];
                            VisualRegisters Item3 = DataItems[r2];



                            //Binary Display translation of instructions
                    
                            BinaryTranslation.Text += Convert.ToString(op, 2).PadLeft(4, '0') + " ";
                            BinaryTranslation.Text += Convert.ToString(dest, 2).PadLeft(4, '0') + " ";
                            BinaryTranslation.Text += Convert.ToString(r1, 2).PadLeft(4, '0') + " ";
                            BinaryTranslation.Text += Convert.ToString(r2, 2).PadLeft(4, '0') + " ";
                            BinaryTranslation.Text += "\n" ;

                            int numb1 = (int)Item2.Val;
                            int numb2 = (int)Item3.Val;

                    
                            //int numb1 = (int)dataGridView1.Rows[r1].Cells[2].Value;
                            //int numb2 = (int)dataGridView1.Rows[r2].Cells[2].Value;

                            switch (op)
                            {
                                //add
                                case 6: DataItems[dest].Val = numb1 + numb2; break;
                                //sub
                                case 7: DataItems[dest].Val = numb1 - numb2; break;


                            }
                           // DataGridCollectionView.ItemsSource = null;
                            //DataGridCollectionView.ItemsSource = DataItems;
                            //  InitializeComponent();
                        }

                    }
                }
        #endregion

    }
}