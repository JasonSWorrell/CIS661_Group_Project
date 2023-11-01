using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend
{
    public class DataItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region //ClassVariables
        private string name;
        private int iD;
        private double val;
        public int ID { get { return iD; } set { if (iD != value) { iD = value; OnPropertyChanged(nameof(ID)); } } }
        public string Name { get { return name; } set { if (name != value) { name = value; OnPropertyChanged(nameof(Name)); } } }
        public double Val { get { return val; } set { if (val != value) {val = value; OnPropertyChanged(nameof(value)); } } }
        #endregion

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
