using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TetrisAI.Engine
{
    class Render
    {
        public Panel BoardPanel { get; private set; }
        private Label[,] Blocks;
        private int BoardX;
        private int BoardY;

        delegate void dSetBlockImage(int x, int y, int ImageIndex);

        public Render(int BoardX, int BoardY, ImageList ImageData)
        {
            // set the size of the board
            this.BoardX = BoardX;
            this.BoardY = BoardY;

            // generate the block array that will be put on the BoardPanel
            this.BoardPanel = new Panel
            {
                Size = new Size(191, 305),
                Location = new Point(12, 12),
                BackColor = Color.Transparent
            };

            // generate all the panel
            this.Blocks = new Label[BoardX, BoardY];

            // initialize the Blocks and put it on the panel
            for (int y = 0; y < BoardY; y++)
            {
                for (int x = 0; x < BoardX; x++)
                {
                    this.Blocks[x, y] = new Label
                    {
                        Text = "",
                        Visible = true,
                        ImageList = ImageData,
                        ImageIndex = 0,
                        Size = new Size(20, 20),
                        Location = new Point((x * 19), (y * 19)),
                        BackColor = Color.Transparent,
                        TabIndex = x + (y * BoardY)
                    };
                    this.BoardPanel.Controls.Add(this.Blocks[x, y]);
                }
            }
        }

        public void DrawBoard(int[,] Board)
        {
            for (int y = 0; y < this.BoardY; y++) 
            {
                for (int x = 0; x < this.BoardX; x++)
                {
                    // only draw the board if the image index is more or equal to 0
                    if (Board[x, y] >= 0)
                    {
                        // set the image index of the blocks based on the value set on the board
                        this.SetBlockImage(x, y, Board[x, y]);
                    }
                }
            }
        }

        public void SetBlockImage(int x, int y, int ImageIndex)
        {
            if (this.Blocks[x, y].InvokeRequired)
            {
                dSetBlockImage d = new dSetBlockImage(SetBlockImage);
                this.Blocks[x, y].Invoke(d, new object[] { x, y, ImageIndex });
            }
            else
            {
                this.Blocks[x, y].ImageIndex = ImageIndex;
            }
        }
    }
}
