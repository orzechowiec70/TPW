using Data;
using Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Input;

namespace ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly MainModel model;
        private int numberOfBalls = 5;
        private readonly SynchronizationContext syncContext;
        public ObservableCollection<BallModel> Balls { get; } = new ObservableCollection<BallModel>();

        private string errorMessage = string.Empty;

        public string ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public int NumberOfBalls
        {
            get => numberOfBalls;
            set
            {
                if (value > 200)
                {
                    numberOfBalls = 200;
                    ErrorMessage = "Możesz wpisać maksymalnie 200 kul.";
                }
                else if (value < 0)
                {
                    numberOfBalls = 0;
                    ErrorMessage = "Liczba kul nie może być ujemna.";
                }
                else
                {
                    numberOfBalls = value;
                    ErrorMessage = string.Empty; 
                }
                OnPropertyChanged(nameof(NumberOfBalls));
            }
        }

        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }

        public MainViewModel(MainModel inModel)
        {
            model = inModel;

            syncContext = SynchronizationContext.Current ?? new SynchronizationContext();

            StartCommand = new Command(Start);
            StopCommand = new Command(Stop);
        }

        private void Start()
        {
            model.Start(NumberOfBalls);

            Balls.Clear();
            foreach (var ball in model.GetBalls())
            {
                Balls.Add(new BallModel(ball));
            }
        }

        private void Stop()
        {
            model.Stop();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}