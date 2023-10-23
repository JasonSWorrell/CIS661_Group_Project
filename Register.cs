using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LISCViewer
{

    public class Register
    {

        public Register() { }

        public int ID { get; set; }
        public string Name { get; set; }
        public int value { get; set; }
        //public List<Register> registers;

        public static string[] regCode = { "zero", "sp", "t0", "t1", "t2", "t3", "s0", "s1", "s2", "s3", "v", "a0", "a1", "a2", "ra", "ker" };



        public static List<Register> initRegisters()
        {
            List<Register> list = new List<Register>();

            for (int i = 0; i < 16; i++)
            {
                list.Add(new Register { ID = i, Name = regCode[i], value = 0 });
            }
            return list;
        }
    }
}
