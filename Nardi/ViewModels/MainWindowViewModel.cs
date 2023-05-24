using Nardi.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Nardi.ViewModels.Base;
using System.Collections.ObjectModel;
using Nardi.Model;

namespace Nardi.ViewModels
{
    using System.ComponentModel;

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private GameModel _gameModel;

        public GameModel GameModel
        {
            get { return _gameModel; }
            set
            {
                if (_gameModel != value)
                {
                    _gameModel = value;
                    OnPropertyChanged(nameof(GameModel));
                }
            }
        }

        public MainWindowViewModel()
        {
            GameModel = new GameModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
