using ChessLibrary.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace ChessLibrary
{
    public class GameState
    {
        public static int GameStateCount = 0;

        private Piece[,] Board { get; set; }

        private List<Move> AvailableMoves { get; set; }

        private HashSet<Field>[] ControlledFields { get; set; } = new HashSet<Field>[2];

        public List<Piece> Pieces { get; private set; }

        public Color NextToMove { get; private set; }

        public Move LastMove { get; private set; }

        public GameResult Result
        {
            get
            {
                return this.GetResult();
            }
        }

        public bool IsGameFinished
        {
            get
            {
                GameResult result = this.GetResult();
                return result != GameResult.InProgress;
            }
        }

        public bool IsPlayerWon(Color color)
        {
            GameResult result = this.GetResult();
            if (result == GameResult.BlackWin && color == Color.Black)
            {
                return true;
            }

            if (result == GameResult.WhiteWin && color == Color.White)
            {
                return true;
            }

            return false;
        }

        private void GenerateBoard()
        {
            this.Board = new Piece[8, 8];

            foreach (var piece in this.Pieces)
            {
                this.Board[piece.Position.Row, piece.Position.Column] = piece;
            }
        }

        public GameState(List<Piece> pieces, Color nextToMove, Move lastMove)
        {
            this.Pieces = pieces;
            this.NextToMove = nextToMove;
            this.LastMove = lastMove;
            this.GenerateBoard();
            GameStateCount++;
        }

        public GameState()
        {
            List<Piece> pieces = new List<Piece>();

            var pieceTypes = new List<PieceType>()
            {
                PieceType.Rook,
                PieceType.Knight,
                PieceType.Bishop,
                PieceType.Queen,
                PieceType.King,
                PieceType.Bishop,
                PieceType.Knight,
                PieceType.Rook
            };

            for (int col = 0; col < 8; col++)
            {
                pieces.Add(PieceFactory.CreatePiece(Field.GetField(0, col), pieceTypes[col], Color.White));
                pieces.Add(PieceFactory.CreatePiece(Field.GetField(1, col), PieceType.Pawn, Color.White));
                pieces.Add(PieceFactory.CreatePiece(Field.GetField(7, col), pieceTypes[col], Color.Black));
                pieces.Add(PieceFactory.CreatePiece(Field.GetField(6, col), PieceType.Pawn, Color.Black));
            }

            this.Pieces = pieces;
            this.NextToMove = Color.White;
            this.LastMove = null;
            this.GenerateBoard();
        }

        public Piece GetPiece(Field field)
        {
            return this.Board[field.Row, field.Column];
        }

        public bool IsOccupied(Field field)
        {
            return this.Board[field.Row, field.Column] != null;
        }

        public bool IsOccupiedByFriend(Field field, Color color)
        {
            Piece piece = this.Board[field.Row, field.Column];
            if (piece == null || piece.Color != color)
            {
                return false;
            }

            return true;
        }

        public bool IsOccupiedByEnemy(Field field, Color color)
        {
            Piece piece = this.Board[field.Row, field.Column];
            if (piece == null || piece.Color == color)
            {
                return false;
            }

            return true;
        }

        public GameState Move(Move move)
        {
            Piece pieceToMove = this.GetPiece(move.Start);

            if (pieceToMove != null && pieceToMove.Color == this.NextToMove && pieceToMove.GetAvailableMoveCandidates(this).Contains(move))
            {
                var newGameState = this.MoveWithoutValidation(move);
                if (newGameState.IsValid())
                {
                    return newGameState;
                }
            }

            return null;
        }

        public GameState MoveWithoutValidation(Move move)
        {
            Piece pieceToMove = this.GetPiece(move.Start);
            List<Piece> newPieces = new List<Piece>(this.Pieces);
            MoveActions moveActions = pieceToMove.Move(this, move);

            foreach (var pieceRemoveAction in moveActions.PieceRemoveActions)
            {
                newPieces.Remove(pieceRemoveAction.Piece);
            }

            foreach (var pieceMoveAction in moveActions.PieceMoveActions)
            {
                Piece piece = pieceMoveAction.Piece;
                newPieces.Remove(piece);
                newPieces.Add(PieceFactory.CreatePiece(pieceMoveAction.Goal, piece.Type, piece.Color, piece.MoveCount + 1));
            }

            foreach (var piecePromotionAction in moveActions.PiecePromotionActions)
            {
                Piece piece = piecePromotionAction.Piece;
                newPieces.Remove(piece);
                newPieces.Add(PieceFactory.CreatePiece(piecePromotionAction.Goal, piecePromotionAction.PromotionType, piece.Color, piece.MoveCount + 1));
            }

            return new GameState(newPieces, this.NextToMove.Enemy(), move);
        }

        public Piece GetKing(Color color)
        {
            return this.Pieces.First(x => x.Type == PieceType.King && x.Color == color);
        }

        public HashSet<Field> GetControlledFields(Color color)
        {
            if (this.ControlledFields[(int)color] == null)
            {
                HashSet<Field> controlledFields = new HashSet<Field>();
                foreach (var piece in this.Pieces.Where(x => x.Color == color))
                {
                    foreach (var field in piece.GetControlledFields(this))
                    {
                        controlledFields.Add(field);
                    }
                }

                this.ControlledFields[(int)color] = controlledFields;
            }

            return this.ControlledFields[(int)color];
        }

        public bool IsKingUnderAttack(Color color)
        {
            Piece king = this.GetKing(color);
            return this.GetControlledFields(color.Enemy()).Contains(king.Position);
        }

        public List<Move> GetPseudoMoves()
        {
            if (this.AvailableMoves == null)
            {
                this.AvailableMoves = new List<Move>();
                foreach (var piece in this.Pieces.Where(x => x.Color == this.NextToMove))
                {
                    foreach (var move in piece.GetAvailableMoveCandidates(this))
                    {
                        this.AvailableMoves.Add(move);
                    }
                }
            }

            return this.AvailableMoves;
        }

        public List<Move> GetAllMoves()
        {
            List<Move> validMoves = new List<Move>();

            foreach (var move in this.GetPseudoMoves())
            {
                GameState gameState = this.MoveWithoutValidation(move);
                if (gameState.IsValid())
                {
                    validMoves.Add(move);
                }
            }

            return validMoves;
        }

        public bool IsValid()
        {
            return !this.IsKingUnderAttack(this.NextToMove.Enemy());
        }

        private GameResult GetResult()
        {
            if (this.GetAllMoves().Count > 0)
            {
                return GameResult.InProgress;
            }

            if (this.IsKingUnderAttack(this.NextToMove))
            {
                if (this.NextToMove == Color.White)
                {
                    return GameResult.BlackWin;
                }
                else
                {
                    return GameResult.WhiteWin;
                }
            }
            else
            {
                return GameResult.Draw;
            }
        }
    }
}
