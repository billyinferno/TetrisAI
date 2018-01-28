using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisAI.Engine.Core;

namespace TetrisAI.Engine.Shape
{
    class O:Tetromino
    {
        public O()
        {
            // set the position of the Tetromino and create the shape of the tetromino
            this.Reset();

            // set the color of the Tetromino
            this.ImageIndex = 7;

            // tetromino I will be based on 2,2 array so create 2,2 array
            this.CreateShape(1, 2, 2);
        }

        public override void CreateShape(int NumOfRotation, int ShapeArrayX, int ShapeArrayY)
        {
            // generate the base information for the shape
            base.CreateShape(NumOfRotation, ShapeArrayX, ShapeArrayY);

            // create the value for the shape
            // first rotation
            //    0 1
            //   +-+-+
            // 0 |1|1|
            //   +-+-+
            // 1 |1|1|
            //   +-+-+
            this.ShapeArray[0, 0, 0] = 1;
            this.ShapeArray[0, 0, 1] = 1;
            this.ShapeArray[0, 1, 0] = 1;
            this.ShapeArray[0, 1, 1] = 1;
            this.ShapeOffsetX[0] = 0;
            this.ShapeOffsetY[0] = 0;
            this.ShapeWidth[0] = 2;
            this.ShapeHeight[0] = 2;
        }

        public override void Reset()
        {
            // reset the position of the tetromino shape to the initialize value.
            // we will do this once the tetromino is landed on the board.

            // set the initial location of the tetromino on the board.
            this.PosX = 4;
            this.PosY = 0;

            // set to the first rotation of the tetromino
            this.CurrentRotation = 0;
        }
    }
}
