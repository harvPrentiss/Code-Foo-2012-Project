using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ConnectFourth
{
    public class Player : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(String property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        string _name;
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged("Name"); } }

        string _color;
        public string Color { get { return _color; } set { _color = value; OnPropertyChanged("Color"); } }

        public Player()
        {
            _name = "Dingus";
            _color = "R";
        }

        public Player(string name, string color)
        {
            _name = name;
            _color = color;
        }
    }
}
