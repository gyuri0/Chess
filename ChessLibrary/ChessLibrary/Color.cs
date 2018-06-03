namespace ChessLibrary
{
    public enum Color
    {
        White = 0,
        Black = 1
    }

    public static class ColorExtensions
    {
        public static Color Enemy(this Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }

            return Color.White;
        }
    }
}
