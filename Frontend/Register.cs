using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend
{

    public class Register : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;

        private int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                if (_ID != value)
                {
                    _ID = value;
                    OnPropertyChanged(nameof(ID));
                }
            }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        private double _value;
        public double value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged(nameof(value));
                }
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //public List<Register> registers;

        public static string[] regCode = { "zero", "sp", "t0", "t1", "t2", "t3", "s0", "s1", "s2", "s3", "v", "a0", "a1", "a2", "ra", "ker" };


    public static List<Register> initRegisters()
        {
            List<Register> list = new List<Register>();

            for (int i = 0; i < 16; i++)
            {
                list.Add(new Register { ID = i, Name = regCode[i], value = i+3 });
            }
            return list;
        }
    }
}
