using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOPGames
{
    public interface IPaintLabyrinth : IPaintGame
    {
        void PaintLabyrinthField(Canvas canvas, ILabyrinthField labyrinthField);
    }

    public interface ILabyrinthField : IGameField
    {
        int this[int r, int c] { get; set; }

        ILabyrinthKachel[][] Kacheln { get; set; }
        ILabyrinthKachel FreeKachel { get; set; }
    }

    public interface ILabyrinthKachel
    {
        int Ways { get; set; }
        int Trophy { get; set; }
        int Player { get; set; }

        void Rotate(int dir);
    }

    public interface ILabyrinthRules : IGameRules
    {
        ILabyrinthField _LabyrinthField { get; }

        void DoLabyrinthGoMove(ILabyrinthGoMove labyrinthMove);
        void DoLabyrinthKachelMove(ILabyrinthKachelMove labyrinthMove);

    }

    public interface ILabyrinthGoMove : IRowMove, IColumnMove 
    { 

    }

    public interface ILabyrinthKachelMove : IRowMove, IColumnMove, IRotareMove
    {

    }

    public interface IHumanLabyrinthPlayer : IHumanGamePlayer
    {
        ILabyrinthGoMove GetGoMove(ILabyrinthGoMove labyrinthMove);

        ILabyrinthKachelMove GetKachelMove(ILabyrinthKachelMove labyrinthMove);

    }

    /*//
    public interface IComputerLabyrinthPlayer : IComputerGamePlayer
    {
        ILabyrinthMove GetMove(ILabyrinthMove labyrinthMove);
    }
    //*/
}
