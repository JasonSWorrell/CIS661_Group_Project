using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend

{
    public class generateBinaryInstructions
    {
        internal static int[] GetBinary(string[] rawInstruct )
        {
            var findOpcode = new findOpcode();
            int[] binInstruct = new int[rawInstruct.Length];
            binInstruct[0] = findOpcode.decodeOpcode(rawInstruct[0]);
            binInstruct[1] = findOpcode.decodeValue(rawInstruct[1]);
            binInstruct[2] = findOpcode.decodeValue(rawInstruct[2]);
            binInstruct[3] = findOpcode.decodeValue(rawInstruct[3]);
            return binInstruct;
        }

    }
}
