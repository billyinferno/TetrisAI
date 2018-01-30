using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisAI.Globals;
using System.Windows.Forms;

namespace TetrisAI.Engine
{
    class Board
    {
        public int[,] CurrentBoard { get; private set; }
        private int[,] PrevMergeBoard;
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

        private List<int> TetrominoBag;
        private List<int> TetrominoHelper;
        private Random TetrominoRandom;
        private int CurrentTetromino;

        public int Level { get; private set; }
        public int Score { get; private set; }
        private bool PreviousIsTetris;

        public AI.TetrisAI GameAI { get; private set; }
        public bool IsAIEnabled { get; private set; }

        public Board(int X, int Y)
        {
            // set current board x and y
            this.x = X;
            this.y = Y;

            // create the board array based on the X and Y parameter passed to the
            // constructor
            this.CurrentBoard = new int[X, Y];
            this.MergeBoard = new int[X, Y];
            this.PrevMergeBoard = new int[X, Y];
            this.DiffBoard = new int[X, Y];

            // initialize the bag
            this.TetrominoBag = new List<int>();
            this.TetrominoHelper = new List<int>();
            this.TetrominoRandom = new Random();
            this.CurrentTetromino = -1;

            // generate the Tetromino shape
            this.TetrominoI = new Shape.I();
            this.TetrominoJ = new Shape.J();
            this.TetrominoL = new Shape.L();
            this.TetrominoO = new Shape.O();
            this.TetrominoS = new Shape.S();
            this.TetrominoT = new Shape.T();
            this.TetrominoZ = new Shape.Z();

            // generate the AI class
            this.GameAI = new AI.TetrisAI(X, Y);
            this.IsAIEnabled = false;

            // initialize the board
            this.InitializeBoard();
        }

        public void EnableAI()
        {
            this.IsAIEnabled = true;
        }

        public void DisableAI()
        {
            this.IsAIEnabled = false;
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

            // initialize the score into 0
            this.Score = 0;
            this.Level = 1;
            this.PreviousIsTetris = false;

            // initialize all the tetromino piece
            this.TetrominoI.Reset();
            this.TetrominoJ.Reset();
            this.TetrominoL.Reset();
            this.TetrominoO.Reset();
            this.TetrominoS.Reset();
            this.TetrominoT.Reset();
            this.TetrominoZ.Reset();
        }

        public void CompareBoard()
        {
            // we will compare the Previous Merger Board and Current Merge Board and give
            // the result on the DiffBoard array.
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    // check whether the CurrentBoard and MergeBoard have different data?
                    if (PrevMergeBoard[i, j] == MergeBoard[i, j])
                    {
                        // no differences
                        DiffBoard[i, j] = -1;
                    }
                    else
                    {
                        // the previous merge board and the current merge board is different
                        DiffBoard[i, j] = MergeBoard[i,j];
                    }
                }
            }
        }

        public void GetPieceFromBag()
        {
            // merge the current board to the merge board
            this.CopyCurrentToMerge();

            // then join the current shape from the next tetromino
            this.CurrentTetromino = this.GetNextTetromino();

            // check which tetromino will be performed
            switch (this.CurrentTetromino)
            {
                case 1:
                    this.TetrominoI.JoinShapeToBoard(this.MergeBoard);
                    break;
                case 2:
                    this.TetrominoJ.JoinShapeToBoard(this.MergeBoard);
                    break;
                case 3:
                    this.TetrominoL.JoinShapeToBoard(this.MergeBoard);
                    break;
                case 4:
                    this.TetrominoO.JoinShapeToBoard(this.MergeBoard);
                    break;
                case 5:
                    this.TetrominoS.JoinShapeToBoard(this.MergeBoard);
                    break;
                case 6:
                    this.TetrominoT.JoinShapeToBoard(this.MergeBoard);
                    break;
                case 7:
                    this.TetrominoZ.JoinShapeToBoard(this.MergeBoard);
                    break;
            }
        }

        public void CheckAI()
        {
            int i;
            // check if this is AI enable
            if (this.IsAIEnabled)
            {
                // check whether we got the location for PutX and PutY
                if (this.GameAI.PutX >= 0 && this.GameAI.PutY >= 0)
                {
                    for (i = 0; i < this.GameAI.PutRotation; i++)
                    {
                        this.RotatePiece(StaticData.RotationType.RotateRight);
                        this.CompareBoard();
                    }

                    // put this piece to the board
                    this.CopyMergeToPrevMerge();
                    switch (this.CurrentTetromino)
                    {
                        case 1:
                            this.TetrominoI.PutTetromino(this.GameAI.PutX, this.GameAI.PutY, this.x, this.y, this.CurrentBoard, this.MergeBoard);
                            break;
                        case 2:
                            this.TetrominoJ.PutTetromino(this.GameAI.PutX, this.GameAI.PutY, this.x, this.y, this.CurrentBoard, this.MergeBoard);
                            break;
                        case 3:
                            this.TetrominoL.PutTetromino(this.GameAI.PutX, this.GameAI.PutY, this.x, this.y, this.CurrentBoard, this.MergeBoard);
                            break;
                        case 4:
                            this.TetrominoO.PutTetromino(this.GameAI.PutX, this.GameAI.PutY, this.x, this.y, this.CurrentBoard, this.MergeBoard);
                            break;
                        case 5:
                            this.TetrominoS.PutTetromino(this.GameAI.PutX, this.GameAI.PutY, this.x, this.y, this.CurrentBoard, this.MergeBoard);
                            break;
                        case 6:
                            this.TetrominoT.PutTetromino(this.GameAI.PutX, this.GameAI.PutY, this.x, this.y, this.CurrentBoard, this.MergeBoard);
                            break;
                        case 7:
                            this.TetrominoZ.PutTetromino(this.GameAI.PutX, this.GameAI.PutY, this.x, this.y, this.CurrentBoard, this.MergeBoard);
                            break;
                    }
                    this.CompareBoard();
                }
            }
        }

        public bool IsPlanted()
        {
            // check which tetromino will be performed
            switch (this.CurrentTetromino)
            {
                case 1:
                    return this.TetrominoI.IsPlanted(this.x, this.y, this.CurrentBoard);
                case 2:
                    return this.TetrominoJ.IsPlanted(this.x, this.y, this.CurrentBoard);
                case 3:
                    return this.TetrominoL.IsPlanted(this.x, this.y, this.CurrentBoard);
                case 4:
                    return this.TetrominoO.IsPlanted(this.x, this.y, this.CurrentBoard);
                case 5:
                    return this.TetrominoS.IsPlanted(this.x, this.y, this.CurrentBoard);
                case 6:
                    return this.TetrominoT.IsPlanted(this.x, this.y, this.CurrentBoard);
                case 7:
                    return this.TetrominoZ.IsPlanted(this.x, this.y, this.CurrentBoard);
            }

            // default to false
            return false;
        }

        public void RotatePiece(StaticData.RotationType rt)
        {
            // copy the current merge board to the previous merge board.
            // so later in the diff board we will get the correct data, which block we need to update?
            this.CopyMergeToPrevMerge();

            // check which tetromino will be performed, and rotate the piece
            switch (this.CurrentTetromino)
            {
                case 1:
                    this.TetrominoI.Rotate(rt, this.x, this.y, this.CurrentBoard, this.MergeBoard);
                    break;
                case 2:
                    this.TetrominoJ.Rotate(rt, this.x, this.y, this.CurrentBoard, this.MergeBoard);
                    break;
                case 3:
                    this.TetrominoL.Rotate(rt, this.x, this.y, this.CurrentBoard, this.MergeBoard);
                    break;
                case 4:
                    this.TetrominoO.Rotate(rt, this.x, this.y, this.CurrentBoard, this.MergeBoard);
                    break;
                case 5:
                    this.TetrominoS.Rotate(rt, this.x, this.y, this.CurrentBoard, this.MergeBoard);
                    break;
                case 6:
                    this.TetrominoT.Rotate(rt, this.x, this.y, this.CurrentBoard, this.MergeBoard);
                    break;
                case 7:
                    this.TetrominoZ.Rotate(rt, this.x, this.y, this.CurrentBoard, this.MergeBoard);
                    break;
            }
        }

        public void MovePiece(StaticData.MoveType mv)
        {
            // copy the current merge board to the previous merge board.
            // so later in the diff board we will get the correct data, which block we need to update?
            this.CopyMergeToPrevMerge();

            // check which tetromino will be performed, and move the piece
            switch (this.CurrentTetromino)
            {
                case 1:
                    this.TetrominoI.Move(mv, this.x, this.y, this.CurrentBoard, this.MergeBoard);
                    break;
                case 2:
                    this.TetrominoJ.Move(mv, this.x, this.y, this.CurrentBoard, this.MergeBoard);
                    break;
                case 3:
                    this.TetrominoL.Move(mv, this.x, this.y, this.CurrentBoard, this.MergeBoard);
                    break;
                case 4:
                    this.TetrominoO.Move(mv, this.x, this.y, this.CurrentBoard, this.MergeBoard);
                    break;
                case 5:
                    this.TetrominoS.Move(mv, this.x, this.y, this.CurrentBoard, this.MergeBoard);
                    break;
                case 6:
                    this.TetrominoT.Move(mv, this.x, this.y, this.CurrentBoard, this.MergeBoard);
                    break;
                case 7:
                    this.TetrominoZ.Move(mv, this.x, this.y, this.CurrentBoard, this.MergeBoard);
                    break;
            }
        }

        public void HardDrop()
        {
            // copy the current merge board to the previous merge board.
            // so later in the diff board we will get the correct data, which block we need to update?
            this.CopyMergeToPrevMerge();

            // check which tetromino will be performed, and move the piece
            switch (this.CurrentTetromino)
            {
                case 1:
                    this.TetrominoI.MoveDrop(this.TetrominoI.DistanceToLand(this.y, this.CurrentBoard), this.x, this.y, this.CurrentBoard, this.MergeBoard);
                    break;
                case 2:
                    this.TetrominoJ.MoveDrop(this.TetrominoI.DistanceToLand(this.y, this.CurrentBoard), this.x, this.y, this.CurrentBoard, this.MergeBoard);
                    break;
                case 3:
                    this.TetrominoL.MoveDrop(this.TetrominoI.DistanceToLand(this.y, this.CurrentBoard), this.x, this.y, this.CurrentBoard, this.MergeBoard);
                    break;
                case 4:
                    this.TetrominoO.MoveDrop(this.TetrominoI.DistanceToLand(this.y, this.CurrentBoard), this.x, this.y, this.CurrentBoard, this.MergeBoard);
                    break;
                case 5:
                    this.TetrominoS.MoveDrop(this.TetrominoI.DistanceToLand(this.y, this.CurrentBoard), this.x, this.y, this.CurrentBoard, this.MergeBoard);
                    break;
                case 6:
                    this.TetrominoT.MoveDrop(this.TetrominoI.DistanceToLand(this.y, this.CurrentBoard), this.x, this.y, this.CurrentBoard, this.MergeBoard);
                    break;
                case 7:
                    this.TetrominoZ.MoveDrop(this.TetrominoI.DistanceToLand(this.y, this.CurrentBoard), this.x, this.y, this.CurrentBoard, this.MergeBoard);
                    break;
            }
        }

        public void JoinPieceToCurrent()
        {
            // check which tetromino will be performed, then joing the tetromino shape to the current board.
            // once finished joined, reset the tetromino so we can used it again later.
            switch (this.CurrentTetromino)
            {
                case 1:
                    this.TetrominoI.JoinShapeToBoard(this.CurrentBoard);
                    this.TetrominoI.Reset();
                    break;
                case 2:
                    this.TetrominoJ.JoinShapeToBoard(this.CurrentBoard);
                    this.TetrominoJ.Reset();
                    break;
                case 3:
                    this.TetrominoL.JoinShapeToBoard(this.CurrentBoard);
                    this.TetrominoL.Reset();
                    break;
                case 4:
                    this.TetrominoO.JoinShapeToBoard(this.CurrentBoard);
                    this.TetrominoO.Reset();
                    break;
                case 5:
                    this.TetrominoS.JoinShapeToBoard(this.CurrentBoard);
                    this.TetrominoS.Reset();
                    break;
                case 6:
                    this.TetrominoT.JoinShapeToBoard(this.CurrentBoard);
                    this.TetrominoT.Reset();
                    break;
                case 7:
                    this.TetrominoZ.JoinShapeToBoard(this.CurrentBoard);
                    this.TetrominoZ.Reset();
                    break;
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

        public int GetNextTetromino()
        {
            int PieceValue;

            // pop the last value of the Tetromino Bag
            if (this.TetrominoBag.Count <= 0)
            {
                // tetromino bag is empty, generate the tetromino first
                this.GenerateTetrominoBag();
            }

            // get the first tetromino piece on the bag
            PieceValue = this.TetrominoBag[0];
            this.TetrominoBag.RemoveAt(0);

            // check whether we got empty bag now?
            if (this.TetrominoBag.Count <= 0)
            {
                // generate the next tetromino bag
                this.GenerateTetrominoBag();
            }

            // check whether we got AI enabled or not?
            if (this.IsAIEnabled)
            {
                // get the location of where we should put the piece from AI
                this.GameAI.GetBestPosition(PieceValue, this.CurrentBoard);

#if DEBUG
                Console.WriteLine("AI Suggested Location: " + this.GameAI.PutX.ToString() + "," + this.GameAI.PutY.ToString() + ", Rotation : " + this.GameAI.PutRotation);
#endif
            }

            // return the piece value
            return PieceValue;
        }

        public void GenerateTetrominoBag()
        {
            int i, piece;

            // clear the tetromino bag
            this.TetrominoBag.Clear();
            this.TetrominoHelper.Clear();

            // initialize the helper
            for (i = 0; i < 7; i++)
            {
                this.TetrominoHelper.Add(i + 1);
            }

            // the tetromino should be generate value from 1 until 7
            while (this.TetrominoHelper.Count > 0)
            {
                // generate which tetromino piece we will put on the bag?
                // the tetromino piece will be presented as integer:
                // 1 - Tetromino I
                // 2 - Tetromino J
                // 3 - Tetromino L
                // 4 - Tetromino O
                // 5 - Tetromino S
                // 6 - Tetromino T
                // 7 - Tetromino Z
                piece = this.TetrominoRandom.Next(0, (this.TetrominoHelper.Count - 1));

                this.TetrominoBag.Add(this.TetrominoHelper[piece]);
                this.TetrominoHelper.RemoveAt(piece);
            }
        }

        public bool CheckLine()
        {
            bool ClearLine;
            int LineTotal = 0;

            // copy current board to merge board
            this.CopyCurrentToMerge();

            // check whether we have complete line on the board or not?
            for (int y = 0; y < this.y; y++)
            {
                ClearLine = true;
                for (int x = 0; x < this.x && ClearLine; x++)
                {
                    // check if all the line is > 0
                    if (this.CurrentBoard[x, y] == 0)
                    {
                        ClearLine = false;
                    }
                }

                // check if we have clear line here?
                if (ClearLine)
                {
                    // pop this line, and add the line total that we cleared during this iteration
                    this.PopLine(y);
                    LineTotal += 1;
                }
            }

            // check how many line we cleared
            if (LineTotal > 0)
            {
                // compute the score based on the standard tetris game scoring and return true
                // to the caller, so we knew whether we need to refresh the display or not?
                switch (LineTotal)
                {
                    case 1:
                        this.Score += (100 * this.Level);
                        this.PreviousIsTetris = false;
                        break;
                    case 2:
                        this.Score += (300 * this.Level);
                        this.PreviousIsTetris = false;
                        break;
                    case 3:
                        this.Score += (500 * this.Level);
                        this.PreviousIsTetris = false;
                        break;
                    case 4:
                        if (this.PreviousIsTetris)
                        {
                            this.Score += (1200 * this.Level);
                        }
                        else
                        {
                            this.Score += (800 * this.Level);
                            this.PreviousIsTetris = true;
                        }
                        break;
                }

                // refresh the display!
                return true;
            }

            // defaulted to return false
            return false;
        }

        private void PopLine(int PopY)
        {
            for (int y = PopY; y > 0; y--)
            {
                for (int x = 0; x < this.x; x++)
                {
                    // move the board from previous line to this line
                    this.CurrentBoard[x, y] = this.CurrentBoard[x, y - 1];
                    this.MergeBoard[x, y] = this.MergeBoard[x, y - 1];
                }
            }

            // remove the firs line of the board
            for (int x = 0; x < this.x; x++)
            {
                this.CurrentBoard[x, 0] = 0;
            }
        }

        public void KeyPress(Keys input)
        {
            switch (input)
            {
                case Keys.A:
                    // rotation left
                    break;
                case Keys.S:
                    // rotation right
                    break;
                case Keys.D:
                    // hard drop
                    break;
                case Keys.Space:
                    // hard drop
                    break;
                case Keys.Left:
                    // move left
                    break;
                case Keys.Right:
                    // move right
                    break;
                case Keys.Down:
                    // move down
                    break;
            }
        }

        private void CopyMergeToPrevMerge()
        {
            for (int i = 0; i < this.x; i++)
            {
                for (int j = 0; j < this.y; j++)
                {
                    this.PrevMergeBoard[i, j] = this.MergeBoard[i, j];
                }
            }
        }

        private void CopyCurrentToMerge()
        {
            for (int i = 0; i < this.x; i++)
            {
                for (int j = 0; j < this.y; j++)
                {
                    this.PrevMergeBoard[i, j] = this.MergeBoard[i, j];
                    this.MergeBoard[i, j] = this.CurrentBoard[i, j];
                }
            }
        }

        private void CopyCurrentToPrevMerge()
        {
            for (int i = 0; i < this.x; i++)
            {
                for (int j = 0; j < this.y; j++)
                {
                    this.PrevMergeBoard[i, j] = this.CurrentBoard[i, j];
                }
            }
        }
    }
}
