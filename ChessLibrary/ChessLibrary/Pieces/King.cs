using System;
using System.Collections.Generic;

namespace ChessLibrary.Pieces
{
    public class King : SimpleMovePiece
    {
        private static MovePattern MovePattern;

        static King()
        {
            int[] dRows = new int[] { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dCols = new int[] { -1, 0, 1, -1, 1, -1, 0, 1 };
            King.MovePattern = new MovePattern(dRows, dCols, false);
        }

        public King(Field pos, Color color, int moveCount) :
            base(pos, PieceType.King, color, King.MovePattern, moveCount)
        {
        }

        public override List<Move> GetAvailableMoveCandidates(GameState gameState)
        {
            var moveCandidates = base.GetAvailableMoveCandidates(gameState);

            if (this.CanLeftCastle(gameState))
            {
                moveCandidates.Add(new Move(this.Position, this.Position.Add(0, -2)));
            }

            if (this.CanRightCastle(gameState))
            {
                moveCandidates.Add(new Move(this.Position, this.Position.Add(0, 2)));
            }

            return moveCandidates;
        }

        private bool CanRightCastle(GameState gameState)
        {
            if (this.MoveCount > 0)
            {
                return false;
            }

            Piece rightRook = gameState.GetPiece(this.Position.Add(0, 3));

            if (rightRook == null || rightRook.MoveCount > 0)
            {
                return false;
            }

            for (int i = 1; i <= 2; i++)
            {
                if (gameState.GetPiece(this.Position.Add(0, i)) != null)
                {
                    return false;
                }
            }

            var controlledFields = gameState.GetControlledFields(this.Color.Enemy());
            for (int i = 0; i <= 2; i++)
            {
                if (controlledFields.Contains(this.Position.Add(0, i)))
                {
                    return false;
                }
            }

            return true;
        }

        private bool CanLeftCastle(GameState gameState)
        {
            if (this.MoveCount > 0)
            {
                return false;
            }

            Piece leftRook = gameState.GetPiece(this.Position.Add(0, -4));

            if (leftRook == null || leftRook.MoveCount > 0)
            {
                return false;
            }

            for (int i = 1; i <= 3; i++)
            {
                if (gameState.GetPiece(this.Position.Add(0, -i)) != null)
                {
                    return false;
                }
            }

            var controlledFields = gameState.GetControlledFields(this.Color.Enemy());
            for (int i = 0; i <= 2; i++)
            {
                if (controlledFields.Contains(this.Position.Add(0, i)))
                {
                    return false;
                }
            }

            return true;
        }

        public override MoveActions Move(GameState gameState, Move move)
        {
            var moveActions = base.Move(gameState, move);

            if (move.Goal.Column - move.Start.Column == 2)
            {
                moveActions.AddPieceMoveAction(new PieceMoveAction(gameState.GetPiece(this.Position.Add(0, 3)), this.Position.Add(0, 1)));
            }
            else if (move.Goal.Column - move.Start.Column == -2)
            {
                moveActions.AddPieceMoveAction(new PieceMoveAction(gameState.GetPiece(this.Position.Add(0, -4)), this.Position.Add(0, -1)));
            }

            return moveActions;
        }
    }
}
