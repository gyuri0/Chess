using System.Linq;
using ChessLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessLibraryTest
{
    [TestClass]
    public class GameResultTests
    {
        [TestMethod]
        public void FastestStalemate()
        {
            GameState gameState = new GameState();
            gameState = gameState.Move(new Move(Field.GetField(1, 2), Field.GetField(3, 2)));
            gameState = gameState.Move(new Move(Field.GetField(6, 7), Field.GetField(4, 7)));
            gameState = gameState.Move(new Move(Field.GetField(1, 7), Field.GetField(3, 7)));
            gameState = gameState.Move(new Move(Field.GetField(6, 0), Field.GetField(4, 0)));
            gameState = gameState.Move(new Move(Field.GetField(0, 3), Field.GetField(3, 0)));
            gameState = gameState.Move(new Move(Field.GetField(7, 0), Field.GetField(5, 0)));
            gameState = gameState.Move(new Move(Field.GetField(3, 0), Field.GetField(4, 0)));
            gameState = gameState.Move(new Move(Field.GetField(5, 0), Field.GetField(5, 7)));
            gameState = gameState.Move(new Move(Field.GetField(4, 0), Field.GetField(6, 2)));
            gameState = gameState.Move(new Move(Field.GetField(6, 5), Field.GetField(5, 5)));
            gameState = gameState.Move(new Move(Field.GetField(6, 2), Field.GetField(6, 3)));
            gameState = gameState.Move(new Move(Field.GetField(7, 4), Field.GetField(6, 5)));
            gameState = gameState.Move(new Move(Field.GetField(6, 3), Field.GetField(6, 1)));
            gameState = gameState.Move(new Move(Field.GetField(7, 3), Field.GetField(2, 3)));
            gameState = gameState.Move(new Move(Field.GetField(6, 1), Field.GetField(7, 1)));
            gameState = gameState.Move(new Move(Field.GetField(2, 3), Field.GetField(6, 7)));
            gameState = gameState.Move(new Move(Field.GetField(7, 1), Field.GetField(7, 2)));
            gameState = gameState.Move(new Move(Field.GetField(6, 5), Field.GetField(5, 6)));
            gameState = gameState.Move(new Move(Field.GetField(7, 2), Field.GetField(5, 4)));

            Assert.AreEqual(GameResult.Draw, gameState.Result);
        }

        [TestMethod]
        public void FoolsMate()
        {
            GameState gameState = new GameState();
            gameState = gameState.Move(new Move(Field.GetField(1, 5), Field.GetField(2, 5)));
            gameState = gameState.Move(new Move(Field.GetField(6, 4), Field.GetField(4, 4)));
            gameState = gameState.Move(new Move(Field.GetField(1, 6), Field.GetField(3, 6)));
            gameState = gameState.Move(new Move(Field.GetField(7, 3), Field.GetField(3, 7)));

            Assert.AreEqual(GameResult.BlackWin, gameState.Result);
        }
    }
}
