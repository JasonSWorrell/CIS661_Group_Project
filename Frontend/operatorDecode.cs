﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend
{
    public class FindOpcode
    {
        #region //ClassVariables
        public int GetDecodeValue () { return 0; }
        #endregion

        public int DecodeOpcode(String instruct)
        {
            string[] opCode = { "loadw", "storew", "sll", "slt", "isEqual", "biz", "add", "sub", "addi", "jal", "jr", "and", "or", "", "", "" };
            for (int i = 0; i < opCode.Length; i++)
            {
                if (instruct == opCode[i])
                {
                    return i;
                }
            }
            return -1; //opcode not found
        }
        public int DecodeValue(String instruct)
        {
            string[] regCode = { "zero", "sp", "t0", "t1", "t2", "t3", "s0", "s1", "s2", "s3", "v", "a0", "a1", "a2", "ra", "ker" };
            int i = 0;
            if (instruct[0] == '$') //This is a register
            {
                instruct = instruct.Replace("$", "");
            }
            while (i < regCode.Length)
            {
                if (instruct == regCode[i])
                {
                    return i;

                }
                i++;
            }
            return -1; // Value not found
        }
    }
}
