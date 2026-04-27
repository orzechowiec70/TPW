using System.ComponentModel;
using System.Runtime.CompilerServices;
using Data;
namespace Model
{
    public class BallModel : INotifyPropertyChanged
    {
        private readonly IBall logicBall;

        public double X => logicBall.position.X;
        public double Y => logicBall.position.Y;
        public double Diameter => logicBall.radius*2;

        public event PropertyChangedEventHandler? PropertyChanged;

        public BallModel(IBall LogicBall)
        {
            logicBall = LogicBall;
        }

        public void Update()
        {
            OnPropertyChanged(nameof(X));
            OnPropertyChanged(nameof(Y));
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

