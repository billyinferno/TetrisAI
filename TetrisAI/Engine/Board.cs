using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisAI.Globals;

namespace TetrisAI.Engine
{
    class Board
    {
        public int[,] CurrentBoard { get; private set; }
        public int[,] MergeBoard { get; private set; }
        public int[,] DiffBoard { get; private set; }

        public int x;
        public int y;

        private Shape.I TetrominoI;
        private Shape.J TetrominoJ;
        private Shape.L TetrominoL;
        private Shape.O TetrominoO;
        private Shape.S TetrominoS;
        private Shape.T TetrominoT;
        private Shape.Z TetrominoZ;

        public Board(int X, int Y)
        {
            // set current board x and y
            this.x = X;
            this.y = Y;

            // create the board array based on the X and Y parameter passed to the
            // constructor
            this.CurrentBoard = new int[X, Y];
            this.MergeBoard = new int[X, Y];
            this.DiffBoard = new int[X, Y];
            this.InitializeBoard();

            // generate the Tetromino shape
            this.TetrominoI = new Shape.I();
            this.TetrominoJ = new Shape.J();
            this.TetrominoL = new Shape.L();
            this.TetrominoO = new Shape.O();
            this.TetrominoS = new Shape.S();
            this.TetrominoT = new Shape.T();
            this.TetrominoZ = new Shape.Z();
        }

        public void InitializeBoard()
        {
            // initialize all the board with "0".
            for (int i = 0; i < this.x; i++)
            {
                for (int j = 0; j < this.y; j++)
                {
                    this.CurrentBoard[i, j] = 0;
                    this.MergeBoard[i, j] = 0;
                    this.DiffBoard[i, j] = 0;
                }
            }
        }

        public void CompareBoard()
        {
            // we will compare the CurrentBoard and MergeBoard and give the
            // result on the DiffBoard array.
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    // check whether the CurrentBoard and MergeBoard have different data?
                    if (CurrentBoard[x, y] == DiffBoard[x, y])
                    {
                        // no differences
                        DiffBoard[x, y] = 0;
                    }
                    else
                    {
                        // the current board and the merge board is different
                        DiffBoard[x, y] = 1;
                    }
                }
            }
        }

        public bool IsGameOver()
        {
            // check whether there are value on the top of the board array
            for (int x = 0; x < this.x; x++)
            {
                if (this.CurrentBoard[x, 0] > 0)
                {
                    // game over!
                    return true;
                }
            }

            // default to return false
            return false;
        }
    }
}
