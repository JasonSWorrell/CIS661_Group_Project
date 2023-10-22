// See https://aka.ms/new-console-template for more information

// input string then parse it into parsed input strings
// take parsed input string and change the register binarys to the output binarys
//Console.WriteLine("Hello, World!");

string stest1 = "zero";
string stest2 = "";
string stest3 = "";

int itest1 = 0;
int itest2 = 0;
int itest3 = 0;

string stemp1 = "";
string stemp2 = "";
string stemp3 = "";

int itemp1 = 0;
int itemp2 = 0;
int itemp3 = 0;

// Register Variables
/*
string Zero_Register = "0000";
string Stack_Pointer_Register = "0000";
string Temp_1_Register = "0000";
string Temp_2_Register = "0000";
string Temp_3_Register = "0000";
string Temp_4_Register = "0000";
string Saved_Values_1_Register = "0000";
string Saved_Values_2_Register = "0000";
string Saved_Values_3_Register = "0000";
string Saved_Values_4_Register = "0000";
string Return_Values_Register = "0000";
string Arguments_1_Register = "0000";
string Arguments_2_Register = "0000";
string Arguments_3_Register = "0000";
string Return_Address_Register = "0000";
string Kernel_Register = "0000";
*/

string szero = "0000000000000100";
string ssp   = "0000000000000000";
string st0   = "0000000000000001";
string st1   = "0000000000000000";
string st2   = "0000000000000000";
string st3   = "0000000000000000";
string ss0   = "0000000000000000";
string ss1   = "0000000000000100";
string ss2   = "0000000000000000";
string ss3   = "0000000000000000";
string sv    = "0000000000000000";
string sa0   = "0000000000000000";
string sa1   = "0000000000000000";
string sa2   = "0000000000000000";
string sra   = "0000000000000000";
string sk    = "0000000000000000";
//


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

//int i = int.Parse(szero);
//it0 = Convert.ToInt32(szero, 2);

//######################################
//Start Main Processing
//######################################

string Input_String = "add a0 t0 s1";
string[] Parsed_Input_String = Input_String.Split(' ');


Console.WriteLine(stest1);
itest1 = Get_The_Int_Value_Of_Register_X(stest1);
Console.WriteLine(itest1);
string sTest = Get_The_Binary_String_Value_Of_An_Int_Register_Numer(itest1);
Console.WriteLine(sTest);

if (Parsed_Input_String[0].Equals("add"))
{
    itemp1 = Get_The_Int_Value_Of_Register_X(Parsed_Input_String[2]);
    itemp2 = Get_The_Int_Value_Of_Register_X(Parsed_Input_String[3]);
    itemp3 = itemp1 + itemp2;
    stemp1 = Get_The_Binary_String_Value_Of_An_Int_Register_Numer(itemp3);
    Console.WriteLine(stemp1);
    Console.WriteLine("op code add");
}

//string[] words = Input_String.Split(' ');
/*
foreach (var word in Parsed_Input_String)
{
    System.Console.WriteLine($"{word}");
    
}
*/


