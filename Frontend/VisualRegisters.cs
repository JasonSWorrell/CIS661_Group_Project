using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
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
        private int val;
        public int ID { get { return iD; } set { if (iD != value) { iD = value; OnPropertyChanged(nameof(ID)); } } }
        public string Name { get { return name; } set { if (name != value) { name = value; OnPropertyChanged(nameof(Name)); } } }       
        public int Val { get { return val; } set { if (val != value) { val = value; OnPropertyChanged(nameof(value)); } } }
        #endregion

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public VisualRegisters(int _id, string _name, int _value)
        {
            ID = _id;
            Name = _name;
            Val = _value;
        }
        public static string[] regCode = { "zero", "sp", "t0", "t1", "t2", "t3", "s0", "s1", "s2", "s3", "v", "a0", "a1", "a2", "ra", "ker" };
        
    }
}
