namespace ChessLibrary.Pieces
{
    public class Knight : SimpleMovePiece
    {
        private static MovePattern MovePattern;

        static Knight()
        {
            int[] dRows = new int[] { -2, -2, -1, -1, 1, 1, 2, 2 };
            int[] dCols = new int[] { -1, 1, -2, 2, -2, 2, -1, 1 };

            Knight.MovePattern = new MovePattern(dRows, dCols, false);
        }

        public Knight(Field pos, Color color, int moveCount) : 
            base(pos, PieceType.Knight, color, Knight.MovePattern, moveCount)
        {
        }
    }
}
