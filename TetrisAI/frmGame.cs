using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TetrisAI
{
    public partial class frmGame : Form
    {
        Engine.Game TetrisGame;

        public frmGame()
        {
            InitializeComponent();

            // initialize the Game Class
            this.TetrisGame = new Engine.Game(10, 20, this.imgTetris);
        }

        private void frmGame_Load(object sender, EventArgs e)
        {
            // attach the control to the form
            this.Controls.Add(this.TetrisGame.GameRender.BoardPanel);
        }

        private void frmGame_KeyDown(object sender, KeyEventArgs e)
        {
            // get the key code from the key event, and passed it to the Game Class
            this.TetrisGame.KeyPress(e.KeyCode);
#if DEBUG
            Console.WriteLine("Key Press!" + e.KeyCode.ToString());
#endif
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.TetrisGame.Start();
            this.Focus();
        }
    }
}
