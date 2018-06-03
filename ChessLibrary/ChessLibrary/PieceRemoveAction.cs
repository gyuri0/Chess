namespace ChessLibrary
{
    public class PieceRemoveAction
    {
        public Piece Piece { get; }

        public PieceRemoveAction(Piece piece)
        {
            this.Piece = piece;
        }
    }
}
