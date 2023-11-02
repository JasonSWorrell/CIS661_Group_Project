namespace Frontend
{
    public class Op_Code
    {
        #region //ClassVariables
        static Register SZERO, SSP, ST0, ST1, ST2, ST3, SS0, SS1, SS2, SS3, SV, SA0, SA1, SA2, SRA, SK, SPC;
        private string[] Op_Code_List = new string[13] { "loadw", "storew", "sll", "slt", "isequal", "biz", "add", "sub", "addi", "jal", "jr", "and", "or" };
        private string[] Commands = new string[11] { "sll", "slt", "isequal", "biz", "add", "sub", "addi", "jal", "jr", "and", "or" };
        readonly List<Register> Register_List = new List<Register> { SZERO, SSP, ST0, ST1, ST2, ST3, SS0, SS1, SS2, SS3, SV, SA0, SA1, SA2, SRA, SK, SPC };
        // Private temp Variables
        private string stemp1;
        private string stemp2;
        private string stemp3;
        private int itemp1;
        private int itemp2;
        private int itemp3;
        private uint utemp1;
        private uint utemp2;
        private uint utemp3;
        private bool btemp1;
        // Memory Array
        private string[] stack;
        private string[] Label_Locations = new string[200];
        #endregion

        public Op_Code()
        {
            stemp1 = "";
            stemp2 = "";
            stemp3 = "";
            itemp1 = 0;
            itemp2 = 0;
            itemp3 = 0;
            utemp1 = 0;
            utemp2 = 0;
            utemp3 = 0;
            btemp1 = false;

            // Memory Array - Stack in ISA goes to 200, but we are only using a 100 element array
            // Conversions done in the push and pull functions
            string[] stack = new string[100];

            SZERO = new Register(0, "zero", 0);
            SSP = new Register(1, "sp", 200);
            ST0 = new Register(2, "t0", 0);
            ST1 = new Register(3, "t1", 0);
            ST2 = new Register(4, "t2", 0);
            ST3 = new Register(5, "t3", 0);
            SS0 = new Register(6, "s0", 0);
            SS1 = new Register(7, "s1", 0);
            SS2 = new Register(8, "s2", 0);
            SS3 = new Register(9, "s3", 0);
            SV = new Register(10, "v", 0);
            SA0 = new Register(11, "a0", 0);
            SA1 = new Register(12, "a1", 0);
            SA2 = new Register(13, "a2", 0);
            SRA = new Register(14, "ra", 0);
            SK = new Register(15, "k", 0);

            SPC = new Register(16, "pc", 0);
        }
        private bool Is_Valid_Op_Code(string Op_Code)
        {
            int itr = 0;
            while (itr < 13)
            {
                if (Op_Code == Op_Code_List[itr])
                {
                    return true;
                }
                itr++;
            }
            return false;
        }
        private void Initialize_Label_Locations(string[] Labels, string[] Commands)
        {
            for (int i = 0; i < Commands.Length; i++)
            {
                string[] Parsed_Commands = Commands[i].Replace("$", "").Replace(",", "").Split(' ');
                if (Is_Valid_Op_Code(Parsed_Commands[0]))
                {
                    Labels[i] = "";
                }
                else
                {
                    Labels[i] = Parsed_Commands[0];
                }
            }
        }
        private void Itterate_PC(int itr)
        {
            itemp1 = SPC.Val;
            itemp1 += itr;
            stemp1 = Get_The_Binary_String_Value_Of_An_Int_Numer(itemp1);
            StoreBinString2Reg(SPC, stemp1);
        }
        private Register GetReg(string register)
        {
            foreach (Register reg in Register_List)
            {
                if (reg.Name == register)
                {
                    return reg;
                }
            }
            return null;
        }
        // Add the word Decimal after int
        private int Get_RegVal_IfValid(Register register)
        {
            foreach (Register reg in Register_List)
            {
                if (reg.Name == register.Name)
                {
                    return register.Val;
                }
            }
            return -69;
        }
        private uint Get_The_Binary_Int_Value_Of_Register_X(Register register)
        {
            {
                foreach (Register reg in Register_List)
                {
                    if (reg.Name == register.Name)
                    {
                        return Convert.ToUInt32(reg.Val);
                    }
                }
                return 69;
            }
        }
        //remove the word Register from this function and add decimal word
        private string Get_The_Binary_String_Value_Of_An_Int_Numer(int Register_int)
        {
            string sbinary = Convert.ToString(Register_int, 2);
            long ibinary = long.Parse(sbinary);
            string return_value = "-69";
            string s = "{0:";
            for (int i = 0; i < 16; i++)
            {
                s += "0";
            }
            s += "}";
            return_value = string.Format(s, ibinary);
            return return_value;
        }
        public string Get_The_Binary_String_Value_Of_A_Binary_Int_Number(int Binary_int)
        {
            string sbinary = Convert.ToString(Binary_int);
            long ibinary = long.Parse(sbinary);
            string return_value = "-69";
            string s = "{0:";
            for (int i = 0; i < 16; i++)
            {
                s += "0";
            }
            s += "}";
            return_value = string.Format(s, ibinary);
            return return_value;
        }
        private void StoreBinString2Reg(Register register, string reg_value)
        {
            foreach (Register reg in Register_List)
            {
                if (reg.Name == register.Name || reg.Name == "0")
                {
                    register.Val = Convert.ToInt32(reg_value);
                }
            }
        }
        private void Push_To_Stack(int value, int offset, int index)   // Pushes a Value to the Stack and Then Increments the Stack Pointer
        {
            string binary_value = Get_The_Binary_String_Value_Of_An_Int_Numer(value);   // convert value to be stored into a binary string 
            index = ((index + offset) / 2) - 1;  // get the index of the stack array to store binary string
            stack[index] = binary_value;
        }
        private int Pull_From_Stack(int offset, int index)
        {
            index = ((index + offset) / 2) - 1;  // get the index of the stack
            int value = Convert.ToInt32(stack[index], 2);
            return value;
        }
        #region //Operators
        private void SLL(string[] Cmds)
        {
            Register reg = GetReg(Cmds[1]);
            // Pull uint value and int value
            utemp1 = Get_The_Binary_Int_Value_Of_Register_X(reg);
            itemp2 = int.Parse(Cmds[2]);
            // Do Shift Left Logical
            utemp3 = utemp1 << itemp2;
            itemp3 = Convert.ToInt32(utemp3);
            // Store Value into Register
            stemp1 = Get_The_Binary_String_Value_Of_An_Int_Numer(itemp3);
            StoreBinString2Reg(reg, stemp1);
            Itterate_PC(1);
        }
        private void SLT(string[] Cmds)
        {
            Register reg1 = GetReg(Cmds[1]);
            Register reg2 = GetReg(Cmds[2]);
            Register reg3 = GetReg(Cmds[3]);
            // Pull int values
            int itemp1 = Get_RegVal_IfValid(reg2);
            itemp2 = Get_RegVal_IfValid(reg3);
            // Do less than Check
            if (itemp1 < itemp2)
            {
                itemp3 = 1;
            }
            else if (itemp1 > itemp2)
            {
                itemp3 = 0;
            }
            // Store Value into Register
            stemp1 = Get_The_Binary_String_Value_Of_An_Int_Numer(itemp3);
            StoreBinString2Reg(reg1, stemp1);
            Itterate_PC(1);
        }
        private void ISEQUAL(string[] Cmds)
        {
            Register reg1 = GetReg(Cmds[1]);
            Register reg2 = GetReg(Cmds[2]);
            Register reg3 = GetReg(Cmds[3]);
            // Pull int values
            itemp1 = Get_RegVal_IfValid(reg2);
            itemp2 = Get_RegVal_IfValid(reg3);
            // Do Equal Check
            if (itemp1.Equals(itemp2))
            {
                itemp3 = 1;
            }
            else if (!itemp1.Equals(itemp2))
            {
                itemp3 = 0;
            }
            // Store Value into Register
            stemp1 = Get_The_Binary_String_Value_Of_An_Int_Numer(itemp3);
            StoreBinString2Reg(reg1, stemp1);
            Itterate_PC(1);
        }
        private void BIZ(string[] Cmds, int Number_Of_Commands)
        {
            Register reg1 = GetReg(Cmds[1]);
            // Pull int values
            itemp1 = Get_RegVal_IfValid(reg1);
            itemp2 = 0;
            // Search for the PC count of the Label
            for (int i = 0; i < Number_Of_Commands; i++)
            {
                if (Cmds[2].Equals(Label_Locations[i]))
                {
                    itemp2 = i + 1;
                    itemp3 = 1;
                    break;
                }
            }
            // If there is no valid Label assume the value is an offset integer
            if (itemp2.Equals(0))
            {
                itemp3 = 0;
                itemp2 = int.Parse(Cmds[2]);
            }
            // Check if there is a branch or not
            if (itemp1.Equals(0))
            {
                // If a Valid label exists then change the PC counter to the label location
                // If no Valid label, increment the PC by the given int
                if (itemp3.Equals(1))
                {
                    stemp1 = Get_The_Binary_String_Value_Of_An_Int_Numer(itemp2);
                    StoreBinString2Reg(SPC, stemp1);
                }
                else if (itemp3.Equals(0))
                {
                    Itterate_PC(itemp2);
                }
            }
            else if (!itemp1.Equals(0))
            {
                Itterate_PC(1);
            }
        }
        private void ADD(string[] Cmds)
        {
            Register reg1 = GetReg(Cmds[1]);
            Register reg2 = GetReg(Cmds[2]);
            Register reg3 = GetReg(Cmds[3]);
            // Pull int values
            itemp1 = Get_RegVal_IfValid(reg2);
            itemp2 = Get_RegVal_IfValid(reg3);
            // Do Addition
            itemp3 = itemp1 + itemp2;
            // Store Value into Register
            stemp1 = Get_The_Binary_String_Value_Of_An_Int_Numer(itemp3);
            StoreBinString2Reg(reg1, stemp1);
            Itterate_PC(1);
        }
        private void SUB(string[] Cmds)
        {
            Register reg1 = GetReg(Cmds[1]);
            Register reg2 = GetReg(Cmds[2]);
            Register reg3 = GetReg(Cmds[3]);
            // Pull int values
            itemp1 = Get_RegVal_IfValid(reg2);
            itemp2 = Get_RegVal_IfValid(reg3);
            // Do Subtraction
            itemp3 = itemp1 - itemp2;
            // Store Value into Register
            stemp1 = Get_The_Binary_String_Value_Of_An_Int_Numer(itemp3);
            StoreBinString2Reg(reg1, stemp1);
            Itterate_PC(1);
        }
        private void ADDI(string[] Cmds)
        {
            Register reg1 = GetReg(Cmds[1]);
            Register reg3 = GetReg(Cmds[3]);
            // Pull int values
            itemp1 = Get_RegVal_IfValid(reg1);
            itemp2 = int.Parse(Cmds[2]);
            // Do Subtraction
            itemp3 = itemp1 + itemp2;
            // Store Value into Register
            stemp1 = Get_The_Binary_String_Value_Of_An_Int_Numer(itemp3);
            StoreBinString2Reg(reg1, stemp1);
            Itterate_PC(1);
        }
        private void JAL(string[] Cmds, int Number_Of_Commands)
        {
            Register register = GetReg(Cmds[1]);
            // Set itemp2 to 0 so 
            itemp2 = 0;
            // Store the next PC number into the ra register
            itemp3 = Get_RegVal_IfValid(SPC);
            itemp3 = itemp3 + 1;
            stemp1 = Get_The_Binary_String_Value_Of_An_Int_Numer(itemp3);
            StoreBinString2Reg(SRA, stemp1);
            // Search for the PC count of the Label
            for (int i = 0; i < Number_Of_Commands; i++)
            {
                if (register.Name == Label_Locations[i])
                {
                    itemp2 = i + 1;
                    itemp3 = 1;
                    break;
                }
            }
            // If there is no valid Label assume the value is an offset integer
            if (itemp2.Equals(0))
            {
                itemp3 = 0;
                itemp2 = int.Parse(Cmds[2]);
            }

            // If a Valid label exists then change the PC counter to the label location
            // If no Valid label, increment the PC by the given int
            if (itemp3.Equals(1))
            {
                stemp1 = Get_The_Binary_String_Value_Of_An_Int_Numer(itemp2);
                StoreBinString2Reg(SPC, stemp1);
            }
            else if (itemp3.Equals(0))
            {
                // Branch the PC to the given register
                Itterate_PC(itemp2);
            }

            // Debug Output
            //Console.WriteLine(sa0);

        }
        private void JR(string[] Cmds)
        {
            Register reg = GetReg(Cmds[1]);
            // Pull int values
            itemp1 = Get_RegVal_IfValid(reg);
            // Change the PC to the given register
            stemp1 = Get_The_Binary_String_Value_Of_An_Int_Numer(itemp1);
            StoreBinString2Reg(SPC, stemp1);
        }
        private void AND(string[] Cmds)
        {
            Register reg1 = GetReg(Cmds[1]);
            Register reg2 = GetReg(Cmds[2]);
            Register reg3 = GetReg(Cmds[3]);
            // Pull uint value and int value
            utemp1 = Get_The_Binary_Int_Value_Of_Register_X(reg2);
            utemp2 = Get_The_Binary_Int_Value_Of_Register_X(reg3);
            // Do Bitwise And then convert to an int32 value
            utemp3 = utemp1 & utemp2;
            itemp3 = Convert.ToInt32(utemp3);
            // Store Value into Register
            stemp1 = Get_The_Binary_String_Value_Of_An_Int_Numer(itemp3);
            StoreBinString2Reg(reg1, stemp1);
            Itterate_PC(1);
        }
        private void OR(string[] Cmds)
        {
            Register reg1 = GetReg(Cmds[1]);
            Register reg2 = GetReg(Cmds[2]);
            Register reg3 = GetReg(Cmds[3]);
            // Pull uint value and int value
            utemp1 = Get_The_Binary_Int_Value_Of_Register_X(reg2);
            utemp2 = Get_The_Binary_Int_Value_Of_Register_X(reg3);
            // Do Shift Left Logical
            utemp2 = utemp1 | utemp2;
            itemp3 = Convert.ToInt32(utemp2);
            // Store Value into Register
            stemp1 = Get_The_Binary_String_Value_Of_An_Int_Numer(itemp3);
            StoreBinString2Reg(reg1, stemp1);
            Itterate_PC(1);
        }
        #endregion
        public void Perform_The_Op_Code(string[] Parsed_String, int Number_Of_Commands)
        {
           switch (Parsed_String[0]) 
            {
                case "sll":
                    SLL(Parsed_String);
                    break;
                case "slt":
                    SLT(Parsed_String);
                    break;
                case "isequal":
                    ISEQUAL(Parsed_String);
                    break;
                case "biz":
                    BIZ(Parsed_String, Number_Of_Commands);
                    break;
                case "add":
                    ADD(Parsed_String);
                    break;
                case "sub":
                    SUB(Parsed_String);
                    break;
                case "addi":
                    ADDI(Parsed_String);
                    break;
                case "jal":
                    JAL(Parsed_String, Number_Of_Commands);
                    break;
                case "jr":
                    JR(Parsed_String);
                    break;
                case "and":
                    AND(Parsed_String);
                    break;
                case "or":
                    OR(Parsed_String);
                    break;
                default:
                    Itterate_PC(1);
                    break;
            }
        }
       
    }
}