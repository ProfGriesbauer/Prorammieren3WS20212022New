using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOPGames
{
    //TicTacToe specific paint game
    //DIESES INTERFACE NICHT ÄNDERN!
    public interface IPaintSnake : IPaintGame
    {
        //Paints the given game field on the given canvas
        //NOTE: Clearing the canvas, etc. has to be done within this function
        void PaintSnakeField(Canvas canvas, ISnakeField currentField);
    }

    //TicTacToe specific game field 3x3
    //DIESES INTERFACE NICHT ÄNDERN!
    public interface ISnakeField : IGameField
    {
        //Indexer: returns 0 for a unused tictactoefield, 1 for player 1, 2 for player 2, etc.
        //indexed by the row r and column c
        int this[int r, int c] { get; set; }
    }
   
    //TicTacToe specific game rules
    //DIESES INTERFACE NICHT ÄNDERN!
    public interface ISnakeRules : IGameRules
    {
        //Gets the current state of the tictactoe field; the class implementing
        //this interface should hold a game field corresponding to the rules
        //it implements
        ISnakeField SnakeField { get; }

        //Adds the given move to the current tictactoe field if possible
        void DoSnakeMove(ISnakeMove move);
    }

  
    //TicTacToeMove which is derived from row and column
    //DIESES INTERFACE NICHT ÄNDERN!
    public interface ISnakeMove : IRowMove, IColumnMove
    {

    }

    //TicTacToe specific human player
    //DIESES INTERFACE NICHT ÄNDERN!
    public interface IHumanSnakePlayer : IHumanGamePlayer
    {
        //Returns a valid move if possible for the given selection and 
        //the given state of the tic tac toe field.
        //IF THE GIVEN SELECTION IS NO VALID MOVE, NULL HAS TO BE RETURNED.
        ISnakeMove GetMove(IMoveSelection selection, ISnakeField field);
    }

    //TicTacToe specific human player
    //DIESES INTERFACE NICHT ÄNDERN!
    public interface IComputerSnakePlayer : IComputerGamePlayer
    {
        //Returns a valid move and the given state of the tic tac toe field.
        ISnakeMove GetMove(ISnakeField field);
    }
}
