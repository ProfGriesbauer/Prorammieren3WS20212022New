using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOPGames
{
    public abstract class BaseVierGewinntPaint : IPaintVierGewinnt
    {
        public abstract string Name { get; }

        public abstract void PaintVierGewinntField(Canvas canvas, IVierGewinntField currentField);

        public void PaintGameField(Canvas canvas, IGameField currentField)
        {
            if (currentField is IVierGewinntField)
            {
                PaintVierGewinntField(canvas, (IVierGewinntField)currentField);
            }
        }
    }

    public abstract class BaseVierGewinntField : IVierGewinntField
    {
        public abstract int this[int r, int c] { get; set; }

        public bool CanBePaintedBy(IPaintGame painter)
        {
            return painter is IPaintVierGewinnt;
        }
    }

    public abstract class BaseVierGewinntRules : IVierGewinntRules
    {
        public abstract IVierGewinntField VierGewinntField { get; }

        public abstract bool MovesPossible { get; }

        public abstract string Name { get; }

        public abstract int CheckIfPLayerWon();

        public abstract void ClearField();

        public abstract void DoVierGewinntMove(IVierGewinntMove move);

        public IGameField CurrentField { get { return VierGewinntField; } }

        public void DoMove(IPlayMove move)
        {
            if (move is IVierGewinntMove)
            {
                DoVierGewinntMove((IVierGewinntMove)move);
            }
        }
    }

    public abstract class BaseHumanVierGewinntPlayer : IHumanVierGewinntPlayer
    {
        public abstract string Name { get; }

        public abstract IVierGewinntMove GetMove(IMoveSelection selection, IVierGewinntField field);

        public abstract void SetPlayerNumber(int playerNumber);

        public abstract IGamePlayer Clone();

        public bool CanBeRuledBy(IGameRules rules)
        {
            return rules is IVierGewinntRules;
        }

        public IPlayMove GetMove(IMoveSelection selection, IGameField field)
        {
            if (field is IVierGewinntField)
            {
                return GetMove(selection, (IVierGewinntField)field);
            }
            else
            {
                return null;
            }
        }
    }

    public abstract class BaseComputerVierGewinntPlayer : IComputerVierGewinntPlayer
    {
        public abstract string Name { get; }

        public abstract void SetPlayerNumber(int playerNumber);

        public abstract IVierGewinntMove GetMove(IVierGewinntField field);

        public abstract IGamePlayer Clone();

        public bool CanBeRuledBy(IGameRules rules)
        {
            return rules is IVierGewinntRules;
        }

        public IPlayMove GetMove(IGameField field)
        {
            if (field is IVierGewinntField)
            {
                return GetMove((IVierGewinntField)field);
            }
            else
            {
                return null;
            }
        }
    }
}
