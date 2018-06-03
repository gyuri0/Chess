using System;
using System.Collections.Generic;

namespace ChessLibrary.Pieces
{
    public class Pawn : Piece
    {
        private int Direction
        {
            get
            {
                if (this.Color == Color.White)
                {
                    return 1;
                }

                return -1;
            }
        }

        public Pawn(Field pos, Color color, int moveCount) :
            base(pos, PieceType.Pawn, color, moveCount)
        {
        }

        public override MoveActions Move(GameState gameState, Move move)
        {
            var moveActions = base.Move(gameState, move);

            if (Math.Abs(move.Goal.Column - move.Start.Column) == 1 && gameState.GetPiece(move.Goal) == null)
            {
                Piece pieceToRemove = gameState.GetPiece(move.Goal.Add(-this.Direction, 0));
                moveActions.AddPieceRemoveAction(new PieceRemoveAction(pieceToRemove));
            }

            return moveActions;
        }

        public override List<Move> GetAvailableMoveCandidates(GameState gameState)
        {
            List<Move> moves = new List<Move>();

            Field forward = this.Position.Add(this.Direction, 0);
            if (!gameState.IsOccupied(forward))
            {
                moves.Add(new Move(this.Position, forward));
            }

            Field forwardLeft = this.Position.Add(this.Direction, -1);
            if (forwardLeft != Field.InvalidField && gameState.IsOccupiedByEnemy(forwardLeft, this.Color))
            {
                moves.Add(new Move(this.Position, forwardLeft));
            }

            Field forwardRight = this.Position.Add(this.Direction, 1);
            if (forwardRight != Field.InvalidField && gameState.IsOccupiedByEnemy(forwardRight, this.Color))
            {
                moves.Add(new Move(this.Position, forwardRight));
            }

            for (int i = moves.Count - 1; i >= 0; i--)
            {
                if (moves[i].Goal.Row == 0 || moves[i].Goal.Row == 7)
                {
                    moves.AddRange(GeneratePromotionMoves(moves[i]));
                    moves.RemoveAt(i);
                }
            }

            Field forwardTwo = this.Position.Add(this.Direction * 2, 0);
            if (this.MoveCount == 0 && !gameState.IsOccupied(forwardTwo) && !gameState.IsOccupied(forward))
            {
                moves.Add(new Move(this.Position, forwardTwo));
            }

            Move enPassantMove = GetEnpassantMove(gameState);
            if (enPassantMove != null)
            {
                moves.Add(enPassantMove);
            }

            return moves;
        }

        private Move GetEnpassantMove(GameState gameState)
        {
            if (gameState.LastMove == null)
            {
                return null;
            }

            Move lastMove = gameState.LastMove;
            
            Piece lastMovedPiece = gameState.GetPiece(lastMove.Goal);

            if (lastMovedPiece.Type != PieceType.Pawn)
            {
                return null;
            }

            if (Math.Abs(lastMove.Goal.Row - lastMove.Start.Row) != 2)
            {
                return null;
            }

            if(lastMove.Goal.Row - this.Position.Row != 0)
            {
                return null;
            }

            if (Math.Abs(lastMovedPiece.Position.Column - this.Position.Column) != 1)
            {
                return null;
            }

            return new Move(this.Position, Field.GetField(lastMovedPiece.Position.Row + this.Direction, lastMovedPiece.Position.Column));
        }

        private List<Move> GeneratePromotionMoves(Move move)
        {
            List<Move> promotionMoves = new List<Move>();
            PieceType[] promotionTypes = new PieceType[] { PieceType.Bishop, PieceType.Knight, PieceType.Rook, PieceType.Queen };

            foreach (var promotionType in promotionTypes)
            {
                promotionMoves.Add(new Move(move.Start, move.Goal, promotionType));
            }

            return promotionMoves;
        }

        public override List<Field> GetControlledFields(GameState gameState)
        {
            List<Field> controlledFields = new List<Field>();

            Field forwardLeft = this.Position.Add(this.Direction, -1);
            if (forwardLeft != Field.InvalidField)
            {
                controlledFields.Add(forwardLeft);
            }

            Field forwardRight = this.Position.Add(this.Direction, 1);
            if (forwardRight != Field.InvalidField)
            {
                controlledFields.Add(forwardRight);
            }

            return controlledFields;
        }
    }
}
