using System.Collections.Generic;

namespace ChessLibrary.Pieces
{
    public class SimpleMovePiece : Piece
    {
        private MovePattern MovePattern;

        public SimpleMovePiece(Field pos, PieceType type, Color color, MovePattern movePattern, int moveCount = 0)
            : base(pos, type, color, moveCount)
        {
            this.MovePattern = movePattern;
        }

        public override List<Move> GetAvailableMoveCandidates(GameState gameState)
        {
            List<Move> moves = new List<Move>();

            var controlledFields = this.GetControlledFields(gameState);

            foreach (var controlledField in controlledFields)
            {
                moves.Add(new Move(this.Position, controlledField));
            }

            return moves;
        }

        public override List<Field> GetControlledFields(GameState gameState)
        {
            List<Field> controlledFields = new List<Field>();
            for (int i = 0; i < this.MovePattern.DRows.Length; i++)
            {
                int dr = this.MovePattern.DRows[i];
                int dc = this.MovePattern.DCols[i];

                Field currField = this.Position;

                while (true)
                {
                    currField = currField.Add(dr, dc);

                    if (currField == Field.InvalidField || gameState.IsOccupiedByFriend(currField, this.Color))
                    {
                        break;
                    }
                    
                    controlledFields.Add(currField);

                    if (!this.MovePattern.Repeatable || gameState.IsOccupiedByEnemy(currField, this.Color))
                    {
                        break;
                    }
                }
            }

            return controlledFields;
        }

    }
}