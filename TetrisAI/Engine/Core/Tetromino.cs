using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisAI.Engine.Core
{
    public class Tetromino
    {
        // array that will hold the shape of tetronimo.
        // this array wil be initialize and filled on the Shape Class.
        public int[,,] ShapeArray { get; protected set; }

        // position of the shape on the board
        public int PosX { get; protected set; }
        public int PosY { get; protected set; }

        // current rotation of the shape
        public int CurrentRotation { get; protected set; }

        // the color of the shape (based on the image index list)
        public int ImageIndex { get; protected set; }

        // type of the shape movement
        public enum MoveType
        {
            MoveLeft, MoveRight, MoveDown
        }

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
            // create the ShapeArray
            this.ShapeArray = new int[NumOfRotation, ShapeArrayX, ShapeArrayY];

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

            // after this the child class should generate the shape based on the 
        }

        public virtual void Reset()
        {
            throw new NotImplementedException("The Reset function is not implemented on Tetromino Class.");
        }

        public virtual void Rotate()
        {
            throw new NotImplementedException("The Rotate function is not implemented on Tetromino Class.");
        }

        public virtual void Move(MoveType mv)
        {
            // check for the movement requested by user
            switch (mv)
            {
                case MoveType.MoveDown:
                    // call the MoveDown function
                    this.MoveDown();
                    break;
                case MoveType.MoveLeft:
                    // call the MoveLeft function
                    this.MoveLeft();
                    break;
                case MoveType.MoveRight:
                    // call the MoveRight function
                    break;
                default:
                    throw new Exception("Invalid move request to the move function on Tetromino Class.");
            }
        }

        protected virtual void MoveLeft()
        {
            throw new NotImplementedException("The Move Left function is not implemented on Tetromino Class.");
        }

        protected virtual void MoveRight()
        {
            throw new NotImplementedException("The Move Right function is not implemented on Tetromino Class.");
        }

        protected virtual void MoveDown()
        {
            throw new NotImplementedException("The Move Down function is not implemented on Tetromino Class.");
        }
    }
}
