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
        public string Name { get { return "DinoPainter"; } }

        public void PaintGameField(Canvas canvas, IGameField currentField)
        {
            canvas.Children.Clear();
            Color bgColor = Color.FromRgb(255, 255, 255);
            canvas.Background = new SolidColorBrush(bgColor);
            Color OColor = Color.FromRgb(0, 0, 255);
            Brush OStroke = new SolidColorBrush(OColor);

            Ellipse OE = new Ellipse() { Margin = new Thickness(100, 100, 0, 0), Width = 100, Height = 100, Stroke = OStroke, StrokeThickness = 3.0 };
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

        public string Name { get { return "Dino_GameRules"; } }

        //public Dino_GameField CurrentField { get { return _Field; } }

        public bool MovesPossible { get { return true; } }

        IGameField IGameRules.CurrentField { get { return _Field; } }

        public int CheckIfPLayerWon()
        {
            return -1;
        }

        public void ClearField()
        {
            
        }

        public void DoMove(IPlayMove move)
        {
            
        }
    }

    public class Dino_GameField : IGameField
    {
        public int _Field = 1;

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

        public string Name
        {
            get
            {
                return "Dino_GamePlayer";
            }
        }

        public bool CanBeRuledBy(IGameRules rules)
        {
            if (rules is Dino_GameRules)
            {
                return true;
            }

            return false;
        }

        public IGamePlayer Clone()
        {
            Dino_GamePlayer ttthp = new Dino_GamePlayer();
            ttthp.SetPlayerNumber(_PlayerNumber);
            return ttthp;
        }

        public void SetPlayerNumber(int playerNumber)
        {
            _PlayerNumber = playerNumber;
        }
    }

    public class Dino_PlayMove : IDino_PlayMove
    {
        public int PlayerNumber => throw new NotImplementedException();
    }
}
