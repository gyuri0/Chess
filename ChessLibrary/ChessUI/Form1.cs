using ChessLibrary;
using ChessAI;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChessUI
{
    public partial class Form1 : Form
    {
        GameState gameState = new GameState();
        int dragX;
        int dragY;
        IChessAI chessAi = new MinimaxAI(3);

        public Form1()
        {
            InitializeComponent();
            RefreshGame();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.White, 0, 0, panel1.Width, panel1.Height);
            int cellWidth = panel1.Width / 8;
            int cellHeight = panel1.Height / 8;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Brush brush;
                    if ((i + j) % 2 == 0)
                    {
                        brush = Brushes.Brown;
                    }
                    else
                    {
                        brush = Brushes.White;
                    }

                    e.Graphics.FillRectangle(brush, j * cellWidth, panel1.Height - (i + 1) * cellHeight, cellWidth, cellHeight);

                    Piece piece = this.gameState.GetPiece(Field.GetField(i, j));
                    if (piece != null)
                    {
                        e.Graphics.DrawImage(PieceImageGetter.GetImageForPiece(piece), j * cellWidth, panel1.Height - (i + 1) * cellHeight, cellWidth, cellHeight);
                    }
                }
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            dragX = e.X;
            dragY = e.Y;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            int cellWidth = panel1.Width / 8;
            int cellHeight = panel1.Height / 8;

            int startRow = (panel1.Height - dragY) / cellHeight;
            int startCol = dragX / cellWidth;

            int goalRow = (panel1.Height - e.Y) / cellHeight;
            int goalCol = e.X / cellWidth;

            Move move = new Move(Field.GetField(startRow, startCol), Field.GetField(goalRow, goalCol));
            if (this.gameState.GetAllMoves().Contains(new Move(move.Start, move.Goal, PieceType.Queen)))
            {
                PromotionChooseForm promotionChooseForm = new PromotionChooseForm();
                promotionChooseForm.ShowDialog();

                move = new Move(move.Start, move.Goal, promotionChooseForm.PromotionType);
            }

            var newState = this.gameState.Move(move);

            if (newState != null)
            {
                this.gameState = this.gameState.Move(move);
                if (this.gameState.Result == GameResult.InProgress)
                {
                    Move aIMove = chessAi.GenerateMove(gameState);
                    this.gameState = this.gameState.Move(aIMove);
                }
                this.RefreshGame();
            }

        }

        private void RefreshGame()
        {
            label1.Text = gameState.Result.ToString();
            label2.Text = $"{gameState.NextToMove} to move";
            panel1.Refresh();
        }
    }
}
