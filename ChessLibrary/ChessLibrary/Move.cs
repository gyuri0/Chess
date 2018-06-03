using System.Collections.Generic;

namespace ChessLibrary
{
    public class Move
    {
        public Field Start { get; private set; }

        public Field Goal { get; private set; }

        public PieceType? Promotion { get; private set; }

        public Move(Field start, Field goal, PieceType? promotion = null)
        {
            this.Start = start;
            this.Goal = goal;
            this.Promotion = promotion;
        }

        public override int GetHashCode()
        {
            var hashCode = 577958911;
            hashCode = hashCode * -1521134295 + EqualityComparer<Field>.Default.GetHashCode(Start);
            hashCode = hashCode * -1521134295 + EqualityComparer<Field>.Default.GetHashCode(Goal);
            hashCode = hashCode * -1521134295 + EqualityComparer<PieceType?>.Default.GetHashCode(Promotion);
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            var move = obj as Move;
            return move != null &&
                   EqualityComparer<Field>.Default.Equals(Start, move.Start) &&
                   EqualityComparer<Field>.Default.Equals(Goal, move.Goal) &&
                   EqualityComparer<PieceType?>.Default.Equals(Promotion, move.Promotion);
        }
    }
}
