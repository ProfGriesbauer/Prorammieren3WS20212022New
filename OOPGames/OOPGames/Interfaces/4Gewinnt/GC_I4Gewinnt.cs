using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOPGames
{
  
    public interface IPaintVierGewinnt : IPaintGame
    {
        void PaintVierGewinntField(Canvas canvas, IVierGewinntField currentField);
    }



    public interface IVierGewinntField : IGameField
    {
        int this[int r, int c] { get; set; }
    }



    public interface IVierGewinntRules : IGameRules
    {
        IVierGewinntField VierGewinntField { get; }
        void DoVierGewinntMove(IVierGewinntMove move);
    }



    public interface IVierGewinntRules_GE : IVierGewinntRules
    {
        void AskForGameSize();
    }

 

    public interface IVierGewinntMove : IRowMove, IColumnMove
    {

    }



    public interface IHumanVierGewinntPlayer : IHumanGamePlayer
    {
        IVierGewinntMove GetMove(IMoveSelection selection, IVierGewinntField field);
    }



    public interface IComputerVierGewinntPlayer : IComputerGamePlayer
    {
        IVierGewinntMove GetMove(IVierGewinntField field);
    }
}