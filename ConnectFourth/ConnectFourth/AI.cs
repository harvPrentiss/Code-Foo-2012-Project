using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnectFourth
{
    public class AI : Player
    {
        string _difficulty = "normal";
        public string Diffculty { get { return _difficulty; } set { _difficulty = value; } }

        public int PickSpot(GameBoard board, Player player)
        {
            int moveColumnOpp, moveColumnMy;
            moveColumnOpp = OpponentVictoryCheck(board, player);
            moveColumnMy = MyVictoryCheck(board);
            if (moveColumnMy != -1)
            {
                return moveColumnMy;
            }
            else if (moveColumnOpp != -1)
            {
                return moveColumnOpp;
            }
            else
            {
                return MyMove();
            }
        }

        private int OpponentVictoryCheck(GameBoard board, Player player)
        {
            int column = -1;

            if (_difficulty == "hard")
            {
                column = board.VictoryCheckSpotCheck(player, true);
            }
            else
            {
                column = board.VictoryCheckSpotCheck(player, false);
            }

            return column;
        }

        private int MyVictoryCheck(GameBoard board)
        {
            int column = -1;

            if (_difficulty == "hard")
            {
                column = board.VictoryCheckSpotCheck(this, true);
            }
            else
            {
                column = board.VictoryCheckSpotCheck(this, false);
            }

            return column;
        }

        private int MyMove()
        {
            Random rand = new Random();

            int spot = rand.Next(0, 7);

            return spot;
        }
    }
}
