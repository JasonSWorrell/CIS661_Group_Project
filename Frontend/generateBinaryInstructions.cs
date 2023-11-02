using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend

{
    public class GenerateBinaryInstructions
    {
        internal static int[] GetBinary(string[] rawInstruct )
        {
            var findOpcode = new FindOpcode();
            int[] binInstruct = new int[rawInstruct.Length];
            binInstruct[0] = findOpcode.DecodeOpcode(rawInstruct[0]);
            binInstruct[1] = findOpcode.DecodeValue(rawInstruct[1]);
            binInstruct[2] = findOpcode.DecodeValue(rawInstruct[2]);
            binInstruct[3] = findOpcode.DecodeValue(rawInstruct[3]);
            return binInstruct;
        }

    }
}
