using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace OOPGames
{
    //JO:in GG_Startrek.cs: CheckCollison() Methode implementieren + in PaintStartrekGameField() Anzeige, wenn Spiel verloren impementieren

    //Magnus: in GG_Startrek.cs in PaintStartrekGameField() Spaceship zeichnen implemenieren

    //Linus + Jannik:in GG_Startrek.cs Methode DoMove() von Rulesklasse implementieren
    // in GG_Istartrek.cs Interface GG_Istartrekmove definieren (und evtl noch zugehörige Klasse inGG_Startrek.cs implementieren)

    //Philipp: Auskommentierte Fehler im Code ausbessern + Humanplayer Klasse Interface+Klassenimplementierung
    public class GG_StartrekPainter : GG_IStartrekPainter
    {
        List<GG_Meteo> _Meteos = new List<GG_Meteo>();
        uint _aufrufe = 0;
        int _spawnspeed = 80; //Takt der Meteoerzeugung
        int _spawnnum = 1; //Anzahl der je Spawn erzeugten Meteos
        int _movespeed = 40; //Takt der MeteoBewegung 
        bool _collision = false;


        public bool Collison { get { return _collision; } }
        public string Name { get { return "StartrekPainter"; } }
        public void checkCollison(GG_IStartrekGamefield currentField)
        {   
            for (int c = 0; c < 6; c++) { 
                if (currentField[5,c] > 2) 
                {
                    string box_msg = "You Lose";
                    string box_Caption = "Game End";
                    System.Windows.MessageBox.Show(box_msg, box_Caption);
                }
            }
        }
        public void spawnMeteos()
        {
            GG_Meteo meteo = new GG_Meteo();
            //Gegen Meteos an gleicher Posititon absichern
            if (_Meteos.Count > 0) //Gegen Fehler bei erstem Aufruf absicher, dort ist Liste noch leer
            {
                while (meteo.PositionColum == _Meteos[_Meteos.Count - 1].PositionColum)
                {
                    meteo = new GG_Meteo();
                }
            }
            _Meteos.Add(meteo);
        }
        public void moveMetos()
        {
            foreach (GG_Meteo meteo in _Meteos)
            {
                meteo.UpdatePos();
            }
            //Meteos auserhalb vom Spielfeld löschen:
            _Meteos.RemoveAll(item => item.PositionRow > 6); 
        }
        //Schreibt Positionen der Meteos in Matrix
        public void updateField(GG_IStartrekGamefield currentField)
        {
            foreach (GG_Meteo meteo in _Meteos)
            {
                currentField[meteo.PositionRow, meteo.PositionColum] += 1; 
            }
        }
        //Löscht bisherige Positionen aus Matrix, dass fallende Meteos keine Striche hinterlassen
        public void removeOldPositions(GG_IStartrekGamefield currentField)
        {
            foreach (GG_Meteo meteo in _Meteos)
            {
                currentField[meteo.PositionRow, meteo.PositionColum] = 0;
            }
        }
        //Übergibt PaintStartrekGameField currentField als passenden Typ GG_IStartrekGamefield
        public void PaintGameField(Canvas canvas, IGameField currentField)
        {
            if (currentField is GG_IStartrekGamefield)
            {   
                PaintStartrekGameField(canvas, (GG_IStartrekGamefield)currentField);
            }
        }
        public void PaintStartrekGameField(Canvas canvas, GG_IStartrekGamefield currentField)
        {
            removeOldPositions(currentField);

            if ((_aufrufe+1) % 100 == 0)
            {
                if (_movespeed > 0)
                {
                    _movespeed -= 3;
                    _spawnspeed = _movespeed * 2;
                }
            }
            if ((_aufrufe+1) % 1000 == 0)
            {
                if (_spawnnum < 4)
                {
                    _spawnnum++;
                }
            }

            if (_aufrufe %_spawnspeed == 0)
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
            updateField(currentField);
            //Tatsächliches Zeichnen:
            canvas.Children.Clear();
            Color bgColor = Color.FromRgb(255, 255, 255);
            canvas.Background = new SolidColorBrush(bgColor);
            Color MeteoColor = Color.FromRgb(0, 255, 0);
            Brush lineStroke = new SolidColorBrush(MeteoColor);
            Color SpaceshipColor = Color.FromRgb(255, 255, 0);
            Brush XStroke = new SolidColorBrush(MeteoColor);
            Brush ShipStroke = new SolidColorBrush(SpaceshipColor);
            //if collision == true => Anzeige, dass Spiel verloren
            checkCollison(currentField);
            // Matrix auswerten und Meteos an entsprechende Stelle zeichnen
           for (int i = 0; i < 6; i++)
           {
               for (int j = 0; j < 6; j++)
               {
                   if (currentField[i, j] == 1)    
                   {
                       Line X1 = new Line() { X1 = 20 + (j * 60), Y1 = 20 + (i * 60), X2 = 80 + (j * 60), Y2 = 80 + (i * 60), Stroke = XStroke, StrokeThickness = 3.0 };
                       canvas.Children.Add(X1);
                       Line X2 = new Line() { X1 = 20 + (j * 60), Y1 = 80 + (i * 60), X2 = 80 + (j * 60), Y2 = 20 + (i * 60), Stroke = XStroke, StrokeThickness = 3.0 };
                       canvas.Children.Add(X2);
                       Line X3 = new Line() { X1 = 20 + (j * 60), Y1 = 50 + (i * 60), X2 = 80 + (j * 60), Y2 = 50 + (i * 60), Stroke = XStroke, StrokeThickness = 3.0 };
                       canvas.Children.Add(X3);
                   }
                   if(currentField[i, j] == 2)
                    {
                        Line X4 = new Line() { X1 = 20 + (j * 60), Y1 = 20 + (i * 60), X2 = 80 + (j * 60), Y2 = 80 + (i * 60), Stroke = ShipStroke, StrokeThickness = 3.0 };
                        canvas.Children.Add(X4);
                    }
                    //ToDo: (currentField[i, j] == 3) Spaceship zeichnen
                }
            }
            _aufrufe++;
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
        int[,] _Field = new int[6, 6] { 
            { 0, 0, 0, 0, 0, 0 }, 
            { 0, 0, 0, 0, 0, 0 }, 
            { 0, 0, 0, 0, 0, 0 }, 
            { 0, 0, 0, 0, 0, 0 }, 
            { 0, 0, 0, 0, 0, 0 }, 
            { 0, 0, 0, 2, 0, 0 } };

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
        public bool CanBePaintedBy(IPaintGame painter)
        {
            return painter is GG_IStartrekPainter;
        }
    }
    public class GG_StartrekRules : GG_IStartrekRules
    {
        GG_StartrekField _Field = new GG_StartrekField();
        public string Name { get { return "StartrekRules"; } }
        public IGameField CurrentField { get { return _Field; } }
        public bool MovesPossible { get { return true; } }

        public int CheckIfPLayerWon()
        {
            for (int c = 0; c < 6; c++)
            {
                if (_Field[5, c] > 2)
                {
                    return 1;
                }
            }
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
            _Field[5, 3] = 2;
        }
        public void DoMove(IPlayMove move)
        {
            //ToDo:
            //Cast auf IStartrekmove->weiterleitung an DoStartrekmove
            //move in currentfield übertragen 
            //move hat direction => negativ für links positiv für rechts
            if (move is GG_IStartrekMove)
            {
                DoStartrekMove((GG_IStartrekMove)move);
            }
        }
        public void DoStartrekMove(GG_IStartrekMove move)
        {
            int _shippos = -1;
            if (move.Direction != 0)
            {
                //Stelle suchen an der 2 steht
                for (int c = 0; c < 6; c++)
                {
                    if (_Field[5, c] == 2)
                    {
                        _shippos = c;
                    }
                }
                //Ship entsprechend nach Direction des Moves bewegen, sofern dann noch im Spielfeld
                if (move.Direction > 0) //rechts
                {
                    if (_shippos < 5)
                    {
                        _Field[5, _shippos + 1] = 2;
                        _Field[5, _shippos] = 0;
                    }
                }
                if (move.Direction < 0) //links
                {
                    if (_shippos > 0)
                    {
                        _Field[5, _shippos - 1] = 2;
                        _Field[5, _shippos] = 0;
                    }
                }
            }
        }
    }

    public class GG_StartrekHumanPlayer : GG_IStartrekHumanPlayer
    {
        public string Name { get { return "StartrekPlayer"; } }
        public bool CanBeRuledBy(IGameRules rules)
        {
            return rules is GG_IStartrekRules;
        }
        public IGamePlayer Clone()
        {
            return new GG_StartrekHumanPlayer();
        }
        public IPlayMove GetMove(IMoveSelection selection, IGameField field)
        {
            //Polymorphie: Wenn Playmove mit Tastatur erfolgt ist wird spezifischere Methode GetStartrekMove aufgerufen
            if (selection is IKeySelection && field is GG_IStartrekGamefield) 
            {
                return GetStartrekMove((IKeySelection)selection, (GG_IStartrekGamefield)field);
            }
            else
            {
                return null;
            }
        }
        public IPlayMove GetStartrekMove(IKeySelection selection, IGameField field)
        {
            int _direction = 0;
            
            if (selection.Key == Key.Left)
            {
                _direction = -1;
            }
            if (selection.Key == Key.Right)
            {
                _direction = 1;
            }
            return new GG_StartrekMove(_direction, 42);
        }

        public void SetPlayerNumber(int playerNumber)
        {
            //Playernumber ist nicht relevant, deshalb keine Implementierung
            //Es wird standartmäßig ein PlayMove mit Spielernummer 42 zurückgegeben
        }
    }
    public class GG_StartrekMove : GG_IStartrekMove
    {
        int _playernumber = 42;
        int _direction = 0;
        public GG_StartrekMove(int direction, int playernumber)
        {
            _playernumber = playernumber;
            _direction = direction;
        }
        public int PlayerNumber { get { return _playernumber; } }
        public int Direction { get { return _direction; } }
    }
}
