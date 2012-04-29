using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnectFourth
{
    public class GameBoard
    {
        BoardSpace[,] _board;
        public BoardSpace[,] Board { get { return _board; } }
        BoardSpace[] _winningSpots;
        public BoardSpace[] WinningSpots { get { return _winningSpots; } }
        const int BOARD_WIDTH = 7;
        const int BOARD_HEIGHT = 6;
        bool _firstSet = true;

        public GameBoard()
        {
            _board = new BoardSpace[BOARD_WIDTH, BOARD_HEIGHT];
            _winningSpots = new BoardSpace[4];
        }

        public string VictoryCheck()
        {
            foreach (BoardSpace b in _board)
            {
                if (b.SpaceColor != "-")
                {
                    if (HorizontalCheck(b) || VerticalCheck(b) || DiagonalCheck(b))
                    {
                        return b.SpaceColor;
                    }

                }
            }
            return "None";
        }

        public int VictoryCheckSpotCheck(Player player, bool hardCheck)
        {
            int spot = -1;

            foreach (BoardSpace b in _board)
            {
                if (b.SpaceColor == player.Color)
                {
                    if (OpenHorizontalCheck(b) || OpenVerticalCheck(b) || OpenDiagonalCheck(b))
                    {
                        spot = _winningSpots[3].XPos;
                    }
                    if (hardCheck)
                    {
                        if (MidOpenHorizontalCheck(b) || MidOpenVerticalCheck(b) || MidOpenDiagonalCheck(b))
                        {
                            spot = _winningSpots[2].XPos;
                        }
                        if (MidBackOpenHorizontalCheck(b) || MidBackOpenVerticalCheck(b) || MidBackOpenDiagonalCheck(b))
                        {
                            spot = _winningSpots[1].XPos;
                        }
                    }
                }
            }

            return spot;
        }

        public void SetBoard()
        {
            if (_firstSet)
            {
                for (int i = 0; i < BOARD_HEIGHT; i++)
                {
                    for (int j = 0; j < BOARD_WIDTH; j++)
                    {
                        _board[j, i] = new BoardSpace("-", j, i);
                    }
                }
                _firstSet = false;
            }
            else
            {
                foreach (BoardSpace b in _board)
                {
                    b.SpaceColor = "-";
                }
            }
            _winningSpots[0] = null;
            _winningSpots[1] = null;
            _winningSpots[2] = null;
            _winningSpots[3] = null;
        }

        public BoardSpace GetSpace(int x, int y)
        {
            if (x >= 0 && x < BOARD_WIDTH && y >= 0 && y < BOARD_HEIGHT)
            {
                return _board[x, y];
            }
            else
            {
                return new BoardSpace("OOB");
            }
        }

        public bool MakeMove(int column, string color)
        {
            for (int i = BOARD_HEIGHT - 1; i >= 0; i--)
            {
                if (_board[column, i].SpaceColor == "-")
                {
                    _board[column, i].SpaceColor = color;
                    return true;
                }
            }
            return false;
        }

        #region Horizontal Checks

        // Checks for the following pattern horizontally "XXXX"
        bool HorizontalCheck(BoardSpace space)
        {
            bool win = true;
            for (int i = 1; i < 4; i++)
            {
                if (GetSpace(space.XPos + i, space.YPos).SpaceColor != space.SpaceColor)
                {
                    win = false;
                }
            }

            if (win)
            {
                _winningSpots[0] = space;
                _winningSpots[1] = GetSpace(space.XPos + 1, space.YPos);
                _winningSpots[2] = GetSpace(space.XPos + 2, space.YPos);
                _winningSpots[3] = GetSpace(space.XPos + 3, space.YPos);
            }

            return win;
        }

        // Checks for the following pattern horizontally "XXX-"
        bool OpenHorizontalCheck(BoardSpace space)
        {
            bool rightWin = false;
            bool leftWin = false;
            if (GetSpace(space.XPos + 1, space.YPos).SpaceColor == space.SpaceColor && GetSpace(space.XPos + 2, space.YPos).SpaceColor == space.SpaceColor &&
                GetSpace(space.XPos + 3, space.YPos).SpaceColor == "-")
            {
                rightWin = true;
            }
            if (GetSpace(space.XPos - 1, space.YPos).SpaceColor == space.SpaceColor && GetSpace(space.XPos - 2, space.YPos).SpaceColor == space.SpaceColor &&
                GetSpace(space.XPos - 3, space.YPos).SpaceColor == "-")
            {
                leftWin = true;
            }

            if (rightWin)
            {
                _winningSpots[0] = space;
                _winningSpots[1] = GetSpace(space.XPos + 1, space.YPos);
                _winningSpots[2] = GetSpace(space.XPos + 2, space.YPos);
                _winningSpots[3] = GetSpace(space.XPos + 3, space.YPos);
            }

            if (leftWin)
            {
                _winningSpots[0] = space;
                _winningSpots[1] = GetSpace(space.XPos - 1, space.YPos);
                _winningSpots[2] = GetSpace(space.XPos - 2, space.YPos);
                _winningSpots[3] = GetSpace(space.XPos - 3, space.YPos);
            }

            if (rightWin || leftWin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Checks for the following pattern horizontally "XX-X"
        bool MidOpenHorizontalCheck(BoardSpace space)
        {
            bool rightWin = false;
            bool leftWin = false;

            if (GetSpace(space.XPos + 1, space.YPos).SpaceColor == space.SpaceColor && GetSpace(space.XPos + 2, space.YPos).SpaceColor == "-" &&
                GetSpace(space.XPos + 3, space.YPos).SpaceColor == space.SpaceColor)
            {
                rightWin = true;
            }
            if (GetSpace(space.XPos - 1, space.YPos).SpaceColor == space.SpaceColor && GetSpace(space.XPos - 2, space.YPos).SpaceColor == "-" &&
                GetSpace(space.XPos - 3, space.YPos).SpaceColor == space.SpaceColor)
            {
                leftWin = true;
            }

            if (rightWin)
            {
                _winningSpots[0] = space;
                _winningSpots[1] = GetSpace(space.XPos + 1, space.YPos);
                _winningSpots[2] = GetSpace(space.XPos + 2, space.YPos);
                _winningSpots[3] = GetSpace(space.XPos + 3, space.YPos);
            }

            if (leftWin)
            {
                _winningSpots[0] = space;
                _winningSpots[1] = GetSpace(space.XPos - 1, space.YPos);
                _winningSpots[2] = GetSpace(space.XPos - 2, space.YPos);
                _winningSpots[3] = GetSpace(space.XPos - 3, space.YPos);
            }

            if (rightWin || leftWin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Checks for the following pattern horizontally "X-XX"
        bool MidBackOpenHorizontalCheck(BoardSpace space)
        {
            bool rightWin = false;
            bool leftWin = false;

            if (GetSpace(space.XPos + 1, space.YPos).SpaceColor == "-" && GetSpace(space.XPos + 2, space.YPos).SpaceColor == space.SpaceColor &&
                GetSpace(space.XPos + 3, space.YPos).SpaceColor == space.SpaceColor)
            {
                rightWin = true;
            }
            if (GetSpace(space.XPos - 1, space.YPos).SpaceColor == "-" && GetSpace(space.XPos - 2, space.YPos).SpaceColor == space.SpaceColor &&
                GetSpace(space.XPos - 3, space.YPos).SpaceColor == space.SpaceColor)
            {
                leftWin = true;
            }

            if (rightWin)
            {
                _winningSpots[0] = space;
                _winningSpots[1] = GetSpace(space.XPos + 1, space.YPos);
                _winningSpots[2] = GetSpace(space.XPos + 2, space.YPos);
                _winningSpots[3] = GetSpace(space.XPos + 3, space.YPos);
            }

            if (leftWin)
            {
                _winningSpots[0] = space;
                _winningSpots[1] = GetSpace(space.XPos - 1, space.YPos);
                _winningSpots[2] = GetSpace(space.XPos - 2, space.YPos);
                _winningSpots[3] = GetSpace(space.XPos - 3, space.YPos);
            }

            if (rightWin || leftWin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion


        #region Vertical Checks
        // Checks for the following pattern vertically "XXXX"
        bool VerticalCheck(BoardSpace space)
        {
            bool win = true;
            for (int i = 1; i < 4; i++)
            {
                if (GetSpace(space.XPos, space.YPos + i).SpaceColor != space.SpaceColor)
                {
                    win = false;
                }
            }

            if (win)
            {
                _winningSpots[0] = space;
                _winningSpots[1] = GetSpace(space.XPos, space.YPos + 1);
                _winningSpots[2] = GetSpace(space.XPos, space.YPos + 2);
                _winningSpots[3] = GetSpace(space.XPos, space.YPos + 3);
            }

            return win;
        }

        // Checks for the following pattern vertically "XXX-"
        bool OpenVerticalCheck(BoardSpace space)
        {
            bool downWin = false;
            bool upWin = false;
            if (GetSpace(space.XPos, space.YPos + 1).SpaceColor == space.SpaceColor && GetSpace(space.XPos, space.YPos + 2).SpaceColor == space.SpaceColor &&
                GetSpace(space.XPos, space.YPos + 3).SpaceColor == "-")
            {
                downWin = true;
            }

            if (GetSpace(space.XPos, space.YPos - 1).SpaceColor == space.SpaceColor && GetSpace(space.XPos, space.YPos - 2).SpaceColor == space.SpaceColor &&
                GetSpace(space.XPos, space.YPos - 3).SpaceColor == "-")
            {
                upWin = true;
            }

            if (downWin)
            {
                _winningSpots[0] = space;
                _winningSpots[1] = GetSpace(space.XPos, space.YPos + 1);
                _winningSpots[2] = GetSpace(space.XPos, space.YPos + 2);
                _winningSpots[3] = GetSpace(space.XPos, space.YPos + 3);
            }

            if (upWin)
            {
                _winningSpots[0] = space;
                _winningSpots[1] = GetSpace(space.XPos, space.YPos - 1);
                _winningSpots[2] = GetSpace(space.XPos, space.YPos - 2);
                _winningSpots[3] = GetSpace(space.XPos, space.YPos - 3);
            }

            if (upWin || downWin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Checks for the following pattern vertically "XX-X"
        bool MidOpenVerticalCheck(BoardSpace space)
        {
            bool downWin = false;
            bool upWin = false;
            if (GetSpace(space.XPos, space.YPos + 1).SpaceColor == space.SpaceColor && GetSpace(space.XPos, space.YPos + 2).SpaceColor == "-" &&
                GetSpace(space.XPos, space.YPos + 3).SpaceColor == space.SpaceColor)
            {
                downWin = true;
            }

            if (GetSpace(space.XPos, space.YPos - 1).SpaceColor == space.SpaceColor && GetSpace(space.XPos, space.YPos - 2).SpaceColor == "-" &&
                GetSpace(space.XPos, space.YPos - 3).SpaceColor == space.SpaceColor)
            {
                upWin = true;
            }

            if (downWin)
            {
                _winningSpots[0] = space;
                _winningSpots[1] = GetSpace(space.XPos, space.YPos + 1);
                _winningSpots[2] = GetSpace(space.XPos, space.YPos + 2);
                _winningSpots[3] = GetSpace(space.XPos, space.YPos + 3);
            }

            if (upWin)
            {
                _winningSpots[0] = space;
                _winningSpots[1] = GetSpace(space.XPos, space.YPos - 1);
                _winningSpots[2] = GetSpace(space.XPos, space.YPos - 2);
                _winningSpots[3] = GetSpace(space.XPos, space.YPos - 3);
            }

            if (upWin || downWin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Checks for the following pattern vertically "X-XX"
        bool MidBackOpenVerticalCheck(BoardSpace space)
        {
            bool downWin = false;
            bool upWin = false;
            if (GetSpace(space.XPos, space.YPos + 1).SpaceColor == "-" && GetSpace(space.XPos, space.YPos + 2).SpaceColor == space.SpaceColor &&
                GetSpace(space.XPos, space.YPos + 3).SpaceColor == space.SpaceColor)
            {
                downWin = true;
            }

            if (GetSpace(space.XPos, space.YPos - 1).SpaceColor == "-" && GetSpace(space.XPos, space.YPos - 2).SpaceColor == space.SpaceColor &&
                GetSpace(space.XPos, space.YPos - 3).SpaceColor == space.SpaceColor)
            {
                upWin = true;
            }

            if (downWin)
            {
                _winningSpots[0] = space;
                _winningSpots[1] = GetSpace(space.XPos, space.YPos + 1);
                _winningSpots[2] = GetSpace(space.XPos, space.YPos + 2);
                _winningSpots[3] = GetSpace(space.XPos, space.YPos + 3);
            }

            if (upWin)
            {
                _winningSpots[0] = space;
                _winningSpots[1] = GetSpace(space.XPos, space.YPos - 1);
                _winningSpots[2] = GetSpace(space.XPos, space.YPos - 2);
                _winningSpots[3] = GetSpace(space.XPos, space.YPos - 3);
            }

            if (upWin || downWin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion


        #region Diagonal Checks

        // Checks for the following pattern diagonally "XXXX"
        bool DiagonalCheck(BoardSpace space)
        {
            bool upRightCheck = true, upLeftCheck = true, downRightCheck= true, downLeftCheck = true;

            for (int i = 1; i < 4; i++)
            {
                if (GetSpace(space.XPos + i, space.YPos + i).SpaceColor != space.SpaceColor)
                {
                    upRightCheck = false;
                }

                if (GetSpace(space.XPos - i, space.XPos - i).SpaceColor != space.SpaceColor)
                {
                    downLeftCheck = false;
                }

                if (GetSpace(space.XPos + i, space.YPos - i).SpaceColor != space.SpaceColor)
                {
                    downRightCheck = false;
                }

                if (GetSpace(space.XPos - i, space.YPos + i).SpaceColor != space.SpaceColor)
                {
                    upLeftCheck = false;
                }
            }

            if (upRightCheck)
            {
                _winningSpots[0] = space;
                _winningSpots[1] = GetSpace(space.XPos + 1, space.YPos + 1);
                _winningSpots[2] = GetSpace(space.XPos + 2, space.YPos + 2);
                _winningSpots[3] = GetSpace(space.XPos + 3, space.YPos + 3);
            }
            else if (upLeftCheck)
            {
                _winningSpots[0] = space;
                _winningSpots[1] = GetSpace(space.XPos - 1, space.YPos + 1);
                _winningSpots[2] = GetSpace(space.XPos - 2, space.YPos + 2);
                _winningSpots[3] = GetSpace(space.XPos - 3, space.YPos + 3);
            }
            else if (downRightCheck)
            {
                _winningSpots[0] = space;
                _winningSpots[1] = GetSpace(space.XPos + 1, space.YPos - 1);
                _winningSpots[2] = GetSpace(space.XPos + 2, space.YPos - 2);
                _winningSpots[3] = GetSpace(space.XPos + 3, space.YPos - 3);
            }
            else if (downLeftCheck)
            {
                _winningSpots[0] = space;
                _winningSpots[1] = GetSpace(space.XPos - 1, space.YPos - 1);
                _winningSpots[2] = GetSpace(space.XPos - 2, space.YPos - 2);
                _winningSpots[3] = GetSpace(space.XPos - 3, space.YPos - 3);
            }

            if (upLeftCheck || upRightCheck || downLeftCheck || downRightCheck)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        // Checks for the following pattern diagonally "XXX-"
        bool OpenDiagonalCheck(BoardSpace space)
        {
            bool upRightCheck = false, upLeftCheck = false, downRightCheck = false, downLeftCheck = false;

            if (GetSpace(space.XPos + 1, space.YPos + 1).SpaceColor == space.SpaceColor && GetSpace(space.XPos + 2, space.YPos + 2).SpaceColor == space.SpaceColor &&
                GetSpace(space.XPos + 3, space.YPos + 3).SpaceColor == "-")
            {
                upRightCheck = true;
            }

            if (GetSpace(space.XPos - 1, space.YPos + 1).SpaceColor == space.SpaceColor && GetSpace(space.XPos - 2, space.YPos + 2).SpaceColor == space.SpaceColor &&
                GetSpace(space.XPos - 3, space.YPos + 3).SpaceColor == "-")
            {
                upLeftCheck = true;
            }

            if (GetSpace(space.XPos + 1, space.YPos - 1).SpaceColor == space.SpaceColor && GetSpace(space.XPos + 2, space.YPos - 2).SpaceColor == space.SpaceColor &&
                GetSpace(space.XPos + 3, space.YPos - 3).SpaceColor == "-")
            {
                downRightCheck = true;
            }

            if (GetSpace(space.XPos - 1, space.YPos - 1).SpaceColor == space.SpaceColor && GetSpace(space.XPos - 2, space.YPos - 2).SpaceColor == space.SpaceColor &&
                GetSpace(space.XPos - 3, space.YPos - 3).SpaceColor == "-")
            {
                downLeftCheck = true;
            }

            if (upRightCheck)
            {
                _winningSpots[0] = space;
                _winningSpots[1] = GetSpace(space.XPos + 1, space.YPos + 1);
                _winningSpots[2] = GetSpace(space.XPos + 2, space.YPos + 2);
                _winningSpots[3] = GetSpace(space.XPos + 3, space.YPos + 3);
            }
            else if (upLeftCheck)
            {
                _winningSpots[0] = space;
                _winningSpots[1] = GetSpace(space.XPos - 1, space.YPos + 1);
                _winningSpots[2] = GetSpace(space.XPos - 2, space.YPos + 2);
                _winningSpots[3] = GetSpace(space.XPos - 3, space.YPos + 3);
            }
            else if (downRightCheck)
            {
                _winningSpots[0] = space;
                _winningSpots[1] = GetSpace(space.XPos + 1, space.YPos - 1);
                _winningSpots[2] = GetSpace(space.XPos + 2, space.YPos - 2);
                _winningSpots[3] = GetSpace(space.XPos + 3, space.YPos - 3);
            }
            else if (downLeftCheck)
            {
                _winningSpots[0] = space;
                _winningSpots[1] = GetSpace(space.XPos - 1, space.YPos - 1);
                _winningSpots[2] = GetSpace(space.XPos - 2, space.YPos - 2);
                _winningSpots[3] = GetSpace(space.XPos - 3, space.YPos - 3);
            }

            if (upLeftCheck || upRightCheck || downLeftCheck || downRightCheck)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        // Checks for the following pattern Diagonally "XX-X"
        bool MidOpenDiagonalCheck(BoardSpace space)
        {
            bool upRightCheck = false, upLeftCheck = false, downRightCheck = false, downLeftCheck = false;

            if (GetSpace(space.XPos + 1, space.YPos + 1).SpaceColor == space.SpaceColor && GetSpace(space.XPos + 2, space.YPos + 2).SpaceColor == "-" &&
                GetSpace(space.XPos + 3, space.YPos + 3).SpaceColor == space.SpaceColor)
            {
                upRightCheck = true;
            }

            if (GetSpace(space.XPos - 1, space.YPos + 1).SpaceColor == space.SpaceColor && GetSpace(space.XPos - 2, space.YPos + 2).SpaceColor == "-" &&
                GetSpace(space.XPos - 3, space.YPos + 3).SpaceColor == space.SpaceColor)
            {
                upLeftCheck = true;
            }

            if (GetSpace(space.XPos + 1, space.YPos - 1).SpaceColor == space.SpaceColor && GetSpace(space.XPos + 2, space.YPos - 2).SpaceColor == "-" &&
                GetSpace(space.XPos + 3, space.YPos - 3).SpaceColor == space.SpaceColor)
            {
                downRightCheck = true;
            }

            if (GetSpace(space.XPos - 1, space.YPos - 1).SpaceColor == space.SpaceColor && GetSpace(space.XPos - 2, space.YPos - 2).SpaceColor == "-" &&
                GetSpace(space.XPos - 3, space.YPos - 3).SpaceColor == space.SpaceColor)
            {
                downLeftCheck = true;
            }

            if (upRightCheck)
            {
                _winningSpots[0] = space;
                _winningSpots[1] = GetSpace(space.XPos + 1, space.YPos + 1);
                _winningSpots[2] = GetSpace(space.XPos + 2, space.YPos + 2);
                _winningSpots[3] = GetSpace(space.XPos + 3, space.YPos + 3);
            }
            else if (upLeftCheck)
            {
                _winningSpots[0] = space;
                _winningSpots[1] = GetSpace(space.XPos - 1, space.YPos + 1);
                _winningSpots[2] = GetSpace(space.XPos - 2, space.YPos + 2);
                _winningSpots[3] = GetSpace(space.XPos - 3, space.YPos + 3);
            }
            else if (downRightCheck)
            {
                _winningSpots[0] = space;
                _winningSpots[1] = GetSpace(space.XPos + 1, space.YPos - 1);
                _winningSpots[2] = GetSpace(space.XPos + 2, space.YPos - 2);
                _winningSpots[3] = GetSpace(space.XPos + 3, space.YPos - 3);
            }
            else if (downLeftCheck)
            {
                _winningSpots[0] = space;
                _winningSpots[1] = GetSpace(space.XPos - 1, space.YPos - 1);
                _winningSpots[2] = GetSpace(space.XPos - 2, space.YPos - 2);
                _winningSpots[3] = GetSpace(space.XPos - 3, space.YPos - 3);
            }

            if (upLeftCheck || upRightCheck || downLeftCheck || downRightCheck)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        // Checks for the following pattern diagonally "X-XX"
        bool MidBackOpenDiagonalCheck(BoardSpace space)
        {
            bool upRightCheck = false, upLeftCheck = false, downRightCheck = false, downLeftCheck = false;

            if (GetSpace(space.XPos + 1, space.YPos + 1).SpaceColor == "-" && GetSpace(space.XPos + 2, space.YPos + 2).SpaceColor == space.SpaceColor &&
                GetSpace(space.XPos + 3, space.YPos + 3).SpaceColor == space.SpaceColor)
            {
                upRightCheck = true;
            }

            if (GetSpace(space.XPos - 1, space.YPos + 1).SpaceColor == "-" && GetSpace(space.XPos - 2, space.YPos + 2).SpaceColor == space.SpaceColor &&
                GetSpace(space.XPos - 3, space.YPos + 3).SpaceColor == space.SpaceColor)
            {
                upLeftCheck = true;
            }

            if (GetSpace(space.XPos + 1, space.YPos - 1).SpaceColor == "-" && GetSpace(space.XPos + 2, space.YPos - 2).SpaceColor == space.SpaceColor &&
                GetSpace(space.XPos + 3, space.YPos - 3).SpaceColor == space.SpaceColor)
            {
                downRightCheck = true;
            }

            if (GetSpace(space.XPos - 1, space.YPos - 1).SpaceColor == "-" && GetSpace(space.XPos - 2, space.YPos - 2).SpaceColor == space.SpaceColor &&
                GetSpace(space.XPos - 3, space.YPos - 3).SpaceColor == space.SpaceColor)
            {
                downLeftCheck = true;
            }

            if (upRightCheck)
            {
                _winningSpots[0] = space;
                _winningSpots[1] = GetSpace(space.XPos + 1, space.YPos + 1);
                _winningSpots[2] = GetSpace(space.XPos + 2, space.YPos + 2);
                _winningSpots[3] = GetSpace(space.XPos + 3, space.YPos + 3);
            }
            else if (upLeftCheck)
            {
                _winningSpots[0] = space;
                _winningSpots[1] = GetSpace(space.XPos - 1, space.YPos + 1);
                _winningSpots[2] = GetSpace(space.XPos - 2, space.YPos + 2);
                _winningSpots[3] = GetSpace(space.XPos - 3, space.YPos + 3);
            }
            else if (downRightCheck)
            {
                _winningSpots[0] = space;
                _winningSpots[1] = GetSpace(space.XPos + 1, space.YPos - 1);
                _winningSpots[2] = GetSpace(space.XPos + 2, space.YPos - 2);
                _winningSpots[3] = GetSpace(space.XPos + 3, space.YPos - 3);
            }
            else if (downLeftCheck)
            {
                _winningSpots[0] = space;
                _winningSpots[1] = GetSpace(space.XPos - 1, space.YPos - 1);
                _winningSpots[2] = GetSpace(space.XPos - 2, space.YPos - 2);
                _winningSpots[3] = GetSpace(space.XPos - 3, space.YPos - 3);
            }

            if (upLeftCheck || upRightCheck || downLeftCheck || downRightCheck)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

#endregion

        public int GetBoardWidth()
        {
            return BOARD_WIDTH;
        }

        public int GetBoardHeight()
        {
            return BOARD_HEIGHT;
        }

    }
}
