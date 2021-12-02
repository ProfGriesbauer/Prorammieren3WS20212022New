using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOPGames
{
    public abstract class BaseSnakePaint : IPaintSnake
    {
        public abstract string Name { get; }

        public abstract void PaintSnakeField(Canvas canvas, ISnakeField currentField);

        public void PaintGameField(Canvas canvas, IGameField currentField)
        {
            if (currentField is ISnakeField)
            {
                PaintSnakeField(canvas, (ISnakeField)currentField);
            }
        }
    }

    public abstract class BaseSnakeField : ISnakeField
    {
        public abstract int this[int r, int c] { get; set; }

        public bool CanBePaintedBy(IPaintGame painter)
        {
            return painter is IPaintSnake;
        }
    }


    

    public abstract class BaseSnakeRules : ISnakeRules
    {
        public abstract ISnakeField SnakeField { get; }

        public abstract bool MovesPossible { get; }

        public abstract string Name { get; }

        public abstract int CheckIfPLayerWon();

        public abstract void ClearField();

        public abstract void DoSnakeMove(ISnakeMove move);

        public IGameField CurrentField { get { return SnakeField; } }

        public void DoMove(IPlayMove move)
        {
            if (move is ISnakeMove)
            {
                DoSnakeMove((ISnakeMove)move);
            }
        }
    }

    public abstract class BaseHumanSnakePlayer : IHumanSnakePlayer
    {
        public abstract string Name { get; }

        public abstract ISnakeMove GetMove(IMoveSelection selection, ISnakeField field);

        public abstract void SetPlayerNumber(int playerNumber);

        public abstract IGamePlayer Clone();

        public bool CanBeRuledBy(IGameRules rules)
        {
            return rules is ISnakeRules;
        }

        public IPlayMove GetMove(IMoveSelection selection, IGameField field)
        {
            if (field is ISnakeField)
            {
                return GetMove(selection, (ISnakeField)field);
            }
            else
            {
                return null;
            }
        }
    }

    public abstract class BaseComputerSnakePlayer : IComputerSnakePlayer
    {
        public abstract string Name { get; }

        public abstract void SetPlayerNumber(int playerNumber);

        public abstract ISnakeMove GetMove(ISnakeField field);

        public abstract IGamePlayer Clone();

        public bool CanBeRuledBy(IGameRules rules)
        {
            return rules is ISnakeRules;
        }

        public IPlayMove GetMove(IGameField field)
        {
            if (field is ISnakeField)
            {
                return GetMove((ISnakeField)field);
            }
            else
            {
                return null;
            }
        }
    }
}
