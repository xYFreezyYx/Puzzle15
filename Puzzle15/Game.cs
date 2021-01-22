﻿// Man rekords Puzzle15 salikšanā: 1 Min 9 Sec jeb 00:01:09 H  H:Min:Sec 
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
        PictureBox colorShuffleOnOff = new PictureBox();
        PictureBox resetButton = new PictureBox();
        PictureBox startpauseButton = new PictureBox();
        Random rand = new Random();
        Label lbl1 = new Label();
        Label lbl2 = new Label();
        Label lbl3 = new Label();
        bool IsActive = true;
        bool IsActive1 = true;
        int clickCounter = 0;
        int winCounter = 0;
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
            StartPauseButtonAdder();
            ColorSwitchOnOff();
            ClockTimer.Start();
        }

        //Organizatione Boxes

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
            this.Height = 540;

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
                    tile.Top = 130 + j * 90;
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
            if (IsActive1 == true)
            {
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
                this.Height = 630;
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
            lblC.Text = "Click";
            lblC.BackColor = Color.White;
            lblC.ForeColor = Color.Black;
            lblC.BorderStyle = BorderStyle.FixedSingle;
            lblC.TextAlign = ContentAlignment.TopCenter;
            lblC.FlatStyle = FlatStyle.System;
            lblC.Font = new Font("Consolas", 20);
            lblC.Width = 80;
            lblC.Height = 40;
            lblC.Location = new Point(20, 20);
            this.Controls.Add(lblC);
        }

        private void TimerLable()
        {
            PictureBox lblT = null;

            lblT = new PictureBox();
            lblT.Width = 260;
            lblT.Height = 30;
            lblT.Location = new Point(110, 25);
            this.Controls.Add(lblT);

            lblT.BackColor = Color.Transparent;
            lblT.SizeMode = PictureBoxSizeMode.StretchImage;

            string pictureName = "Time";
            lblT.Image = (Image)Resources.ResourceManager.GetObject(pictureName);
        }

        private void WinCounterLable()
        {
            Label lblW = null;

            lblW = new Label();
            lblW.Text = "Wins";
            lblW.BackColor = Color.White;
            lblW.ForeColor = Color.Black;
            lblW.BorderStyle = BorderStyle.FixedSingle;
            lblW.TextAlign = ContentAlignment.TopCenter;
            lblW.FlatStyle = FlatStyle.System;
            lblW.Font = new Font("Consolas", 25);
            lblW.Width = 80;
            lblW.Height = 40;
            lblW.Location = new Point(380, 20);
            this.Controls.Add(lblW);
        }

        //Click counter

        private void ClickDisplayAdder()
        {
            lbl1.Text = "0";
            lbl1.BackColor = Color.White;
            lbl1.ForeColor = Color.Black;
            lbl1.BorderStyle = BorderStyle.FixedSingle;
            lbl1.TextAlign = ContentAlignment.TopCenter;
            lbl1.FlatStyle = FlatStyle.System;
            lbl1.Font = new Font("Consolas", 30);
            lbl1.Width = 80;
            lbl1.Height = 50;
            lbl1.Location = new Point(20, 70);
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
            lbl2.Location = new Point(380, 70);
            this.Controls.Add(lbl2);
        }

        private void WinCounter()
        {            
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
            lbl3.Location = new Point(110, 70);
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
            resetButton.Width = 377;
            resetButton.Height = 80;
            resetButton.Location = new Point(50, 495);
            this.Controls.Add(resetButton);
            resetButton.Visible = false;

            resetButton.BackColor = Color.Transparent;
            resetButton.SizeMode = PictureBoxSizeMode.StretchImage;

            string pictureName = "Replay";
            resetButton.Image = (Image)Resources.ResourceManager.GetObject(pictureName);

            resetButton.Click += ResetButton_Click;
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.GhostWhite;
            this.Height = 540;
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

            colorShuffleOnOff.Tag = "on";
            colorShuffleOnOff.BackColor = Color.Transparent;
            colorShuffleOnOff.SizeMode = PictureBoxSizeMode.StretchImage;

            string pictureNameOO = "On";
            colorShuffleOnOff.Image = (Image)Resources.ResourceManager.GetObject(pictureNameOO);
            IsActive = true;

            startpauseButton.Tag = "pause";
            startpauseButton.BackColor = Color.Transparent;
            startpauseButton.SizeMode = PictureBoxSizeMode.StretchImage;

            string pictureNamePS = "Pause";
            startpauseButton.Image = (Image)Resources.ResourceManager.GetObject(pictureNamePS);
            IsActive1 = true;

            StartText();
            ShuffleTiles();
            ClockTimer.Start();
        }

        //Start/Pause Button

        private void StartPauseButtonAdder()
        {
            startpauseButton.Tag = "pause";
            startpauseButton.Width = 40;
            startpauseButton.Height = 30;
            startpauseButton.Location = new Point(20, 130);
            this.Controls.Add(startpauseButton);
            startpauseButton.BackColor = Color.Transparent;
            startpauseButton.SizeMode = PictureBoxSizeMode.StretchImage;

            string pictureName = "Pause";
            startpauseButton.Image = (Image)Resources.ResourceManager.GetObject(pictureName);

            startpauseButton.Click += StartPauseButton_Click;
        }

        private void StartPauseButton_Click(object sender, EventArgs e)
        {
            if (startpauseButton.Tag == "pause")
            {
                this.Height = 630;
                resetButton.Visible = true;
                startpauseButton.Tag = "start";
                IsActive1 = false;
                ClockTimer.Stop();

                startpauseButton.BackColor = Color.Transparent;
                startpauseButton.SizeMode = PictureBoxSizeMode.StretchImage;

                string pictureName = "Start";
                startpauseButton.Image = (Image)Resources.ResourceManager.GetObject(pictureName);
            }
            else if (startpauseButton.Tag == "start")
            {
                this.Height = 540;
                resetButton.Visible = false;
                startpauseButton.Tag = "pause";
                IsActive1 = true;
                ClockTimer.Start();

                startpauseButton.BackColor = Color.Transparent;
                startpauseButton.SizeMode = PictureBoxSizeMode.StretchImage;

                string pictureName = "Pause";
                startpauseButton.Image = (Image)Resources.ResourceManager.GetObject(pictureName);
            }
        }

        //Color switch on/off

        private void ColorSwitchOnOff()
        {
            colorShuffleOnOff.Tag = "on";
            colorShuffleOnOff.Width = 60;
            colorShuffleOnOff.Height = 30;
            colorShuffleOnOff.Location = new Point(420, 130);
            this.Controls.Add(colorShuffleOnOff);

            colorShuffleOnOff.BackColor = Color.Transparent;
            colorShuffleOnOff.SizeMode = PictureBoxSizeMode.StretchImage;

            string pictureName = "On";
            colorShuffleOnOff.Image = (Image)Resources.ResourceManager.GetObject(pictureName);

            colorShuffleOnOff.Click += ColorShuffleOff_Click;
        }

        private void ColorShuffleOff_Click(object sender, EventArgs e)
        {
            if (colorShuffleOnOff.Tag == "on")
            {
                PlayHand();
                this.BackColor = Color.GhostWhite;
                IsActive = false;
                colorShuffleOnOff.Tag = "off";

                colorShuffleOnOff.BackColor = Color.Transparent;
                colorShuffleOnOff.SizeMode = PictureBoxSizeMode.StretchImage;

                string pictureName = "Off";
                colorShuffleOnOff.Image = (Image)Resources.ResourceManager.GetObject(pictureName);
            }
            else if (colorShuffleOnOff.Tag == "off")
            {
                PlayHand();
                IsActive = true;
                colorShuffleOnOff.Tag = "on";

                colorShuffleOnOff.BackColor = Color.Transparent;
                colorShuffleOnOff.SizeMode = PictureBoxSizeMode.StretchImage;

                string pictureName = "On";
                colorShuffleOnOff.Image = (Image)Resources.ResourceManager.GetObject(pictureName);
            }           
        }

        private void ChangeBackColorForGame()
        {
            int R, G, B;
            R = rand.Next(0, 180);
            G = rand.Next(0, 180);
            B = rand.Next(0, 180);
            BackColor = Color.FromArgb(R, G, B);
        }
    }
}

