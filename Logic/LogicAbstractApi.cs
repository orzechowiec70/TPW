using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Data;
namespace Logic
{
    public abstract class LogicAbstractApi
    {
        public abstract void StartSimulation(int numberOfBalls);
        public abstract void StopSimulation();
        public abstract IEnumerable<IBall> GetBalls();
        public abstract event EventHandler PositionChanged;

        public static LogicAbstractApi CreateApi(IBoard board)
        {
            return new LogicAPI(board);
        }
    }
}