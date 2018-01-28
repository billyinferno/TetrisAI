using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisAI.Engine.Core;

namespace TetrisAI.Engine.Shape
{
    class I : Tetromino
    {
        public I()
        {
            // set the position of the Tetromino and create the shape of the tetromino
            this.Reset();

            // set the color of the Tetromino
            this.ImageIndex = 1;

            // tetromino I will be based on 4,4 array so create 4,4 array
            this.CreateShape(4, 4, 4);
        }

        public override void CreateShape(int NumOfRotation, int ShapeArrayX, int ShapeArrayY)
        {
            // generate the array for the shape
            base.CreateShape(NumOfRotation, ShapeArrayX, ShapeArrayY);

            // create the value for the shape
            // first rotation
            // +-+-+-+-+
            // |0|0|0|0|
            // +-+-+-+-+
            // |0|0|0|0|
            // +-+-+-+-+
            // |1|1|1|1|
            // +-+-+-+-+
            // |0|0|0|0|
            // +-+-+-+-+
            this.ShapeArray[0, 0, 2] = 1;
            this.ShapeArray[0, 1, 2] = 1;
            this.ShapeArray[0, 2, 2] = 1;
            this.ShapeArray[0, 3, 2] = 1;
            this.ShapeOffsetX[0] = 0;
            this.ShapeOffsetY[0] = 2;
            this.ShapeWidth[0] = 4;
            this.ShapeHeight[0] = 1;

            // second rotation
            // +-+-+-+-+
            // |0|0|1|0|
            // +-+-+-+-+
            // |0|0|1|0|
            // +-+-+-+-+
            // |0|0|1|0|
            // +-+-+-+-+
            // |0|0|1|0|
            // +-+-+-+-+
            this.ShapeArray[1, 2, 0] = 1;
            this.ShapeArray[1, 2, 1] = 1;
            this.ShapeArray[1, 2, 2] = 1;
            this.ShapeArray[1, 2, 3] = 1;
            this.ShapeOffsetX[1] = 2;
            this.ShapeOffsetY[1] = 0;
            this.ShapeWidth[1] = 1;
            this.ShapeHeight[1] = 4;

            // third rotation
            // +-+-+-+-+
            // |0|0|0|0|
            // +-+-+-+-+
            // |0|0|0|0|
            // +-+-+-+-+
            // |1|1|1|1|
            // +-+-+-+-+
            // |0|0|0|0|
            // +-+-+-+-+
            this.ShapeArray[2, 0, 2] = 1;
            this.ShapeArray[2, 1, 2] = 1;
            this.ShapeArray[2, 2, 2] = 1;
            this.ShapeArray[2, 3, 2] = 1;
            this.ShapeOffsetX[2] = 0;
            this.ShapeOffsetY[2] = 2;
            this.ShapeWidth[2] = 4;
            this.ShapeHeight[2] = 1;

            // fourth rotation
            // +-+-+-+-+
            // |0|1|0|0|
            // +-+-+-+-+
            // |0|1|0|0|
            // +-+-+-+-+
            // |0|1|0|0|
            // +-+-+-+-+
            // |0|1|0|0|
            // +-+-+-+-+
            this.ShapeArray[3, 1, 0] = 1;
            this.ShapeArray[3, 1, 1] = 1;
            this.ShapeArray[3, 1, 2] = 1;
            this.ShapeArray[3, 1, 3] = 1;
            this.ShapeOffsetX[3] = 1;
            this.ShapeOffsetY[3] = 0;
            this.ShapeWidth[3] = 1;
            this.ShapeHeight[3] = 4;
        }

        public override void Reset()
        {
            // reset the position of the tetromino shape to the initialize value.
            // we will do this once the tetromino is landed on the board.

            // set the initial location of the tetromino on the board.
            this.PosX = 3;
            this.PosY = -2;

            // set to the first rotation of the tetromino
            this.CurrentRotation = 0;
        }
    }
}
