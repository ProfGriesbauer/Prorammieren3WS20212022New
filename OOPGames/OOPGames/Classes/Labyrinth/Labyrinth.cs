using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOPGames
{
    public class PaintLabyrinth // : IPaintLabyrinth
    {
        
    }

    public class LabyrinthField : ILabyrinthField
    {
        int _Size = 7;

        BaseLabyrinthKachel[,] _Kacheln;
        BaseLabyrinthKachel _FreeKachel;

        public LabyrinthField ()
        {
            _Kacheln = new BaseLabyrinthKachel[_Size, _Size];
        }

        public int this[int r, int c]
        {
            get
            {
                if (r >= 0 && r < _Size && c >= 0 && c < _Size)
                {
                    return _Kacheln[r, c].Player;
                }
                else
                {
                    return -1;
                }
            }

            set
            {
                if (r >= 0 && r < _Size && c >= 0 && c < _Size)
                {
                    _Kacheln[r, c].Player = value;
                }
            }
        }

        public ILabyrinthKachel[,] Kacheln { get { return _Kacheln; } set => throw new NotImplementedException(); }
        public ILabyrinthKachel FreeKachel { get { return _FreeKachel; } set => throw new NotImplementedException(); }

        public bool CanBePaintedBy(IPaintGame painter)
        {
            return painter is IPaintLabyrinth;
        }
    }

    public abstract class BaseLabyrinthKachel : ILabyrinthKachel
    {
        protected int _Ways, _Trophy, _Player;

        //*//
        public BaseLabyrinthKachel()
        {
            _Ways = 0;
            _Trophy = 0;
            _Player = 0;
            Rotate(1);
        }
        //*/

        public int Ways { get { return _Ways; } set => throw new NotImplementedException(); }
        public int Trophy { get { return _Trophy; } set => throw new NotImplementedException(); }
        public int Player { get { return _Player; } set { _Player = value; } }

        public abstract void Rotate(int dir);
    }

    public class LabyrinthLKachel : BaseLabyrinthKachel
    {
        
        public override void Rotate(int dir)
        {
            switch (dir)
            {
                case 0:
                    switch (_Ways)
                    {
                        case 0b0011: _Ways = 0b0110; break;
                        case 0b0110: _Ways = 0b1100; break;
                        case 0b1100: _Ways = 0b1001; break;
                        case 0b1001: _Ways = 0b0011; break;
                        default: _Ways = 0b0011; break;
                    }
                    break;
                case 1:
                    switch (_Ways)
                    {
                        case 0b1100: _Ways = 0b0110; break;
                        case 0b0110: _Ways = 0b0011; break;
                        case 0b0011: _Ways = 0b1001; break;
                        case 0b1001: _Ways = 0b1100; break;
                        default: _Ways = 0b0011; break;
                    }
                    break;
                default: break;
            }
        }
    }

    public class LabyrinthIKachel : BaseLabyrinthKachel
    {
       
        public override void Rotate(int dir)
        {

        }
    }

    public class LabyrinthTKachel : BaseLabyrinthKachel
    {
        
        public override void Rotate(int dir)
        {

        }
    }

    public class LabyrinthXKachel : BaseLabyrinthKachel
    {
        public LabyrinthXKachel()
        {
            _Ways = 0b1111;
        }

        public override void Rotate(int dir)
        {
            
        }
    }

    public class LabyrinthRules // : ILabyrinthRules
    {
        
    }

    public class LabyrinthGoMove // : ILabyrinthGoMove
    {

    }

    public class LabyrinthKachelMove // : ILabyrinthKachelMove 
    {

    }

    public class HumanLabyrinthPlayer // : IHumanLabyrinthPlayer
    {
        
    }

    /*//
    public class ComputerLabyrinthPlayer // : IComputerLabyrinthPlayer
    {
        
    }
    //*/
}
