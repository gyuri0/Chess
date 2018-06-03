namespace ChessLibrary
{
    public class PiecePromotionAction
    {
        public Piece Piece { get; }

        public Field Goal { get; }

        public PieceType PromotionType { get; }

        public PiecePromotionAction(Piece piece, Field goal, PieceType promotionType)
        {
            this.Piece = piece;
            this.Goal = goal;
            this.PromotionType = promotionType;
        }
    }
}
