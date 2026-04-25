using Data;
namespace DataTest
{
    [TestClass]
    public class DataApiTest
    {
        [TestMethod]
        public void CreateApiAndBoardTest()
        {
            int width = 800;
            int height = 400;

            DataAbstractApi api = DataAbstractApi.CreateApi(width, height);
            IBoard board = api.GetBoard();

            Assert.IsNotNull(api, "API nie powinno być nullem.");
            Assert.IsNotNull(board, "Board nie powinien być nullem.");
            Assert.AreEqual(width, board.width, "Szerokość planszy jest niepoprawna.");
            Assert.AreEqual(height, board.height, "Wysokość planszy jest niepoprawna.");
        }

        [TestMethod]
        public void CreateBallTest()
        {
          
            IBall ball = DataAbstractApi.CreateBall(10, 20, 1, 2, 15);

            Assert.AreEqual(10, ball.position.X);
            Assert.AreEqual(20, ball.position.Y);
            Assert.AreEqual(15, ball.radius);
        }
    }
}
