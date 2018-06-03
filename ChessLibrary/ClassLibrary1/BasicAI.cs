using ChessLibrary;
using System;

namespace ChessAI
{
    public class BasicAI : IChessAI
    {
        public Move GenerateMove(GameState gameState)
        {
            var moves = gameState.GetAllMoves();
            Tuple<Move, double> moveWithMaxHeuristicValue = null;
            foreach (var move in moves)
            {
                GameState newGameState = gameState.Move(move);
                double heuristicValue = GenerateHeuristicValueForGameState(newGameState, gameState.NextToMove);
                if (moveWithMaxHeuristicValue == null || moveWithMaxHeuristicValue.Item2 < heuristicValue)
                {
                    moveWithMaxHeuristicValue = new Tuple<Move, double>(move, heuristicValue);
                }
            }


            return moveWithMaxHeuristicValue.Item1;
        }

        private double GenerateHeuristicValueForGameState(GameState newGameState, Color color)
        {
            var gameResult = newGameState.Result;

            if(gameResult == GameResult.Draw)
            {
                return 0;
            }

            if(gameResult == GameResult.BlackWin && color == Color.Black || gameResult == GameResult.WhiteWin && color == Color.White)
            {
                return 1;
            }
            
            if(gameResult == GameResult.BlackWin && color == Color.White || gameResult == GameResult.WhiteWin && color == Color.Black)
            {
                return -1;
            }

            double myValue = 0;
            double enemyValue = 0;

            foreach(var piece in newGameState.Pieces)
            {
                if(piece.Color == color)
                {
                    myValue += GetValueForPiece(piece);
                }
                else
                {
                    enemyValue += GetValueForPiece(piece);
                }
            }

            return (myValue - enemyValue) / Math.Max(myValue, enemyValue);
        }

        private double GetValueForPiece(Piece piece)
        {
            switch(piece.Type)
            {
                case PieceType.Bishop:
                    return 3;
                case PieceType.Knight:
                    return 3;
                case PieceType.Pawn:
                    return 1;
                case PieceType.Queen:
                    return 9;
                case PieceType.Rook:
                    return 5;
                default:
                    return 0;
            }
        }
    }
}
