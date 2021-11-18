using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Shapes;
//using System.Drawing.Point;
//ToDo:
/*
 Am montag den Griesbauer Fragen: 
-Warum gehts nicht?
-Test
Bisschen einlesen

Test

 */

namespace OOPGames
{
    public class GE_TicTacToePaint : BaseTicTacToePaint
    {
        public override string Name { get { return "GE_TicTacToePaint"; } }

        public override void PaintTicTacToeField(Canvas canvas, ITicTacToeField currentField)
        {
            canvas.Children.Clear();
            Color bgColor = Color.FromRgb(255, 255, 255);
            canvas.Background = new SolidColorBrush(bgColor);
            Color lineColor = Color.FromRgb(0, 255, 255);
            Brush lineStroke = new SolidColorBrush(lineColor);
            Color XColor = Color.FromRgb(0, 255, 0);
            Brush XStroke = new SolidColorBrush(XColor);
            Color OColor = Color.FromRgb(0, 0, 255);
            Brush OStroke = new SolidColorBrush(OColor);

            Line l1 = new Line() { X1 = 120, Y1 = 20, X2 = 120, Y2 = 320, Stroke = lineStroke, StrokeThickness = 3.0 };
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = 220, Y1 = 20, X2 = 220, Y2 = 320, Stroke = lineStroke, StrokeThickness = 3.0 };
            canvas.Children.Add(l2);
            Line l3 = new Line() { X1 = 20, Y1 = 120, X2 = 320, Y2 = 120, Stroke = lineStroke, StrokeThickness = 3.0 };
            canvas.Children.Add(l3);
            Line l4 = new Line() { X1 = 20, Y1 = 220, X2 = 320, Y2 = 220, Stroke = lineStroke, StrokeThickness = 3.0 };
            canvas.Children.Add(l4);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (currentField[i, j] == 1)
                    {
                        Line X1 = new Line() { X1 = 20 + (j * 100), Y1 = 20 + (i * 100), X2 = 120 + (j * 100), Y2 = 120 + (i * 100), Stroke = XStroke, StrokeThickness = 3.0 };
                        canvas.Children.Add(X1);
                        Line X2 = new Line() { X1 = 20 + (j * 100), Y1 = 120 + (i * 100), X2 = 120 + (j * 100), Y2 = 20 + (i * 100), Stroke = XStroke, StrokeThickness = 3.0 };
                        canvas.Children.Add(X2);
                    }
                    else if (currentField[i, j] == 2)
                    {
                        Ellipse OE = new Ellipse() { Margin = new Thickness(20 + (j * 100), 20 + (i * 100), 0, 0), Width = 100, Height = 100, Stroke = OStroke, StrokeThickness = 3.0 };
                        canvas.Children.Add(OE);
                    }
                }
            }
        }
    }


    
    public class GE_TicTacToeRules : BaseTicTacToeRules_GE
    {
        GE_TicTacToeField _Field = new GE_TicTacToeField(5);

        public override ITicTacToeField TicTacToeField { get { return _Field; } }

        public override bool MovesPossible 
        { 
            get 
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (_Field[i, j] == 0)
                        {
                            return true;
                        }
                    }
                }

                return false; 
            } 
        }

        public override string Name { get { return "GE_TicTacToeRules"; } }

        public override int CheckIfPLayerWon()
        {
            for (int p = 1; p < 3; p++)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (_Field[i, 0] > 0 && _Field[i, 0] == _Field[i, 1] && _Field[i, 1] == _Field[i, 2])
                    {
                        return p;
                    }
                    else if (_Field[0, i] > 0 && _Field[0, i] == _Field[1, i] && _Field[1, i] == _Field[2, i])
                    {
                        return p;
                    }
                }

                if ((_Field[0, 0] > 0 && _Field[0, 0] == _Field[1, 1] && _Field[1, 1] == _Field[2, 2]) ||
                    (_Field[0, 2] > 0 && _Field[0, 2] == _Field[1, 1] && _Field[1, 1] == _Field[2, 0]))
                {
                    return p;
                }
            }

            return -1;
        }

        public override void ClearField()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _Field[i, j] = 0;
                }
            }
        }

        public override void DoTicTacToeMove(ITicTacToeMove move)
        {
            if (move.Row >= 0 && move.Row < 3 && move.Column >= 0 && move.Column < 3)
            {
                _Field[move.Row, move.Column] = move.PlayerNumber;
            }
        }

        public override void AskForGameSize()
        {
            //int size = Prompt.ShowDialog("Test", "123");

        }
        //commentar
        /*
        public static class Prompt
        {
            public static int ShowDialog(string text, string caption)
            {
                Form prompt = new Form();
                prompt.Width = 500;
                prompt.Height = 100;
                prompt.Text = caption;
                System.Windows.Forms.Label textLabel = new System.Windows.Forms.Label() { Left = 50, Top = 20, Text = text };
                string[] auswahl = { "klein", "mittel", "groß"};
                System.Windows.Forms.ComboBox dropDown = new System.Windows.Forms.ComboBox();
                dropDown.Items.AddRange(auswahl);
                dropDown.Location = new System.Drawing.Point(10, 30);
                dropDown.IntegralHeight = false;
                dropDown.MaxDropDownItems = 3;
                dropDown.DropDownStyle = ComboBoxStyle.DropDownList;
                dropDown.Name = "ComboBox1";
                dropDown.Size = new System.Drawing.Size(136, 81);
                dropDown.TabIndex = 0;
                dropDown.SelectedIndexChanged += 
                    new System.EventHandler((e)=>Console.log("sdlkfj"));

                System.Windows.Forms.Button confirmation = new System.Windows.Forms.Button() { Text = "Ok", Left = 350, Width = 100, Top = 70 };
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.Controls.Add(dropDown);
                prompt.ShowDialog();
                return (int)dropDown.Value;
            }
        }
        */

    }


    /*
    public class GE_TicTacToeRules : BaseTicTacToeRules
    {

        int[,] fieldSize = {{3,3}, {6,6}, {9,9}}; //Besser implementieren: WO soll die Variable hin? GB
        int auswahl = 0;
        TicTacToeField _Field;// = new GE_TicTacToeField();

        public override ITicTacToeField TicTacToeField { get { return _Field; } }

        public override bool MovesPossible //muss auf die spielfeldgröße angepasst werden
        { 
            get 
            {
                for (int i = 0; i < fieldSize[auswahl][0]; i++)
                {
                    for (int j = 0; j < fieldSize[auswahl][1]; j++)
                    {
                        if (_Field[i, j] == 0)
                        {
                            return true;
                        }
                    }
                }
                return false; 
            } 
        }

        public override string Name { get { return "GE_TicTacToeRules"; } }

        public override int CheckIfPLayerWon()//anpassen auf spielfeld größe; (vlt anpassen ab wie viel man gewinnt)
        {
            for (int p = 1; p < 3; p++)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (_Field[i, 0] > 0 && _Field[i, 0] == _Field[i, 1] && _Field[i, 1] == _Field[i, 2])
                    {
                        return p;
                    }
                    else if (_Field[0, i] > 0 && _Field[0, i] == _Field[1, i] && _Field[1, i] == _Field[2, i])
                    {
                        return p;
                    }
                }

                if ((_Field[0, 0] > 0 && _Field[0, 0] == _Field[1, 1] && _Field[1, 1] == _Field[2, 2]) ||
                    (_Field[0, 2] > 0 && _Field[0, 2] == _Field[1, 1] && _Field[1, 1] == _Field[2, 0]))
                {
                    return p;
                }
            }

            return -1;
        }

        public override void ClearField()//anpassen
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _Field[i, j] = 0;
                }
            }
        }

        public override void DoTicTacToeMove(ITicTacToeMove move)//anpassen
        {
            if (move.Row >= 0 && move.Row < 3 && move.Column >= 0 && move.Column < 3)
            {
                _Field[move.Row, move.Column] = move.PlayerNumber;
            }
        }
    }
    */

    public abstract class BaseTicTacToeRules_GE : ITicTacToeRules_GE
    {
        public abstract ITicTacToeField TicTacToeField { get; }

        public abstract bool MovesPossible { get; }

        public abstract string Name { get; }

        public abstract int CheckIfPLayerWon();

        public abstract void ClearField();

        public abstract void DoTicTacToeMove(ITicTacToeMove move);

        public IGameField CurrentField { get { return TicTacToeField; } }

        public void DoMove(IPlayMove move)
        {
            if (move is ITicTacToeMove)
            {
                DoTicTacToeMove((ITicTacToeMove)move);
            }
        }

        public abstract void AskForGameSize();
        /*
        {
            //open window
        }
        */
    }

    public class GE_TicTacToeField : BaseTicTacToeField
    {
        int _Size;
        int[,] _Field;
        public GE_TicTacToeField(int s)
        {
            _Size = s;
            _Field = new int[s, s];
            for(int i = 0; i < s; i++)
            {
                for(int j = 0; j < s; j++)
                {
                    _Field[i,j] = 0;
                }
            }
        }

        public override int this[int r, int c]
        {
            get
            {
                if (r >= 0 && r < 3 && c >= 0 && c < 3)//anpassen
                {
                    return _Field[r, c];
                }
                else
                {
                    return -1;
                }
            }

            set
            {
                if (r >= 0 && r < 3 && c >= 0 && c < 3)//anpassen
                {
                    _Field[r, c] = value;
                }
            }
        }

        public void AskForGameSize()
        {
            throw new NotImplementedException();
        }

    }
    
    public class GE_TicTacToeMove : ITicTacToeMove
    {
        int _Row = 0;
        int _Column = 0;
        int _PlayerNumber = 0;

        public GE_TicTacToeMove (int row, int column, int playerNumber)
        {
            _Row = row;
            _Column = column;
            _PlayerNumber = playerNumber;
        }

        public int Row { get { return _Row; } }

        public int Column { get { return _Column; } }

        public int PlayerNumber { get { return _PlayerNumber; } }
    }

    public class GE_TicTacToeHumanPlayer : BaseHumanTicTacToePlayer
    {
        int _PlayerNumber = 0;

        public override string Name { get { return "GE_HumanTicTacToePlayer"; } }

        public override IGamePlayer Clone()
        {
            GE_TicTacToeHumanPlayer ttthp = new GE_TicTacToeHumanPlayer();
            ttthp.SetPlayerNumber(_PlayerNumber);
            return ttthp;
        }

        public override ITicTacToeMove GetMove(IMoveSelection selection, ITicTacToeField field)//support für tasten und hidden modus
        {
            for (int i = 0; i < 3; i++)//anpassen
            {
                for (int j = 0; j < 3; j++)//anpassen
                {
                    if (selection.XClickPos > 20 + (j*100) && selection.XClickPos < 120 + (j*100) &&
                        selection.YClickPos > 20 + (i*100) && selection.YClickPos < 120 + (i*100))
                    {
                        return new GE_TicTacToeMove(i, j, _PlayerNumber);
                    }
                }
            }

            return null;
        }

        public override void SetPlayerNumber(int playerNumber)
        {
            _PlayerNumber = playerNumber;
        }
    }

   public class GE_TicTacToeComputerPlayer : BaseComputerTicTacToePlayer //lege neuen computerspieler an
    {
        int _PlayerNumber = 0;

        public override string Name { get { return "GE_ComputerTicTacToePlayer"; } }

        public override IGamePlayer Clone()
        {
            GE_TicTacToeComputerPlayer ttthp = new GE_TicTacToeComputerPlayer(); //make new player
            ttthp.SetPlayerNumber(_PlayerNumber);
            return ttthp;
        }

        public override ITicTacToeMove GetMove(ITicTacToeField field) //change auf unser spielfeld
        {
            Random rand = new Random();
            int f = rand.Next(0, 8);
            for (int i = 0; i < 9; i++)
            {
                int c = f % 3;
                int r = ((f - c) / 3) % 3;
                if (field[r, c] <= 0)
                {
                    return new TicTacToeMove(r, c, _PlayerNumber);
                }
                else
                {
                    f++;
                }
            }

            return null;
        }

        public override void SetPlayerNumber(int playerNumber)
        {
            _PlayerNumber = playerNumber;
        }
    }
}
