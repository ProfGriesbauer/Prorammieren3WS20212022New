//Aufgaben:
//Lena Field
//Moritz Paints
//Markus HumanPlayer
//Raphi rules
//Michi Move ->
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace OOPGames
{
    public class GC_VierGewinntPaint : BaseVierGewinntPaint
    {
        public override string Name { get { return "GC_VierGewinntPaint"; } }

        public override void PaintVierGewinntField(Canvas canvas, IVierGewinntField currentField)
        {
            canvas.Children.Clear();
            Color bgColor = Color.FromRgb(255, 255, 255);
            canvas.Background = new SolidColorBrush(bgColor);
            Color lineColor = Color.FromRgb(0, 255, 0);
            Brush lineStroke = new SolidColorBrush(lineColor);
            Color XColor = Color.FromRgb(0, 255, 0);
            Brush XStroke = new SolidColorBrush(XColor);
            Color O1Color = Color.FromRgb(255, 0, 0);
            Color O2Color = Color.FromRgb(0, 255, 0);
            Brush O1Stroke = new SolidColorBrush(O1Color);
            Brush O2Stroke = new SolidColorBrush(O2Color);
            for (int i = 0; i < 7; i++)
            {
                Line Z1 = new Line() { X1 = 20, Y1 = 20 + i * 50, X2 = 320, Y2 = 20 + i * 50, Stroke = lineStroke, StrokeThickness = 3.0 };
                canvas.Children.Add(Z1)
            }
            for (int i = 0; i < 8; i++)
            {
                Line S1 = new Line() { X1 = 20 + i * 37, 5, Y1 = 20, X2 = 20 + i * 37, 5, Y2 = 320, Stroke = lineStroke, StrokeThickness = 3.0 };
                canvas.Children.Add(S1);
            }
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (currentField[i, j] == 1)
                    {
                        Ellipse O1 = new Ellipse() { Margin = new Thickness(20 + (i * 50), 20 + (j * 300 / 7), 0, 0), Width = 100, Height = 100, Stroke = O1Stroke, StrokeThickness = 3.0 };
                        canvas.Children.Add(O1);
                    }
                    else if (currentField[i, j] == 2)
                    {
                        Ellipse O2 = new Ellipse() { Margin = new Thickness(20 + (i * 50), 20 + (j * 300 / 7), 0, 0), Width = 100, Height = 100, Stroke = O2Stroke, StrokeThickness = 3.0 };
                        canvas.Children.Add(O2);
                    }
                }
            }
        }
    }



    public class GC_TicTacToeRules : BaseTicTacToeRules
    {
        GC_TicTacToeField _Field = new GC_TicTacToeField();

        public override ITicTacToeField TicTacToeField { get { return _Field; } }

        public override bool MovesPossible
        {
            get
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (_Field[i, j] == 0)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }

        public override string Name { get { return "GC_TicTacToeRules"; } }

        public override int CheckIfPLayerWon()
        {
            for (int p = 1; p < 3; p++)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (_Field[i, 0] > 0 && _Field[i, 0] == _Field[i, 1] && _Field[i, 1] == _Field[i, 2])
                    {
                        return p;
                    }
                    else if (_Field[0, i] > 0 && _Field[0, i] == _Field[1, i] && _Field[1, i] == _Field[2, i])
                    {
                        return p;
                    }
                }

                if ((_Field[0, 0] > 0 && _Field[0, 0] == _Field[1, 1] && _Field[1, 1] == _Field[2, 2]) ||
                    (_Field[0, 2] > 0 && _Field[0, 2] == _Field[1, 1] && _Field[1, 1] == _Field[2, 0]))
                {
                    return p;
                }
            }

            return -1;
        }

        public override void ClearField()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _Field[i, j] = 0;
                }
            }
        }

        public override void DoTicTacToeMove(ITicTacToeMove move)
        {
            if (move.Row >= 0 && move.Row < 3 && move.Column >= 0 && move.Column < 3)
            {
                _Field[move.Row, move.Column] = move.PlayerNumber;
            }
        }
    }

    public class GC_TicTacToeField : BaseTicTacToeField
    {
        int[,] _Field = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };

        public override int this[int r, int c]
        {
            get
            {
                if (r >= 0 && r < 3 && c >= 0 && c < 3)
                {
                    return _Field[r, c];
                }
                else
                {
                    return -1;
                }
            }

            set
            {
                if (r >= 0 && r < 3 && c >= 0 && c < 3)
                {
                    _Field[r, c] = value;
                }
            }
        }
    }

    public class GC_TicTacToeMove : ITicTacToeMove
    {
        int _Row = 0;
        int _Column = 0;
        int _PlayerNumber = 0;

        public GC_TicTacToeMove(int row, int column, int playerNumber)
        {
            _Row = row;
            _Column = column;
            _PlayerNumber = playerNumber;
        }

        public int Row { get { return _Row; } }

        public int Column { get { return _Column; } }

        public int PlayerNumber { get { return _PlayerNumber; } }
    }

    public class GC_TicTacToeHumanPlayer : BaseHumanTicTacToePlayer
    {
        int _PlayerNumber = 0;

        public override string Name { get { return "GC_HumanTicTacToePlayer"; } }

        public override IGamePlayer Clone()
        {
            GC_TicTacToeHumanPlayer ttthp = new GC_TicTacToeHumanPlayer();
            ttthp.SetPlayerNumber(_PlayerNumber);
            return ttthp;
        }

        public override ITicTacToeMove GetMove(IMoveSelection selection, ITicTacToeField field)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (selection.XClickPos > 20 + (j * 100) && selection.XClickPos < 120 + (j * 100) &&
                        selection.YClickPos > 20 + (i * 100) && selection.YClickPos < 120 + (i * 100))
                    {
                        return new GC_TicTacToeMove(i, j, _PlayerNumber);
                    }
                }
            }

            return null;
        }

        public override void SetPlayerNumber(int playerNumber)
        {
            _PlayerNumber = playerNumber;
        }
    }

    public class GC_TicTacToeComputerPlayer : BaseComputerTicTacToePlayer
    {
        int _PlayerNumber = 0;

        public override string Name { get { return "GC_ComputerTicTacToePlayer"; } }

        public override IGamePlayer Clone()
        {
            GC_TicTacToeComputerPlayer ttthp = new GC_TicTacToeComputerPlayer();
            ttthp.SetPlayerNumber(_PlayerNumber);
            return ttthp;
        }

        public override ITicTacToeMove GetMove(ITicTacToeField field)
        {
            Random rand = new Random();
            int f = rand.Next(0, 8);
            for (int i = 0; i < 9; i++)
            {
                int c = f % 3;
                int r = ((f - c) / 3) % 3;
                if (field[r, c] <= 0)
                {
                    return new GC_TicTacToeMove(r, c, _PlayerNumber);
                }
                else
                {
                    f++;
                }
            }

            return null;
        }

        public override void SetPlayerNumber(int playerNumber)
        {
            _PlayerNumber = playerNumber;
        }
    }
}
