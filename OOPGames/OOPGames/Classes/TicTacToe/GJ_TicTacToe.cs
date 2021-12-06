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

namespace OOPGames
{
    public class GJ_TicTacToePaint : BaseTicTacToePaint
    {
        public override string Name { get { return "TicTacToePainter GJ"; } }

        public override void PaintTicTacToeField(Canvas canvas, ITicTacToeField currentField)
        {
            byte R = 0;
            byte G = 0;
            byte B = 0;
            byte R2 = 0;
            byte G2 = 0;
            byte B2 = 0;
            int Border = 20;
            int Tilesize = 100;
            int selectedindex = 0;
            int selectedindex2 = 0;
            if (currentField is ITicTacToeField_GJ)
            {
                ITicTacToeField_GJ currentField_GJ = (ITicTacToeField_GJ)currentField;
                currentField_GJ.Set_Tile_and_Border(canvas);
                Border = currentField_GJ.Border;
                Tilesize = currentField_GJ.Tile;
                selectedindex = currentField_GJ.selectedIndex;
                selectedindex2 = currentField_GJ.selectedIndex2;
            }
            if (selectedindex == 0)
            {
                R = 255;
                G = 0;
                B = 0;
            }
            else if (selectedindex == 1)
            {
                R = 0;
                G = 255;
                B = 0;
            }
            else if (selectedindex == 2)
            {
                R = 0;
                G = 0;
                B = 255;
            }
            if (selectedindex2 == 0)
            {
                R2 = 255;
                G2 = 0;
                B2 = 0;
            }
            else if (selectedindex2 == 1)
            {
                R2 = 0;
                G2 = 255;
                B2 = 0;
            }
            else if (selectedindex2 == 2)
            {
                R2 = 0;
                G2 = 0;
                B2 = 255;
            }
            canvas.Children.Clear();
            Color bgColor = Color.FromRgb(64, 224, 208);
            canvas.Background = new SolidColorBrush(bgColor);
            Color lineColor = Color.FromRgb(64, 180, 150);
            Brush lineStroke = new SolidColorBrush(lineColor);
            Color XColor = Color.FromRgb(R, G, B);
            Brush XStroke = new SolidColorBrush(XColor);
            Color OColor = Color.FromRgb(R2, G2, B2);
            Brush OStroke = new SolidColorBrush(OColor);

            // These are the deviding lines which cut the field in its squares
            Line l1 = new Line() { X1 = Tilesize + Border, Y1 = Border, X2 = Tilesize + Border, Y2 = 3 * Tilesize + Border, Stroke = lineStroke, StrokeThickness = 3.0 };
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = (2 * Tilesize) + Border, Y1 = Border, X2 = (2 * Tilesize) + Border, Y2 = (3 * Tilesize) + Border, Stroke = lineStroke, StrokeThickness = 3.0 };
            canvas.Children.Add(l2);
            Line l3 = new Line() { X1 = Border, Y1 = Tilesize + Border, X2 = (3 * Tilesize) + Border, Y2 = Tilesize + Border, Stroke = lineStroke, StrokeThickness = 3.0 };
            canvas.Children.Add(l3);
            Line l4 = new Line() { X1 = Border, Y1 = (2 * Tilesize) + Border, X2 = (3 * Tilesize) + Border, Y2 = (2 * Tilesize) + Border, Stroke = lineStroke, StrokeThickness = 3.0 };
            canvas.Children.Add(l4);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (currentField[i, j] == 1)
                    {
                        int x11 = Border + (j * Tilesize);
                        int y11 = Border + (i * Tilesize);
                        int x21 = Tilesize + Border + (j * Tilesize);
                        int y21 = Tilesize + Border + (i * Tilesize);
                        Line X1 = new Line() { X1 = x11, Y1 = y11, X2 = x21, Y2 = y21, Stroke = XStroke, StrokeThickness = 3.0 };

                        canvas.Children.Add(X1);
                        int x12 = Border + (j * Tilesize);
                        int y12 = Tilesize + Border + (i * Tilesize);
                        int x22 = Tilesize + Border + (j * Tilesize);
                        int y22 = Border + (i * Tilesize);
                        Line X2 = new Line() { X1 = x12, Y1 = y12, X2 = x22, Y2 = y22, Stroke = XStroke, StrokeThickness = 3.0 };
                        canvas.Children.Add(X2);
                    }
                    else if (currentField[i, j] == 2)
                    {
                        int left = Border + (j * Tilesize);
                        int right = Border + (i * Tilesize);
                        Ellipse OE = new Ellipse() { Margin = new Thickness(left, right, 0, 0), Width = Tilesize, Height = Tilesize, Stroke = OStroke, StrokeThickness = 3.0 };
                        canvas.Children.Add(OE);
                    }
                }
            }
        }
    }



    public class GJ_TicTacToeRules : BaseTicTacToeRules_GJ
    {
        GJ_TicTacToeField _Field = new GJ_TicTacToeField();

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

        public override string Name { get { return "TicTacToeRules GJ"; } }

        public override int CheckIfPLayerWon()
        {
            for (int i = 0; i < 3; i++)
            {
                if (_Field[i, 0] > 0 && _Field[i, 0] == _Field[i, 1] && _Field[i, 1] == _Field[i, 2])
                {
                    return _Field[i, 0];
                }
                else if (_Field[0, i] > 0 && _Field[0, i] == _Field[1, i] && _Field[1, i] == _Field[2, i])
                {
                    return _Field[0, i];
                }
            }

            if (_Field[0, 0] > 0 && _Field[0, 0] == _Field[1, 1] && _Field[1, 1] == _Field[2, 2])
            {
                return _Field[0, 0];
            }
            else if (_Field[0, 2] > 0 && _Field[0, 2] == _Field[1, 1] && _Field[1, 1] == _Field[2, 0])
            {
                return _Field[0, 2];
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
        public override void AskForGameColour()
        {
            ShowDialog("Bitte Farbe wählen:", "Farbe Festlegen");
        }
        
        
       
        public void ShowDialog(string text, string caption)
        {

            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            string[] auswahl0 = { "Rot", "Grün", "Blau" };
            System.Windows.Forms.Label textLabel = new System.Windows.Forms.Label() { Left = 10, Top = 10, Text = text };
            System.Windows.Forms.ComboBox textBox = new System.Windows.Forms.ComboBox();
            System.Windows.Forms.ComboBox textBox2 = new System.Windows.Forms.ComboBox();
            textBox.Items.AddRange(auswahl0);
            textBox.Location = new System.Drawing.Point(10, 60);
            textBox.IntegralHeight = false;
            textBox.MaxDropDownItems = 3;
            textBox.DropDownStyle = ComboBoxStyle.DropDownList;
            textBox.Name = "ComboBox1";
            textBox.Size = new System.Drawing.Size(136, 81);
            textBox.TabIndex = 0;
            textBox.SelectedIndexChanged += TextBox_SelectedIndexChanged;

            textBox2.Items.AddRange(auswahl0);
            textBox2.Location = new System.Drawing.Point(170, 60);
            textBox2.IntegralHeight = false;
            textBox2.MaxDropDownItems = 3;
            textBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            textBox2.Name = "ComboBox2";
            textBox2.Size = new System.Drawing.Size(136, 81);
            textBox2.TabIndex = 0;
            textBox2.SelectedIndexChanged += TextBox2_SelectedIndexChanged;

            System.Windows.Forms.Button confirmation = new System.Windows.Forms.Button() { Text = "OK", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(textBox2);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;
            prompt.ShowDialog(); // nicht vergessen am ende zu führen


        }
        public void TextBox_SelectedIndexChanged(object sender, EventArgs e)
        {
                System.Windows.Forms.ComboBox cmb = (System.Windows.Forms.ComboBox)sender;

            _Field.selectedIndex = cmb.SelectedIndex;
                return;
               
        }
        public void TextBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox cmb = (System.Windows.Forms.ComboBox)sender;

            _Field.selectedIndex2 = cmb.SelectedIndex;
            return;

        }


    }
    public abstract class BaseTicTacToeRules_GJ : ITicTacToeRules_GJ
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

        public abstract void AskForGameColour();
        /*
        {
            //open window
        }
        */
    }
    public class GJ_TicTacToeField : BaseTicTacToeField, ITicTacToeField_GJ
    {
        int[,] _Field = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };

        public int _Border = 10;
        public int _Tile = 100;
        public int _selectedIndex = 0;
        public int _selectedIndex2 = 0;
        public override int this[int r, int c]
        {
            get
            {
                if (r >= 0 && r < 3 && c >= 0 && c < 3)
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
                if (r >= 0 && r < 3 && c >= 0 && c < 3)
                {
                    _Field[r, c] = value;
                }

            }
        }
        public int Border
        {
            get
            {
                return _Border;
            }
        }

        public int Tile
        {
            get
            {
                return _Tile;
            }
        }

        public int selectedIndex 
        { 
            get
            {
                return _selectedIndex;
            }
            set
            {
                _selectedIndex = value;
            }
        
        }
        public int selectedIndex2
        {
            get
            {
                return _selectedIndex2;
            }
            set
            {
                _selectedIndex2 = value;
            }

        }
        public void Set_Tile_and_Border(Canvas canvas)
        {
            int width = (int)Math.Round(canvas.ActualWidth);
            int hight = (int)Math.Round(canvas.ActualHeight);
            int Fieldsize = 0;

            if (width < hight)
            {
                Fieldsize = width;
            }
            else
            {
                Fieldsize = hight;
            }

            _Border = (int)Math.Round(Fieldsize * 0.1);
            int Tilespace = Fieldsize - _Border;
            _Border = _Border / 2;
            _Tile = Tilespace / 3;
        }
        public void AskForGameColour()
        {
            throw new NotImplementedException();
        }
    }

    public class GJ_TicTacToeMove : ITicTacToeMove
    {
        int _Row = 0;
        int _Column = 0;
        int _PlayerNumber = 0;

        public void TicTacToeMove (int row, int column, int playerNumber)
        {
            _Row = row;
            _Column = column;
            _PlayerNumber = playerNumber;
        }

        public int Row { get { return _Row; } }

        public int Column { get { return _Column; } }

        public int PlayerNumber { get { return _PlayerNumber; } }
    }

    public class GJ_TicTacToeHumanPlayer : BaseHumanTicTacToePlayer
    {
        int _PlayerNumber = 0;

        public override string Name { get { return "HumanTicTacToePlayer GJ"; } }

        public override IGamePlayer Clone()
        {
            GJ_TicTacToeHumanPlayer ttthp = new GJ_TicTacToeHumanPlayer();
            ttthp.SetPlayerNumber(_PlayerNumber);
            return ttthp;
        }

        public override ITicTacToeMove GetMove(IMoveSelection selection, ITicTacToeField field)
        {
            int Border = 20;
            int Tilesize = 100;

            if (field is ITicTacToeField_GJ)
            {
                ITicTacToeField_GJ field_GJ = (ITicTacToeField_GJ)field;
                Border = field_GJ.Border;
                Tilesize = field_GJ.Tile;
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (selection.XClickPos > Border + (j*Tilesize) && selection.XClickPos < Tilesize+Border + (j*Tilesize) &&
                        selection.YClickPos > Border + (i*Tilesize) && selection.YClickPos < Tilesize+Border + (i*Tilesize) &&
                        field[i, j] <= 0)
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

    public class GJ_TicTacToeComputerPlayer : BaseComputerTicTacToePlayer
    {
        int _PlayerNumber = 0;

        public override string Name { get { return "ComputerTicTacToePlayer GJ"; } }

        public override IGamePlayer Clone()
        {
            TicTacToeComputerPlayer ttthp = new TicTacToeComputerPlayer();
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
}
