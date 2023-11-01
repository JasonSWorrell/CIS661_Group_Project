using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend
{

    public class Register
    {
        #region //ClassVariables
        private int val;
        private int iD;
        private string name;
        public int ID
        {
            get { return iD; }
            set
            {
                iD = value;

            }
        }
        public int Val
        {
            get { return val; }
            set
            {
                val = value;

            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;

            }
        }
        #endregion

        public Register(int _id, string _name, int _value) 
        {
            ID = _id;
            Name = _name;
            Val = _value;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        
        

        
        

        
        

        
    }
}
