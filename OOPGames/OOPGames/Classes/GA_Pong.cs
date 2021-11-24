using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOPGames
{
    public class ManageValues
    {
        public int _BallXPos;
        public int _BallYPos;
        public int _BallRad;
        public int _vXBall;
        public int _vYBall;
        //Values Player 1
        public int _p1ULXPos;       //X Pos of Upper Left Corner of Player 1 Design
        public int _p1ULYPos;       //Y Pos of Upper Left Corner of Player 1 Design
        public int _p1Width;        //Width (Length in X Dir.) of Player 1 Design
        public int _p1Height;       //Height (Length in Y Dir.) of Player 1 Design
        //Values Player 2
        public int _p2ULXPos;       //X Pos of Upper Left Corner of Player 2 Design
        public int _p2ULYPos;       //Y Pos of Upper Left Corner of Player 2 Design
        public int _p2Width;        //Width (Length in X Dir.) of Player 2 Design
        public int _p2Height;       //Height (Length in Y Dir.) of Player 2 Design

        public void setP1(int p1x, int p1y, int p1w, int p1h)
        {
            _p1ULXPos = p1x;
            _p1ULYPos = p1y; 
            _p1Width = p1w; 
            _p1Height = p1h;  
        }

        public void setP2(int p2x, int p2y, int p2w, int p2h)
        {
            _p2ULXPos = p2x;
            _p2ULYPos = p2y; 
            _p2Width = p2w; 
            _p2Height = p2h;  
        }

        public void updateBall(int bx, int by, int br, int vxb, int vyb)
        {
            _BallXPos = bx;
            _BallYPos = by;
            _BallRad = br;
            _vXBall = vxb;
            _vYBall = vyb;
        }

        public int p1x()
        {
            return _p1ULXPos;
        }

        public int p1y()
        {
            return _p1ULYPos;
        }

        public int p1w()
        {
            return _p1Width;
        }

        public int p1h()
        {
            return _p1Height;
        }

        public int p2x()
        {
            return _p2ULXPos;
        }

        public int p2y()
        {
            return _p2ULYPos;
        }

        public int p2w()
        {
            return _p2Width;
        }

        public int p2h()
        {
            return _p2Height;
        }

        public int bx()
        {
            return _BallXPos;
        }

        public int by()
        {
            return _BallYPos;
        }

        public int br()
        {
            return _BallRad;
        }

        public int vxb()
        {
            return _vXBall;
        }

        public int vyb()
        {
            return _vYBall;
        }
    }


    public static class Values
    {
        public static ManageValues _Val = new ManageValues();

        public static void setP1(int p1x, int p1y, int p1w, int p1h)
        {
            _Val.setP1(p1x, p1y, p1w, p1h);
        }

        public static void setP2(int p2x, int p2y, int p2w, int p2h)
        {
            _Val.setP2(p2x, p2y, p2w, p2h);
        }

        public static void updateBall(int bx, int by, int br, int vxb, int vyb)
        {
            _Val.updateBall(bx, by, br, vxb, vyb);
        }

        public int p1x()
        {
            return _Val.p1x();
        }

        public int p1y()
        {
            return _Val.p1y();
        }

        public int p1w()
        {
            return _Val.p1w();
        }

        public int p1h()
        {
            return _Val.p1h();
        }

        public int p2x()
        {
            return _Val.p2x();
        }

        public int p2y()
        {
            return _Val.p2y();
        }

        public int p2w()
        {
            return _Val.p2w();
        }

        public int p2h()
        {
            return _Val.p2h();
        }

        public int bx()
        {
            return _Val.bx();
        }

        public int by()
        {
            return _Val.by();
        }

        public int br()
        {
            return _Val.br();
        }

        public int vxb()
        {
            return _Val.vxb();
        }

        public int vyb()
        {
            return _Val.vyb();
        }
    }

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
