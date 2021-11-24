using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOPGames
{
    public abstract class IDino_GamePlayer : IHumanGamePlayer
    {
        public abstract string Name { get; }
        public abstract bool CanBeRuledBy(IGameRules rules);
        public abstract IGamePlayer Clone();

        public abstract Dino_PlayMove GetMove(IMoveSelection selection, Dino_GameField field);

        public IPlayMove GetMove(IMoveSelection selection, IGameField field)
        {
            return GetMove(selection, (Dino_GameField) field);
        }

        public abstract void SetPlayerNumber(int playerNumber);
    }

    public abstract class IDino_GameRules : IGameRules
    {
        public abstract string Name { get; }
        public abstract IGameField CurrentField { get; }
        public abstract bool MovesPossible { get; }

        public abstract int CheckIfPLayerWon();
        public abstract void ClearField();
        public abstract void DoMove(Dino_PlayMove move);
        
        public void DoMove(IPlayMove move)
        {
            if (move is Dino_PlayMove)
            {
                DoMove((Dino_PlayMove) move);
            }
        }
    }

    public interface IDino_GameField : IGameField
    {
        int this[int i] { get; set; }
    }

    public abstract class IDino_PaintGame : IPaintGame
    {
        public abstract string Name { get; }

        public abstract void PaintDinoGameField(Canvas canvas, IDino_GameField currentField);

        public void PaintGameField(Canvas canvas, IGameField currentField)
        {
            if (currentField is IDino_GameField)
            {
                PaintDinoGameField(canvas, (IDino_GameField) currentField);
            }
        }

    }

    public interface IDino_PlayMove : IPlayMove
    {
        
    }
}
