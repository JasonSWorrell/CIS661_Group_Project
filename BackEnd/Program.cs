// See https://aka.ms/new-console-template for more information

// input string then parse it into parsed input strings
// take parsed input string and change the register binarys to the output binarys

// I tried to find a way to just leave a comment on this, but commenting was the only way I could find quickly. I'll do better next time.
// Anyway... This is a really good start, We would need to create this into at least one class though and the class variables should have public and or private types.
// Ex. private string stemp1;
//     public string Stemp1 {get;} // set intentionally left out for the public class variable in this case.
// I would suggest doing both because we can control more about how the code in interacted in the GUI that way. It is possible to accidently alter the output. 

using System.Data.SqlTypes;

string stemp1 = "";
string stemp2 = "";
string stemp3 = "";

int itemp1 = 0;
int itemp2 = 0;
int itemp3 = 0;

string[] stack;

// Saved Register Variables
string szero = "0000000000000000";
string ssp = "0000000000000000";
string st0 = "0000000000000001";
string st1 = "0000000000000000";
string st2 = "0000000000000000";
string st3 = "0000000000000000";
string ss0 = "0000000000000000";
string ss1 = "0000000000000100";
string ss2 = "0000000000000000";
string ss3 = "0000000000000000";
string sv = "0000000000000000";
string sa0 = "0000000000000000";
string sa1 = "0000000000000000";
string sa2 = "0000000000000000";
string sra = "0000000000000000";
string sk = "0000000000000000";
//

string Input_String = "add a0 t0 s1";
string[] Parsed_Input_String = Input_String.Split(' ');

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

    return return_value;
}

string Get_The_Binary_String_Value_Of_An_Int_Register_Numer(int Register_int)
{
    string sbinary = Convert.ToString(Register_int, 2);
    int ibinary = int.Parse(sbinary);
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

}

void Push_To_Stack(int value)   // Pushes a Value to the Stack and Then Increments the Stack Pointer
{
    string binary_value = Get_The_Binary_String_Value_Of_An_Int_Register_Numer(value);
    int stack_pointer = Get_The_Int_Value_Of_Register_X(ssp);
    stack[stack_pointer] = binary_value;
    stack_pointer++;
    string binary_stack_pointer = Get_The_Binary_String_Value_Of_An_Int_Register_Numer(stack_pointer);
    ssp = binary_stack_pointer;
}

int Pull_From_Stack(int index)
{

}

void Perform_The_Op_Code(string[] Parsed_Input_Strin)
{
    if (Parsed_Input_String[0].Equals("add"))
    {
        // Pull int values to add
        itemp1 = Get_The_Int_Value_Of_Register_X(Parsed_Input_String[2]);
        itemp2 = Get_The_Int_Value_Of_Register_X(Parsed_Input_String[3]);

        // Do Addition
        itemp3 = itemp1 + itemp2;

        // Store Value into Register
        stemp1 = Get_The_Binary_String_Value_Of_An_Int_Register_Numer(itemp3);
        Store_Binary_String_Value_Into_A_Register_String_Variable(Parsed_Input_String[1], stemp1);

        // Debug Output
        Console.WriteLine(sa0);
    }
    else if (Parsed_Input_String[0].Equals("sub"))
    {
        // Pull int values to add
        itemp1 = Get_The_Int_Value_Of_Register_X(Parsed_Input_String[2]);
        itemp2 = Get_The_Int_Value_Of_Register_X(Parsed_Input_String[3]);

        // Do Subtraction
        itemp3 = itemp1 - itemp2;

        // Store Value into Register
        stemp1 = Get_The_Binary_String_Value_Of_An_Int_Register_Numer(itemp3);
        Store_Binary_String_Value_Into_A_Register_String_Variable(Parsed_Input_String[1], stemp1);

        // Debug Output
        Console.WriteLine(sa0);
    }
    else if (Parsed_Input_String[0].Equals("addi"))
    {
        // Pull int values to add
        itemp1 = Get_The_Int_Value_Of_Register_X(Parsed_Input_String[2]);
        itemp2 = int.Parse(Parsed_Input_String[3]);

        // Do Subtraction
        itemp3 = itemp1 + itemp2;

        // Store Value into Register
        stemp1 = Get_The_Binary_String_Value_Of_An_Int_Register_Numer(itemp3);
        Store_Binary_String_Value_Into_A_Register_String_Variable(Parsed_Input_String[1], stemp1);

        // Debug Output
        Console.WriteLine(sa0);
    }
}


//######################################
//Start Main Processing
//######################################

Perform_The_Op_Code(Parsed_Input_String);



























/*
Console.WriteLine(stest1);
itest1 = Get_The_Int_Value_Of_Register_X(stest1);
Console.WriteLine(itest1);
string sTest = Get_The_Binary_String_Value_Of_An_Int_Register_Numer(itest1);
Console.WriteLine(sTest);
*/


/*
if (Parsed_Input_String[0].Equals("add"))
{
    // Pull int values to add
    itemp1 = Get_The_Int_Value_Of_Register_X(Parsed_Input_String[2]);
    itemp2 = Get_The_Int_Value_Of_Register_X(Parsed_Input_String[3]);

    // Do Addition
    itemp3 = itemp1 + itemp2;

    // Store Value into Register
    stemp1 = Get_The_Binary_String_Value_Of_An_Int_Register_Numer(itemp3);
    Store_Binary_String_Value_Into_A_Register_String_Variable(Parsed_Input_String[1], stemp1);

    Console.WriteLine(sa0);
    //Console.WriteLine("op code add");
}

//string[] words = Input_String.Split(' ');
/*
foreach (var word in Parsed_Input_String)
{
    System.Console.WriteLine($"{word}");
    
}
*/
