using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnectFourth
{
    public class BoardSpace
    {
        int _xPos, _yPos;
        public int XPos { get { return _xPos; } set { _xPos = value; } }
        public int YPos { get { return _yPos; } set { _yPos = value; } }
        string _spaceColor;
        public string SpaceColor {get {return _spaceColor; } set { _spaceColor = value; } }

        public BoardSpace()
        {
            _spaceColor = "";
        }

        public BoardSpace(string color)
        {
            _spaceColor = color;
        }

        public BoardSpace(string color, int xPos, int yPos)
        {
            _xPos = xPos;
            _yPos = yPos;
            _spaceColor = color;
        }
    }
}
