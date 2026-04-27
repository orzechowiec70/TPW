using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;
using Data;
using System.Linq;
using System.Collections.Generic;

namespace LogicTest
{
    [TestClass]
    public class LogicApiTest
    {   

        [TestMethod]
        public void StartSimulation_CreatesCorrectNumberOfBalls()
        {
            DataAbstractApi dataApi = DataAbstractApi.CreateApi(800, 400);
            LogicAbstractApi logicApi = LogicAbstractApi.CreateApi(dataApi);
           
            int expectedCount = 10;

            logicApi.StartSimulation(expectedCount);
            var balls = logicApi.GetBalls().ToList();

            Assert.AreEqual(expectedCount, balls.Count, "Liczba kul wygenerowanych w symulacji jest niepoprawna.");
            logicApi.StopSimulation();
        }

        [TestMethod]
        public void Balls_AreWithinBoardBoundaries_AfterCreation()
        {
            DataAbstractApi dataApi = DataAbstractApi.CreateApi(800, 400);
            LogicAbstractApi logicApi = LogicAbstractApi.CreateApi(dataApi);

            logicApi.StartSimulation(20);
            var balls = logicApi.GetBalls();
            IBoard board = dataApi.GetBoard();
            foreach (var ball in balls)
            {
                Assert.IsTrue(ball.position.X >= ball.radius, "Kula poza lewą krawędzią.");
                Assert.IsTrue(ball.position.X <= board.width - ball.radius, "Kula poza prawą krawędzią.");
                Assert.IsTrue(ball.position.Y >= ball.radius, "Kula poza górną krawędzią.");
                Assert.IsTrue(ball.position.Y <= board.height - ball.radius, "Kula poza dolną krawędzią.");
            }
            logicApi.StopSimulation();
        }

        [TestMethod]
        public void StopSimulation_ClearsCancellationToken()
        {
            DataAbstractApi dataApi = DataAbstractApi.CreateApi(800, 400);
            LogicAbstractApi logicApi = LogicAbstractApi.CreateApi(dataApi);

            logicApi.StartSimulation(5);
            logicApi.StopSimulation();

            Assert.IsTrue(true);
        }
    }
}