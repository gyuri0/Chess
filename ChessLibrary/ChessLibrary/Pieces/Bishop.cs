namespace ChessLibrary.Pieces
{
    public class Bishop : SimpleMovePiece
    {
        private static MovePattern MovePattern;

        static Bishop()
        {
            int[] dRows = new int[] { -1, -1, 1, 1 };
            int[] dCols = new int[] { -1, 1, -1, 1 };
            Bishop.MovePattern = new MovePattern(dRows, dCols, true);
        }

        public Bishop(Field pos, Color color, int moveCount) : 
            base(pos, PieceType.Bishop, color, Bishop.MovePattern, moveCount)
        {
        }
    }
}
