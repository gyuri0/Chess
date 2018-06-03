using ChessLibrary;
using System;
using System.Linq;

namespace ChessAI
{
    public class RandomAI : IChessAI
    {
        Random random = new Random();

        public Move GenerateMove(GameState gameState)
        {
            var moves = gameState.GetAllMoves().ToList();

            return moves[random.Next(0, moves.Count - 1)];
        }
    }
}
