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
        Engine.Render clsRender;
        // create a board
        Engine.Board clsBoard = new Engine.Board(10, 16);

        // create a shape
        Engine.Shape.J clsI = new Engine.Shape.J();

        public frmGame()
        {
            InitializeComponent();
            clsRender = new Engine.Render(10, 16, imgTetris);
        }

        private void frmGame_Load(object sender, EventArgs e)
        {
#if DEBUG
            // try to check if the shape for I is already correct or not?
            //for (int i = 0; i < clsI.MaxRotation; i++)
            //{
            //    Console.WriteLine((clsI.CurrentRotation + 1).ToString() + " Rotation");
            //    Console.WriteLine("--------------");
            //    for (int j = 0; j < clsI.MaxY; j++)
            //    {
            //        for (int k = 0; k < clsI.MaxX; k++)
            //        {
            //            Console.Write(clsI.ShapeArray[clsI.CurrentRotation, k, j].ToString());
            //        }
            //        Console.WriteLine("");
            //    }
            //    clsI.Rotate(Globals.StaticData.RotationType.RotateRight);
            //    Console.WriteLine("");
            //}

            // move the I to the down of the board until it cannot move
            int i = 0;
            int j = 0;
            Random rnd = new Random();
            while (!clsBoard.IsGameOver())
            {
                clsI.Move(Globals.StaticData.MoveType.MoveDown, clsBoard.x, clsBoard.y, clsBoard.CurrentBoard, clsBoard.MergeBoard);
                i = rnd.Next(0, 5);
                if (i % 5 < 3)
                {
                    clsI.Move(Globals.StaticData.MoveType.MoveLeft, clsBoard.x, clsBoard.y, clsBoard.CurrentBoard, clsBoard.MergeBoard);
                    clsI.Rotate(Globals.StaticData.RotationType.RotateRight, clsBoard.x, clsBoard.y, clsBoard.CurrentBoard, clsBoard.MergeBoard);
                }
                else
                {
                    clsI.Move(Globals.StaticData.MoveType.MoveRight, clsBoard.x, clsBoard.y, clsBoard.CurrentBoard, clsBoard.MergeBoard);
                    clsI.Rotate(Globals.StaticData.RotationType.RotateLeft, clsBoard.x, clsBoard.y, clsBoard.CurrentBoard, clsBoard.MergeBoard);
                }
                // print the board
                Console.WriteLine("Move - " + j.ToString());
                this.PrintMergeBoard(clsBoard.MergeBoard);

                // check if this is planted already?
                if (clsI.IsPlanted(clsBoard.x, clsBoard.y, clsBoard.CurrentBoard))
                {
                    // put the MergeBoard into CurrentBoard
                    clsI.JoinShapeToBoard(clsBoard.CurrentBoard);
                    // once finished joined, reset the tetromino
                    clsI.Reset();
                    // add J for the second piece
                    j += 1;
                }
            }

            //// move down
            //clsI.Move(Globals.StaticData.MoveType.MoveDown, clsBoard.x, clsBoard.y, clsBoard.CurrentBoard, clsBoard.MergeBoard);
            //PrintMergeBoard(clsBoard.MergeBoard);
            //// rotate right
            //clsI.Rotate(Globals.StaticData.RotationType.RotateRight, clsBoard.x, clsBoard.y, clsBoard.CurrentBoard, clsBoard.MergeBoard);
            //PrintMergeBoard(clsBoard.MergeBoard);
            //// move down
            //clsI.Move(Globals.StaticData.MoveType.MoveDown, clsBoard.x, clsBoard.y, clsBoard.CurrentBoard, clsBoard.MergeBoard);
            //PrintMergeBoard(clsBoard.MergeBoard);
            //// move right
            //clsI.Move(Globals.StaticData.MoveType.MoveRight, clsBoard.x, clsBoard.y, clsBoard.CurrentBoard, clsBoard.MergeBoard);
            //PrintMergeBoard(clsBoard.MergeBoard);
            //// move down
            //clsI.Move(Globals.StaticData.MoveType.MoveDown, clsBoard.x, clsBoard.y, clsBoard.CurrentBoard, clsBoard.MergeBoard);
            //PrintMergeBoard(clsBoard.MergeBoard);
            //// move right
            //clsI.Move(Globals.StaticData.MoveType.MoveRight, clsBoard.x, clsBoard.y, clsBoard.CurrentBoard, clsBoard.MergeBoard);
            //PrintMergeBoard(clsBoard.MergeBoard);
            //// move down
            //clsI.Move(Globals.StaticData.MoveType.MoveDown, clsBoard.x, clsBoard.y, clsBoard.CurrentBoard, clsBoard.MergeBoard);
            //PrintMergeBoard(clsBoard.MergeBoard);
            //// move right
            //clsI.Move(Globals.StaticData.MoveType.MoveRight, clsBoard.x, clsBoard.y, clsBoard.CurrentBoard, clsBoard.MergeBoard);
            //PrintMergeBoard(clsBoard.MergeBoard);
            //// move down
            //clsI.Move(Globals.StaticData.MoveType.MoveDown, clsBoard.x, clsBoard.y, clsBoard.CurrentBoard, clsBoard.MergeBoard);
            //PrintMergeBoard(clsBoard.MergeBoard);
            //// rotate right
            //clsI.Rotate(Globals.StaticData.RotationType.RotateLeft, clsBoard.x, clsBoard.y, clsBoard.CurrentBoard, clsBoard.MergeBoard);
            //PrintMergeBoard(clsBoard.MergeBoard);
            //// move down
            //clsI.Move(Globals.StaticData.MoveType.MoveDown, clsBoard.x, clsBoard.y, clsBoard.CurrentBoard, clsBoard.MergeBoard);
            //PrintMergeBoard(clsBoard.MergeBoard);
            //// rotate right
            //clsI.Rotate(Globals.StaticData.RotationType.RotateLeft, clsBoard.x, clsBoard.y, clsBoard.CurrentBoard, clsBoard.MergeBoard);
            //PrintMergeBoard(clsBoard.MergeBoard);
            //// move right
            //clsI.Move(Globals.StaticData.MoveType.MoveRight, clsBoard.x, clsBoard.y, clsBoard.CurrentBoard, clsBoard.MergeBoard);
            //PrintMergeBoard(clsBoard.MergeBoard);
            //// move right
            //clsI.Move(Globals.StaticData.MoveType.MoveRight, clsBoard.x, clsBoard.y, clsBoard.CurrentBoard, clsBoard.MergeBoard);
            //PrintMergeBoard(clsBoard.MergeBoard);
            //// move down
            //clsI.Move(Globals.StaticData.MoveType.MoveDown, clsBoard.x, clsBoard.y, clsBoard.CurrentBoard, clsBoard.MergeBoard);
            //PrintMergeBoard(clsBoard.MergeBoard);
            //// move right
            //clsI.Move(Globals.StaticData.MoveType.MoveRight, clsBoard.x, clsBoard.y, clsBoard.CurrentBoard, clsBoard.MergeBoard);
            //PrintMergeBoard(clsBoard.MergeBoard);
            //// move down
            //clsI.Move(Globals.StaticData.MoveType.MoveDown, clsBoard.x, clsBoard.y, clsBoard.CurrentBoard, clsBoard.MergeBoard);
            //PrintMergeBoard(clsBoard.MergeBoard);
            //// rotate right
            //clsI.Rotate(Globals.StaticData.RotationType.RotateLeft, clsBoard.x, clsBoard.y, clsBoard.CurrentBoard, clsBoard.MergeBoard);
            //PrintMergeBoard(clsBoard.MergeBoard);
            //// move down
            //clsI.Move(Globals.StaticData.MoveType.MoveDown, clsBoard.x, clsBoard.y, clsBoard.CurrentBoard, clsBoard.MergeBoard);
            //PrintMergeBoard(clsBoard.MergeBoard);

            this.Controls.Add(this.clsRender.BoardPanel);
            this.clsRender.DrawBoard(clsBoard.CurrentBoard);
#endif
        }

#if DEBUG
        private void PrintMergeBoard(int[,] Board)
        {
            Console.WriteLine("--------------");
            for (int y = 0; y < Board.GetLength(1); y++)
            {
                for (int x = 0; x < Board.GetLength(0); x++)
                {
                    Console.Write(Board[x, y].ToString());
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }
#endif
    }
}
