using ChessLibrary;
using System;
using System.Diagnostics;

namespace ChessAI
{
    public class MinimaxAI : IChessAI
    {
        private int depth;
        private int[] cutCounts;

        public MinimaxAI(int depth)
        {
            this.depth = depth;
            this.cutCounts = new int[depth + 1];
        }

        private void SetCutCountToZero()
        {
            for (int i = 0; i < this.cutCounts.Length; i++)
            {
                this.cutCounts[i] = 0;
            }
        }

        public Move GenerateMove(GameState gameState)
        {
            this.SetCutCountToZero();

            int beforeGameStateCount = GameState.GameStateCount;
            Stopwatch sw = new Stopwatch();
            sw.Start();

            int alpha = int.MinValue;
            int beta = int.MaxValue;

            Move bestMove = null;
            int bestValue = int.MinValue;

            foreach (var move in gameState.GetAllMoves())
            {
                GameState newGameState = gameState.MoveWithoutValidation(move);

                int minMaxValue = GetMinimaxValue(gameState.MoveWithoutValidation(move), gameState.NextToMove, this.depth - 1, alpha, beta, false);
                if (minMaxValue > bestValue)
                {
                    bestValue = minMaxValue;
                    bestMove = move;
                }

                alpha = Math.Max(alpha, bestValue);

                if (beta <= alpha)
                {
                    this.cutCounts[depth]++;
                    break;
                }
            }

            sw.Stop();
            int examinedNodeCount = GameState.GameStateCount - beforeGameStateCount;

            Debug.WriteLine(examinedNodeCount / (sw.ElapsedMilliseconds / 1000.0) + " n/s (" + examinedNodeCount + "node in " + sw.ElapsedMilliseconds + " ms)");
            Debug.WriteLine(string.Join("|", cutCounts));

            return bestMove;
        }

        private int GetMinimaxValue(GameState gameState, Color color, int depth, int alpha, int beta, bool isMaximizing)
        {
            if (depth == 0 || gameState.IsGameFinished)
            {
                return this.GetHeuristicValue(gameState, color);
            }

            if (isMaximizing)
            {
                int bestValue = int.MinValue;
                foreach (var move in gameState.GetAllMoves())
                {
                    bestValue = Math.Max(bestValue, GetMinimaxValue(gameState.MoveWithoutValidation(move), color, depth - 1, alpha, beta, false));
                    alpha = Math.Max(alpha, bestValue);

                    if (beta <= alpha)
                    {
                        this.cutCounts[depth]++;
                        break;
                    }
                }

                return bestValue;
            }
            else
            {

                int worstValue = int.MaxValue;
                foreach (var move in gameState.GetAllMoves())
                {
                    worstValue = Math.Min(worstValue, GetMinimaxValue(gameState.MoveWithoutValidation(move), color, depth - 1, alpha, beta, true));
                    beta = Math.Min(beta, worstValue);

                    if (beta <= alpha)
                    {
                        this.cutCounts[depth]++;
                        break;
                    }
                }

                return worstValue;
            }
        }

        private int GetHeuristicValue(GameState gameState, Color color)
        {
            if (gameState.IsPlayerWon(color))
            {
                return int.MaxValue - 1;
            }

            if (gameState.IsPlayerWon(color.Enemy()))
            {
                return int.MinValue + 1;
            }

            if (gameState.Result == GameResult.Draw)
            {
                return 0;
            }

            int value = 0;

            foreach (var piece in gameState.Pieces)
            {
                if (piece.Color == color)
                {
                    value += GetValueForPiece(piece);
                }
                else
                {
                    value -= GetValueForPiece(piece);
                }
            }

            value *= 1000;
            value += gameState.GetControlledFields(color).Count - gameState.GetControlledFields(color.Enemy()).Count;

            return value;
        }

        private int GetValueForPiece(Piece piece)
        {
            switch (piece.Type)
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
