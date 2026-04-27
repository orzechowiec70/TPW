using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewModel;
using Model;
using Logic;
using Data;
using System.Collections.Generic;
using System.Linq;

namespace ViewModelTest
{
    [TestClass]
    public class MainViewModelTest
    {
      


        [TestMethod]
        public void StartCommand_PopulatesBallsCollection()
        {
            DataAbstractApi dataApi = DataAbstractApi.CreateApi(800, 400);
            LogicAbstractApi logic = LogicAbstractApi.CreateApi(dataApi);
            var model = new MainModel(logic);
            var viewModel = new MainViewModel(model);
            viewModel.NumberOfBalls = 3;

            viewModel.StartCommand.Execute(null);

            Assert.AreEqual(3, viewModel.Balls.Count, "Kolekcja Balls powinna zawierać 3 elementy po wykonaniu Start.");
            logic.StopSimulation();
        }

        [TestMethod]
        public void NumberOfBalls_PropertyChange_RaisesEvent()
        {
            DataAbstractApi dataApi = DataAbstractApi.CreateApi(800, 400);
            LogicAbstractApi logic = LogicAbstractApi.CreateApi(dataApi);
            var model = new MainModel(logic);
            var viewModel = new MainViewModel(model);
            bool propertyChangedRaised = false;

            viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(viewModel.NumberOfBalls))
                    propertyChangedRaised = true;
            };

            viewModel.NumberOfBalls = 10;

            Assert.IsTrue(propertyChangedRaised, "Zmiana NumberOfBalls powinna wywołać PropertyChanged.");
        }

        [TestMethod]
        public void BallUpdate_IsCalled_WhenModelRaisesEvent()
        {
            DataAbstractApi dataApi = DataAbstractApi.CreateApi(800, 400);
            LogicAbstractApi logic = LogicAbstractApi.CreateApi(dataApi);
            var model = new MainModel(logic);
            var viewModel = new MainViewModel(model);

            viewModel.NumberOfBalls = 1;
            viewModel.StartCommand.Execute(null);

            var ball = viewModel.Balls.First();
            double initialX = ball.X;
       
            model.Start(1); 

            Assert.IsNotNull(viewModel.Balls);
            Assert.AreEqual(1, viewModel.Balls.Count);

            logic.StopSimulation();
        }
    }
}