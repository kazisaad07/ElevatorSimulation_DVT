using NUnit.Framework;
using ElevatorSimulation_DVT;

namespace ElevatorSimulationTest
{
    [TestFixture]
    public class ElevatorSimulationTest
    {
        [Test]
        public void TestElevatorMoveToFloor()
        {
            Elevator elevator = new Elevator(10);
            elevator.MoveToFloor(3);

            Assert.IsTrue(elevator.IsMoving);
            Assert.AreEqual(Direction.Up, elevator.CurrentDirection);
            Assert.AreEqual(1, elevator.CurrentFloor);
            Assert.AreEqual(1, elevator.FloorsToStop.Count);
            Assert.Contains(3, elevator.FloorsToStop);
        }

        [Test]
        public void TestElevatorStopAtFloor()
        {
            Elevator elevator = new Elevator(10);
            elevator.MoveToFloor(3);

            elevator.StopAtFloor(3);

            Assert.IsFalse(elevator.IsMoving);
            Assert.AreEqual(3, elevator.CurrentFloor);
            Assert.AreEqual(0, elevator.FloorsToStop.Count);
        }

        [Test]
        public void TestElevatorAddPerson()
        {
            Elevator elevator = new Elevator(10);

            elevator.AddPerson();
            Assert.AreEqual(1, elevator.PeopleCount);
        }

        [Test]
        public void TestElevatorRemovePerson()
        {
            Elevator elevator = new Elevator(10);
            elevator.AddPerson();

            elevator.RemovePerson();

            Assert.AreEqual(0, elevator.PeopleCount);
        }
    }

    [TestFixture]
    public class ElevatorControllerTests
    {
        [Test]
        public void TestElevatorControllerCallElevator()
        {
            ElevatorController controller = new ElevatorController(2, 10);

            controller.CallElevator(3);
        }

        [Test]
        public void TestElevatorControllerSetPeopleWaitingOnFloor()
        {
            ElevatorController controller = new ElevatorController(2, 10);

            controller.SetPeopleWaitingOnFloor(3, 4);
        }
    }
}
