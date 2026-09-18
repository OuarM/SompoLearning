using System;

namespace ChessGame
{
    enum PieceColor
    {
        White,
        Black
    }

    abstract class Piece
    {
        public PieceColor Color { get; set; }

        public Piece(PieceColor color)
        {
            Color = color;
        }

        public abstract string Symbol { get; }

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

        public override bool IsValidMove(
            int startRow,
            int startCol,
            int endRow,
            int endCol,
            Piece[,] board)
        {
            int direction =
                Color == PieceColor.White ? -1 : 1;

            if (startCol == endCol &&
                board[endRow, endCol] == null)
            {
                if (endRow == startRow + direction)
                    return true;
            }

            if (Math.Abs(endCol - startCol) == 1 &&
                endRow == startRow + direction)
            {
                Piece target = board[endRow, endCol];

                if (target != null &&
                    target.Color != Color)
                {
                    return true;
                }
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

        public override bool IsValidMove(
            int startRow,
            int startCol,
            int endRow,
            int endCol,
            Piece[,] board)
        {
            return startRow == endRow ||
                   startCol == endCol;
        }
    }

    class ChessBoard
    {
        public Piece[,] Board { get; private set; }

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

            Console.WriteLine("  A B C D E F G H");

            for (int row = 0; row < 8; row++)
            {
                Console.Write(8 - row + " ");

                for (int col = 0; col < 8; col++)
                {
                    if (Board[row, col] == null)
                    {
                        Console.Write(". ");
                    }
                    else
                    {
                        Console.Write(
                            Board[row, col].Symbol + " ");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }

    class ChessGame
    {
        private ChessBoard chessBoard;

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

                Console.WriteLine(
                    $"Current Turn: {currentTurn}");

                Console.WriteLine(
                    "Enter move (e2 e3) or exit:");

                string input =
                    Console.ReadLine();

                if (input.ToLower() == "exit")
                    break;

                string[] parts =
                    input.Split(' ');

                if (parts.Length != 2)
                {
                    Console.WriteLine(
                        "Invalid format.");
                    Console.ReadKey();
                    continue;
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
                    Console.WriteLine(
                        "Invalid coordinates.");
                    Console.ReadKey();
                    continue;
                }

                MovePiece(
                    startRow,
                    startCol,
                    endRow,
                    endCol);
            }
        }

        private bool TryParsePosition(
            string pos,
            out int row,
            out int col)
        {
            row = -1;
            col = -1;

            if (pos.Length != 2)
                return false;

            col =
                char.ToLower(pos[0]) - 'a';

            if (!int.TryParse(
                pos[1].ToString(),
                out int rank))
            {
                return false;
            }

            row = 8 - rank;

            return row >= 0 &&
                   row < 8 &&
                   col >= 0 &&
                   col < 8;
        }

        private void MovePiece(
            int startRow,
            int startCol,
            int endRow,
            int endCol)
        {
            Piece piece =
                chessBoard.Board[startRow, startCol];

            if (piece == null)
            {
                Console.WriteLine(
                    "No piece selected.");

                Console.ReadKey();
                return;
            }

            if (piece.Color != currentTurn)
            {
                Console.WriteLine(
                    "Not your turn.");

                Console.ReadKey();
                return;
            }

            Piece target =
                chessBoard.Board[endRow, endCol];

            if (target != null &&
                target.Color == piece.Color)
            {
                Console.WriteLine(
                    "Cannot capture own piece.");

                Console.ReadKey();
                return;
            }

            if (!piece.IsValidMove(
                startRow,
                startCol,
                endRow,
                endCol,
                chessBoard.Board))
            {
                Console.WriteLine(
                    "Illegal move.");

                Console.ReadKey();
                return;
            }

            chessBoard.Board[endRow, endCol]
                = piece;

            chessBoard.Board[startRow, startCol]
                = null;

            currentTurn =
                currentTurn == PieceColor.White
                ? PieceColor.Black
                : PieceColor.White;
        }
    }

    class Program
    {
        static void Main()
        {
            ChessGame game =
                new ChessGame();

            game.Run();
        }
    }
}
