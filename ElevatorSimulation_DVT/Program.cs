using System;
using System.Collections.Generic;

namespace ElevatorSimulation_DVT
{
    // Direction of elevator
    public enum Direction
    {
        Up,
        Down
    }

    public class Elevator
    {
        public int CurrentFloor { get; private set; }
        public bool IsMoving { get; private set; }
        public Direction CurrentDirection { get; private set; }
        public int PeopleCount { get; private set; }
        public int MaxWeight { get; private set; }
        public List<int> FloorsToStop { get; private set; }

        public Elevator(int maxWeight)
        {
            CurrentFloor = 1;
            IsMoving = false;
            CurrentDirection = Direction.Up;
            PeopleCount = 0;
            MaxWeight = maxWeight;
            FloorsToStop = new List<int>();
        }

        public void MoveToFloor(int floor)
        {
            IsMoving = true;
            FloorsToStop.Add(floor);

            if (CurrentFloor < floor)
                CurrentDirection = Direction.Up;
            else
                CurrentDirection = Direction.Down;
        }

        public void StopAtFloor(int floor)
        {
            IsMoving = false;
            CurrentFloor = floor;
            FloorsToStop.Remove(floor);
        }

        public void AddPerson()
        {
            if (PeopleCount < MaxWeight)
                PeopleCount++;
            else
                Console.WriteLine("Elevator is at maximum weight capacity.");
        }

        public void RemovePerson()
        {
            if (PeopleCount > 0)
                PeopleCount--;
        }
    }

    public class ElevatorController
    {
        private List<Elevator> elevators;

        public ElevatorController(int numberOfElevators, int maxWeight)
        {
            elevators = new List<Elevator>();
            for (int i = 0; i < numberOfElevators; i++)
            {
                elevators.Add(new Elevator(maxWeight));
            }
        }

        public void CallElevator(int floor)
        {
            Elevator nearestElevator = GetNearestAvailableElevator(floor);

            if (nearestElevator != null)
            {
                nearestElevator.MoveToFloor(floor);
                Console.WriteLine($"Elevator {elevators.IndexOf(nearestElevator) + 1} is moving to floor {floor}.");
            }
            else
            {
                Console.WriteLine("No available elevators at the moment.");
            }
        }

        public void SetPeopleWaitingOnFloor(int floor, int peopleCount)
        {
            // Assuming floors are 1-indexed
            if (floor > 0 && floor <= elevators.Count)
            {
                elevators[floor - 1].AddPerson();
                Console.WriteLine($"Waiting at floor {floor}: {peopleCount} people.");
            }
            else
            {
                Console.WriteLine("Invalid floor number.");
            }
        }

        private Elevator GetNearestAvailableElevator(int floor)
        {
            Elevator nearestElevator = null;
            int shortestDistance = int.MaxValue;

            foreach (var elevator in elevators)
            {
                if (!elevator.IsMoving)
                {
                    int distance = Math.Abs(elevator.CurrentFloor - floor);
                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        nearestElevator = elevator;
                    }
                }
            }

            return nearestElevator;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Max weight = 10
            ElevatorController controller = new ElevatorController(5, 10);

            // Call elevator to the 5th floor
            controller.CallElevator(5);

            // Set 4 people waiting on the 3rd floor
            controller.SetPeopleWaitingOnFloor(3, 4);

            // Call an elevator to the 3rd floor
            controller.CallElevator(3);

            // Set 5 people waiting on the 1st floor
            controller.SetPeopleWaitingOnFloor(1, 5);

            // Call an elevator to the 1st floor
            controller.CallElevator(1);

            Console.ReadLine();
        }
    }
}
