using System;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace LISCViewer
{

    public partial class Form1 : Form
    {

        string calcTotal;
        int num1;
        int num2;
        string option;
        int result;
        string[] stringInstruction;
        int[] binaryInstruction;


        public List<memBank> memoryBank { get; set; }
        public List<Register> registerBank { get; set; }

        public Form1()
        {
            memoryBank = memBank.GetMemory();
            registerBank = Register.initRegisters();
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var memBank = this.memoryBank; //memBank.Clear();
            var registers = this.registerBank; // current registers and their initial values
            dataGridView4.DataSource = memBank;
            dataGridView1.DataSource = registers;

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            txtDisplayTotal.Text = txtDisplayTotal.Text + btnSave.Text;
        }



        private void btnRun_Click(object sender, EventArgs e)
        {
            // txtDisplayTotal.Text = codeBox.Text;
            binaryDisplayCode.Clear();
            String[] fullCode = codeBox.Text.Split('\n');

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

                    //Binary Display translation of instructions
                    binaryDisplayCode.Text += Convert.ToString(op, 2).PadLeft(4, '0') + " ";
                    binaryDisplayCode.Text += Convert.ToString(dest, 2).PadLeft(4, '0') + " ";
                    binaryDisplayCode.Text += Convert.ToString(r1, 2).PadLeft(4, '0') + " ";
                    binaryDisplayCode.Text += Convert.ToString(r2, 2).PadLeft(4, '0') + " ";
                    binaryDisplayCode.Text += "\n";

                    int numb1 = (int)dataGridView1.Rows[r1].Cells[2].Value;
                    int numb2 = (int)dataGridView1.Rows[r2].Cells[2].Value;

                    switch (op)
                    {
                        //add
                        case 6: dataGridView1.Rows[dest].Cells[2].Value = numb1 + numb2; break;
                        //sub
                        case 7: dataGridView1.Rows[dest].Cells[2].Value = numb1 - numb2; break;

                        
                    }

                }


                // var LISCRunner = new LISCRunner();
                // LISCRunner.ComputeValues(stringInstruction, binaryInstruction);

            }
            // txtDisplayTotal.Text = txtDisplayTotal.Text + opClear.Text;

        }

        private void btnStep_Click(object sender, EventArgs e)
        {
            txtDisplayTotal.Text = txtDisplayTotal.Text + btnStep.Text;
        }



        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtDisplayTotal.Text = txtDisplayTotal.Text + btn1.Text;
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtDisplayTotal.Text = txtDisplayTotal.Text + btn2.Text;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtDisplayTotal.Text = txtDisplayTotal.Text + btn3.Text;
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtDisplayTotal.Text = txtDisplayTotal.Text + btn4.Text;
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtDisplayTotal.Text = txtDisplayTotal.Text + btn5.Text;
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtDisplayTotal.Text = txtDisplayTotal.Text + btn6.Text;
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtDisplayTotal.Text = txtDisplayTotal.Text + btn7.Text;
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtDisplayTotal.Text = txtDisplayTotal.Text + btn8.Text;
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtDisplayTotal.Text = txtDisplayTotal.Text + btn9.Text;
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            txtDisplayTotal.Text = txtDisplayTotal.Text + btn0.Text;
        }

        private void opSub_Click(object sender, EventArgs e)
        {
            //txtDisplayTotal.Text = txtDisplayTotal.Text + opSub.Text;
            option = opSub.Text; //"+"
            num1 = int.Parse(txtDisplayTotal.Text);

            txtDisplayTotal.Clear();
        }
        private void opAdd_Click(object sender, EventArgs e)
        {
            //txtDisplayTotal.Text = txtDisplayTotal.Text + opAdd.Text;
            option = opAdd.Text; //"+"
            num1 = int.Parse(txtDisplayTotal.Text);

            txtDisplayTotal.Clear();

        }

        private void opMul_Click(object sender, EventArgs e)
        {
            //txtDisplayTotal.Text = txtDisplayTotal.Text + opMul.Text;
            option = opMul.Text; //"+"
            num1 = int.Parse(txtDisplayTotal.Text);

            txtDisplayTotal.Clear();
        }

        private void opDiv_Click(object sender, EventArgs e)
        {
            //txtDisplayTotal.Text = txtDisplayTotal.Text + opDiv.Text;
            option = opDiv.Text; //"+"
            num1 = int.Parse(txtDisplayTotal.Text);

            txtDisplayTotal.Clear();
        }

        private void opMod_Click(object sender, EventArgs e)
        {
            //txtDisplayTotal.Text = txtDisplayTotal.Text + opMod.Text;
            option = opMod.Text; //"+"
            num1 = int.Parse(txtDisplayTotal.Text);

            txtDisplayTotal.Clear();
        }
        private void opEquals_Click(object sender, EventArgs e)
        {
            //txtDisplayTotal.Text = txtDisplayTotal.Text + opEquals.Text;
            num2 = int.Parse(txtDisplayTotal.Text);

            switch (option)
            {
                case "+": result = num1 + num2; break;
                case "-": result = num1 - num2; break;
                case "*": result = num1 * num2; break;
                case "/": result = num1 / num2; break;
                case "%": result = num1 % num2; break;
                default: throw new ArgumentException("op");

            }

            // calcTotal = result.ToString();
            txtDisplayTotal.Clear();
            txtDisplayTotal.Text = result.ToString();
        }
        private void opClear_Click(object sender, EventArgs e)
        {
            // txtDisplayTotal.Text = txtDisplayTotal.Text + opClear.Text;
            txtDisplayTotal.Clear();
            result = (0);
            num1 = 0;
            num2 = 0;
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox32_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox35_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox71_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox75_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox75_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView4_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView4_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void binaryDisplayCode_TextChanged(object sender, EventArgs e)
        {

        }
    }
}