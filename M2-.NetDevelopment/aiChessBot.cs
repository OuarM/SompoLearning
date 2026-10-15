using System;
using System.Collections.Generic;
using System.Threading;

namespace ChessGame
{
    enum PieceColor
    {
        White,
        Black
    }

    abstract class Piece
    {
        public PieceColor Color { get; }

        public bool HasMoved { get; set; }

        protected Piece(PieceColor color)
        {
            Color = color;
        }

        public abstract string Symbol { get; }

        public abstract int Value { get; }

        public abstract bool IsValidMove(
            int startRow,
            int startCol,
            int endRow,
            int endCol,
            Piece[,] board);
    }

    class Pawn : Piece
    {
        public Pawn(PieceColor color) : base(color)
        {
        }

        public override string Symbol =>
            Color == PieceColor.White ? "P" : "p";

        public override int Value => 1;

        public override bool IsValidMove(
            int startRow,
            int startCol,
            int endRow,
            int endCol,
            Piece[,] board)
        {
            int direction =
                Color == PieceColor.White ? -1 : 1;

            int startingRow =
                Color == PieceColor.White ? 6 : 1;

            int rowDifference = endRow - startRow;
            int columnDifference = endCol - startCol;

            // Move forwards by one square.
            if (columnDifference == 0 &&
                rowDifference == direction &&
                board[endRow, endCol] == null)
            {
                return true;
            }

            // Move forwards by two squares from the starting position.
            if (columnDifference == 0 &&
                rowDifference == direction * 2 &&
                startRow == startingRow &&
                !HasMoved &&
                board[endRow, endCol] == null)
            {
                int middleRow = startRow + direction;

                return board[middleRow, startCol] == null;
            }

            // Capture diagonally.
            if (Math.Abs(columnDifference) == 1 &&
                rowDifference == direction)
            {
                Piece target = board[endRow, endCol];

                return target != null &&
                       target.Color != Color;
            }

            return false;
        }
    }

    class Rook : Piece
    {
        public Rook(PieceColor color) : base(color)
        {
        }

        public override string Symbol =>
            Color == PieceColor.White ? "R" : "r";

        public override int Value => 5;

        public override bool IsValidMove(
            int startRow,
            int startCol,
            int endRow,
            int endCol,
            Piece[,] board)
        {
            bool isStraightMove =
                startRow == endRow ||
                startCol == endCol;

            if (!isStraightMove)
            {
                return false;
            }

            int rowDirection =
                Math.Sign(endRow - startRow);

            int columnDirection =
                Math.Sign(endCol - startCol);

            int currentRow =
                startRow + rowDirection;

            int currentColumn =
                startCol + columnDirection;

            while (currentRow != endRow ||
                   currentColumn != endCol)
            {
                if (board[currentRow, currentColumn] != null)
                {
                    return false;
                }

                currentRow += rowDirection;
                currentColumn += columnDirection;
            }

            return true;
        }
    }

    class ChessMove
    {
        public int StartRow { get; }
        public int StartCol { get; }
        public int EndRow { get; }
        public int EndCol { get; }
        public int Score { get; }

        public ChessMove(
            int startRow,
            int startCol,
            int endRow,
            int endCol,
            int score)
        {
            StartRow = startRow;
            StartCol = startCol;
            EndRow = endRow;
            EndCol = endCol;
            Score = score;
        }
    }

    class ChessBoard
    {
        public Piece[,] Board { get; }

        public ChessBoard()
        {
            Board = new Piece[8, 8];
            SetupBoard();
        }

        private void SetupBoard()
        {
            for (int col = 0; col < 8; col++)
            {
                Board[1, col] =
                    new Pawn(PieceColor.Black);

                Board[6, col] =
                    new Pawn(PieceColor.White);
            }

            Board[0, 0] =
                new Rook(PieceColor.Black);

            Board[0, 7] =
                new Rook(PieceColor.Black);

            Board[7, 0] =
                new Rook(PieceColor.White);

            Board[7, 7] =
                new Rook(PieceColor.White);
        }

        public void Display()
        {
            Console.Clear();

            Console.WriteLine("Basic Chess Game");
            Console.WriteLine("You are White. The computer is Black.");
            Console.WriteLine();

            Console.WriteLine("    A B C D E F G H");
            Console.WriteLine("   -----------------");

            for (int row = 0; row < 8; row++)
            {
                Console.Write($"{8 - row} | ");

                for (int col = 0; col < 8; col++)
                {
                    Piece piece = Board[row, col];

                    Console.Write(
                        piece == null
                            ? ". "
                            : piece.Symbol + " ");
                }

                Console.WriteLine($"| {8 - row}");
            }

            Console.WriteLine("   -----------------");
            Console.WriteLine("    A B C D E F G H");
            Console.WriteLine();
        }
    }

    class ChessGame
    {
        private readonly ChessBoard chessBoard;

        private readonly Random random = new Random();

        private readonly PieceColor humanColor =
            PieceColor.White;

        private readonly PieceColor aiColor =
            PieceColor.Black;

        private PieceColor currentTurn =
            PieceColor.White;

        public ChessGame()
        {
            chessBoard = new ChessBoard();
        }

        public void Run()
        {
            while (true)
            {
                chessBoard.Display();

                if (!HasAnyLegalMove(currentTurn))
                {
                    Console.WriteLine(
                        $"{currentTurn} has no legal moves.");

                    Console.WriteLine("Game over.");
                    break;
                }

                if (currentTurn == humanColor)
                {
                    bool shouldExit = RunHumanTurn();

                    if (shouldExit)
                    {
                        break;
                    }
                }
                else
                {
                    RunAiTurn();
                }
            }
        }

        private bool RunHumanTurn()
        {
            Console.WriteLine("Your turn.");
            Console.WriteLine(
                "Enter a move such as e2 e4, or enter exit:");

            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                ShowMessage("Please enter a move.");
                return false;
            }

            input = input.Trim();

            if (input.Equals(
                    "exit",
                    StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            string[] parts = input.Split(
                ' ',
                StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)
            {
                ShowMessage(
                    "Invalid format. Use a move such as e2 e4.");

                return false;
            }

            if (!TryParsePosition(
                    parts[0],
                    out int startRow,
                    out int startCol) ||
                !TryParsePosition(
                    parts[1],
                    out int endRow,
                    out int endCol))
            {
                ShowMessage("Invalid coordinates.");
                return false;
            }

            bool moveSucceeded = TryMovePiece(
                startRow,
                startCol,
                endRow,
                endCol,
                humanColor,
                out string message);

            if (!moveSucceeded)
            {
                ShowMessage(message);
                return false;
            }

            currentTurn = aiColor;

            return false;
        }

        private void RunAiTurn()
        {
            Console.WriteLine("Computer is choosing a move...");

            Thread.Sleep(700);

            List<ChessMove> legalMoves =
                GenerateLegalMoves(aiColor);

            if (legalMoves.Count == 0)
            {
                ShowMessage(
                    "The computer has no legal moves.");

                return;
            }

            ChessMove selectedMove =
                SelectAiMove(legalMoves);

            Piece movingPiece =
                chessBoard.Board[
                    selectedMove.StartRow,
                    selectedMove.StartCol];

            Piece capturedPiece =
                chessBoard.Board[
                    selectedMove.EndRow,
                    selectedMove.EndCol];

            string startPosition = PositionToText(
                selectedMove.StartRow,
                selectedMove.StartCol);

            string endPosition = PositionToText(
                selectedMove.EndRow,
                selectedMove.EndCol);

            TryMovePiece(
                selectedMove.StartRow,
                selectedMove.StartCol,
                selectedMove.EndRow,
                selectedMove.EndCol,
                aiColor,
                out _);

            chessBoard.Display();

            if (capturedPiece != null)
            {
                Console.WriteLine(
                    $"Computer moved {movingPiece.Symbol} " +
                    $"from {startPosition} to {endPosition} " +
                    $"and captured {capturedPiece.Symbol}.");
            }
            else
            {
                Console.WriteLine(
                    $"Computer moved {movingPiece.Symbol} " +
                    $"from {startPosition} to {endPosition}.");
            }

            Console.WriteLine();
            Console.WriteLine(
                "Press any key to continue.");

            Console.ReadKey();

            currentTurn = humanColor;
        }

        private ChessMove SelectAiMove(
            List<ChessMove> legalMoves)
        {
            int highestScore = int.MinValue;

            foreach (ChessMove move in legalMoves)
            {
                if (move.Score > highestScore)
                {
                    highestScore = move.Score;
                }
            }

            List<ChessMove> bestMoves =
                new List<ChessMove>();

            foreach (ChessMove move in legalMoves)
            {
                if (move.Score == highestScore)
                {
                    bestMoves.Add(move);
                }
            }

            int selectedIndex =
                random.Next(bestMoves.Count);

            return bestMoves[selectedIndex];
        }

        private List<ChessMove> GenerateLegalMoves(
            PieceColor color)
        {
            List<ChessMove> moves =
                new List<ChessMove>();

            for (int startRow = 0;
                 startRow < 8;
                 startRow++)
            {
                for (int startCol = 0;
                     startCol < 8;
                     startCol++)
                {
                    Piece piece =
                        chessBoard.Board[
                            startRow,
                            startCol];

                    if (piece == null ||
                        piece.Color != color)
                    {
                        continue;
                    }

                    for (int endRow = 0;
                         endRow < 8;
                         endRow++)
                    {
                        for (int endCol = 0;
                             endCol < 8;
                             endCol++)
                        {
                            if (!IsLegalMove(
                                    startRow,
                                    startCol,
                                    endRow,
                                    endCol,
                                    color))
                            {
                                continue;
                            }

                            Piece target =
                                chessBoard.Board[
                                    endRow,
                                    endCol];

                            int score =
                                CalculateMoveScore(
                                    piece,
                                    target,
                                    endRow,
                                    endCol);

                            moves.Add(
                                new ChessMove(
                                    startRow,
                                    startCol,
                                    endRow,
                                    endCol,
                                    score));
                        }
                    }
                }
            }

            return moves;
        }

        private int CalculateMoveScore(
            Piece movingPiece,
            Piece target,
            int endRow,
            int endCol)
        {
            int score = 0;

            // Prefer captures.
            if (target != null)
            {
                score += target.Value * 100;

                // Slightly discourage exchanging a more valuable
                // piece for a less valuable one.
                score -= movingPiece.Value * 10;
            }

            // Prefer squares closer to the centre.
            bool isCentralSquare =
                endRow >= 2 &&
                endRow <= 5 &&
                endCol >= 2 &&
                endCol <= 5;

            if (isCentralSquare)
            {
                score += 5;
            }

            // Encourage black pawns to move forwards.
            if (movingPiece is Pawn)
            {
                score += endRow;
            }

            // Random variation for moves with similar value.
            score += random.Next(0, 3);

            return score;
        }

        private bool HasAnyLegalMove(
            PieceColor color)
        {
            return GenerateLegalMoves(color).Count > 0;
        }

        private bool TryMovePiece(
            int startRow,
            int startCol,
            int endRow,
            int endCol,
            PieceColor movingColor,
            out string message)
        {
            message = string.Empty;

            if (!IsLegalMove(
                    startRow,
                    startCol,
                    endRow,
                    endCol,
                    movingColor))
            {
                Piece selectedPiece =
                    chessBoard.Board[
                        startRow,
                        startCol];

                if (selectedPiece == null)
                {
                    message = "No piece selected.";
                }
                else if (selectedPiece.Color != movingColor)
                {
                    message = "You cannot move that piece.";
                }
                else
                {
                    message = "Illegal move.";
                }

                return false;
            }

            Piece piece =
                chessBoard.Board[
                    startRow,
                    startCol];

            chessBoard.Board[endRow, endCol] =
                piece;

            chessBoard.Board[startRow, startCol] =
                null;

            piece.HasMoved = true;

            message = "Move completed.";

            return true;
        }

        private bool IsLegalMove(
            int startRow,
            int startCol,
            int endRow,
            int endCol,
            PieceColor movingColor)
        {
            if (!IsInsideBoard(startRow, startCol) ||
                !IsInsideBoard(endRow, endCol))
            {
                return false;
            }

            if (startRow == endRow &&
                startCol == endCol)
            {
                return false;
            }

            Piece piece =
                chessBoard.Board[
                    startRow,
                    startCol];

            if (piece == null ||
                piece.Color != movingColor)
            {
                return false;
            }

            Piece target =
                chessBoard.Board[
                    endRow,
                    endCol];

            if (target != null &&
                target.Color == piece.Color)
            {
                return false;
            }

            return piece.IsValidMove(
                startRow,
                startCol,
                endRow,
                endCol,
                chessBoard.Board);
        }

        private static bool IsInsideBoard(
            int row,
            int col)
        {
            return row >= 0 &&
                   row < 8 &&
                   col >= 0 &&
                   col < 8;
        }

        private static bool TryParsePosition(
            string position,
            out int row,
            out int col)
        {
            row = -1;
            col = -1;

            if (position.Length != 2)
            {
                return false;
            }

            col =
                char.ToLowerInvariant(position[0]) - 'a';

            if (!int.TryParse(
                    position[1].ToString(),
                    out int rank))
            {
                return false;
            }

            row = 8 - rank;

            return IsInsideBoard(row, col);
        }

        private static string PositionToText(
            int row,
            int col)
        {
            char file = (char)('a' + col);
            int rank = 8 - row;

            return $"{file}{rank}";
        }

        private static void ShowMessage(
            string message)
        {
            Console.WriteLine();
            Console.WriteLine(message);
            Console.WriteLine();
            Console.WriteLine(
                "Press any key to continue.");

            Console.ReadKey();
        }
    }

    class Program
    {
        static void Main()
        {
            Console.Title =
                "Chess Game Against Computer";

            ChessGame game =
                new ChessGame();

            game.Run();

            Console.WriteLine();
            Console.WriteLine(
                "Thank you for playing.");
        }
    }
}