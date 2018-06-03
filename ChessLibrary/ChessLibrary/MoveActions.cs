using System.Collections.Generic;

namespace ChessLibrary
{
    public class MoveActions
    {
        public List<PieceMoveAction> PieceMoveActions { get; }

        public List<PieceRemoveAction> PieceRemoveActions { get; }

        public List<PiecePromotionAction> PiecePromotionActions { get; }

        public MoveActions()
        {
            this.PieceMoveActions = new List<PieceMoveAction>();
            this.PieceRemoveActions = new List<PieceRemoveAction>();
            this.PiecePromotionActions = new List<PiecePromotionAction>();
        }

        public void AddPieceMoveAction(PieceMoveAction moveAction)
        {
            this.PieceMoveActions.Add(moveAction);
        }

        public void AddPieceRemoveAction(PieceRemoveAction removeAction)
        {
            this.PieceRemoveActions.Add(removeAction);
        }

        public void AddPiecePromotionAction(PiecePromotionAction promotionAction)
        {
            this.PiecePromotionActions.Add(promotionAction);
        }
    }
}
