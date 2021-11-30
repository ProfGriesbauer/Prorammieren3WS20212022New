using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOPGames.Interfaces.Labyrint
{
    public interface IPaintLabyrinth : IPaintGame
    {
        void PaintLabyrinthField(Canvas canvas, ILabyrinthField labyrinthField);
    }

    public interface ILabyrinthField : IGameField
    {
        int this[int r, int c] { get; set; }
    }

    public interface ILabyrinthRules : IGameRules
    {
        ILabyrinthField _LabyrinthField { get; }

        void DoLabyrinthMove(ILabyrinthMove labyrinthMove);
    }

    public interface ILabyrinthMove : IRowMove, IColumnMove 
    { 

    }

    public interface IHumanLabyrinthPlayer : IHumanGamePlayer
    {
        ILabyrinthMove GetMove(ILabyrinthMove labyrinthMove);
    }

    public interface IComputerLabyrinthPlayer : IComputerGamePlayer
    {
        ILabyrinthMove GetMove(ILabyrinthMove labyrinthMove);
    }
}
