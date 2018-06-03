using System;

namespace ChessLibrary
{
    public struct Field : IEquatable<Field>
    {
        private static Field[,] AllFields;

        public static Field InvalidField;

        public int Row { get; private set; }
        public int Column { get; private set; }

        static Field()
        {
            Field.AllFields = new Field[8, 8];

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    AllFields[i, j] = new Field(i, j);
                }
            }

            Field.InvalidField = new Field(-1, -1);
        }

        public static Field GetField(int row, int column)
        {
            return AllFields[row, column];
        }

        private Field(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        public Field Add(int row, int column)
        {
            int newRow = this.Row + row;
            int newCol = this.Column + column;

            if (newRow < 0 || newRow > 7 || newCol < 0 || newCol > 7)
            {
                return Field.InvalidField;
            }

            return AllFields[newRow, newCol];
        }

        public static bool operator ==(Field x, Field y)
        {
            return x.Row == y.Row && x.Column == y.Column;
        }

        public static bool operator !=(Field x, Field y)
        {
            return !(x == y);
        }

        public override int GetHashCode()
        {
            return this.Row * 8 + this.Column;
        }

        public bool Equals(Field other)
        {
            return this == other;
        }
    }
}
