﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisAI.Engine.Core;

namespace TetrisAI.Engine.Shape
{
    class T:Tetromino
    {
        public T()
        {
            // set the position of the Tetromino and create the shape of the tetromino
            this.Reset();

            // set the color of the Tetromino
            this.ImageIndex = 2;

            // tetromino I will be based on 3,3 array so create 3,3 array
            this.CreateShape(4, 3, 3);
        }

        public override void CreateShape(int NumOfRotation, int ShapeArrayX, int ShapeArrayY)
        {
            // generate the base information for the shape
            base.CreateShape(NumOfRotation, ShapeArrayX, ShapeArrayY);

            // create the value for the shape
            // first rotation
            //    0 1 2
            //   +-+-+-+
            // 0 |0|0|0|
            //   +-+-+-+
            // 1 |1|1|1|
            //   +-+-+-+
            // 2 |0|1|0|
            //   +-+-+-+
            this.ShapeArray[0, 0, 1] = 1;
            this.ShapeArray[0, 1, 1] = 1;
            this.ShapeArray[0, 2, 1] = 1;
            this.ShapeArray[0, 1, 2] = 1;
            this.ShapeOffsetX[0] = 0;
            this.ShapeOffsetY[0] = 1;
            this.ShapeWidth[0] = 3;
            this.ShapeHeight[0] = 2;

            // second rotation
            //    0 1 2
            //   +-+-+-+
            // 0 |0|1|0|
            //   +-+-+-+
            // 1 |1|1|0|
            //   +-+-+-+
            // 2 |0|1|0|
            //   +-+-+-+
            this.ShapeArray[1, 1, 0] = 1;
            this.ShapeArray[1, 0, 1] = 1;
            this.ShapeArray[1, 1, 1] = 1;
            this.ShapeArray[1, 1, 2] = 1;
            this.ShapeOffsetX[1] = 0;
            this.ShapeOffsetY[1] = 0;
            this.ShapeWidth[1] = 2;
            this.ShapeHeight[1] = 3;

            // third rotation
            //    0 1 2
            //   +-+-+-+
            // 0 |0|0|0|
            //   +-+-+-+
            // 1 |0|1|0|
            //   +-+-+-+
            // 2 |1|1|1|
            //   +-+-+-+
            this.ShapeArray[2, 1, 1] = 1;
            this.ShapeArray[2, 0, 2] = 1;
            this.ShapeArray[2, 1, 2] = 1;
            this.ShapeArray[2, 2, 2] = 1;
            this.ShapeOffsetX[2] = 0;
            this.ShapeOffsetY[2] = 1;
            this.ShapeWidth[2] = 3;
            this.ShapeHeight[2] = 2;

            // fourth rotation
            //    0 1 2
            //   +-+-+-+
            // 0 |0|1|0|
            //   +-+-+-+
            // 1 |0|1|1|
            //   +-+-+-+
            // 2 |0|1|0|
            //   +-+-+-+
            this.ShapeArray[3, 1, 0] = 1;
            this.ShapeArray[3, 1, 1] = 1;
            this.ShapeArray[3, 2, 1] = 1;
            this.ShapeArray[3, 1, 2] = 1;
            this.ShapeOffsetX[3] = 1;
            this.ShapeOffsetY[3] = 0;
            this.ShapeWidth[3] = 2;
            this.ShapeHeight[3] = 3;
        }

        public override void Reset()
        {
            // reset the position of the tetromino shape to the initialize value.
            // we will do this once the tetromino is landed on the board.

            // set the initial location of the tetromino on the board.
            this.PosX = 3;
            this.PosY = -1;

            // set to the first rotation of the tetromino
            this.CurrentRotation = 0;
        }

        public override int DistanceToLand(int BoardY, int[,] CurrentBoard)
        {
            int NearestLandPosition = -1;

            // botom position will always going to be 2
            int BottomPos = 2;

            // loop for all the width of the array
            for (int i = 0; i < this.MaxX; i++)
            {
                // check whether this bottom position is "1", if yes then check on the
                // board, what is nearest land from this position
                if (this.ShapeArray[this.CurrentRotation, i, BottomPos] == 1)
                {
                    // check from this pos Y to bottom
                    for (int j = (this.PosY + this.ShapeOffsetY[this.CurrentRotation]); j < BoardY; j++)
                    {
                        // check whether the board is more than 0?
                        if (CurrentBoard[(this.PosX + this.ShapeOffsetX[this.CurrentRotation]), j] > 0)
                        {
                            // check whether this is more than current nearest land position?
                            if (j > NearestLandPosition)
                            {
                                NearestLandPosition = j;
                            }
                        }
                    }
                }
            }
            // if nearest position is still -1, it means that there are no land!
            // so just put it as BoardY!
            if (NearestLandPosition < 0)
            {
                NearestLandPosition = (BoardY - this.ShapeHeight[this.CurrentRotation]);
            }

            // return the nearest land position
            return NearestLandPosition;
        }
    }
}
