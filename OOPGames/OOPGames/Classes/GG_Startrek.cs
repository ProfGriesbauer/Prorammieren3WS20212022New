using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace OOPGames
{
    public class GG_StartrekPainter : GG_IStartrekPainter
    {
        List<GG_Meteo> _Meteos = new List<GG_Meteo>();
        uint _aufrufe = 0;
        int _spawnspeed = 20; //Takt der Meteoerzeugung
        int _spawnnum = 2; //Anzahl der je Spawn erzeugten Meteos
        int _movespeed = 20; //Takt der Bewegung 
        bool _collision = false;


        bool GG_IStartrekPainter.Collison { get { return _collision; } }

        public string Name => throw new NotImplementedException();

        public void checkCollison()
        {
            //Kollision prüfen
            throw new NotImplementedException();
        }

        public void spawnMeteos()
        {
            GG_Meteo meteo = new GG_Meteo();
            //Gegen Meteos an gleicher Posititon absichern
            while (meteo.PositionColum == _Meteos[_Meteos.Count].PositionColum)
            {
                meteo = new GG_Meteo();
            }
            _Meteos.Add(meteo);
        }

        public void moveMetos()
        {
            foreach (GG_Meteo meteo in _Meteos)
            {
                meteo.UpdatePos();
                //Wenn Meteo aus Spielfeld -> Löschen
                if (meteo.PositionRow > 6)
                {
                    _Meteos.Remove(meteo);
                }
            }
        }

        public void PaintGameField(Canvas canvas, IGameField currentField)
        {
            _aufrufe++;

            if ((_aufrufe + _spawnspeed) % _spawnspeed == 0) //+spwanspeed dass bei Spielstart gleich gespawnd wird
            {
                for (int i = 0; i < _spawnnum; i++)
                {
                    spawnMeteos();
                }

            }
            if (_aufrufe % _movespeed == 0)
            {
                moveMetos();
            }

            //Tatsächliches Zeichnen:
            canvas.Children.Clear();
            Color bgColor = Color.FromRgb(255, 255, 255);
            canvas.Background = new SolidColorBrush(bgColor);
            Color MeteoColor = Color.FromRgb(0, 255, 0);
            Brush lineStroke = new SolidColorBrush(MeteoColor);
            Color SpaceshipColor = Color.FromRgb(0, 255, 0);
            Brush XStroke = new SolidColorBrush(SpaceshipColor);

            // Matrix auswerten und Meteos an entsprechende Stelle zeichnen
 /*           for (int i = 0; i < 6; i++)
           {
               for (int j = 0; j < 6; j++)
               {
                   if (currentField[j, i] == 1)     // falsche Variable!?
                   {
                       Line X1 = new Line() { X1 = 20 + (j * 60), Y1 = 20 + (i * 60), X2 = 80 + (j * 60), Y2 = 80 + (i * 60), Stroke = XStroke, StrokeThickness = 3.0 };
                       canvas.Children.Add(X1);
                       Line X2 = new Line() { X1 = 20 + (j * 60), Y1 = 80 + (i * 60), X2 = 80 + (j * 60), Y2 = 20 + (i * 60), Stroke = XStroke, StrokeThickness = 3.0 };
                       canvas.Children.Add(X2);
                       Line X3 = new Line() { X1 = 20 + (j * 60), Y1 = 50 + (i * 60), X2 = 80 + (j * 60), Y2 = 50 + (i * 60), Stroke = XStroke, StrokeThickness = 3.0 };
                       canvas.Children.Add(X3);
                   }
               }
           }
 */

        }
    }

    public class GG_Meteo : GG_IMeteo
    {
        private int _PositionRow;
        private int _PositionColum;
        public int PositionRow { get { return _PositionRow; } }

        public int PositionColum { get { return _PositionColum; } }

        public GG_Meteo()
        {   //Bei erstellen des Objekts wird der Meteo in einer zufälligen Spalte in Reihe
            //0 gesetzt
            Random rand = new Random();
            _PositionRow = 0;
            _PositionColum = rand.Next(0, 5);
        }

        public void UpdatePos()
        {
            _PositionRow++;
        }
    }
    public class GG_StartrekField : GG_IStartrekGamefield
    {
        int[,] _Field = new int[6, 6] { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } };

        public int this[int r, int c]
        {
            get
            {
                if (r >= 0 && r < 6 && c >= 0 && c < 6)
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
                if (r >= 0 && r < 6 && c >= 0 && c < 6)
                {
                    _Field[r, c] = value;
                }
            }
        }

        // Vorläufige nichtimplementierung der Funktion bis der Painter steht
        bool IGameField.CanBePaintedBy(IPaintGame painter)
        {
            throw new NotImplementedException();
        }

        public class GG_StartrekRules : GG_IStartrekRules
        {
            GG_StartrekField _Field = new GG_StartrekField();

            public string Name { get { return "StartrekRules"; } }


            public IGameField CurrentField { get { return _Field; } }

            public bool MovesPossible { get { return true; } }

            public int CheckIfPLayerWon()
            {
                return -1;
            }

            public void ClearField()
            {
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        _Field[i, j] = 0;
                    }
                }
            }

            public void DoMove(IPlayMove move)
            {

            }
        }
    }
}
