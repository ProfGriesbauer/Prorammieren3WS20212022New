using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace OOPGames
{
    public class SnakePaint : BaseSnakePaint
    {
        int count = 0;
        int X = 0;
        int Y = 0;
        public void generateFood()
        {
    
            Random random = new Random();
            X = random.Next(0, 400);
            Y= random.Next(0, 400);

           
             
        }
        public override string Name { get { return "SnakePaint"; } }

        public override void PaintSnakeField(Canvas canvas, ISnakeField currentField)
        {
            canvas.Children.Clear();
            Color bgColor = Color.FromRgb(255, 255, 255);
            canvas.Background = new SolidColorBrush(bgColor);
            Color FoodColor = Color.FromRgb(139, 26, 26);
            Brush FoodStroke = new SolidColorBrush(FoodColor);
            Color SnakeColor = Color.FromRgb(0, 255, 0);
            Brush SnakeStroke = new SolidColorBrush(SnakeColor);

            Ellipse OE = new Ellipse() { Margin = new Thickness(200 + ( 1* 10), 200 + (1 * 10), 0, 0), Width = 10, Height = 10, Stroke = SnakeStroke, StrokeThickness = 3.0 };
            canvas.Children.Add(OE);
            Ellipse Oa = new Ellipse { Margin = new Thickness(X, Y, 0, 0), Width = 10, Height = 10, Stroke = FoodStroke, StrokeThickness = 3.0 };
            canvas.Children.Add(Oa);
            count++;
            if (count == 1)
            {
               
               generateFood();
            }

           
        }

    }



    public class SnakeRules : BaseSnakeRules
    {
        SnakeField _Field = new SnakeField();

        public override ISnakeField SnakeField { get { return _Field; } }

        public override bool MovesPossible
        {
            get
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
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

        public override string Name { get { return "SnakeRules"; } }

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

        public override void DoSnakeMove(ISnakeMove move)
        {
            if (move.Row >= 0 && move.Row < 3 && move.Column >= 0 && move.Column < 3)
            {
                _Field[move.Row, move.Column] = move.PlayerNumber;
            }
        }
    }

    public class SnakeField : BaseSnakeField
    {
        int[,] _Field = new int[10, 10] { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };

        public override int this[int r, int c]
        {
            get
            {
                if (r >= 0 && r < 10 && c >= 0 && c < 10)
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
                if (r >= 0 && r < 10 && c >= 0 && c < 10)
                {
                    _Field[r, c] = value;
                }
            }
        }
    }

    public class SnakeMove : ISnakeMove
    {
        int _Row = 0;
        int _Column = 0;
        int _PlayerNumber = 0;

        public SnakeMove(int row, int column, int playerNumber)
        {
            _Row = row;
            _Column = column;
            _PlayerNumber = playerNumber;
        }

        public int Row { get { return _Row; } }

        public int Column { get { return _Column; } }

        public int PlayerNumber { get { return _PlayerNumber; } }
    }

    public class SnakeHumanPlayer : BaseHumanSnakePlayer
    {
        int _PlayerNumber = 0;

        public override string Name { get { return "SnakePlayer"; } }

        public override IGamePlayer Clone()
        {
            SnakeHumanPlayer ttthp = new SnakeHumanPlayer();
            ttthp.SetPlayerNumber(_PlayerNumber);
            return ttthp;
        }

        public override ISnakeMove GetMove(IMoveSelection selection, ISnakeField field)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (selection.XClickPos > 20 + (j * 100) && selection.XClickPos < 120 + (j * 100) &&
                        selection.YClickPos > 20 + (i * 100) && selection.YClickPos < 120 + (i * 100))
                    {
                        return new SnakeMove(i, j, _PlayerNumber);
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

    public class SnakeComputerPlayer : BaseComputerSnakePlayer
    {
        int _PlayerNumber = 0;

        public override string Name { get { return "ComputerSnakePlayer"; } }

        public override IGamePlayer Clone()
        {
            SnakeComputerPlayer ttthp = new SnakeComputerPlayer();
            ttthp.SetPlayerNumber(_PlayerNumber);
            return ttthp;
        }

        public override ISnakeMove GetMove(ISnakeField field)
        {
            Random rand = new Random();
            int f = rand.Next(0, 99);
            for (int i = 0; i < 99; i++)
            {
                int c = f % 10;
                int r = ((f - c) / 10) % 10;
                if (field[r, c] <= 0)
                {
                    return new SnakeMove(r, c, _PlayerNumber);
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
