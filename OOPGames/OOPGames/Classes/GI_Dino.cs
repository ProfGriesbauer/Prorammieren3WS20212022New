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
    /*  Implementieren des Painters:Painter hat die Aufgaben:
     *  -Abhängig von Zustand des currentField eine Grafik auszugeben
     */

    public class Dino_PaintGame : IDino_PaintGame
    {
        public override string Name { get { return "GI_Dino_Painter"; } }


        public override void PaintDinoGameField(Canvas canvas, IDino_GameField currentField)
        {
            currentField[0]++;
            int counter = currentField[0] ;
            currentField[2] += currentField[3];

            if (currentField[2]!=0) 
            {
                currentField[3] -= 2;
            }
            else
            {
                currentField[3] = 0;
            }

            


            canvas.Children.Clear();
            Color bgColor = Color.FromRgb(255, 255, 255);
            canvas.Background = new SolidColorBrush(bgColor);
            Color OColor = Color.FromRgb(0, 0, 255);
            Brush OStroke = new SolidColorBrush(OColor);
            Color lineColor = Color.FromRgb(0, 0, 0);
            Brush lineStroke = new SolidColorBrush(lineColor);

            Ellipse OE = new Ellipse() { Margin = new Thickness(100, 300 - currentField[2], 0, 0), Width = 10, Height = 10, Stroke = OStroke, StrokeThickness = 3.0 };
            canvas.Children.Add(OE);
            

            int[] obstacles = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0 };

            int vobst = 5;
            
            for (int i = -100; i < 400; i = i + vobst)
            {
                if (obstacles.Length > counter + i / vobst && counter + i / vobst > 0)
                {
                    if (obstacles[counter + i / vobst] == 1)
                    {
                        int plc = i+100;

                        Line obst = new Line() { X1 = plc, Y1 = 280, X2 = plc, Y2 = 310, Stroke = lineStroke, StrokeThickness = 3.0 };
                        canvas.Children.Add(obst);
                    }

                    if (obstacles[counter + i / vobst] == 1 && i == 0)
                    {
                        currentField[1] = obstacles[counter + i / vobst] *20;
                    }
                    else
                    {
                        currentField[1] = 0;
                    }

                    if (currentField[1] > currentField[2] && currentField[1] != 0)
                    {
                        currentField[4] = 1;
                        Console.WriteLine("Verloren");
                    }


                }
                
            }

        }

    }

    /*  Implementieren der Spielregeln: Aufgaben:
     *  -CurrentField: Beinhaltet Spielfeldobjekt und damit den Zustand des Spiels
     *  -MovesPossible: Prüft ob weiterer Spielzug möglich ist
     *  -CheckIfPlayerWon: Prüft ob es einen Gewinner gibt
     *  -Clear Field: Zellen des CurrentFields werden auf null gesetzt
     *  -DoMove: Ordnet durch Spielzug Zellen Spieler zu
     */

    public class Dino_GameRules : IDino_GameRules
    {
        Dino_GameField _Field = new Dino_GameField();

        public IGameField Dino_GameField { get { return _Field; } }

        public override string Name { get { return "GI_Dino_GameRules"; } }

        //public Dino_GameField CurrentField { get { return _Field; } }

        public override bool MovesPossible { get { return true; } }

        //IGameField IGameRules.CurrentField { get { return _Field; } }         //alt, wo IDino_GameRules noch Interface war

        public override IGameField CurrentField { get { return _Field; } }

        public override int CheckIfPLayerWon()
        {
            if (_Field[4] == 1 )
            {
                return 1;
            }

            return -1;
        }

        public override void ClearField()
        {
            for (int i = 0; i < 5; i++)
            {
                _Field[i] = 0;
            }
            
        }

        public override void DoMove(Dino_PlayMove move)
        {
            if (_Field[2] == 0)
            {
                _Field[3] = 20;
            }
        }
    }

    public class Dino_GameField : IDino_GameField
    {
        public int[] _Field = new int[5]{ 0, 0, 0, 0, 0 };

        public int this [int i]
        {
            get
            {
                if (i >= 0 && i <= 4)
                {
                    return _Field[i];
                }

                return -1;
            }

            set
            {
                if (i >= 0 && i <= 4)
                {
                    _Field[i] = value;
                }
            }
        }

        public bool CanBePaintedBy(IPaintGame painter)
        {
            return painter is Dino_PaintGame;
        }
    }

    /*-CanBeRuledBy: Prüft Kompatibilität Regeln und Spieler
     */

    public class Dino_GamePlayer : IDino_GamePlayer
    {
        int _PlayerNumber = 0;

        public override string Name { get { return "GI_Dino_GamePlayer"; } }

        public override bool CanBeRuledBy(IGameRules rules)
        {
            if (rules is Dino_GameRules)
            {
                return true;
            }

            return false;
        }

        public override IGamePlayer Clone()
        {
            Dino_GamePlayer ttthp = new Dino_GamePlayer();
            ttthp.SetPlayerNumber(_PlayerNumber);
            return ttthp;
        }

        public override void SetPlayerNumber(int playerNumber)
        {
            _PlayerNumber = playerNumber;
        }

        public override Dino_PlayMove GetMove(IMoveSelection selection, Dino_GameField field)
        {
            if (selection.XClickPos > 0 && selection.XClickPos < 1000 &&
                selection.YClickPos > 0 && selection.YClickPos < 1000)
            {
                return new Dino_PlayMove();
            }

            return null;
        }
    }

    public class Dino_PlayMove : IDino_PlayMove
    {
        public int PlayerNumber { get { return 1; } }

        public Dino_PlayMove()
        {

        }
    }
}
