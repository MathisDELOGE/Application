﻿using System;
using WinEchek.Model;
using WinEchek.Model.Piece;
using Type = WinEchek.Model.Piece.Type;

namespace WinEchek.Engine.Command
{
    /// <summary>
    /// Represents a Move to execute on the model
    /// </summary>
    [Serializable]
    public class MoveCommand : ICompensableCommand
    {
        private Move _move;

        private Piece _piece;
        private Piece _removedPiece;

        private bool _hasChangedState;
        private Square _targetSquare;
        private Square _startSquare;

        private Board _board;


        /// <summary>
        /// MoveCommand constructor
        /// </summary>
        /// <param name="move">The move to do</param>
        /// <param name="board">The board the command executes on</param>
        public MoveCommand(Move move, Board board)
        {
            _move = move;
            _board = board;
        }

        private MoveCommand(MoveCommand command, Board board)
        {
            _board = board;
            _move = command._move; 
        }

        /// <summary>
        /// Execute the move on the Board
        /// </summary>
        public void Execute()
        {
            _targetSquare = _board.SquareAt(_move.TargetCoordinate);
            _startSquare = _board.SquareAt(_move.StartCoordinate);
            _piece = _startSquare.Piece;

            //Has moved update
            if (!_piece.HasMoved)
            {
                _piece.HasMoved = true;
                _hasChangedState = true;
            }

            //Square is empty of piece
            if (_targetSquare.Piece == null)
            {
                _startSquare.Piece = null;
                _piece.Square = _targetSquare;
                _targetSquare.Piece = _piece;
            }
            //There is a taken piece
            else
            {
                _removedPiece = _targetSquare.Piece;
                _targetSquare.Piece = null;
                _piece.Square.Piece = null;
                _piece.Square = _targetSquare;
                _targetSquare.Piece = _piece;
            }
        }

        /// <summary>
        /// Undo the move
        /// </summary>
        public void Compensate()
        {
            if (_hasChangedState) _piece.HasMoved = false;

            _targetSquare.Piece = _removedPiece;
            _startSquare.Piece = _piece;
            _piece.Square = _startSquare;
        }

        public Type PieceType => _move.PieceType;

        public Color PieceColor => _move.PieceColor;

        public ICompensableCommand Copy(Board board) => new MoveCommand(this, board);

        public override string ToString() => _piece + " de " + _move.StartCoordinate + " vers " + _move.TargetCoordinate;
    }
}