using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using Puzzle15.Properties;

namespace Puzzle15
{
    public partial class Puzzle : Form
    {
        List<Point> initialLocations = new List<Point>();
        List<Button> tiles = new List<Button>();                       
        Button colorShuffleOff = new Button();
        Button colorShuffleOn = new Button();
        Button resetButton = new Button();        
        Random rand = new Random();
        Label lbl1 = new Label();
        Label lbl2 = new Label();
        Label lbl3 = new Label();
        bool IsActive = true;
        int clickCounter = 0;
        int dsec = 0;
        int sec = 0;
        int min = 0;
        int hou = 0;

        public Puzzle()
        {
            StartText();
            InitializeComponent();
            InitializPuzzle();
            MisaleniusStuff();
            ClockStuff();
            ClickStuff();
            WinStuff();
            Colorstuff();
            ClockTimer.Start();
        }

        //Organizatione Boxes

        private void Colorstuff()
        {
            ColorSwitchOff();
            ColorSwitchOn();
        }

        private void WinStuff()
        {
            WinCounterDisply();
            WinCounterLable();            
        }

        private void ClickStuff()
        {
            ClickCounterLable();
            ClickDisplayAdder();
        }

        private void ClockStuff()
        {
            ClockDisplayAdder();
            TimerLable();
        }

        private void MisaleniusStuff()
        {
            ResetButtonAdder();
            ShuffleTiles();            
        }

        //Tile adder and Game[Design] chnager

        private void InitializPuzzle()
        {
            int tileCounter = 1;
            Button tile = null;

            this.BackColor = Color.GhostWhite;
            this.Width = 500;
            this.Height = 525;

            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    tile = new Button();
                    tile.BackColor = Color.SteelBlue;
                    tile.ForeColor = Color.White;
                    tile.FlatStyle = FlatStyle.System;
                    tile.Font = new Font("Consolas", 20);
                    tile.Width = 80;
                    tile.Height = 80;
                    tile.Top = 115 + j * 90;
                    tile.Left = 65 + i * 90;
                    tile.Text = tileCounter.ToString();

                    if (tileCounter == 16)
                    {
                        tile.Text = string.Empty;
                        tile.Name = "TileEmpty";
                    }
                    else
                    {
                        tile.Text = tileCounter.ToString();
                    }

                    this.Controls.Add(tile);
                    tiles.Add(tile);
                    initialLocations.Add(tile.Location);

                    tile.Click += Tile_Click;

                    tileCounter++;
                }
            }            
        }

        //Ability to swap tiles

        private void SwapTiles(Button tile)
        {
            Button tileEmpty = (Button)this.Controls["TileEmpty"];

            Point tileOldLocatione = tile.Location;
            tile.Location = tileEmpty.Location;
            tileEmpty.Location = tileOldLocatione;
        }

        private void Tile_Click(object sender, EventArgs e)
        {
            Button tile = (Button)sender;

            if (CanSwap(tile))
            {
                SwapTiles(tile);
                CheckForWin();
                PlaySimpleSound();
                ClickCounter();

                if (IsActive == true)
                {
                    ChangeBackColorForGame();
                }
            }            
        }

        private bool CanSwap(Button tile)
        {
            Button tileEmpty = (Button)this.Controls["TileEmpty"];

            double a = 0, b = 0, c = 0;

            a = tileEmpty.Left - tile.Left;
            b = tileEmpty.Top - tile.Top;
            c = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));

            if (c <= 90)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Tile mixer

        private void ShuffleTiles()
        {
            for (int i = 0; i < 100; i++)
            {
                SwapTiles(tiles[rand.Next(0, 15)]);
            }
        }

        //Win checker

        private void CheckForWin()
        {
            bool win = true;
            for (int i = 0; i < 16; i++)
            {
                win = win & tiles[i].Location == initialLocations[i];
            }

            if (win)
            {
                ClockTimer.Stop();
                GameOver();
                PlayExclamation();
                resetButton.Visible = true;
                this.Height = 620;
                WinCounter();
            }
        }

        //Texts for Star and End

        private void GameOver()
        {
            MessageBox.Show("Congrats! You Solved Puzzle 15! Your a genius!");            
        }

        private void StartText()
        {
            MessageBox.Show(GlueText("To Start Press", "'OK'", " "));           
            PlayHand();
        }

        private string GlueText(string firstText, string secondText, string delimiter)
        {
            return firstText + delimiter + secondText;
        }      

        //Sound players

        private void PlaySimpleSound() //Click sound
        {
            SoundPlayer simpleSound = new SoundPlayer(@"c:\Windows\Media\chimes.wav");
            simpleSound.Play();
        }

        private void PlayHand() //Start sound
        {
            SystemSounds.Hand.Play();
        }

        private void PlayExclamation() //Win sound
        {
            SystemSounds.Exclamation.Play();
        }

        //Labes

        private void ClickCounterLable()
        {
            Label lblC = null;

            lblC = new Label();
            lblC.Text = "Clicks";
            lblC.BackColor = Color.GhostWhite;
            lblC.ForeColor = Color.Black;
            lblC.BorderStyle = BorderStyle.FixedSingle;
            lblC.TextAlign = ContentAlignment.TopCenter;
            lblC.FlatStyle = FlatStyle.System;
            lblC.Font = new Font("Consolas", 15);
            lblC.Width = 80;
            lblC.Height = 25;
            lblC.Location = new Point(20, 20);
            this.Controls.Add(lblC);
        }

        private void TimerLable()
        {
            Label lblT = null;

            lblT = new Label();
            lblT.Text = "H:Min:Sec:MiliSec";
            lblT.BackColor = Color.GhostWhite;
            lblT.ForeColor = Color.Black;
            lblT.BorderStyle = BorderStyle.FixedSingle;
            lblT.TextAlign = ContentAlignment.TopCenter;
            lblT.FlatStyle = FlatStyle.System;
            lblT.Font = new Font("Consolas", 15);
            lblT.Width = 260;
            lblT.Height = 25;
            lblT.Location = new Point(110, 20);
            this.Controls.Add(lblT);
        }

        private void WinCounterLable()
        {
            Label lblW = null;

            lblW = new Label();
            lblW.Text = "Wins";
            lblW.BackColor = Color.GhostWhite;
            lblW.ForeColor = Color.Black;
            lblW.BorderStyle = BorderStyle.FixedSingle;
            lblW.TextAlign = ContentAlignment.TopCenter;
            lblW.FlatStyle = FlatStyle.System;
            lblW.Font = new Font("Consolas", 15);
            lblW.Width = 80;
            lblW.Height = 25;
            lblW.Location = new Point(380, 20);
            this.Controls.Add(lblW);
        }

        //Click counter

        private void ClickDisplayAdder()
        {
            lbl1.Text = "0";
            lbl1.BackColor = Color.GhostWhite;
            lbl1.ForeColor = Color.Black;
            lbl1.BorderStyle = BorderStyle.FixedSingle;
            lbl1.TextAlign = ContentAlignment.TopCenter;
            lbl1.FlatStyle = FlatStyle.System;
            lbl1.Font = new Font("Consolas", 30);
            lbl1.Width = 80;
            lbl1.Height = 50;
            lbl1.Location = new Point(20, 55);
            this.Controls.Add(lbl1);            
        }

        private void ClickCounter()
        {
            clickCounter++;
            lbl1.Text = clickCounter.ToString();
        }

        //Win counter

        private void WinCounterDisply()
        {
            lbl2.Text = "0";
            lbl2.BackColor = Color.GhostWhite;
            lbl2.ForeColor = Color.Black;
            lbl2.BorderStyle = BorderStyle.FixedSingle;
            lbl2.TextAlign = ContentAlignment.TopCenter;
            lbl2.FlatStyle = FlatStyle.System;
            lbl2.Font = new Font("Consolas", 30);
            lbl2.Width = 80;
            lbl2.Height = 50;
            lbl2.Location = new Point(380, 55);
            this.Controls.Add(lbl2);
        }

        private void WinCounter()
        {
            int winCounter = 0;
            winCounter++;
            lbl2.Text = winCounter.ToString();
        }

        //Clock

        private void ClockDisplayAdder()
        {            
            lbl3.Text = "00:00:00:0";
            lbl3.BackColor = Color.GhostWhite;
            lbl3.ForeColor = Color.Black;
            lbl3.BorderStyle = BorderStyle.FixedSingle;
            lbl3.TextAlign = ContentAlignment.TopCenter;
            lbl3.FlatStyle = FlatStyle.System;
            lbl3.Font = new Font("Consolas", 30);
            lbl3.Width = 260;
            lbl3.Height = 50;
            lbl3.Location = new Point(110, 55);
            this.Controls.Add(lbl3);
        }

        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            dsec += 1;
            if (dsec == 10)
            {
                dsec = 0;
                sec += 1;
            }
            if (sec == 60)
            {
                sec = 0;
                min += 1;
            }
            if (min == 60)
            {
                min = 0;
                hou += 1;
            }
            if (hou == 60)
            {
                hou = 0;
            }
            UpdateClockDisplay();
        }

        private void UpdateClockDisplay()
        {
            string timeText;
            timeText = hou.ToString("00") + ":";
            timeText += min.ToString("00") + ":";
            timeText += sec.ToString("00") + ":";
            timeText += dsec.ToString();
            lbl3.Text = timeText;
        }

        //Full reset button

        private void ResetButtonAdder()
        {
            resetButton.Text = "Reset";
            resetButton.BackColor = Color.White;
            resetButton.ForeColor = Color.Black;
            resetButton.FlatStyle = FlatStyle.System;
            resetButton.Font = new Font("Consolas", 30);
            resetButton.Width = 440;
            resetButton.Height = 80;
            resetButton.Location = new Point(20, 475);
            this.Controls.Add(resetButton);
            resetButton.Visible = false;

            resetButton.Click += ResetButton_Click;
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.GhostWhite;
            this.Width = 500;
            resetButton.Visible = false;
            ClockTimer.Stop();
            dsec = 0;
            sec = 0;
            min = 0;
            hou = 0;
            UpdateClockDisplay();
            lbl3.Text = "00:00:00:0";
            lbl1.Text = "0";
            clickCounter = 0;
            StartText();
            ShuffleTiles();
            ClockTimer.Start();
        }

        //Color switch on/off

        private void ColorSwitchOff()
        {
            colorShuffleOff.Text = "Off";
            colorShuffleOff.BackColor = Color.White;
            colorShuffleOff.ForeColor = Color.Black;
            colorShuffleOff.FlatStyle = FlatStyle.System;
            colorShuffleOff.Font = new Font("Consolas", 15);
            colorShuffleOff.Width = 40;
            colorShuffleOff.Height = 30;
            colorShuffleOff.Location = new Point(420, 115);
            this.Controls.Add(colorShuffleOff);

            colorShuffleOff.Click += ColorShuffleOff_Click;
        }

        private void ColorSwitchOn()
        {
            colorShuffleOn.Text = "On";
            colorShuffleOn.BackColor = Color.White;
            colorShuffleOn.ForeColor = Color.Black;
            colorShuffleOn.FlatStyle = FlatStyle.System;
            colorShuffleOn.Font = new Font("Consolas", 15);
            colorShuffleOn.Width = 40;
            colorShuffleOn.Height = 30;
            colorShuffleOn.Location = new Point(20, 115);
            this.Controls.Add(colorShuffleOn);

            colorShuffleOn.Click += ColorShuffleOn_Click;
        }

        private void ColorShuffleOff_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.GhostWhite;
            IsActive = false;
        }

        private void ColorShuffleOn_Click(object sender, EventArgs e)
        {
            IsActive = true;
        }

        private void ChangeBackColorForGame()
        {
            int R, G, B;
            R = rand.Next(0, 190);
            G = rand.Next(0, 190);
            B = rand.Next(0, 190);
            BackColor = Color.FromArgb(R, G, B);
        }
    }
}

