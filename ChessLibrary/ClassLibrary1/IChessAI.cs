using ChessLibrary;

namespace ChessAI
{
    public interface IChessAI
    {
        Move GenerateMove(GameState gameState);
    }
}
