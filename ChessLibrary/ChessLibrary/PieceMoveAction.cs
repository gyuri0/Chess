namespace ChessLibrary
{
    public class PieceMoveAction
    {
        public Piece Piece { get; }

        public Field Goal { get; }

        public PieceMoveAction(Piece piece, Field goal)
        {
            this.Piece = piece;
            this.Goal = goal;
        }
    }
}
