namespace ChessLibrary.Pieces
{
    public class MovePattern
    {
        public int[] DRows { get; }

        public int[] DCols { get; }

        public bool Repeatable { get; }

        public MovePattern(int[] dRows, int[] dCols, bool repeatable)
        {
            this.DRows = dRows;
            this.DCols = dCols;
            this.Repeatable = repeatable;
        }
    }
}
