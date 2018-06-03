using ChessLibrary;
using System.Drawing;

namespace ChessUI
{
    public static class PieceImageGetter
    {
        public static Bitmap GetImageForPiece(Piece piece)
        {
            if(piece.Color == ChessLibrary.Color.Black)
            {
                switch(piece.Type)
                {
                    case PieceType.Pawn:
                        return PieceImages.BlackPawn;
                    case PieceType.Bishop:
                        return PieceImages.BlackBishop;
                    case PieceType.King:
                        return PieceImages.BlackKing;
                    case PieceType.Knight:
                        return PieceImages.BlackKnight;
                    case PieceType.Queen:
                        return PieceImages.BlackQueen;
                    case PieceType.Rook:
                        return PieceImages.BlackRook;
                    default:
                        return null;
                }
            }

            switch (piece.Type)
            {
                case PieceType.Pawn:
                    return PieceImages.WhitePawn;
                case PieceType.Bishop:
                    return PieceImages.WhiteBishop;
                case PieceType.King:
                    return PieceImages.WhiteKing;
                case PieceType.Knight:
                    return PieceImages.WhiteKnight;
                case PieceType.Queen:
                    return PieceImages.WhiteQueen;
                case PieceType.Rook:
                    return PieceImages.WhiteRook;
                default:
                    return null;
            }
        }
    }
}
