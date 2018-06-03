using ChessLibrary.Pieces;
using System;
using System.Collections.Generic;

namespace ChessLibrary
{
    public abstract class Piece
    {
        public int MoveCount { get; }

        public PieceType Type { get; }

        public Field Position { get; }

        public Color Color { get; }

        public Piece(Field pos, PieceType type, Color color, int moveCount = 0)
        {
            this.Position = pos;
            this.Type = type;
            this.Color = color;
            this.MoveCount = moveCount;
        }

        public abstract List<Move> GetAvailableMoveCandidates(GameState gameState);

        public abstract List<Field> GetControlledFields(GameState gameState);

        public virtual MoveActions Move(GameState gameState, Move move)
        {
            MoveActions moveActions = new MoveActions();

            if (move.Promotion == null)
            {
                moveActions.AddPieceMoveAction(new PieceMoveAction(this, move.Goal));
            }
            else
            {
                moveActions.AddPiecePromotionAction(new PiecePromotionAction(this, move.Goal, move.Promotion.Value));
            }

            if (gameState.IsOccupiedByEnemy(move.Goal, this.Color))
            {
                moveActions.AddPieceRemoveAction(new PieceRemoveAction(gameState.GetPiece(move.Goal)));
            }

            return moveActions;
        }
    }
}
