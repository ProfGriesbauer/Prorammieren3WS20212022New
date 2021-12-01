using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOPGames
{
    //Pong specific paint game
    public interface IPaintPong : IPaintGame
    {
        //Paints the given game field on the given canvas
        //NOTE: Clearing the canvas, etc. has to be done within this function
        void PaintPongField(Canvas canvas, IPongField currentField);
    }

    //Pong specific game field 3x3
    public interface IPongField : IGameField
    {
        //Indexer: returns 0 for a unused Pongfield, 1 for player 1, 2 for player 2, etc.
        //indexed by the row r and column c
        int this[int r, int c] { get; set; }
    }

    //Pong specific game rules
    public interface IPongRules : IGameRules
    {
        //Gets the current state of the Pong field; the class implementing
        //this interface should hold a game field corresponding to the rules
        //it implements
        IPongField PongField { get; }

        //Adds the given move to the current Pong field if possible
        void DoPongMove(IPongMove move);
    }

    public interface IPongRules : IPongRules
    {
        void AskForGameSize();
    }

    //PongMove which is derived from row and column
    public interface IPongMove : IRowMove, IColumnMove
    {

    }

    //Pong specific human player
    public interface IHumanPongPlayer : IHumanGamePlayer
    {
        //Returns a valid move if possible for the given selection and 
        //the given state of the tic tac toe field.
        //IF THE GIVEN SELECTION IS NO VALID MOVE, NULL HAS TO BE RETURNED.
        IPongMove GetMove(IMoveSelection selection, IPongField field);
    }

    //Pong specific human player
    public interface IComputerPongPlayer : IComputerGamePlayer
    {
        //Returns a valid move and the given state of the tic tac toe field.
        IPongMove GetMove(IPongField field);
    }
}
