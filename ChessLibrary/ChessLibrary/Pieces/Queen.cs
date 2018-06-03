namespace ChessLibrary.Pieces
{
    public class Queen : SimpleMovePiece
    {
        private static MovePattern MovePattern;

        static Queen()
        {
            int[] dRows = new int[] { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dCols = new int[] { -1, 0, 1, -1, 1, -1, 0, 1 };
            Queen.MovePattern = new MovePattern(dRows, dCols, true);
        }

        public Queen(Field pos, Color color, int moveCount) : 
            base(pos, PieceType.Queen, color, Queen.MovePattern, moveCount)
        {
        }
    }
}
