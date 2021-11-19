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
        public override string Name { get { return "DinoPainter"; } }

        public override void PaintDinoGameField(Canvas canvas, IDino_GameField currentField)
        {
            canvas.Children.Clear();
            Color bgColor = Color.FromRgb(255, 255, 255);
            canvas.Background = new SolidColorBrush(bgColor);
            Color OColor = Color.FromRgb(0, 0, 255);
            Brush OStroke = new SolidColorBrush(OColor);

            int i = currentField[1];

            Console.WriteLine(i);

            Ellipse OE = new Ellipse() { Margin = new Thickness(100 + i*100, 100, 0, 0), Width = 100, Height = 100, Stroke = OStroke, StrokeThickness = 3.0 };
            canvas.Children.Add(OE);
                   
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

        public override string Name { get { return "Dino_GameRules"; } }

        //public Dino_GameField CurrentField { get { return _Field; } }

        public override bool MovesPossible { get { return true; } }

        //IGameField IGameRules.CurrentField { get { return _Field; } }         //alt, wo IDino_GameRules noch Interface war

        public override IGameField CurrentField { get { return _Field; } }

        public override int CheckIfPLayerWon()
        {
            return -1;
        }

        public override void ClearField()
        {
            
        }

        public override void DoMove(Dino_PlayMove move)
        {
            _Field[1] = _Field[1] + 1;
            Console.WriteLine(_Field[1]+100);
        }
    }

    public class Dino_GameField : IDino_GameField
    {
        public int _Field = 1;

        public int this [int i]
        {
            get
            {
                if (i == 1)
                {
                    return _Field;
                }

                return -1;
            }

            set
            {
                if (i == 1)
                {
                    _Field = value;
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

        public override string Name { get { return "Dino_GamePlayer"; } }

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
