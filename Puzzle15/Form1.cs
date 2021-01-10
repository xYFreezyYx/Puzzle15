﻿using System;
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
        List<Button> tiles = new List<Button>();
        List<Point> initialLocations = new List<Point>();
        Random rand = new Random();
        Label lbl1 = new Label();
        Label lbl2 = new Label();
        Label lbl3 = new Label();
        Label lbl4 = new Label();
        private int clickCounter = 0;
        int dsec = 0;
        int sec = 0;
        int min = 0;
        int hou = 0;

        public Puzzle()
        {
            StartText();            
            InitializeComponent();
            InitializPuzzle();
            ShuffleTiles();
            ClickCounterDisplayAdder();
            ClockDisplayAdder();
            ClickCounterLable();
            TimerLable();
            ClockTimer.Start();
        }

        private void InitializPuzzle()
        {            
            int tileCounter = 1;
            Button tile = null;

            this.Width = 410;
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
                    tile.Left = 20 + i * 90;
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

        private void ClickCounterDisplayAdder()
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
            lbl1.Location = new Point(20, 55);
            this.Controls.Add(lbl1);
        }

        private void ClockDisplayAdder()
        {
            lbl2.Text = "00:00:00:0";
            lbl2.BackColor = Color.White;
            lbl2.ForeColor = Color.Black;
            lbl2.BorderStyle = BorderStyle.FixedSingle;
            lbl2.TextAlign = ContentAlignment.TopCenter;
            lbl2.FlatStyle = FlatStyle.System;
            lbl2.Font = new Font("Consolas", 30);
            lbl2.Width = 260;
            lbl2.Height = 50;
            lbl2.Location = new Point(110, 55);
            this.Controls.Add(lbl2);
        }

        private void ClickCounter()
        {            
            clickCounter++;
            lbl1.Text = clickCounter.ToString();      
        }

        private void Tile_Click(object sender, EventArgs e)
        {
            Button tile = (Button)sender;

            if (CanSwap(tile))
            {
                SwapTiles(tile);
                CheckForWin();
                PlaySimpleSound();
                ChangeBackColorForForm1();
                ClickCounter();                
            }
        }

        private void SwapTiles(Button tile)
        {
            Button tileEmpty = (Button)this.Controls["TileEmpty"];

            Point tileOldLocatione = tile.Location;
            tile.Location = tileEmpty.Location;
            tileEmpty.Location = tileOldLocatione;
        }

        private void ShuffleTiles()
        {
            for (int i = 0; i < 100; i++)
            {
                SwapTiles(tiles[rand.Next(0, 15)]);
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

        private void CheckForWin()
        {
            bool win = true;
            for(int i = 0; i < 16; i++)
            {
                win = win & tiles[i].Location == initialLocations[i];
            }

            if (win)
            {
                GameOver();
                PlaySimpleSound();
            }
        }

        private void GameOver()
        {
            ClockTimer.Stop();
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

        private void ChangeBackColorForForm1()
        {
            int R, G, B;
            R = rand.Next(0, 190);
            G = rand.Next(0, 190);
            B = rand.Next(0, 190);
            BackColor = Color.FromArgb(R, G, B);
        }

        private void PlaySimpleSound()
        {
            SoundPlayer simpleSound = new SoundPlayer(@"c:\Windows\Media\chimes.wav");
            simpleSound.Play();
        }

        private void PlayHand()
        {
            SystemSounds.Hand.Play();
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
            lbl2.Text = timeText;
        }

        private void ClickCounterLable()
        {
            lbl3.Text = "Clicks";
            lbl3.BackColor = Color.White;
            lbl3.ForeColor = Color.Black;
            lbl3.BorderStyle = BorderStyle.FixedSingle;
            lbl3.TextAlign = ContentAlignment.TopCenter;
            lbl3.FlatStyle = FlatStyle.System;
            lbl3.Font = new Font("Consolas", 15);
            lbl3.Width = 80;
            lbl3.Height = 25;
            lbl3.Location = new Point(20, 20);
            this.Controls.Add(lbl3);
        }

        private void TimerLable()
        {
            lbl4.Text = "H:Min:Sec:MiliSec";
            lbl4.BackColor = Color.White;
            lbl4.ForeColor = Color.Black;
            lbl4.BorderStyle = BorderStyle.FixedSingle;
            lbl4.TextAlign = ContentAlignment.TopCenter;
            lbl4.FlatStyle = FlatStyle.System;
            lbl4.Font = new Font("Consolas", 15);
            lbl4.Width = 260;
            lbl4.Height = 25;
            lbl4.Location = new Point(110, 20);
            this.Controls.Add(lbl4);
        }
    }
}
