using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend
{
    public class VisualMemBank
    {
        #region //ClassVariables
        private string address;
        private int offset0;
        private int offset2;
        private int offset4;
        private int offset6;
        private int offset8;
        private int offset10;
        private int offset12;
        private int offset14;

        public string? Address { get; set; }
        public int Offset0 { get; set; }
        public int Offset2 { get; set; }
        public int Offset4 { get; set; }
        public int Offset6 { get; set; }
        public int Offset8 { get; set; }
        public int Offset10 { get; set; }
        public int Offset12 { get; set; }
        public int Offset14 { get; set; }
        #endregion

        public static List<VisualMemBank> GetMemory()
        {
            List<VisualMemBank> list = new List<VisualMemBank>();
            int kernelOffset = 256;
            int maxMemory = 4096;
            for (int i = kernelOffset; i < maxMemory; i++)
            {
                list.Add(new VisualMemBank()
                {
                    address = "0x" + (i * 16).ToString("X4"),
                    offset0 = 0,
                    offset2 = 0,
                    offset4 = 0,
                    offset6 = 0,
                    offset8 = 0,
                    offset10 = 0,
                    offset12 = 0,
                    offset14 = 0
                });
            }
            return list;
        }
    }
}
