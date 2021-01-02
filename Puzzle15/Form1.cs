using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puzzle15
{
    public partial class Puzzle : Form
    {
        List<Button> tiles = new List<Button>();
        Random rand = new Random();

        public Puzzle()
        {
            InitializeComponent();
            InitializPuzzle();
            ShuffleTiles();
        }

        private void InitializPuzzle()
        {
            int tileCounter = 1;
            Button tile = null;
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    tile = new Button();                   
                    tile.BackColor = Color.SteelBlue;
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

                    tile.Click += Tile_Click;

                    tileCounter++;
                }
            }
        }

        private void Tile_Click(object sender, EventArgs e)
        {
            Button tile = (Button)sender;
            SwapTiles(tile);
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
    }
}
