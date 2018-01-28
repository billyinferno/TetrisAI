using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisAI.Globals
{
    public static class StaticData
    {
        // type of the shape movement
        public enum MoveType
        {
            MoveLeft, MoveRight, MoveDown
        }

        // type of the shape rotation
        public enum RotationType
        {
            RotateLeft, RotateRight
        }
    }
}
