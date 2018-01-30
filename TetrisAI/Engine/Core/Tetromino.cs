using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisAI.Globals;

namespace TetrisAI.Engine.Core
{
    public class Tetromino
    {
        // array that will hold the shape of tetronimo.
        // this array wil be initialize and filled on the Shape Class.
        public int[,,] ShapeArray { get; protected set; }
        public int[] ShapeOffsetX { get; protected set; }
        public int[] ShapeOffsetY { get; protected set; }
        public int[] ShapeWidth { get; protected set; }
        public int[] ShapeHeight { get; protected set; }

        // position of the shape on the board
        public int PosX { get; protected set; }
        public int PosY { get; protected set; }

        // the size of the shape array
        public int MaxRotation { get; protected set; }
        public int MaxX { get; protected set; }
        public int MaxY { get; protected set; }

        // current rotation of the shape
        public int CurrentRotation { get; protected set; }

        // the color of the shape (based on the image index list)
        public int ImageIndex { get; protected set; }

        public Tetromino()
        {
            // set the Tetromino Position in 0,0
            this.PosX = 0;
            this.PosY = 0;

            // set the Current Rotation as the first rotation which is 0
            this.CurrentRotation = 0;
        }

        public virtual void CreateShape(int NumOfRotation, int ShapeArrayX, int ShapeArrayY)
        {
            // set the ShapeArrayX and ShapeArrayT into MaxX and MaxY
            this.MaxRotation = NumOfRotation;
            this.MaxX = ShapeArrayX;
            this.MaxY = ShapeArrayY;

            // create the ShapeArray
            this.ShapeArray = new int[NumOfRotation, ShapeArrayX, ShapeArrayY];

            // create the ShapeOffset array
            this.ShapeOffsetX = new int[NumOfRotation];
            this.ShapeOffsetY = new int[NumOfRotation];

            // create the ShapeWidth and ShapeHeight array
            this.ShapeWidth = new int[NumOfRotation];
            this.ShapeHeight = new int[NumOfRotation];

            // initialize the ShapeArray
            for (int i = 0; i < NumOfRotation; i++)
            {
                for (int j = 0; j < ShapeArrayX; j++)
                {
                    for (int k = 0; k < ShapeArrayY; k++)
                    {
                        this.ShapeArray[i, j, k] = 0;
                    }
                }
            }

            // after this the child class should generate the shape based on the type of tetromino wil
            // be created, and fille the ShapeOffset for each rotation type performed.
        }

        public virtual void Reset()
        {
            throw new NotImplementedException("The Reset function is not implemented on Tetromino Class.");
        }

        public bool Rotate(StaticData.RotationType rt, int BoardX, int BoardY, int[,] CurrentBoard, int[,] MergeBoard)
        {
            // first copy the CurrentBoard to the MergeBoard
            this.CopyBoard(BoardX, BoardY, CurrentBoard, MergeBoard);

            // check the rotation performed on the tetromino
            switch (rt)
            {
                case Globals.StaticData.RotationType.RotateLeft:
                    return this.RotateLeft(BoardX, BoardY, CurrentBoard, MergeBoard);

                case Globals.StaticData.RotationType.RotateRight:
                    return this.RotateRight(BoardX, BoardY, CurrentBoard, MergeBoard);

                default:
                    throw new NotImplementedException("Unknown rotate function on the Tetromino Class.");
            }
        }

        private bool RotateLeft(int BoardX, int BoardY, int[,] CurrentBoard, int[,] MergeBoard)
        {
            // backup current rotation position
            int TempRotation = this.CurrentRotation;

            // for this we will substract the current rotation with 1.
            // if the value is < 0, then it means it should go to the maximum rotation - 1.
            this.CurrentRotation -= 1;
            if (this.CurrentRotation < 0)
            {
                this.CurrentRotation = this.MaxRotation - 1;
            }

            // check current x and y position
            int TempPostX;
            int TempPostY;

            // get the offset for X and Y
            int OffsetX = (this.ShapeOffsetX[CurrentRotation] - this.ShapeOffsetX[TempRotation]) * -1;
            int OffsetY = (this.ShapeOffsetY[CurrentRotation] - this.ShapeOffsetY[TempRotation]) * -1;

            TempPostX = this.PosX + OffsetX;
            TempPostY = this.PosY + OffsetY;

            // check if the position if passed the Board Boundaries?
            // if so then perform wall kick and put it closed to the wall, instead reject the rotation
            // performed to the tetromino
            if ((TempPostX + this.ShapeWidth[CurrentRotation]) >= BoardX)
            {
                // put this near to the wall
                TempPostX = TempPostX - ((TempPostX + this.ShapeWidth[CurrentRotation]) - BoardX);
            }

            // now check whether we cross the board limit or not?
            if (this.IsOutBoundaries(TempPostX, TempPostY, BoardX, BoardY, MergeBoard))
            {
                // return back the current rotation
                this.CurrentRotation = TempRotation;
                // copy current position and rotation to the merge board
                this.MergeShapeToBoard(BoardX, BoardY, CurrentBoard, MergeBoard);
                // cannot perform this rotation
                return false;
            }

            // move temporary position to the current position
            this.PosX = TempPostX;
            this.PosY = TempPostY;

            // we can perform the rotation, return true
            return true;
        }

        private bool RotateRight(int BoardX, int BoardY, int[,] CurrentBoard, int[,] MergeBoard)
        {
            // backup current rotation position
            int TempRotation = this.CurrentRotation;

            // for this we will add the current rotation with 1.
            // if the value is >= max rotation, then it means it should go to the first rotation (0).
            this.CurrentRotation += 1;
            if (this.CurrentRotation >= this.MaxRotation)
            {
                this.CurrentRotation = 0;
            }

            // check current x and y position
            int TempPostX;
            int TempPostY;

            // get the offset for X and Y
            int OffsetX = (this.ShapeOffsetX[CurrentRotation] - this.ShapeOffsetX[TempRotation]) * -1;
            int OffsetY = (this.ShapeOffsetY[CurrentRotation] - this.ShapeOffsetY[TempRotation]) * -1;
           
            TempPostX = this.PosX + OffsetX;
            TempPostY = this.PosY + OffsetY;

            // check if the position if passed the Board Boundaries?
            // if so then perform wall kick and put it closed to the wall, instead reject the rotation
            // performed to the tetromino
            if ((TempPostX + this.ShapeWidth[CurrentRotation]) >= BoardX)
            {
                // put this near to the wall
                TempPostX = TempPostX - ((TempPostX + this.ShapeWidth[CurrentRotation]) - BoardX);
            }

            if ((TempPostY + this.ShapeHeight[CurrentRotation]) >= BoardY)
            {
                // recalculate the height position
                TempPostY = TempPostY - ((TempPostY + this.ShapeHeight[CurrentRotation]) - BoardY);
            }

            // now check whether we cross the board limit or not?
            if (this.IsOutBoundaries(TempPostX, TempPostY, BoardX, BoardY, MergeBoard))
            {
                // return back the current rotation
                this.CurrentRotation = TempRotation;
                // copy current position and rotation to the merge board
                this.MergeShapeToBoard(BoardX, BoardY, CurrentBoard, MergeBoard);
                // cannot perform this rotation
                return false;
            }

            // move temporary position to the current position
            this.PosX = TempPostX;
            this.PosY = TempPostY;

            // we can perform the rotation, return true
            return true;
        }

        public bool Move(StaticData.MoveType mv, int BoardX, int BoardY, int[,] CurrentBoard, int[,] MergeBoard)
        {
            // first let's copy the CurrentBoard into MergeBoard
            this.CopyBoard(BoardX, BoardY, CurrentBoard, MergeBoard);

            // check for the movement requested by user
            switch (mv)
            {
                case StaticData.MoveType.MoveDown:
                    // call the MoveDown function
                    return this.MoveDown(BoardX, BoardY, CurrentBoard, MergeBoard);
                case StaticData.MoveType.MoveLeft:
                    // call the MoveLeft function
                    return this.MoveLeft(BoardX, BoardY, CurrentBoard, MergeBoard);
                case StaticData.MoveType.MoveRight:
                    // call the MoveRight function
                    return this.MoveRight(BoardX, BoardY, CurrentBoard, MergeBoard);
                default:
                    throw new Exception("Invalid move request to the move function on Tetromino Class.");
            }
        }

        public bool MoveDrop(int NumDrop, int BoardX, int BoardY, int[,] CurrentBoard, int[,] MergeBoard)
        {
            int i = 0;
            while (i < NumDrop)
            {
                // move the piece
                if (!this.Move(StaticData.MoveType.MoveDown, BoardX, BoardY, CurrentBoard, MergeBoard))
                {
                    // move is finished
                    return false;    
                }

                // next drop
                i += 1;
            }

            // defaulted to return false
            return false;
        }

        private bool MoveLeft(int BoardX, int BoardY, int[,] CurrentBoard, int[,] MergeBoard)
        {
            // check whether we can move left the shape on the board or not?
            // then move down the shape
            int TempPosX = this.PosX - 1;
            int TempPosY = this.PosY;

            // check whether we are out boundaries or not?
            if (this.IsOutBoundaries(TempPosX, TempPosY, BoardX, BoardY, MergeBoard))
            {
                // shape is out of boundaries
                // just use previous shape location and put it on the merge board
                this.MergeShapeToBoard(BoardX, BoardY, CurrentBoard, MergeBoard);
                return false;
            }

            // set this is the new X position for the shape
            this.PosX = TempPosX;

            // default the return value into true
            return true;
        }

        private bool MoveRight(int BoardX, int BoardY, int[,] CurrentBoard, int[,] MergeBoard)
        {
            // check whether we can move right the shape on the board or not?
            // then move down the shape
            int TempPosX = this.PosX + 1;
            int TempPosY = this.PosY;

            // check whether we are out boundaries or not?
            if (this.IsOutBoundaries(TempPosX, TempPosY, BoardX, BoardY, MergeBoard))
            {
                // shape is out of boundaries
                // just use previous shape location and put it on the merge board
                this.MergeShapeToBoard(BoardX, BoardY, CurrentBoard, MergeBoard);
                return false;
            }

            // set this is the new X position for the shape
            this.PosX = TempPosX;

            // default the return value into true
            return true;
        }

        private bool MoveDown(int BoardX, int BoardY, int[,] CurrentBoard, int[,] MergeBoard)
        {
            // check whether we can move down the shape on the board or not?
            // then move down the shape
            int TempPosX = this.PosX;
            int TempPosY = this.PosY + 1;

            // check whether we are out boundaries or not?
            if (this.IsOutBoundaries(TempPosX, TempPosY, BoardX, BoardY, MergeBoard))
            {
                // shape is out of boundaries
                // just use previous shape location and put it on the merge board
                this.MergeShapeToBoard(BoardX, BoardY, CurrentBoard, MergeBoard);
                return false;
            }

            // set this is the new Y position for the shape
            this.PosY = TempPosY;

            // default the return value into true
            return true;
        }

        public bool IsPlanted(int BoardX, int BoardY, int[,] CurrentBoard)
        {
            // for is planted function, we can just check whether we can still move down again or not?
            // loop for the shape, and check whether it can still move down or not?
            for (int i = 0; i < this.MaxX; i++)
            {
                for (int j = 0; j < this.MaxY; j++)
                {
                    // if the shape is 1, then ensure the board is empty
                    if (this.ShapeArray[this.CurrentRotation, i, j] == 1)
                    {
                        // check whether we can put this shape on the array?
                        if ((this.PosX + i >= 0) && (this.PosX + i < BoardX) &&
                            (this.PosY + j >= 0) && (this.PosY + j < BoardY))
                        {
                            // check whether this is already reached the end of the board?
                            if ((this.PosY + j) == (BoardY - 1))
                            {
                                // already reached the end of the board
                                return true;
                            }
                            else
                            {
                                // check below if this piece whether there piece already added
                                // on the ground or not?
                                if (!(CurrentBoard[(this.PosX + i), (this.PosY + j + 1)] == 0))
                                {
                                    // cannot move the tetromino here!
                                    return true;
                                }
                            }
                        }
                        else
                        {
                            // shape is out of boundaries
                            return true;
                        }
                    }
                }
            }

            // default to return false
            return false;
        }

        public virtual int DistanceToLand(int BoardY, int[,] CurrentBoard)
        {
            throw new NotImplementedException("Distance To Land function is not implemented on Tetromino class.");
        }

        public void JoinShapeToBoard(int [,] CurrentBoard)
        {
            for (int y = 0; y < this.MaxY; y++)
            {
                for (int x = 0; x < this.MaxX; x++)
                {
                    if (this.ShapeArray[this.CurrentRotation,x, y] == 1)
                    {
                        CurrentBoard[(this.PosX + x), (this.PosY + y)] = this.ImageIndex;
                    }
                }
            }
        }

        private void CopyBoard(int BoardX, int BoardY, int[,] CurrentBoard, int[,] MergeBoard)
        {
            for (int i = 0; i < BoardX; i++)
            {
                for (int j = 0; j < BoardY; j++)
                {
                    MergeBoard[i, j] = CurrentBoard[i, j];
                }
            }
        }

        public void MergeShapeToBoard(int BoardX, int BoardY, int[,] CurrentBoard, int[,] MergeBoard)
        {
            // merge current shape to the merge board.
            // this function will not going to perform any checking whether
            // the shape is fit on the board or not?
            // 
            // this function will just blindly loop the board and put the
            // shape on the position regardless the previous data on Current Board
            this.CopyBoard(BoardX, BoardY, CurrentBoard, MergeBoard);

            for (int j = 0; j < this.MaxY; j++)
            {
                for (int i = 0; i < this.MaxX; i++)
                {
                    if (this.ShapeArray[this.CurrentRotation, i, j] == 1)
                    {
                        MergeBoard[this.PosX + i, this.PosY + j] = this.ImageIndex;
                    }
                }
            }
        }

        private bool IsOutBoundaries(int PosX, int PosY, int BoardX, int BoardY, int[,] MergeBoard)
        {
            // check whether all the board is empty for this move or not?
            for (int i = 0; i < this.MaxX; i++)
            {
                for (int j = 0; j < this.MaxY; j++)
                {
                    // if the shape is 1, then ensure the board is empty
                    if (this.ShapeArray[this.CurrentRotation, i, j] == 1)
                    {
                        // check whether we can put this shape on the array?
                        if ((PosX + i >= 0) && (PosX + i < BoardX) &&
                            (PosY + j >= 0) && (PosY + j < BoardY))
                        {
                            if (MergeBoard[(PosX + i), (PosY + j)] == 0)
                            {
                                // put the image index value to the board
                                MergeBoard[(PosX + i), (PosY + j)] = this.ImageIndex;
                            }
                            else
                            {
                                // cannot move the tetromino here!
                                return true;
                            }
                        }
                        else
                        {
                            // shape is out of boundaries
                            return true;
                        }
                    }
                }
            }

            // shape is not out of boundaries
            return false;
        }
    }
}
