using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPGames
{
    //Erbreihenfolge: BaseTicTactoe(abstrakte Klasse) : ITickTacToe : I
    class GG_HumanTicTacToePlayer : BaseHumanTicTacToePlayer
    {
        //CanBeRuled und GetMove sind bereits in abstrakter Klasse implementiert

        int _PlayerNumber = 0;
        //Name of the Game Player: possibly use a unique name
        public override string Name { get { return "GGHumanTicTacToePlayer"; }  }

        //Returns new object being a clone of the current player
        public override IGamePlayer Clone()
        {
            TicTacToeHumanPlayer ttthpg = new TicTacToeHumanPlayer();
            ttthpg.SetPlayerNumber(_PlayerNumber);
            return ttthpg;
        }
        //GetMove in Abstrakter Klasse prüft nur, ob field== TicTacToeField
        //Warum wird dann die Methode dieser Subklasse aufgerufen? -> Methode überladen, Methode mit passendem Parameter wird aufgerufen
        //Methode werden zwei Koordinaten übergeben, prüft, welches Feld aus 3x3 Matrix betroffen, gibt TTTMove zurück
        public override ITicTacToeMove GetMove(IMoveSelection selection, ITicTacToeField field)
        {
            for (int i = 0; i<3; i++)
            {
                for (int j = 0; j<3; j++)
                {
                    if (selection.XClickPos > 20 + (j*100) && selection.XClickPos < 120 + (j*100) &&
                        selection.YClickPos > 20 + (i*100) && selection.YClickPos < 120 + (i*100))
                    {
                        return new TicTacToeMove(i, j, _PlayerNumber);
                    }
                }
            }

            return null;
            
        }
        //Sets the player number of this player; this number should be
        //stored by the implementing class and should be used for any
        //given play move
        public override void SetPlayerNumber(int playerNumber)
        {
            _PlayerNumber = playerNumber;
        }
    }

    public class GG_ComputerTicTacToePlayer : BaseComputerTicTacToePlayer
    {
        int _PlayerNumber = 0;
        public override string Name
        {
            get { return "GGComputerPlayer"; }
        }

        public override IGamePlayer Clone()
        {
            TicTacToeComputerPlayer tttcp = new TicTacToeComputerPlayer();
            tttcp.SetPlayerNumber(_PlayerNumber);
            return tttcp;
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
                    return new TicTacToeMove(r, c, _PlayerNumber);
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
    public class GG_TicTacToeMove : ITicTacToeMove
    {
        int _Row = 0;
        int _Column = 0;
        int _PlayerNumber = 0;

        public GG_TicTacToeMove(int row, int column, int playerNumber)
        {
            _Row = row;
            _Column = column;
            _PlayerNumber = playerNumber;
        }

        public int Row { get { return _Row; } }

        public int Column { get { return _Column; } }

        public int PlayerNumber { get { return _PlayerNumber; } }
    }

    public class GG_TicTacToeRules : BaseTicTacToeRules
    {
        TicTacToeField _Field = new TicTacToeField();
        public override ITicTacToeField TicTacToeField
        {
            get { return _Field; }
        }
        public override bool MovesPossible
        {
            get
            {
                for (int i=0; i<3; i++)
                {
                    for (int j=0; i<3; j++)
                    {
                        if (_Field[i,j] == 0)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }
        public override string Name
        {
            get { return "GG Rulez"; }
        }

        public override int CheckIfPLayerWon()
        { 
            int check = 0;
            //3 Gleiche in Reihe:
            for (int r = 0; r < 3; r++)
            {
                for (int c = 1; c < 3; c++)
                {
                    if (_Field[r, c-1] > 0 && _Field[r, c-1] == _Field[r, c])
                    {
                        check++;
                    }
                }
                if (check == 2)
                {
                    return _Field[r, 0];
                }
                check = 0;
            }
            check = 0;
            //3 Gleiche in Spalte:
            for (int c = 0; c < 3; c++)
            {
                for (int r = 1; r < 3; r++)
                {
                    if (_Field[r-1, c] > 0 && _Field[r-1, c] == _Field[r, c])
                    {
                        check++;
                    }
                }
                if (check == 2)
                {
                    return _Field[0, c];
                }
                check = 0;
            }
            //3 Gleiche Diagonal:
            if (_Field[0, 0] > 0 && _Field[0, 0] == _Field[1, 1] && _Field[1, 1] == _Field[2, 2])
            {
                return _Field[0, 0];
            }
            else if (_Field[0, 2] > 0 && _Field[0, 2] == _Field[1, 1] && _Field[1, 1] == _Field[2, 0])
            {
                return _Field[0, 2];
            }

            return -1;
        }

        public override void ClearField()
        {
            for (int i = 0; i<3; i++)
            {
                for (int j = 0; j<3; j++)
                {
                    _Field[i, j] = 0;
                }
            }
        }

        public override void DoTicTacToeMove(ITicTacToeMove move)
        {   //Spielzug im Spielfeld?
            if (move.Row >=0 && move.Row<3 && move.Column>=0 && move.Column<3)
            {   //Spielfeld leer?
                if(_Field[move.Row, move.Column] == 0)
                {
                    _Field[move.Row, move.Column] = move.PlayerNumber;
                }
            }
        }
    }
    
    public class GG_TicTacToeField : BaseTicTacToeField
    {   //Erzeugt "leere" 3x3 Matrix -> für Rulesklasse
        int[,] _Field = new int [3,3] { {0,0,0}, {0,0,0}, {0,0,0} };

        public override int this[int r, int c]
        {
            get
            {
                if (r >=0 && r<3 && c>= 0 && c<3)
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
}
