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
                Line Z1 = new Line() { X1 = 20, Y1 = 20 + i * 50, X2 = 315, Y2 = 20 + i * 50, Stroke = lineStroke, StrokeThickness = 3.0 };
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
                        Ellipse O1 = new Ellipse() { Margin = new Thickness(20 + (j * (300/7)), 21 + (i * 50), 0, 0), Width = 40, Height = 40, Stroke = O1Stroke, Fill = O1Stroke, StrokeThickness = 3.0 };
                        canvas.Children.Add(O1);
                    }
                    else if (currentField[i, j] == 2)
                    {
                        Ellipse O2 = new Ellipse() { Margin = new Thickness(20 + (j * (300/7)), 21 + (i * 50), 0, 0), Width = 40, Height = 40, Stroke = O2Stroke, Fill = O2Stroke, StrokeThickness = 3.0 };
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
                for (int j = 0; j < 7; j++) // Checke Senkrecht 
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (_Field[i, j] > 0 && _Field[i, j] == _Field[i + 1, j] && _Field[i + 1, j] == _Field[i + 2, j] && _Field[i + 2, j] == _Field[i + 3, j])
                        {
                            return _Field[i, j];
                        }
                    }
                }
                for (int i = 0; i < 6; i++) // Checke Waagrecht
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (_Field[i, j] > 0 && _Field[i, j] == _Field[i, j + 1] && _Field[i, j + 1] == _Field[i, j + 2] && _Field[i, j + 2] == _Field[i, j + 3])
                        {
                            return _Field[i, j];
                        }
                    }
                }
               for (int i = 0; i < 3; i++) // Checke links oben nach rechts unten 
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (_Field[i, j] > 0 && _Field[i, j] == _Field[i + 1, j + 1] && _Field[i + 1, j + 1] == _Field[i + 2, j + 2] && _Field[i + 2, j + 2] == _Field[i + 3, j + 3])
                        {
                            return _Field[i, j];
                        }
                    }
                }
                for (int i = 5; i > 2; i--) // Checke links oben nach recht unten 
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (_Field[i, j] > 0 && _Field[i, j] == _Field[i - 1, j + 1] && _Field[i - 1, j + 1] == _Field[i - 2, j + 2] && _Field[i - 2, j + 2] == _Field[i - 3, j + 3])
                        {
                            return _Field[i, j];
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

        public override void DoVierGewinntMove(IVierGewinntMove move)// Überarbeitet
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
                if (Leer == true)
                {
                    _Field[Zeile, move.Column] = move.PlayerNumber;
                }
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
            
            int player = _PlayerNumber;
            bool fieldempty = false;
            int oppositeplayer = 0;
            int nullzaehler = 0;
            
            int[,] horizontalarray = new int[20, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 },
                { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }};
            int[,] verticalarray = new int[20, 3]{ { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 },
                { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 },
                { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
            
            // Überprüfen ob im Spiel ein schon ein Zug getätigt wurde
            for (int j = 0; j < 7; j++)
            {
                for (int i = 5; i >= 0 && fieldempty == true; i--)
                {
                    if (field[i, j] > 0)
                    {
                        fieldempty = false;
                    }

                }
            }
            if (player > 0 && player < 3)
            {
                if (player == 1)
                {
                    oppositeplayer = 2;
                }
                else
                {
                    oppositeplayer = 1;
                }
            }
            
            // Zuerst muss geprüft werden ob der Gegenspieler  gewinnen kann
            if (fieldempty == false)
            {
                // checke waagrecht von links nach rechts
                for (int i = 5; i >= 0; i--)
                {
                    nullzaehler = 0;
                    for (int j = 0; j < 5; j++) // Kleiner 5, da es nur noch zwei Felder bis zum Rand sind 
                    {
                        if (field[i, j] == oppositeplayer)
                        {
                            if (field[i, j + 1] == oppositeplayer)
                            {
                                
                                if (field[i, j + 2] == oppositeplayer)
                                {
                                    if ((j + 3) < 7 && field[i, j + 3] == 0 && (i == 5 || field[i + 1, j + 3] > 0))
                                    {
                                        return new GC_VierGewinntMove(i, j + 3, _PlayerNumber);
                                    }
                                    else if ((j - 1) >= 0 && field[i, j - 1] == 0 && (i == 5 || field[i + 1, j - 1] > 0))
                                    {
                                        return new GC_VierGewinntMove(i, j - 1, _PlayerNumber);
                                    }

                                }
                            }
                        }
                        else if (field[i, j] == 0)
                        {
                            nullzaehler++;
                        }

                    }

                }
                

                // checke Senkrecht

                for (int j = 0; j < 7; j++)
                {

                    for (int i = 5; field[i, j] > 0 && i >= 0; i--) // Kleiner 5, da es nur noch zwei Felder bis zum Rand sind 
                    {
                        if (field[i, j] == oppositeplayer)
                        {
                            if (field[i - 1, j] == oppositeplayer)
                            {
                                if (field[i - 2, j] == oppositeplayer)
                                {
                                    if ((i - 3) >= 0 && field[i - 3, j] == 0)
                                    {
                                        return new GC_VierGewinntMove(i - 3, j, _PlayerNumber);
                                    }
                                }
                            }


                        }
                    }
                }
            // Selbst einen Zug machen, um zu gewinnen
            // bevorzugt waagrecht
            
                for (int i = 5; i>=0; i--) 
                {
                    for (int j = 0; j<7; j++)
                    {
                        if (field[i, j] == player)
                        {
                            if (field[i, j + 1] == player)
                            {
                                
                                for (int z = 1; horizontalarray[z, 1] != 0; z++)
                                {
                                    if (horizontalarray[z, 1] == 0)
                                    {
                                        horizontalarray[z, 1] = z;
                                        horizontalarray[z, 2] = i;
                                        horizontalarray[z, 3] = j;
                                        horizontalarray[z, 4] = j + 1;

                                    }
                                }
                                

                                if (field[i, j + 2] == player)
                                {
                                    if ((j + 3) < 7 && field[i, j + 3] == 0)
                                    {
                                        if (i == 5 || field[(i + 1), j] > 0)
                                        {
                                            return new GC_VierGewinntMove(i, j+3, _PlayerNumber);
                                        }

                                    }
                                }
                                else if ((j - 1) >= 0 && field[i, j - 1] == 0)
                                {
                                    if (i == 5 || field[(i + 1), j] > 0)
                                    {
                                        return new GC_VierGewinntMove(i, j, _PlayerNumber);
                                    }
                                }

                            }
                        }

                    }

                }
            
                // Senkrecht einen Zug machen, um zu gewinnen 
                for (int j = 0; j < 7; j++)
                {
                    for (int i = 5; i >= 0; i--)
                    {
                        if (field[i, j] == player)
                        {
                            if (field[(i-1), j] == player)
                            {
                                for (int z = 1; verticalarray[z, 1] != 0; z++)
                                {
                                    if (verticalarray[z, 1] == 0)
                                    {
                                        verticalarray[z, 1] = z;
                                        verticalarray[z, 2] = j;
                                        verticalarray[z, 3] = i-1;

                                    }
                                }

                                if (field[i-2, j] == player)
                                {
                                    if ((i-3) >=0 && field[i-3, j] == 0)
                                    {
                                        return new GC_VierGewinntMove(i - 3, j, _PlayerNumber);
                                        

                                    }
                                }
                            }
                        }

                    }

                }
                
                // Versuchen mit zwei Zügen nach rechts zu gewinnen
                for (int z = 1; horizontalarray[z, 1] != 0; z++)
                {                    
                    int ro = horizontalarray[z, 2];
                    int co = horizontalarray[z, 3];
                    if (co-2 >=0 && field[ro,co-1] == 0 && (ro==5 || field[ro+1, co-1]>0) && 
                        (field[ro, co - 2] == 0|| field[ro, co - 2] == player) && (ro == 5 || field[ro + 1, co - 1] > 0))
                    {
                        return new GC_VierGewinntMove(ro, co-1, _PlayerNumber);
                    }
                }
                // Versuchen mit jeweils einen nach rechts und links zu gewinnen
                for (int z = 1; horizontalarray[z, 1] != 0; z++)
                {                    
                    int ro = horizontalarray[z, 2];
                    int col = horizontalarray[z, 3];
                    int cor = horizontalarray[z, 4];
                    if (col - 1 >= 0  && cor+1<=6 && field[ro, col - 1] == 0 && (ro == 5 || field[ro + 1, col - 1] > 0) &&
                        field[ro, cor +1 ] == 0) //&& (ro == 5 || field[ro + 1, cor + 1] > 0)
                    {
                        return new GC_VierGewinntMove(ro, col-1, _PlayerNumber);
                    }
                }
                // Versuchen mit zwei Zügen nach links zu gewinnen
                for (int z = 1; horizontalarray[z, 1] != 0; z++)
                {                    
                    int ro = horizontalarray[z, 2];
                    int co = horizontalarray[z, 4];
                    if (co + 2 <= 6 && field[ro, co + 1] == 0 && (ro == 5 || field[ro + 1, co + 1] > 0) &&
                        (field[ro, co + 2] == 0 || field[ro, co + 2] == player) && (ro == 5 || field[ro + 1, co + 1] > 0))
                    {
                        return new GC_VierGewinntMove(ro, co + 1, _PlayerNumber);
                    }
                }
                // Setze Stein senkrecht 
                for (int z = 1; verticalarray[z, 1] != 0; z++)
                {
                    int ro = horizontalarray[z, 3];
                    int co = horizontalarray[z, 2];
                    if ( field[ro-1,co]==0)
                    {
                        return new GC_VierGewinntMove(ro-1, co, _PlayerNumber);
                    }
                }
                
                

                


            }
            
                

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