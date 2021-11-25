using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Reflection;

namespace OOPGames
{
    public class GA_Timer
    {
        public int _TimeLimit;
        public int _TimeLeft;
        public long _LastChanged;
        public int _Stopped = 0;
        public bool _TimeOut = false;

        public GA_Timer(int limit)
        {
            _TimeLimit = limit;
            _TimeLeft = _TimeLimit;
        }

        public void check()
        {
            //Variable verringern um Zähler anzeigen zu lassen
            DateTime currentTime = DateTime.Now;
            long elapsedTicks = currentTime.Ticks - _LastChanged;
            if (elapsedTicks > 10000000)
            {
                if (_Stopped == 0)
                {
                    _TimeLeft--;
                }
                _LastChanged = currentTime.Ticks;
            }
            if(_TimeLeft <= 0)
            {
                TimerHelp.stop();
                _TimeOut = true;
            }
        }

        public int tLeft()
        {
            return _TimeLeft;
        }

        public void reset()
        {
            _TimeLeft = _TimeLimit;
            _TimeOut = false;
        }

        public void stop()
        {
            _Stopped = 1;
        }

        public void start()
        {
            _Stopped = 0;
        }
        public bool timeout()
        {
            return _TimeOut;
        }

    }

    public static class TimerHelp
    {
        public static GA_Timer _Tim;

        public static void create(int time)
        {
            _Tim = new GA_Timer(time);
        }

        public static GA_Timer state()
        {
            return _Tim;
        }

        public static void check()
        {
            _Tim.check();
        }

        public static int tLeft()
        {
            return _Tim.tLeft();
        }

        public static void reset()
        {
            _Tim.reset();
        }

        public static void start()
        {
            _Tim.start();
        }

        public static void stop()
        {
            _Tim.stop();
        }

        public static bool timeout()
        {
            if (_Tim is GA_Timer)
            {
                return _Tim.timeout();
            }
            return false;
        }

    }

    public class GA_TTTPainter : BaseTicTacToePaint
    {
        
        public override string Name { get { return "GA_TicTacToePainter"; } }

        public override void PaintTicTacToeField(Canvas canvas, ITicTacToeField currentField)
        {
            if(TimerHelp.state() is GA_Timer)
            {
                TimerHelp.check();
            }
            else
            {
                TimerHelp.create(10);
            }
            
            canvas.Children.Clear();
            Color bgColor = Color.FromRgb(255, 255, 255);
            canvas.Background = new SolidColorBrush(bgColor);
            Color lineColor = Color.FromRgb(0, 0, 0);
            Brush lineStroke = new SolidColorBrush(lineColor);
            Color shadowColor = Color.FromRgb(64, 64, 64);
            Brush shadowStroke = new SolidColorBrush(shadowColor);
            Color XColor = Color.FromRgb(0, 0, 255);
            Brush XStroke = new SolidColorBrush(XColor);
            Color OColor = Color.FromRgb(255, 0, 0);
            Brush OStroke = new SolidColorBrush(OColor);

            Line l1 = new Line() { X1 = 20, Y1 = 20, X2 = 20, Y2 = 320, Stroke = lineStroke, StrokeThickness = 3.0 };
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = 120, Y1 = 20, X2 = 120, Y2 = 320, Stroke = lineStroke, StrokeThickness = 3.0 };
            canvas.Children.Add(l2);
            Line l3 = new Line() { X1 = 220, Y1 = 20, X2 = 220, Y2 = 320, Stroke = lineStroke, StrokeThickness = 3.0 };
            canvas.Children.Add(l3);
            Line l4 = new Line() { X1 = 320, Y1 = 20, X2 = 320, Y2 = 320, Stroke = lineStroke, StrokeThickness = 3.0 };
            canvas.Children.Add(l4);
            Line l5 = new Line() { X1 = 20, Y1 = 20, X2 = 320, Y2 = 20, Stroke = lineStroke, StrokeThickness = 3.0 };
            canvas.Children.Add(l5);
            Line l6 = new Line() { X1 = 20, Y1 = 120, X2 = 320, Y2 = 120, Stroke = lineStroke, StrokeThickness = 3.0 };
            canvas.Children.Add(l6);
            Line l7 = new Line() { X1 = 20, Y1 = 220, X2 = 320, Y2 = 220, Stroke = lineStroke, StrokeThickness = 3.0 };
            canvas.Children.Add(l7);
            Line l8 = new Line() { X1 = 20, Y1 = 320, X2 = 320, Y2 = 320, Stroke = lineStroke, StrokeThickness = 3.0 };
            canvas.Children.Add(l8);

            //Anzeige der Verbleibenden Sekunden
            TextBlock textBlock = new TextBlock();
            TextBlock.SetFontSize(textBlock, 20);
            textBlock.Text = "Time Left " + TimerHelp.tLeft();
            Color textColor = Color.FromRgb(0, 0, 0);
            textBlock.Foreground = new SolidColorBrush(textColor);
            Canvas.SetLeft(textBlock, 30);
            Canvas.SetTop(textBlock, 320);
            canvas.Children.Add(textBlock);
            Line l9 = new Line() { X1 = 20, Y1 = 320, X2 = 20, Y2 = 350, Stroke = lineStroke, StrokeThickness = 3.0 };
            canvas.Children.Add(l9);
            Line l10 = new Line() { X1 = 20, Y1 = 350, X2 = 150, Y2 = 350, Stroke = lineStroke, StrokeThickness = 3.0 };
            canvas.Children.Add(l10);
            Line l11 = new Line() { X1 = 150, Y1 = 350, X2 = 150, Y2 = 320, Stroke = lineStroke, StrokeThickness = 3.0 };
            canvas.Children.Add(l11);

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
    
    public class GA_TTTPainterGlow : BaseTicTacToePaint
    {

        public override string Name { get { return "GA_TicTacToePainterGlow"; } }


        public override void PaintTicTacToeField(Canvas canvas, ITicTacToeField currentField)
        {
            if (TimerHelp.state() is GA_Timer)
            {
                TimerHelp.check();
            }
            else
            {
                TimerHelp.create(10);
            }

            canvas.Children.Clear();
            Color bgColor = Color.FromRgb(255, 255, 255);
            canvas.Background = new SolidColorBrush(bgColor);
            Brush bgStroke = new SolidColorBrush(bgColor);
            Color boardColor = Color.FromRgb(0, 0, 0);
            Brush boardStroke = new SolidColorBrush(boardColor);
            Color fieldColor = Color.FromRgb(230, 230, 230);
            Brush fieldStroke = new SolidColorBrush(fieldColor);

            Color xColor = Color.FromRgb(0, 0, 255);
            Brush xStroke = new SolidColorBrush(xColor);
            Color xgColor = Color.FromRgb(100, 100, 255);
            Brush xgStroke = new SolidColorBrush(xgColor);
            Color xggColor = Color.FromRgb(200, 200, 255);
            Brush xggStroke = new SolidColorBrush(xggColor);

            Color oColor = Color.FromRgb(255, 0, 0);
            Brush oStroke = new SolidColorBrush(oColor);
            Color ogColor = Color.FromRgb(255, 100, 100);
            Brush ogStroke = new SolidColorBrush(ogColor);
            Color oggColor = Color.FromRgb(255, 200, 200);
            Brush oggStroke = new SolidColorBrush(oggColor);

            Rectangle Board = new Rectangle() { Margin = new Thickness(20, 20, 0, 0), Width = 300, Height = 300, Stroke = boardStroke, StrokeThickness = 3.0 };
            Board.Fill = boardStroke;
            canvas.Children.Add(Board);

            //Anzeige der Verbleibenden Sekunden
            Rectangle timerField = new Rectangle() { Margin = new Thickness(20, 320, 0, 0), Width = 130, Height = 30, Stroke = boardStroke, StrokeThickness = 3.0 };
            timerField.Fill = boardStroke;
            canvas.Children.Add(timerField);
            TextBlock textBlock = new TextBlock();
            TextBlock.SetFontSize(textBlock, 20);
            textBlock.Text = "Time Left " + TimerHelp.tLeft();
            Color textColor = Color.FromRgb(255, 255, 255);
            textBlock.Foreground = new SolidColorBrush(textColor);
            Canvas.SetLeft(textBlock, 30);
            Canvas.SetTop(textBlock, 320);
            canvas.Children.Add(textBlock);
            

            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    Ellipse El = new Ellipse() { Margin = new Thickness(20 + (j * 100), 20 + (i * 100), 0, 0), Width = 100, Height = 100, Stroke = boardStroke, StrokeThickness = 2.0 };
                    El.Fill = fieldStroke;
                    canvas.Children.Add(El);

                    if(currentField[i,j] == 1)
                    {
                        Line X11 = new Line() { X1 = 40 + (j * 100), Y1 = 40 + (i * 100), X2 = 100 + (j * 100), Y2 = 100 + (i * 100), Stroke = xggStroke, StrokeThickness = 10.0 };
                        canvas.Children.Add(X11);
                        Line X21 = new Line() { X1 = 40 + (j * 100), Y1 = 100 + (i * 100), X2 = 100 + (j * 100), Y2 = 40 + (i * 100), Stroke = xggStroke, StrokeThickness = 10.0 };
                        canvas.Children.Add(X21);
                        Line X12 = new Line() { X1 = 42 + (j * 100), Y1 = 42 + (i * 100), X2 = 98 + (j * 100), Y2 = 98 + (i * 100), Stroke = xgStroke, StrokeThickness = 6.0 };
                        canvas.Children.Add(X12);
                        Line X22 = new Line() { X1 = 42 + (j * 100), Y1 = 98 + (i * 100), X2 = 98 + (j * 100), Y2 = 42 + (i * 100), Stroke = xgStroke, StrokeThickness = 6.0 };
                        canvas.Children.Add(X22);
                        Line X13 = new Line() { X1 = 44 + (j * 100), Y1 = 44 + (i * 100), X2 = 96 + (j * 100), Y2 = 96 + (i * 100), Stroke = xStroke, StrokeThickness = 3.0 };
                        canvas.Children.Add(X13);
                        Line X23 = new Line() { X1 = 44 + (j * 100), Y1 = 96 + (i * 100), X2 = 96 + (j * 100), Y2 = 44 + (i * 100), Stroke = xStroke, StrokeThickness = 3.0 };
                        canvas.Children.Add(X23);
                        Line X14 = new Line() { X1 = 45 + (j * 100), Y1 = 45 + (i * 100), X2 = 95 + (j * 100), Y2 = 95 + (i * 100), Stroke = xggStroke, StrokeThickness = 1.0 };
                        canvas.Children.Add(X14);
                        Line X24 = new Line() { X1 = 45 + (j * 100), Y1 = 95 + (i * 100), X2 = 95 + (j * 100), Y2 = 45 + (i * 100), Stroke = xggStroke, StrokeThickness = 1.0 };
                        canvas.Children.Add(X24);
                    }
                    else if(currentField[i,j] == 2)
                    {
                        Ellipse OE11 = new Ellipse() { Margin = new Thickness(30 + (j * 100), 30 + (i * 100), 0, 0), Width = 80, Height = 80, Stroke = oggStroke, StrokeThickness = 10.0 };
                        canvas.Children.Add(OE11);
                        Ellipse OE12 = new Ellipse() { Margin = new Thickness(31 + (j * 100), 31 + (i * 100), 0, 0), Width = 78, Height = 78, Stroke = ogStroke, StrokeThickness = 6.0 };
                        canvas.Children.Add(OE12);
                        Ellipse OE13 = new Ellipse() { Margin = new Thickness(32 + (j * 100), 32 + (i * 100), 0, 0), Width = 76, Height = 76, Stroke = oStroke, StrokeThickness = 3.0 };
                        canvas.Children.Add(OE13);
                        Ellipse OE14 = new Ellipse() { Margin = new Thickness(33 + (j * 100), 33 + (i * 100), 0, 0), Width = 75, Height = 75, Stroke = oggStroke, StrokeThickness = 1.0 };
                        canvas.Children.Add(OE14);
                    }
                }
            }
        }
    }
    
    public class GA_TTTRules : BaseTicTacToeRules
    {
        public TicTacToeField _Board = new TicTacToeField();

        public override ITicTacToeField TicTacToeField { get { return _Board; } }

        public override bool MovesPossible
        {
            get
            {
                if (TimerHelp.timeout())
                {
                    return false;
                }
                else
                {

                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (_Board[i, j] == 0)
                            {
                                return true;
                            }
                        }
                    }
                    if (TimerHelp.state() is GA_Timer)
                    {
                        TimerHelp.stop();
                    }
                    return false;
                }
            }
        }
        public override string Name { get { return "GA_TicTacToeRules"; } }

        public override int CheckIfPLayerWon()
        {
            for (int p = 1; p < 3; p++)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (_Board[i, 0] == p && _Board[i, 1] == p && _Board[i, 2] == p)
                    {
                        if (TimerHelp.state() is GA_Timer)
                        {
                            TimerHelp.stop();
                        }
                        return p;
                    }
                    else if (_Board[0, i] == p && _Board[1, i] == p && _Board[2, i] == p)
                    {
                        if (TimerHelp.state() is GA_Timer)
                        {
                            TimerHelp.stop();
                        }
                        return p;
                    }
                }

                if (_Board[0, 0] == p && _Board[1, 1] == p && _Board[2, 2] == p ||
                    _Board[0, 2] == p && _Board[1, 1] == p && _Board[2, 0] == p)
                {
                    if (TimerHelp.state() is GA_Timer)
                    {
                        TimerHelp.stop();
                    }
                    return p;
                }
            }
            if (TimerHelp.timeout())
            {
                int n1 = 0;
                int n2 = 0;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (_Board[i, j] == 1)
                        {
                            n1++;
                        }
                        else if (_Board[i, j] == 2)
                        {
                            n2++;
                        }
                    }
                }
                if (n1 > n2)
                {
                    return 1;
                }
                else if (n1 == n2)
                {
                    return 2;
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
                    _Board[i, j] = 0;
                }
            }
            if (TimerHelp.state() is GA_Timer)
            {
                TimerHelp.reset();
                TimerHelp.start();
            }
        }

        public override void DoTicTacToeMove(ITicTacToeMove move)
        {
            if (move.Row >= 0 && move.Row < 3 && move.Column >= 0 && move.Column < 3 &&_Board[move.Row, move.Column] == 0)
            {
                _Board[move.Row, move.Column] = move.PlayerNumber;
            }
            if (TimerHelp.state() is GA_Timer)
            {
                TimerHelp.reset();
            }
        }

    }

    public class GA_TicTacToeComputerPlayer : BaseComputerTicTacToePlayer
    {
        int _PlayerNumber = 0;

        public override string Name { get { return "GA_ComputerTicTacToePlayer"; } }

        public override IGamePlayer Clone()
        {
            GA_TicTacToeComputerPlayer ttthp = new GA_TicTacToeComputerPlayer();
            ttthp.SetPlayerNumber(_PlayerNumber);
            return ttthp;
        }

        public override ITicTacToeMove GetMove(ITicTacToeField field)
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

    public class GA_TicTacToeHumanPlayer : BaseHumanTicTacToePlayer
    {
        int _PlayerNumber = 0;

        public override string Name { get { return "Gruppe A HumanTicTacToePlayer"; } }

        public override IGamePlayer Clone()
        {
            GA_TicTacToeHumanPlayer ttthp = new GA_TicTacToeHumanPlayer();
            ttthp.SetPlayerNumber(_PlayerNumber);
            return ttthp;
        }

        public override ITicTacToeMove GetMove(IMoveSelection selection, ITicTacToeField field) //evtl Eigene funktion (Wie bei Gruppe B)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (selection.XClickPos > 20 + (j * 100) && selection.XClickPos < 120 + (j * 100) &&
                        selection.YClickPos > 20 + (i * 100) && selection.YClickPos < 120 + (i * 100))
                    {
                        return new TicTacToeMove(i, j, _PlayerNumber);
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
}