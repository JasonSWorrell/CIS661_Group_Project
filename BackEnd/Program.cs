// See https://aka.ms/new-console-template for more information

// input string then parse it into parsed input strings
// take parsed input string and change the register binarys to the output binarys

// I tried to find a way to just leave a comment on this, but commenting was the only way I could find quickly. I'll do better next time.
// Anyway... This is a really good start, We would need to create this into at least one class though and the class variables should have public and or private types.
// Ex. private string stemp1;
//     public string Stemp1 {get;} // set intentionally left out for the public class variable in this case.
// I would suggest doing both because we can control more about how the code in interacted in the GUI that way. It is possible to accidently alter the output. 


using System;
using System.Data.SqlTypes;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;

string stemp1 = "";
string stemp2 = "";
string stemp3 = "";

int itemp1 = 0;
int itemp2 = 0;
int itemp3 = 0;

uint utemp1 = 0;
uint utemp2 = 0;
uint utemp3 = 0;

bool btemp1 = false;

// Memory Array - Stack in ISA goes to 200, but we are only using a 100 element array
// Conversions done in the push and pull functions
string[] stack = new string[100];

// Saved Register Variables
string szero = "0000000000000000";
string ssp   = "0000000011001000";  // initialized to the max value of the stack array: 200
string st0   = "0000000000000000";
string st1   = "0000000000000000";
string st2   = "0000000000000000";
string st3   = "0000000000000000";
string ss0   = "0000000000000000";
string ss1   = "0000000000000000";
string ss2   = "0000000000000000";
string ss3   = "0000000000000000";
string sv    = "0000000000000000";
string sa0   = "0000000000000000";
string sa1   = "0000000000000000";
string sa2   = "0000000000000000";
string sra   = "0000000000000000";
string sk    = "0000000000000000";

string spc   = "0000000000000000";
//
//string Input_String = "sll t3 4";
// Simple Swap Task
string[] Swap_Task = { "addi s0 5", "addi s1 7", "add t0 s1 zero", "add s1 s0 zero", "add s0 t0 zero" };
string[] Yes_Branch_Task_With_Label = { "addi s0 69", "addi s1 68", "slt t0 s0 s1", "biz t0 exit", "addi s1 20","exit", "addi s1 5"};
string[] Yes_Branch_Task_With_No_Label = { "addi s0 69", "addi s1 68", "slt t0 s0 s1", "biz t0 2", "addi s1 20", "addi s1 5" };
string[] No_Branch_Task_With_Label = { "addi s0 69", "addi s1 70", "slt t0 s0 s1", "biz t0 exit", "addi s1 20", "exit", "addi s1 5" };
string[] No_Branch_Task_With_No_Label = { "addi s0 69", "addi s1 70", "slt t0 s0 s1", "biz t0 2", "addi s1 20", "addi s1 5" };

string[] Jal_Task = {"addi s3 3", "biz zero skipahead", "func", "addi s2 2", "jr ra", "addi t0 5", "skipahead", "jal func", "addi s1 1"};

string[] Load_and_Store = { "addi sp -8", "addi t0 7", "storew t0 0(sp)", "add t0 zero zero", "addi t0 69", "storew t0 2(sp)", "add t0 zero zero", "addi t0 10", "storew t0 4(sp)", "add t0 zero zero", "addi t0 5", "storew t0 6(sp)", "add t0 zero zero", "addi t0 42", "storew t0 8(sp)", "add t0 zero zero", "add s0 sp zero", "addi a1 2", "addi a2 3", "addi ra 35", "swap:", "addi sp -2", "storew s0 0(sp)", "add t1 a1 zero", "sll t1 1", "add t1 t1 a0", "loadw s0 0(t1)", "add t2 a2 zero", "sll t2 1", "add t2 t2 a0", "loadw t0 0(t2)", "storew t0 0(t1)", "storew s0 0(t2)", "loadw s0 0(sp)", "addi sp 2"};


string Input_String = "and t3 t0 s1";
string[] Parsed_Input_String = Input_String.Replace("$","").Replace(",","").Split(' ');
string[] Label_Locations = new string[200];

int Is_Valid_Op_Code(string Op_Code)
{
    int return_value = 0;

    if (Op_Code.Equals("loadw"))
    {
        return_value = 1;
    }
    else if (Op_Code.Equals("storew"))
    {
        return_value = 1;
    }
    else if (Op_Code.Equals("sll"))
    {
        return_value = 1;
    }
    else if (Op_Code.Equals("slt"))
    {
        return_value = 1;
    }
    else if (Op_Code.Equals("isequal"))
    {
        return_value = 1;
    }
    else if (Op_Code.Equals("biz"))
    {
        return_value = 1;
    }
    else if (Op_Code.Equals("add"))
    {
        return_value = 1;
    }
    else if (Op_Code.Equals("sub"))
    {
        return_value = 1;
    }
    else if (Op_Code.Equals("addi"))
    {
        return_value = 1;
    }
    else if (Op_Code.Equals("jal"))
    {
        return_value = 1;
    }
    else if (Op_Code.Equals("jr"))
    {
        return_value = 1;
    }
    else if (Op_Code.Equals("and"))
    {
        return_value = 1;
    }
    else if (Op_Code.Equals("or"))
    {
        return_value = 1;
    }

    return return_value;
}

void Initialize_Label_Locations(string[] Labels, string[] Commands)
{

    for (int i = 0; i < Commands.Length; i++)
    {

        string[] Parsed_Commands = Commands[i].Replace("$", "").Replace(",", "").Split(' ');

        if (Is_Valid_Op_Code(Parsed_Commands[0]) == 1)
        {
            Labels[i] = "";
        }
        else
        {
            Labels[i] = Parsed_Commands[0];
        }

        //Console.WriteLine(Labels[i]);

    }
}

void Increment_The_PC_By_X(int Increment)
{
    itemp1 = Get_The_Int_Value_Of_Register_X("pc");
    itemp1 = itemp1 + Increment;
    stemp1 = Get_The_Binary_String_Value_Of_An_Int_Numer(itemp1);
    Store_Binary_String_Value_Into_A_Register_String_Variable("pc", stemp1);
}

// Add the word Decimal after int
int Get_The_Int_Value_Of_Register_X(string Register_String)
{
    int return_value = -69;
    if (Register_String.Equals("zero") || Register_String.Equals("0"))
    {
        return_value = Convert.ToInt32(szero, 2);
    }
    else if (Register_String.Equals("sp"))
    {
        return_value = Convert.ToInt32(ssp, 2);
    }
    else if (Register_String.Equals("t0"))
    {
        return_value = Convert.ToInt32(st0, 2);
    }
    else if (Register_String.Equals("t1"))
    {
        return_value = Convert.ToInt32(st1, 2);
    }
    else if (Register_String.Equals("t2"))
    {
        return_value = Convert.ToInt32(st2, 2);
    }
    else if (Register_String.Equals("t3"))
    {
        return_value = Convert.ToInt32(st3, 2);
    }
    else if (Register_String.Equals("s0"))
    {
        return_value = Convert.ToInt32(ss0, 2);
    }
    else if (Register_String.Equals("s1"))
    {
        return_value = Convert.ToInt32(ss1, 2);
    }
    else if (Register_String.Equals("s2"))
    {
        return_value = Convert.ToInt32(ss2, 2);
    }
    else if (Register_String.Equals("s3"))
    {
        return_value = Convert.ToInt32(ss3, 2);
    }
    else if (Register_String.Equals("v"))
    {
        return_value = Convert.ToInt32(sv, 2);
    }
    else if (Register_String.Equals("a0"))
    {
        return_value = Convert.ToInt32(sa0, 2);
    }
    else if (Register_String.Equals("a1"))
    {
        return_value = Convert.ToInt32(sa1, 2);
    }
    else if (Register_String.Equals("a2"))
    {
        return_value = Convert.ToInt32(sa2, 2);
    }
    else if (Register_String.Equals("ra"))
    {
        return_value = Convert.ToInt32(sra, 2);
    }
    else if (Register_String.Equals("k"))
    {
        return_value = Convert.ToInt32(sk, 2);
    }
    else if (Register_String.Equals("pc"))
    {
        return_value = Convert.ToInt32(spc, 2);
    }

    return return_value;
}

uint Get_The_Binary_Int_Value_Of_Register_X(string Register_String)
{
    uint return_value = 69;
    if (Register_String.Equals("zero") || Register_String.Equals("0"))
    {
        return_value = Convert.ToUInt32(szero, 2); //Convert.ToInt32(szero);
    }
    else if (Register_String.Equals("sp"))
    {
        return_value = Convert.ToUInt32(ssp, 2);
    }
    else if (Register_String.Equals("t0"))
    {
        return_value = Convert.ToUInt32(st0, 2);
    }
    else if (Register_String.Equals("t1"))
    {
        return_value = Convert.ToUInt32(st1, 2);
    }
    else if (Register_String.Equals("t2"))
    {
        return_value = Convert.ToUInt32(st2, 2);
    }
    else if (Register_String.Equals("t3"))
    {
        return_value = Convert.ToUInt32(st3, 2);
    }
    else if (Register_String.Equals("s0"))
    {
        return_value = Convert.ToUInt32(ss0, 2);
    }
    else if (Register_String.Equals("s1"))
    {
        return_value = Convert.ToUInt32(ss1, 2);
    }
    else if (Register_String.Equals("s2"))
    {
        return_value = Convert.ToUInt32(ss2, 2);
    }
    else if (Register_String.Equals("s3"))
    {
        return_value = Convert.ToUInt32(ss3, 2);
    }
    else if (Register_String.Equals("v"))
    {
        return_value = Convert.ToUInt32(sv, 2);
    }
    else if (Register_String.Equals("a0"))
    {
        return_value = Convert.ToUInt32(sa0, 2);
    }
    else if (Register_String.Equals("a1"))
    {
        return_value = Convert.ToUInt32(sa1, 2);
    }
    else if (Register_String.Equals("a2"))
    {
        return_value = Convert.ToUInt32(sa2, 2);
    }
    else if (Register_String.Equals("ra"))
    {
        return_value = Convert.ToUInt32(sra, 2);
    }
    else if (Register_String.Equals("k"))
    {
        return_value = Convert.ToUInt32(sk, 2);
    }
    else if (Register_String.Equals("pc"))
    {
        return_value = Convert.ToUInt32(spc, 2);
    }

    return return_value;
}


//remove the word Register from this function and add decimal word
string Get_The_Binary_String_Value_Of_An_Int_Numer(int Register_int)
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

string Get_The_Binary_String_Value_Of_A_Binary_Int_Number(int Binary_int)
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

void Store_Binary_String_Value_Into_A_Register_String_Variable(string Register, string Value)
{

    if (Register.Equals("zero") || Register.Equals("0"))
    {
        szero = Value;
    }
    else if (Register.Equals("sp"))
    {
        ssp = Value;
    }
    else if (Register.Equals("t0"))
    {
        st0 = Value;
    }
    else if (Register.Equals("t1"))
    {
        st1 = Value;
    }
    else if (Register.Equals("t2"))
    {
        st2 = Value;
    }
    else if (Register.Equals("t3"))
    {
        st3 = Value;
    }
    else if (Register.Equals("s0"))
    {
        ss0 = Value;
    }
    else if (Register.Equals("s1"))
    {
        ss1 = Value;
    }
    else if (Register.Equals("s2"))
    {
        ss2 = Value;
    }
    else if (Register.Equals("s3"))
    {
        ss3 = Value;
    }
    else if (Register.Equals("v"))
    {
        sv = Value;
    }
    else if (Register.Equals("a0"))
    {
        sa0 = Value;
    }
    else if (Register.Equals("a1"))
    {
        sa1 = Value;
    }
    else if (Register.Equals("a2"))
    {
        sa2 = Value;
    }
    else if (Register.Equals("ra"))
    {
        sra = Value;
    }
    else if (Register.Equals("k"))
    {
        sk = Value;
    }
    else if (Register.Equals("pc"))
    {
        spc = Value;
    }

}

void Push_To_Stack(int value, int offset, int index)   // Pushes a Value to the Stack and Then Increments the Stack Pointer
{
    string binary_value = Get_The_Binary_String_Value_Of_An_Int_Numer(value);   // convert value to be stored into a binary string 
    index = ((index + offset) / 2) - 1;  // get the index of the stack array to store binary string
    stack[index] = binary_value;
}

int Pull_From_Stack(int offset, int index)
{
    index = ((index + offset) / 2) - 1;  // get the index of the stack
    int value = Convert.ToInt32(stack[index], 2);
    return value;
}

void Perform_The_Op_Code(string[] Parsed_String, int Number_Of_Commands)
{
    if (Parsed_String[0].Equals("sll"))
    {
        // Pull uint value and int value
        utemp1 = Get_The_Binary_Int_Value_Of_Register_X(Parsed_String[1]);
        itemp2 = int.Parse(Parsed_String[2]);

        // Do Shift Left Logical
        utemp3 = utemp1 << itemp2;
        itemp3 = Convert.ToInt32(utemp3);

        // Store Value into Register
        stemp1 = Get_The_Binary_String_Value_Of_An_Int_Numer(itemp3);
        Store_Binary_String_Value_Into_A_Register_String_Variable(Parsed_String[1], stemp1);
        Increment_The_PC_By_X(1);

        // Debug Output
        Console.WriteLine(st3);

    }
    else if (Parsed_String[0].Equals("slt"))
    {
        // Pull int values
        itemp1 = Get_The_Int_Value_Of_Register_X(Parsed_String[2]);
        itemp2 = Get_The_Int_Value_Of_Register_X(Parsed_String[3]);

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
        Store_Binary_String_Value_Into_A_Register_String_Variable(Parsed_String[1], stemp1);
        Increment_The_PC_By_X(1);

        // Debug Output
        //Console.WriteLine(sa0);
    }
    else if (Parsed_String[0].Equals("isequal"))
    {
        // Pull int values
        itemp1 = Get_The_Int_Value_Of_Register_X(Parsed_String[2]);
        itemp2 = Get_The_Int_Value_Of_Register_X(Parsed_String[3]);

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
        Store_Binary_String_Value_Into_A_Register_String_Variable(Parsed_String[1], stemp1);
        Increment_The_PC_By_X(1);

        // Debug Output
        //Console.WriteLine(sa0);
    }
    else if (Parsed_String[0].Equals("biz"))
    {
        // Pull int values
        itemp1 = Get_The_Int_Value_Of_Register_X(Parsed_String[1]);
        itemp2 = 0;

        // Search for the PC count of the Label
        for (int i = 0; i < Number_Of_Commands; i++)
        {
            if (Parsed_String[2].Equals(Label_Locations[i]))
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
            itemp2 = int.Parse(Parsed_String[2]);
        }


        // Check if there is a branch or not
        if (itemp1.Equals(0))
        {
            // If a Valid label exists then change the PC counter to the label location
            // If no Valid label, increment the PC by the given int
            if (itemp3.Equals(1))
            {
                stemp1 = Get_The_Binary_String_Value_Of_An_Int_Numer(itemp2);
                Store_Binary_String_Value_Into_A_Register_String_Variable("pc", stemp1);
            }
            else if (itemp3.Equals(0))
            {
                Increment_The_PC_By_X(itemp2);
            }

        }
        else if (!itemp1.Equals(0))
        {
            Increment_The_PC_By_X(1);
        }

        // Debug Output
        //Console.WriteLine(sa0);

    }
    else if (Parsed_String[0].Equals("add"))
    {
        // Pull int values
        itemp1 = Get_The_Int_Value_Of_Register_X(Parsed_String[2]);
        itemp2 = Get_The_Int_Value_Of_Register_X(Parsed_String[3]);

        // Do Addition
        itemp3 = itemp1 + itemp2;

        // Store Value into Register
        stemp1 = Get_The_Binary_String_Value_Of_An_Int_Numer(itemp3);
        Store_Binary_String_Value_Into_A_Register_String_Variable(Parsed_String[1], stemp1);
        Increment_The_PC_By_X(1);

        // Debug Output
        //Console.WriteLine(sa0);

    }
    else if (Parsed_String[0].Equals("sub"))
    {
        // Pull int values
        itemp1 = Get_The_Int_Value_Of_Register_X(Parsed_String[2]);
        itemp2 = Get_The_Int_Value_Of_Register_X(Parsed_String[3]);

        // Do Subtraction
        itemp3 = itemp1 - itemp2;

        // Store Value into Register
        stemp1 = Get_The_Binary_String_Value_Of_An_Int_Numer(itemp3);
        Store_Binary_String_Value_Into_A_Register_String_Variable(Parsed_String[1], stemp1);
        Increment_The_PC_By_X(1);

        // Debug Output
        //Console.WriteLine(sa0);
    }
    else if (Parsed_String[0].Equals("addi"))
    {
        // Pull int values
        itemp1 = Get_The_Int_Value_Of_Register_X(Parsed_String[1]);
        itemp2 = int.Parse(Parsed_String[2]);

        // Do Subtraction
        itemp3 = itemp1 + itemp2;

        // Store Value into Register
        stemp1 = Get_The_Binary_String_Value_Of_An_Int_Numer(itemp3);
        Store_Binary_String_Value_Into_A_Register_String_Variable(Parsed_String[1], stemp1);
        Increment_The_PC_By_X(1);

        // Debug Output
        //Console.WriteLine(sa0);

    }
    else if (Parsed_String[0].Equals("storew"))
    {
        // Pull int values
        itemp1 = Get_The_Int_Value_Of_Register_X(Parsed_String[1]); // get value to be stored
        itemp2 = int.Parse(Parsed_String[2]); // offset
        itemp3 = Get_The_Int_Value_Of_Register_X(Parsed_String[3]); // get the address or index of the location to store value

        // Store Value
        Push_To_Stack(itemp1, itemp2, itemp3);
        Increment_The_PC_By_X(1);
    }
    else if (Parsed_String[0].Equals("loadw"))
    {
        // Pull int values
        itemp1 = int.Parse(Parsed_String[2]);   // offset
        itemp2 = Get_The_Int_Value_Of_Register_X(Parsed_String[3]); // get the address or index of the location for the value to be loaded

        // Load Value
        itemp3 = Pull_From_Stack(itemp1, itemp2);

        // Store Value Into Register
        stemp1 = Get_The_Binary_String_Value_Of_An_Int_Numer(itemp3);
        Store_Binary_String_Value_Into_A_Register_String_Variable(Parsed_String[1], stemp1);
        Increment_The_PC_By_X(1);
    }
    else if (Parsed_String[0].Equals("jal"))
    {
        // Set itemp2 to 0 so 
        itemp2 = 0;

        // Store the next PC number into the ra register
        itemp3 = Get_The_Int_Value_Of_Register_X("pc");
        itemp3 = itemp3 + 1;
        stemp1 = Get_The_Binary_String_Value_Of_An_Int_Numer(itemp3);
        Store_Binary_String_Value_Into_A_Register_String_Variable("ra", stemp1);

        // Search for the PC count of the Label
        for (int i = 0; i < Number_Of_Commands; i++)
        {
            if (Parsed_String[1].Equals(Label_Locations[i]))
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
            itemp2 = int.Parse(Parsed_String[2]);
        }

        // If a Valid label exists then change the PC counter to the label location
        // If no Valid label, increment the PC by the given int
        if (itemp3.Equals(1))
        {
            stemp1 = Get_The_Binary_String_Value_Of_An_Int_Numer(itemp2);
            Store_Binary_String_Value_Into_A_Register_String_Variable("pc", stemp1);
        }
        else if (itemp3.Equals(0))
        {
            // Branch the PC to the given register
            Increment_The_PC_By_X(itemp2);
        }

        // Debug Output
        //Console.WriteLine(sa0);

    }
    else if (Parsed_String[0].Equals("jr"))
    {
        // Pull int values
        itemp1 = Get_The_Int_Value_Of_Register_X(Parsed_String[1]);

        // Change the PC to the given register
        stemp1 = Get_The_Binary_String_Value_Of_An_Int_Numer(itemp1);
        Store_Binary_String_Value_Into_A_Register_String_Variable("pc", stemp1);

        // Debug Output
        //Console.WriteLine(sa0);

    }
    else if (Parsed_String[0].Equals("and"))
    {
        // Pull uint value and int value
        utemp1 = Get_The_Binary_Int_Value_Of_Register_X(Parsed_String[2]);
        utemp2 = Get_The_Binary_Int_Value_Of_Register_X(Parsed_String[3]);

        // Do Bitwise And then convert to an int32 value
        utemp3 = utemp1 & utemp2;
        itemp3 = Convert.ToInt32(utemp3);

        // Store Value into Register
        stemp1 = Get_The_Binary_String_Value_Of_An_Int_Numer(itemp3);
        Store_Binary_String_Value_Into_A_Register_String_Variable(Parsed_String[1], stemp1);
        Increment_The_PC_By_X(1);

        // Debug Output
        //Console.WriteLine(st3);

    }
    else if (Parsed_String[0].Equals("or"))
    {
        // Pull uint value and int value
        utemp1 = Get_The_Binary_Int_Value_Of_Register_X(Parsed_String[2]);
        utemp2 = Get_The_Binary_Int_Value_Of_Register_X(Parsed_String[3]);

        // Do Shift Left Logical
        utemp2 = utemp1 | utemp2;
        itemp3 = Convert.ToInt32(utemp2);

        // Store Value into Register
        stemp1 = Get_The_Binary_String_Value_Of_An_Int_Numer(itemp3);
        Store_Binary_String_Value_Into_A_Register_String_Variable(Parsed_String[1], stemp1);
        Increment_The_PC_By_X(1);

        // Debug Output
        Console.WriteLine(ss2);
    }
    else
    {
        Increment_The_PC_By_X(1);
    }

}


//######################################
//Start Main Processing
//######################################

void Debut_Prints(string Command)
{
    Console.WriteLine("");
    Console.WriteLine(Command);
    Console.WriteLine("PC = " + Get_The_Int_Value_Of_Register_X("pc"));
    Console.WriteLine("s1 = " + ss1);
    Console.WriteLine("s2 = " + ss2);
    Console.WriteLine("s3 = " + ss3);
    Console.WriteLine("t0 = " + st0);
    Console.WriteLine("ra = " + sra);
}

Console.WriteLine("Initialize");
Console.WriteLine("PC = " + spc);
Console.WriteLine("s1 = " + ss1);
Console.WriteLine("s2 = " + ss2);
Console.WriteLine("s3 = " + ss3);
Console.WriteLine("t0 = " + st0);
Console.WriteLine("ra = " + sra);

Initialize_Label_Locations(Label_Locations, Load_and_Store);

for (int i = 0; i < Load_and_Store.Length; i = Get_The_Int_Value_Of_Register_X("pc"))
{
    Console.ReadLine();
    string[] Parsed_Command = Load_and_Store[i].Replace("$", "").Replace(",", "").Replace("(", " ").Replace(")", "").Split(' ');
    Perform_The_Op_Code(Parsed_Command, Load_and_Store.Length);
    Debut_Prints(Load_and_Store[i].Replace("$", "").Replace(",", ""));
}

//Print Stack for Debug
//Console.WriteLine();
//for (int i = 99; i > 49; i--)
//{
//    Console.Write("[");
//    Console.Write(i);
//    Console.Write("] = ");
//    Console.WriteLine(stack[i]);
//}

//Print Stack for Debug with Binary Conversion and Index Conversion
Console.WriteLine();
for (int i = 99; i > 73; i--)
{
    Console.Write("[");
    Console.Write((i * 2) + 2); // print converted index
    Console.Write("] = ");
    Console.WriteLine(Convert.ToInt32(stack[i], 2));
}
Console.WriteLine();
Console.Write("Stack Pointer = ");
Console.WriteLine((Get_The_Binary_Int_Value_Of_Register_X("ssp") * 2) + 2);
