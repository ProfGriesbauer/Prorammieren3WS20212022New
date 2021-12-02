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
        int _Ways = 0;

        public int Ways { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Trophy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Player { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Rotate(int dir)
        {
            throw new NotImplementedException();
        }
    }

    public class LabyrinthLKachel : BaseLabyrinthKachel
    {

    }

    public class LabyrinthIKachel : BaseLabyrinthKachel
    {

    }

    public class LabyrinthTKachel : BaseLabyrinthKachel
    {

    }

    public class LabyrinthXKachel : BaseLabyrinthKachel
    {

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
