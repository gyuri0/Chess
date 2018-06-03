using System;
using System.Collections.Generic;
using System.Text;

namespace ChessLibrary.Pieces
{
    public static class PieceFactory
    {
        public static Piece CreatePiece(Field pos, PieceType pieceType, Color color, int moveCount = 0)
        {
            switch(pieceType)
            {
                case PieceType.Pawn:
                    return new Pawn(pos, color, moveCount);
                case PieceType.Rook:
                    return new Rook(pos, color, moveCount);
                case PieceType.Knight:
                    return new Knight(pos, color, moveCount);
                case PieceType.Bishop:
                    return new Bishop(pos, color, moveCount);
                case PieceType.Queen:
                    return new Queen(pos, color, moveCount);
                case PieceType.King:
                    return new King(pos, color, moveCount);
                default:
                    return null;
            }
        }
    }
}
