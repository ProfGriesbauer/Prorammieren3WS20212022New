using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOPGames
{
    public class GA_PongPaint : IPaintPong
    {
        public string Name { get { return "GA_PongPainter"; } }

        public void PaintPongField(Canvas canvas, IPongField currentField)
        {

        }

        public void PaintGameField(Canvas canvas, IGameField currentField)
        {
            if (currentField is IPongField)
            {
                PaintPongField(canvas, (IPongField)currentField);
            }
        }
    }
    /*
     * 
     ***Biemel
    Das Feld hier muss auf jeden Fall noch irgendwie deklariert werden. Siehe hierzu das 
    Deklarierungsbeispiel in Griesbausers TicTacToe.cs Line 134-161 
    
    public class GA_PongField : IPongField
    {
        public int this[int r, int c] { get; set; }

        public bool CanBePaintedBy(IPaintGame painter)
        {
            return painter is IPaintPong;
        }
    }
    ***Biemel
    *
    */

    public class GA_PongRules : IPongRules
    {
        public IPongField PongField { get; }

        public bool MovesPossible { get; }

        public string Name { get; }

        public int CheckIfPLayerWon()
        {
            /*
             * 
             ***Biemel***
            Hier muss deklariert werden, wann welcher Spieler gewonnen hat.
            z.B.: 
            /*
            .
            .
            .
            if(xBallPos > 400)      //Ball rechts außerhalb des Spielfelds
            {
            p = 1;                  //Spieler 1 gewinnt
            }
            else if (xBallPos < 0)  //Ball links außerhalb des Spielfelds
            {
            p = 2;                  //Spieler 2 gewinnt
            }
            ***Biemel***
            *
            */

            int p;      //***Biemel*** Diese Zeilen sind nur Platzhalter, weil die Funktion    
            p = 1;      //einen Fehler wirft wenn sie keinen Rückgabewert hat  ***Biemel***
            return p;

        }

        public void ClearField()
        {

        }

        public void DoPongMove(IPongMove move)
        {

        }

        public IGameField CurrentField { get { return PongField; } }

        public void DoMove(IPlayMove move)
        {
            if (move is IPongMove)
            {
                DoPongMove((IPongMove)move);
            }
        }
    }

    public class PongMove : IPongMove
    {
       int _Movement = 0;
       int _PlayerNumber = 0;
       int _Row;
       int _Column;

       public PongMove (int movement, int playerNumber)
        {
            _Movement = movement;
            _PlayerNumber = playerNumber;
   
        }

        public int Row { get { return _Row; } }
        public int Column { get { return _Column; } }
        public int Movement { get { return _Movement; } }
        public int PlayerNumber { get { return _PlayerNumber; } }
    }

    public class GA_HumanPongPlayer : IHumanPongPlayer
    {
        int _PlayerNumber = 0;
        public string Name { get { return "GA_HumanPongPlayer"; } }

        public IPongMove GetMove(IMoveSelection selection, IPongField field)
        {
            /*
             * 
             ***Biemel**
            Hier ist nur Beispielhaft etwas reingeschrieben, damit kein Fehler geworfen wird.
            Hier müssen wir überlegen, wie wir den Move deklarieren, weil wir ja nicht mit der
            Maus klicken wollen.
            Evtl. müssen wir hier dei ganze GetMoveFunktion umgehen
            ***Biemel***
            *
            */
            int m = 1;
            int p = 1;
            return new PongMove(m, p);
        }

        public void SetPlayerNumber(int playerNumber)
        {
            _PlayerNumber = playerNumber;
        }

        public IGamePlayer Clone()
        {
            GA_HumanPongPlayer hpp = new GA_HumanPongPlayer();
            hpp.SetPlayerNumber(_PlayerNumber);
            return hpp;
        }

        public bool CanBeRuledBy(IGameRules rules)
        {
            return rules is IPongRules;
        }

        public IPlayMove GetMove(IMoveSelection selection, IGameField field)
        {
            if (field is IPongField)
            {
                return GetMove(selection, (IPongField)field);
            }
            else
            {
                return null;
            }
        }
    }

    public class GA_ComputerPongPlayer : IComputerPongPlayer
    {
        int _PlayerNumber = 0;
        public string Name { get; }

        public void SetPlayerNumber(int playerNumber)
        {
            _PlayerNumber = playerNumber;
        }

        public IPongMove GetMove(IPongField field)
        {
            /*
             * 
             ***Biemel***
            Hier muss beschrieben werden, wie der Computer reagiert, bzw. seine Züge macht.
            
            z.B.:
            if(vBallY > 0)                          //Ball bewegt sich nacht oben
            {
            return new PongMove(1, _PlayerNumber);  //Move wird mit movement = 1 übergeben
            }
            else
            {
            return new PongMove(0, _ Playernumber);
            }
            ***Biemel***
            *
            */

            //***Biemel***Hier wieder nur eine Beispielzeile, damit kein Fahler geworfen wird
            return new PongMove(_PlayerNumber, _PlayerNumber);
        }

        public IGamePlayer Clone()
        {
            GA_ComputerPongPlayer cpp = new GA_ComputerPongPlayer();
            cpp.SetPlayerNumber(_PlayerNumber);
            return cpp;
        }

        public bool CanBeRuledBy(IGameRules rules)
        {
            return rules is IPongRules;
        }

        public IPlayMove GetMove(IGameField field)
        {
            if (field is IPongField)
            {
                return GetMove((IPongField)field);
            }
            else
            {
                return null;
            }
        }
    }
}
