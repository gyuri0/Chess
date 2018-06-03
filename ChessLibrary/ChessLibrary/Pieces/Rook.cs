namespace ChessLibrary.Pieces
{
    public class Rook : SimpleMovePiece
    {
        private static MovePattern MovePattern;

        static Rook()
        {
            int[] dRows = new int[] { -1, 0, 0, 1 };
            int[] cRows = new int[] { 0, -1, 1, 0 };
            Rook.MovePattern = new MovePattern(dRows, cRows, true);
        }

        public Rook(Field pos, Color color, int moveCount) : 
            base(pos, PieceType.Rook, color, Rook.MovePattern, moveCount)
        {
        }
    }
}
