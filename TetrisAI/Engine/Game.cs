using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace TetrisAI.Engine
{
    class Game
    {
        public Board GameBoard { get; private set; }
        public Render GameRender { get; private set; }

        private System.Timers.Timer GameTimer;
        private int Gravity;

        private bool IsGameStart;
        private bool KeyPressAllowed;

        public Game(int X, int Y, ImageList ImageData)
        {
            // initialize the board and render
            this.GameBoard = new Board(X, Y);
            this.GameRender = new Render(X, Y, ImageData);

            // initialize the timer
            this.GameTimer = new System.Timers.Timer();
            this.GameTimer.Interval = 1000;
            this.GameTimer.Elapsed += OnTimedEvent;

            // initialize the game start indicator into false
            this.IsGameStart = false;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            // check whether our piece is planted or not?
            if (this.GameBoard.IsPlanted())
            {
                // no key press is allowed now
                this.KeyPressAllowed = false;

                // join the shape with current board
                this.GameBoard.JoinPieceToCurrent();

                // check if we got line finished?
                if (this.GameBoard.CheckLine())
                {
                    // got finisihed line, compare the current merge board and previous merge board
                    // and render it to the screen
                    this.GameBoard.CompareBoard();
                    this.GameRender.DrawBoard(this.GameBoard.DiffBoard);
                }

                // get the current piece from bag
                this.GameBoard.GetPieceFromBag();

                // compare the difference between current merge board with current board
                this.GameBoard.CompareBoard(); // TODO:comparison should be between Prev Merge with Current Merge
                this.GameRender.DrawBoard(this.GameBoard.DiffBoard);

                // check whether this is game over or not?
                if (this.GameBoard.IsGameOver())
                {
                    this.GameOver();
                }
                else
                {
                    this.KeyPressAllowed = true;
                }
            }
            else
            {
                // move the piece down
                this.GameBoard.MovePiece(Globals.StaticData.MoveType.MoveDown);

                // draw the current diff board
                this.GameBoard.CompareBoard();
                this.GameRender.DrawBoard(this.GameBoard.DiffBoard);

                // reset the frame back into 0
                this.KeyPressAllowed = true;
            }
        }

        public void GameOver()
        {
            // stop the timer
            this.Stop();

            // show the game over message
            // TODO:
        }

        public void Start()
        {
            // start the game
            this.IsGameStart = true;

            // reset the board
            this.GameBoard.InitializeBoard();

            // generate the tetromino bag
            this.GameBoard.GenerateTetrominoBag();

            // draw the current board
            this.GameRender.DrawBoard(this.GameBoard.CurrentBoard);

            // get the current piece from bag
            this.GameBoard.GetPieceFromBag();

            // compare the difference between current merge board with current board
            this.GameBoard.CompareBoard();
            this.GameRender.DrawBoard(this.GameBoard.DiffBoard);

            // enable the key press
            this.KeyPressAllowed = true;

            // at last, start the timer!
            this.GameTimer.Enabled = true;
        }

        public void Stop()
        {
            // stop the game
            this.IsGameStart = false;

            // enable the key press
            this.KeyPressAllowed = false;

            // then stop the timer!
            this.GameTimer.Enabled = false;
        }

        public void KeyPress(Keys input)
        {
            // key press only will be available during game start only
            if (this.IsGameStart)
            {
                if (this.KeyPressAllowed)
                {
                    // get the input key from FORM and passed it to the
                    // GameBoard
                    switch (input)
                    {
                        case Keys.J:
                            // rotation left
                            this.GameBoard.RotatePiece(Globals.StaticData.RotationType.RotateLeft);
                            // compare the board and draw the diff
                            this.GameBoard.CompareBoard();
                            this.GameRender.DrawBoard(this.GameBoard.DiffBoard);
                            break;
                        case Keys.K:
                            // rotation right
                            this.GameBoard.RotatePiece(Globals.StaticData.RotationType.RotateRight);
                            // compare the board and draw the diff
                            this.GameBoard.CompareBoard();
                            this.GameRender.DrawBoard(this.GameBoard.DiffBoard);
                            break;
                        case Keys.L:
                            // hard drop
                            this.GameBoard.HardDrop();
                            // compare the board and draw the diff
                            this.GameBoard.CompareBoard();
                            this.GameRender.DrawBoard(this.GameBoard.DiffBoard);
                            // do not disable the key press to perform the lock delay
                            break;
                        case Keys.A:
                            // move left
                            this.GameBoard.MovePiece(Globals.StaticData.MoveType.MoveLeft);
                            // compare the board and draw the diff
                            this.GameBoard.CompareBoard();
                            this.GameRender.DrawBoard(this.GameBoard.DiffBoard);
                            break;
                        case Keys.D:
                            // move right
                            this.GameBoard.MovePiece(Globals.StaticData.MoveType.MoveRight);
                            // compare the board and draw the diff
                            this.GameBoard.CompareBoard();
                            this.GameRender.DrawBoard(this.GameBoard.DiffBoard);
                            break;
                        case Keys.S:
                            // move down
                            break;
                    }
                }
#if DEBUG
                // for debug we will print whether the key is still locked
                // or not?
                else
                {
                    Console.WriteLine("KeyPress Locked!");
                }
#endif
            }
        }
    }
}
