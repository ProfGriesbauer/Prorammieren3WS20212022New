using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
//rules -> Raphi
//paint -> Moritz
//humanplayer -> Markus
//field -> Lena
//move -> Michi

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
            Color lineColor = Color.FromRgb(0, 0, 0);
            Brush lineStroke = new SolidColorBrush(lineColor);
            Color XColor = Color.FromRgb(0, 255, 0);
            Brush XStroke = new SolidColorBrush(XColor);
            Color O1Color = Color.FromRgb(255, 0, 0);
            Color O2Color = Color.FromRgb(255, 255, 0);
            Brush O1Stroke = new SolidColorBrush(O1Color);
            Brush O2Stroke = new SolidColorBrush(O2Color);
            for (int i = 0; i < 7; i++)
            {
                Line Z1 = new Line() { X1 = 20, Y1 = 20 + i * 50, X2 = 320, Y2 = 20 + i * 50, Stroke = lineStroke, StrokeThickness = 3.0 };
                canvas.Children.Add(Z1);
            }
            for (int i = 0; i < 8; i++)
            {
                Line S1 = new Line() { X1 = 20 + i * (300/7), Y1 = 20, X2 = 20 + i * (300/7), Y2 = 320, Stroke = lineStroke, StrokeThickness = 3.0 };
                canvas.Children.Add(S1);
            }
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (currentField[i, j] == 1)
                    {
                        Ellipse O1 = new Ellipse() { Margin = new Thickness(20 + (j * (300/7)), 20 + (i * 50), 0, 0), Width = (42), Height = 42, Stroke = O1Stroke, StrokeThickness = 3.0 };
                        canvas.Children.Add(O1);
                    }
                    else if (currentField[i, j] == 2)
                    {
                        Ellipse O2 = new Ellipse() { Margin = new Thickness(20 + (j * (300/7)), 20 + (i * 50), 0, 0), Width = 42, Height = 42, Stroke = O2Stroke, StrokeThickness = 3.0 };
                        canvas.Children.Add(O2);
                    }
                }
            }
        }
    }



    public class GC_VierGewinntRules : BaseVierGewinntRules
    {
        GC_VierGewinntField _Field = new GC_VierGewinntField();

        public override IVierGewinntField VierGewinntField { get { return _Field; } }

        public override bool MovesPossible
        {
            get
            {
                for (int i = 0; i <  6;i++)
                {
                    for (int j = 0; j < 7; j++)
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

        public override string Name { get { return "GC_VierGewinntRules"; } }

        public override int CheckIfPLayerWon()
        {
            for (int p = 1; p < 3; p++)
            {
                for (int j = 0; j < 7; j++) // Checke Senkrecht 
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (_Field[i, j] > 0 && _Field[i, j] == _Field[i + 1, j] && _Field[i + 1, j] == _Field[i + 2, j] && _Field[i + 2, j] == _Field[i + 3, j])
                        {
                            return p;
                        }
                    }
                }
                for (int i = 0; i < 6; i++) // Checke Waagrecht
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (_Field[i, j] > 0 && _Field[i, j] == _Field[i, j + 1] && _Field[i, j + 1] == _Field[i, j + 2] && _Field[i, j + 2] == _Field[i, j + 3])
                        {
                            return p;
                        }
                    }
                }
               for (int i = 0; i < 3; i++) // Checke links oben nach rechts unten 
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (_Field[i, j] > 0 && _Field[i, j] == _Field[i + 1, j + 1] && _Field[i + 1, j + 1] == _Field[i + 2, j + 2] && _Field[i + 2, j + 2] == _Field[i + 3, j + 3])
                        {
                            return p;
                        }
                    }
                }
                for (int i = 5; i > 2; i--) // Checke links oben nach recht unten 
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (_Field[i, j] > 0 && _Field[i, j] == _Field[i - 1, j + 1] && _Field[i - 1, j + 1] == _Field[i - 2, j + 2] && _Field[i - 2, j + 2] == _Field[i - 3, j + 3])
                        {
                            return p;
                        }
                    }
                }
            }
            return -1;
        }

        public override void ClearField()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    _Field[i, j] = 0;
                }
            }
        }

        public override void DoVierGewinntMove(IVierGewinntMove move)
        {
            if (move.Column >= 0 && move.Column < 7)
            {
                int Zeile = 5;
                bool Leer = false;
                for (int i = 5; i >-1 && Leer == false; i--)
                {
                    if (_Field[i, move.Column] == 0)
                    {
                        Leer = true;
                        Zeile = i;
                    }


                }
                _Field[Zeile, move.Column] = move.PlayerNumber;
            }
        }
    }

    public class GC_VierGewinntField : BaseVierGewinntField
    {
        int[,] _Field = new int[6, 7] { { 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0 } };

        public override int this[int r, int c]
        {
            get
            {
                if (r >= 0 && r < 6 && c >= 0 && c < 7)
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
                if (r >= 0 && r < 6 && c >= 0 && c < 7)
                {
                    _Field[r, c] = value;
                }
            }
        }
    }

    public class GC_VierGewinntMove : IVierGewinntMove
    {
        int _Row = 0;
        int _Column = 0;
        int _PlayerNumber = 0;

        public GC_VierGewinntMove(int row, int column, int playerNumber)
        {
            _Row = row;
            _Column = column;
            _PlayerNumber = playerNumber;
        }

        public int Row { get { return _Row; } }

        public int Column { get { return _Column; } }

        public int PlayerNumber { get { return _PlayerNumber; } }
    }

    public class GC_VierGewinntHumanPlayer : BaseHumanVierGewinntPlayer
    {
        int _PlayerNumber = 0;

        public override string Name { get { return "GC_HumanVierGewinntPlayer"; } }

        public override IGamePlayer Clone()
        {
            GC_VierGewinntHumanPlayer ttthp = new GC_VierGewinntHumanPlayer();
            ttthp.SetPlayerNumber(_PlayerNumber);
            return ttthp;
        }

        public override IVierGewinntMove GetMove(IMoveSelection selection, IVierGewinntField field)
        {
            
            
            for (int j = 0; j < 7; j++) // Überarbeitet am 29.11 durch Raphael
            {
                if (selection.XClickPos > 20 + (j * (300/7)) && selection.XClickPos < (20+300/7) + (j * (300/7)) &&
                        selection.YClickPos > 20 && selection.YClickPos < 370)
                {
                    return new GC_VierGewinntMove(0, j, _PlayerNumber); //Wenn die Komplette Klasse überarbeitet wird, kann man die Null rausstreichen 
                
                }
                
            }

            return null;
        }

        public override void SetPlayerNumber(int playerNumber)
        {
            _PlayerNumber = playerNumber;
        }
    }

   public class GC_VierGewinntComputerPlayer : BaseComputerVierGewinntPlayer
    {
        int _PlayerNumber = 0;

        public override string Name { get { return "GC_ComputerVierGewinntPlayer"; } }

        public override IGamePlayer Clone()
        {
            GC_VierGewinntComputerPlayer ttthp = new GC_VierGewinntComputerPlayer();
            ttthp.SetPlayerNumber(_PlayerNumber);
            return ttthp;
        }

        public override IVierGewinntMove GetMove(IVierGewinntField field)
        {
            Random rand = new Random();
            int f = rand.Next(0, 8);
            for (int i = 0; i < 42; i++)
            {
                int c = f % 6;
                int r = ((f - c) / 6) % 7;
                if (field[r, c] <= 0)
                {
                    return new GC_VierGewinntMove(r, c, _PlayerNumber);
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