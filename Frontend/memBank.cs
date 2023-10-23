using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend
{
    public class memBank
    {
        public string? address { get; set; }
        //public string? Offset0 {  get; set; }
        public int Offset0 { get; set; }
        public int Offset2 { get; set; }
        public int Offset4 { get; set; }
        public int Offset6 { get; set; }
        public int Offset8 { get; set; }
        public int Offset10 { get; set; }
        public int Offset12 { get; set; }
        public int Offset14 { get; set; }



        public static List<memBank> GetMemory()
        {
            List<memBank> list = new List<memBank>();

            int kernelOffset = 256;
            int maxMemory = 4096;

            for (int i = kernelOffset; i < maxMemory; i++)
            {
                list.Add(new memBank()
                {
                    address = "0x" + (i * 16).ToString("X4"),
                    Offset0 = 0,
                    Offset2 = 0,
                    Offset4 = 0,
                    Offset6 = 0,
                    Offset8 = 0,
                    Offset10 = 0,
                    Offset12 = 0,
                    Offset14 = 0
                });
            }
            return list;
        }
    }



}
