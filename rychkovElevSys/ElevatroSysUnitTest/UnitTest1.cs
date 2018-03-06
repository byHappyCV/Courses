using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication1;

namespace ElevatroSysUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]

        public void Call_4elevators_2returned()
        {
            //arrange
            Elevator elev1 = new Elevator(1, Status.Up, 6) ;
            Elevator elev2 = new Elevator(8, Status.Staing, 8);
            Elevator elev3 = new Elevator(9,Status.Down, 6);
            Elevator elev4 = new Elevator(9,Status.Up,10);
            Elevator expected = elev2;

            //act
            Building building = new Building();

            building.AddElevator(elev1);
            building.AddElevator(elev2);
            building.AddElevator(elev3);
            building.AddElevator(elev4);
            var actual = building.Call(new Person { Location = 7, Direction = Status.Up });

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Call_4elevators_3returned()
        {
            //arrange
            Elevator elev1 = new Elevator(2, Status.Up, 5);
            Elevator elev3 = new Elevator(5, Status.Staing, 5);
            Elevator elev2 = new Elevator(10, Status.Down, 6);
            Elevator elev4 = new Elevator(4, Status.Up, 10);
            Elevator expected = elev3;

            //act
            Building building = new Building();

            building.AddElevator(elev1);
            building.AddElevator(elev2);
            building.AddElevator(elev3);
            building.AddElevator(elev4);
            var actual = building.Call(new Person { Location = 6, Direction = Status.Up });

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Call_4elevators_4returned()
        {
            //arrange
            Elevator elev1 = new Elevator(12, Status.Down, 1);
            Elevator elev2 = new Elevator(5, Status.Up, 6);
            Elevator elev3 = new Elevator(1, Status.Up, 10);
            Elevator elev4 = new Elevator(8, Status.Down, 1);
            Elevator expected = elev3;

            //act
            Building building = new Building();

            building.AddElevator(elev1);
            building.AddElevator(elev2);
            building.AddElevator(elev3);
            building.AddElevator(elev4);
            var actual = building.Call(new Person { Location = 7, Direction = Status.Up });

            Assert.AreEqual(expected, actual);
        }
        
    }
}
