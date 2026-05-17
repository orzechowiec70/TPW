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

        public int NumberOfBalls
        {
            get => numberOfBalls;
            set
            {
                numberOfBalls = value;
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
            model.PositionChanged += OnPositionChanged;
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

        private void OnPositionChanged(object sender, EventArgs e)
        {
   
            syncContext.Post(_ =>
            {
                foreach (var ballModel in Balls)
                {
                    ballModel.Update();
                }
            }, null);
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