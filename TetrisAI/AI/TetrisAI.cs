using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisAI.Engine.Shape;

namespace TetrisAI.AI
{
    class LandedLocation
    {
        public int x;
        public int y;
    }

    class TetrisAI
    {
        public int PutX { get; private set; }
        public int PutY { get; private set; }
        public int PutRotation { get; private set; }
        public double PutKoefisien { get; private set; }

        private I TetrominoI;
        private J TetrominoJ;
        private L TetrominoL;
        private O TetrominoO;
        private S TetrominoS;
        private T TetrominoT;
        private Z TetrominoZ;

        private List<LandedLocation> ListOfLandedLocation;

        int[,] TempBoard;
        int BoardX;
        int BoardY;

        int[] KoefHeight;

        double WAggregateHeight;
        double WCompleteLines;
        double WHoles;
        double WBumpiness;

        public TetrisAI(int BoardX, int BoardY)
        {
            this.PutX = -1;
            this.PutY = -1;
            this.PutKoefisien = 999999d;

            this.TetrominoI = new I();
            this.TetrominoJ = new J();
            this.TetrominoL = new L();
            this.TetrominoO = new O();
            this.TetrominoS = new S();
            this.TetrominoT = new T();
            this.TetrominoZ = new Z();

            this.ListOfLandedLocation = new List<LandedLocation>();

            this.TempBoard = new int[BoardX, BoardY];
            this.BoardX = BoardX;
            this.BoardY = BoardY;

            this.KoefHeight = new int[BoardX];

            this.WAggregateHeight = -0.510066;
            this.WCompleteLines = 0.760666;
            this.WHoles = -0.35663;
            this.WBumpiness = -0.184483;
        }

        public void GetBestPosition(int TetrominoPiece, int[,] CurrentBoard)
        {
            bool IsFound;
            // this will set the PutX and PutY value that can be used
            // to put the TetrominoPiece

            // first clear the list of landed location from previous result
            this.ListOfLandedLocation.Clear();

            // loop from top to bottom until we find the first > 0
            for (int x = 0; x < this.BoardX; x++)
            {
                IsFound = false;
                for (int y = 0; y < this.BoardY && !IsFound; y++)
                {
                    if (CurrentBoard[x, y] > 0)
                    {
                        // found!
                        IsFound = true;

                        // put this location as the LandedLocation
                        LandedLocation ll = new LandedLocation();
                        ll.x = x;
                        ll.y = y;

                        // add landed location class to list
                        this.ListOfLandedLocation.Add(ll);
                    }
                }

                // if IsFound still false, it means that the landed location is (BoardY - 1)
                if (!IsFound)
                {
                    LandedLocation ll = new LandedLocation();
                    ll.x = x;
                    ll.y = (this.BoardY - 1);

                    this.ListOfLandedLocation.Add(ll);
                }
            }

            // set the koefisien and put x and y into initial value.
            this.PutX = -1;
            this.PutY = -1;
            this.PutKoefisien = 999999d;

            // once we got the landed location, loop through all the landed location
            // and tetromino position, and if this is not out of boundaries, then move to the next
            // rotation
            for (int i = 0; i < this.ListOfLandedLocation.Count; i++)
            {
                // check for all rotation
                switch (TetrominoPiece)
                {
                    case 1:
                        CheckRotationI(this.ListOfLandedLocation[i].x, this.ListOfLandedLocation[i].y, CurrentBoard);
                        break;
                    case 2:
                        CheckRotationJ(this.ListOfLandedLocation[i].x, this.ListOfLandedLocation[i].y, CurrentBoard);
                        break;
                    case 3:
                        CheckRotationL(this.ListOfLandedLocation[i].x, this.ListOfLandedLocation[i].y, CurrentBoard);
                        break;
                    case 4:
                        CheckRotationO(this.ListOfLandedLocation[i].x, this.ListOfLandedLocation[i].y, CurrentBoard);
                        break;
                    case 5:
                        CheckRotationS(this.ListOfLandedLocation[i].x, this.ListOfLandedLocation[i].y, CurrentBoard);
                        break;
                    case 6:
                        CheckRotationT(this.ListOfLandedLocation[i].x, this.ListOfLandedLocation[i].y, CurrentBoard);
                        break;
                    case 7:
                        CheckRotationZ(this.ListOfLandedLocation[i].x, this.ListOfLandedLocation[i].y, CurrentBoard);
                        break;
                }
            }
        }

        private void CopyCurrentBoardToTemp(int[,] CurrentBoard)
        {
            for (int x = 0; x < this.BoardX; x++)
            {
                for (int y = 0; y < this.BoardY; y++)
                {
                    this.TempBoard[x, y] = CurrentBoard[x, y];
                }
            }
        }

        private void CheckRotationI(int x, int y, int[,] CurrentBoard)
        {
            int TempY;
            // loop through all rotation for Tetromino I
            for (int rotation = 0; rotation < this.TetrominoI.MaxRotation; rotation++)
            {
                // put y value to temp y
                TempY = y;

                // set the rotation for this tetromino
                this.TetrominoI.SetCurrentRotation(rotation);

                // copy current board to temporary board for further processing
                this.CopyCurrentBoardToTemp(CurrentBoard);

                // get the current rotation Offset Y and Height, we will substract those
                // from the y position.
                TempY -= (this.TetrominoI.ShapeOffsetY[rotation] + this.TetrominoI.ShapeHeight[rotation]);
#if DEBUG
                Console.WriteLine("Compute for " + x.ToString() + "," + TempY.ToString());
                Console.WriteLine("-----------------");
#endif

                // check whether we can put the tetromino here or not?
                if (!this.TetrominoI.IsOutBoundaries(x, TempY, BoardX, BoardY, TempBoard))
                {
                    // we can put the tetromino here, check the koefision of this location
                    this.CheckKoefisionOfTemporaryTable(x, TempY, rotation);
                }
            }
        }

        private void CheckRotationJ(int x, int y, int[,] CurrentBoard)
        {
            int TempY;
            // loop through all rotation for Tetromino I
            for (int rotation = 0; rotation < this.TetrominoJ.MaxRotation; rotation++)
            {
                // put y value to temp y
                TempY = y;

                // set the rotation for this tetromino
                this.TetrominoJ.SetCurrentRotation(rotation);

                // copy current board to temporary board for further processing
                this.CopyCurrentBoardToTemp(CurrentBoard);

                // get the current rotation Offset Y and Height, we will substract those
                // from the y position.
                TempY -= (this.TetrominoJ.ShapeOffsetY[rotation] + this.TetrominoJ.ShapeHeight[rotation]);
#if DEBUG
                Console.WriteLine("Compute for " + x.ToString() + "," + TempY.ToString());
                Console.WriteLine("-----------------");
#endif

                // check whether we can put the tetromino here or not?
                if (!this.TetrominoJ.IsOutBoundaries(x, TempY, BoardX, BoardY, TempBoard))
                {
                    // we can put the tetromino here, check the koefision of this location
                    this.CheckKoefisionOfTemporaryTable(x, TempY, rotation);
                }
            }
        }

        private void CheckRotationL(int x, int y, int[,] CurrentBoard)
        {
            int TempY;

            // loop through all rotation for Tetromino I
            for (int rotation = 0; rotation < this.TetrominoL.MaxRotation; rotation++)
            {
                // put y value to temp y
                TempY = y;

                // set the rotation for this tetromino
                this.TetrominoL.SetCurrentRotation(rotation);

                // copy current board to temporary board for further processing
                this.CopyCurrentBoardToTemp(CurrentBoard);

                // get the current rotation Offset Y and Height, we will substract those
                // from the y position.
                TempY -= (this.TetrominoL.ShapeOffsetY[rotation] + this.TetrominoL.ShapeHeight[rotation]);
#if DEBUG
                Console.WriteLine("Compute for " + x.ToString() + "," + TempY.ToString());
                Console.WriteLine("-----------------");
#endif

                // check whether we can put the tetromino here or not?
                if (!this.TetrominoL.IsOutBoundaries(x, TempY, BoardX, BoardY, TempBoard))
                {
                    // we can put the tetromino here, check the koefision of this location
                    this.CheckKoefisionOfTemporaryTable(x, TempY, rotation);
                }
            }
        }

        private void CheckRotationO(int x, int y, int[,] CurrentBoard)
        {
            int TempY;

            // loop through all rotation for Tetromino I
            for (int rotation = 0; rotation < this.TetrominoO.MaxRotation; rotation++)
            {
                // put y value to temp y
                TempY = y;

                // set the rotation for this tetromino
                this.TetrominoO.SetCurrentRotation(rotation);

                // copy current board to temporary board for further processing
                this.CopyCurrentBoardToTemp(CurrentBoard);

                // get the current rotation Offset Y and Height, we will substract those
                // from the y position.
                TempY -= (this.TetrominoO.ShapeOffsetY[rotation] + this.TetrominoO.ShapeHeight[rotation]);
#if DEBUG
                Console.WriteLine("Compute for " + x.ToString() + "," + TempY.ToString());
                Console.WriteLine("-----------------");
#endif

                // check whether we can put the tetromino here or not?
                if (!this.TetrominoO.IsOutBoundaries(x, TempY, BoardX, BoardY, TempBoard))
                {
                    // we can put the tetromino here, check the koefision of this location
                    this.CheckKoefisionOfTemporaryTable(x, TempY, rotation);
                }
            }
        }

        private void CheckRotationS(int x, int y, int[,] CurrentBoard)
        {
            int TempY;

            // loop through all rotation for Tetromino I
            for (int rotation = 0; rotation < this.TetrominoS.MaxRotation; rotation++)
            {
                // put y value to temp y
                TempY = y;

                // set the rotation for this tetromino
                this.TetrominoS.SetCurrentRotation(rotation);

                // copy current board to temporary board for further processing
                this.CopyCurrentBoardToTemp(CurrentBoard);

                // get the current rotation Offset Y and Height, we will substract those
                // from the y position.
                TempY -= (this.TetrominoS.ShapeOffsetY[rotation] + this.TetrominoS.ShapeHeight[rotation]);
#if DEBUG
                Console.WriteLine("Compute for " + x.ToString() + "," + TempY.ToString());
                Console.WriteLine("-----------------");
#endif

                // check whether we can put the tetromino here or not?
                if (!this.TetrominoS.IsOutBoundaries(x, TempY, BoardX, BoardY, TempBoard))
                {
                    // we can put the tetromino here, check the koefision of this location
                    this.CheckKoefisionOfTemporaryTable(x, TempY, rotation);
                }
            }
        }

        private void CheckRotationT(int x, int y, int[,] CurrentBoard)
        {
            int TempY;

            // loop through all rotation for Tetromino I
            for (int rotation = 0; rotation < this.TetrominoT.MaxRotation; rotation++)
            {
                // put y value to temp y
                TempY = y;

                // set the rotation for this tetromino
                this.TetrominoT.SetCurrentRotation(rotation);

                // copy current board to temporary board for further processing
                this.CopyCurrentBoardToTemp(CurrentBoard);

                // get the current rotation Offset Y and Height, we will substract those
                // from the y position.
                TempY -= (this.TetrominoT.ShapeOffsetY[rotation] + this.TetrominoT.ShapeHeight[rotation]);
#if DEBUG
                Console.WriteLine("Compute for " + x.ToString() + "," + TempY.ToString());
                Console.WriteLine("-----------------");
#endif

                // check whether we can put the tetromino here or not?
                if (!this.TetrominoT.IsOutBoundaries(x, TempY, BoardX, BoardY, TempBoard))
                {
                    // we can put the tetromino here, check the koefision of this location
                    this.CheckKoefisionOfTemporaryTable(x, TempY, rotation);
                }
            }
        }

        private void CheckRotationZ(int x, int y, int[,] CurrentBoard)
        {
            int TempY;

            // loop through all rotation for Tetromino I
            for (int rotation = 0; rotation < this.TetrominoZ.MaxRotation; rotation++)
            {
                // put y value to temp y
                TempY = y;

                // set the rotation for this tetromino
                this.TetrominoZ.SetCurrentRotation(rotation);

                // copy current board to temporary board for further processing
                this.CopyCurrentBoardToTemp(CurrentBoard);

                // get the current rotation Offset Y and Height, we will substract those
                // from the y position.
                TempY -= (this.TetrominoZ.ShapeOffsetY[rotation] + this.TetrominoZ.ShapeHeight[rotation]);
#if DEBUG
                Console.WriteLine("Compute for " + x.ToString() + "," + TempY.ToString());
                Console.WriteLine("-----------------");
#endif

                // check whether we can put the tetromino here or not?
                if (!this.TetrominoZ.IsOutBoundaries(x, TempY, BoardX, BoardY, TempBoard))
                {
                    // we can put the tetromino here, check the koefision of this location
                    this.CheckKoefisionOfTemporaryTable(x, TempY, rotation);
                }
            }
        }

        private void CheckKoefisionOfTemporaryTable(int CurrentX, int CurrentY, int CurrentRotation)
        {
            int x, y, edge;
            bool IsFound;

            // AI computation
            double AggregateHeight = 0d;
            double CompleteLines = 0d;
            double Holes = 0d;
            double Bumpiness = 0d;
            double TotalWeight = 0d;
            int TempBumpiness;

            // first get each line height.
            // we will use this value a lot later on.
            for (x = 0; x < this.BoardX; x++)
            {
                IsFound = false;
                for (y = 0; y < this.BoardY && !IsFound; y++)
                {
                    if (this.TempBoard[x, y] > 0)
                    {
                        IsFound = true;
                        this.KoefHeight[x] = 16 - y;
                    }
                    else
                    {
                        // check for holes.
                        // holes can be found if both top and bottom of this location is > 0
                        edge = 0;

                        // check for upper block
                        if (y > 0)
                        {
                            // check the previous blocks, is it more > 0?
                            if (this.TempBoard[x, y - 1] > 0)
                            {
                                edge += 1;
                            }
                        }
                        else
                        {
                            // assume this is blocked
                            edge += 1;
                        }

                        // check for bottom block
                        if (y < (this.BoardY - 1))
                        {
                            // check the next blocks, is it more > 0?
                            if (this.TempBoard[x, y + 1] > 0)
                            {
                                edge += 1;
                            }
                        }
                        else
                        {
                            // assume this is blocked
                            edge += 1;
                        }

                        // check edge value? if 2, then we need to add the holes data
                        Holes += 1d;
                    }
                }

                // check if the IsFound is still false.
                // if yes, it means that this board doesn't have any height at all
                if (!IsFound)
                {
                    this.KoefHeight[x] = 0;
                }

                // add the koef height to the aggregate height for this column
                AggregateHeight += (double)this.KoefHeight[x];

                // check whether we can compute the bumpiness or not?
                if (x > 0)
                {
                    TempBumpiness = KoefHeight[x - 1] - KoefHeight[x];
                    if (TempBumpiness < 0)
                    {
                        TempBumpiness *= -1;
                    }

                    // add temp bumpiness to the bumpiness total
                    Bumpiness += (double)TempBumpiness;
                }
            }

            // now check how many complete lines is available for this board?
            for (y = 0; y < this.BoardY; y++)
            {
                IsFound = true;
                for (x = 0; x < this.BoardX && IsFound; x++)
                {
                    // check whether we have 0 on this blocks row?
                    if (this.TempBoard[x, y] <= 0)
                    {
                        IsFound = false;
                    }
                }

                // check whether the IsFound is true or false?
                // if true then it means we have complete lines here
                if (IsFound)
                {
                    CompleteLines += 1d;
                }
            }

            // once we got everything now compute the weight.
            TotalWeight = (this.WAggregateHeight * AggregateHeight) + (this.WCompleteLines * CompleteLines) + (this.WHoles * Holes) + (this.WBumpiness + Bumpiness);

#if DEBUG
            Console.WriteLine("Aggregate Height : " + AggregateHeight.ToString());
            Console.WriteLine("Complete Lines   : " + CompleteLines.ToString());
            Console.WriteLine("Holes            : " + Holes.ToString());
            Console.WriteLine("Bumpiness        : " + Bumpiness.ToString());
            Console.WriteLine("Total Weight     : " + TotalWeight.ToString());
#endif

            // check total weight
            if ((TotalWeight < PutKoefisien) && ((PutKoefisien - TotalWeight) > 0))
            {
                // this is means that this move is better that the previous one
                this.PutX = CurrentX;
                this.PutY = CurrentY;
                this.PutRotation = CurrentRotation;
                this.PutKoefisien = TotalWeight;
            }
        }
    }
}
