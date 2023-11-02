using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend
{

    public class VisualRegisters : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        #region //ClassVariables
        private int iD;
        private string name;
        private double val;
        public int ID { get { return iD; } set { if (iD != value) { iD = value; OnPropertyChanged(nameof(ID)); } } }
        public string Name { get { return name; } set { if (name != value) { name = value; OnPropertyChanged(nameof(Name)); } } }       
        public double Val { get { return val; } set { if (val != value) { val = value; OnPropertyChanged(nameof(value)); } } }
        #endregion

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //public List<Register> registers;
        public static string[] regCode = { "zero", "sp", "t0", "t1", "t2", "t3", "s0", "s1", "s2", "s3", "v", "a0", "a1", "a2", "ra", "ker" };
        public static List<VisualRegisters> initRegisters() //TODO: Refactor based on inputs from program.
        {
            List<VisualRegisters> list = new List<VisualRegisters>();
            for (int i = 0; i < 16; i++)
            {
                list.Add(new VisualRegisters { iD = i, name = regCode[i], val = i + 3 });
            }
            return list;
        }
    }
}
