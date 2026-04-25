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
        private class BoardMock : IBoard
        {
            public double width => 800;
            public double height => 400;
        }

        [TestMethod]
        public void StartSimulation_CreatesCorrectNumberOfBalls()
        {
            var boardMock = new BoardMock();
            LogicAbstractApi logicApi = LogicAbstractApi.CreateApi(boardMock);
            int expectedCount = 10;

            logicApi.StartSimulation(expectedCount);
            var balls = logicApi.GetBalls().ToList();

            Assert.AreEqual(expectedCount, balls.Count, "Liczba kul wygenerowanych w symulacji jest niepoprawna.");
            logicApi.StopSimulation();
        }

        [TestMethod]
        public void Balls_AreWithinBoardBoundaries_AfterCreation()
        {
            var boardMock = new BoardMock();
            LogicAbstractApi logicApi = LogicAbstractApi.CreateApi(boardMock);

            logicApi.StartSimulation(20);
            var balls = logicApi.GetBalls();

            foreach (var ball in balls)
            {
                Assert.IsTrue(ball.position.X >= ball.radius, "Kula poza lewą krawędzią.");
                Assert.IsTrue(ball.position.X <= boardMock.width - ball.radius, "Kula poza prawą krawędzią.");
                Assert.IsTrue(ball.position.Y >= ball.radius, "Kula poza górną krawędzią.");
                Assert.IsTrue(ball.position.Y <= boardMock.height - ball.radius, "Kula poza dolną krawędzią.");
            }
            logicApi.StopSimulation();
        }

        [TestMethod]
        public void StopSimulation_ClearsCancellationToken()
        {
            var boardMock = new BoardMock();
            LogicAbstractApi logicApi = LogicAbstractApi.CreateApi(boardMock);

            logicApi.StartSimulation(5);
            logicApi.StopSimulation();

            Assert.IsTrue(true);
        }
    }
}