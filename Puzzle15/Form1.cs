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
        List<Button> tiles = new List<Button>();
        List<Point> initialLocations = new List<Point>();
        Random rand = new Random();

        public Puzzle()
        {
            StartText();
            InitializeComponent();
            InitializPuzzle();
            ShuffleTiles();
        }

        private void InitializPuzzle()
        {            
            int tileCounter = 1;
            Button tile = null;

            this.Width = 420;
            this.Height = 440;

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
                    tile.Top = 20 + j * 90;
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

        private void Tile_Click(object sender, EventArgs e)
        {
            Button tile = (Button)sender;

            if (CanSwap(tile))
            {
                SwapTiles(tile);
                CheckForWin();
                PlaySimpleSound();
                ChangeBackColorForForm1();
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
            R = rand.Next(0, 200);
            G = rand.Next(0, 200);
            B = rand.Next(0, 200);
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
    }
}
