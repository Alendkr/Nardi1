using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Nardi.Model
{
    public class GameModel : INotifyPropertyChanged
    {
        private List<int> _cube;
        private ObservableCollection<List<int>> _board;

        public event PropertyChangedEventHandler PropertyChanged;

        public List<int> Cube
        {
            get { return _cube; }
            set
            {
                _cube = value;
                OnPropertyChanged("Cube");
            }
        }

        public ObservableCollection<List<int>> Board
        {
            get { return _board; }
            set
            {
                _board = value;
                OnPropertyChanged("Board");
            }
        }

        public GameModel()
        {
            InitializeBoard();
            RollDice();
        }

        private void InitializeBoard()
        {
            _board = new ObservableCollection<List<int>>();
            for (int i = 0; i < 24; i++)
            {
                _board.Add(new List<int>());
            }
            _board[0].Add(0);
            _board[23].Add(0);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RollDice()
        {
            Random random = new Random();
            _cube = new List<int> { random.Next(1, 7), random.Next(1, 7) };
            OnPropertyChanged("Cube");
        }

        public void MakeMove(int startPoint, int endPoint)
        {
            if (startPoint < 0 || startPoint > 23 || endPoint < 0 || endPoint > 23)
                throw new ArgumentException("Неверные координаты точек");

            if (_board[startPoint].Count == 0 || _board[startPoint][0] > 0)
                throw new ArgumentException("Недопустимый ход");

            if (endPoint < startPoint && _board[endPoint].Count > 0 && _board[endPoint][0] != _board[startPoint][0])
                throw new ArgumentException("Недопустимый ход");

            if (startPoint == endPoint)
                throw new ArgumentException("Недопустимый ход");

            int moveDistance = Math.Abs(endPoint - startPoint);
            if (!_cube.Contains(moveDistance))
                throw new ArgumentException("Недопустимый ход");

            if (endPoint < 18 && _board[endPoint].Count > 0 && _board[endPoint][0] == 1)
                throw new ArgumentException("Недопустимый ход");

            _board[startPoint].RemoveAt(0);
            _board[endPoint].Add(1);

            if (_board[endPoint].Count > 2)
                _board[endPoint].Add(0);

            OnPropertyChanged("Board");
        }

        public bool IsGameOver()
        {
            return _board[0].Count == 15 || _board[23].Count == 15;
        }


        public bool IsValidMoveAvailable()
        {
            for (int startPoint = 0; startPoint < 24; startPoint++)
            {
                if (_board[startPoint].Count > 0 && _board[startPoint][0] == 1)
                {
                    foreach (int diceValue in _cube)
                    {
                        int endPoint = startPoint + diceValue;
                        if (endPoint < 24 && IsValidMove(startPoint, endPoint))
                            return true;
                    }
                }
            }
            return false;
        }

        public bool IsValidMove(int startPoint, int endPoint)
        {
            if (startPoint < 0 || startPoint > 23 || endPoint < 0 || endPoint > 23)
                return false;

            if (_board[startPoint].Count == 0 || _board[startPoint][0] > 0)
                return false;

            if (endPoint < startPoint && _board[endPoint].Count > 0 && _board[endPoint][0] != _board[startPoint][0])
                return false;

            if (startPoint == endPoint)
                return false;

            int moveDistance = Math.Abs(endPoint - startPoint);
            if (!_cube.Contains(moveDistance))
                return false;

            if (endPoint < 18 && _board[endPoint].Count > 0 && _board[endPoint][0] == 1)
                return false;

            return true;
        }

        public List<int> GetPossibleMoves(int startPoint)
        {
            List<int> moves = new List<int>();
            foreach (int diceValue in _cube)
            {
                int endPoint = startPoint + diceValue;
                if (endPoint < 24 && IsValidMove(startPoint, endPoint))
                    moves.Add(endPoint);
            }
            return moves;
        }
    
    }
}
