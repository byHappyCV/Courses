using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Building
    {
        private int _topFloor = 12;
        private int _botFloor = 1;
        private List<Elevator> _elevators = new List<Elevator>(); 

        public void AddElevator(Elevator elevator)
        {
            string name = ""; 
            if (_elevators.Count == 0)
            {
                name = "1";
            }
            else
            {
            name = "" + (int.Parse(_elevators.Last().ElevatorNumber) + 1);
            }
            elevator.ElevatorNumber = name;
            _elevators.Add(elevator);
        }
        public void AddElevator(Elevator elevator, int floor)
        {
            if (floor > _topFloor || floor < _botFloor)
            {
                throw new Exception("incorrect floor value");
            }
            elevator.ElevatorNumber = "" + (int.Parse(_elevators.Last().ElevatorNumber) + 1);
            elevator.CurrentFloor = floor;
            _elevators.Add(elevator);
        }
        public void MoveElevator(Elevator elevator, Status direction, int endPoint)
        {
            if (endPoint > _topFloor || endPoint < _botFloor)
            {
                throw new Exception("incorrect floor value");
            }
            var elev = _elevators.Find(c => c == elevator);
            elev.EndPoint = endPoint;
            elev.Status = direction;
        }

        public Elevator Call(Person person)
        {
            if (person.Location > _topFloor)
            {
                throw new Exception("in our building only 12 floors");
            }
            List<Elevator> suitableElevators = new List<Elevator>();
            foreach (var elevator in _elevators)
            {
                if (elevator.Suitable(person))
                {
                    suitableElevators.Add(elevator);
                }
            }
            return ElevatorCheck(person, suitableElevators);
        }
        public Elevator ElevatorCheck(Person person, List<Elevator> suitableElevators)
        {
            Elevator SuitableElevator = new Elevator();
            int suitable = 0;
            if (person.Direction == Status.Down)
            {
                suitable = _topFloor;
            }
            else if(person.Direction == Status.Up)
            {
                suitable = _botFloor;
            }
            foreach (var elevator in suitableElevators)
            {
                int res = 0;
                if (person.Location > elevator.CurrentFloor)
                {
                    res = person.Location - elevator.CurrentFloor;
                }
                else
                {
                    res = elevator.CurrentFloor - person.Location;
                }
                if(suitableElevators.Count == 1)
                {
                    return elevator;
                }
                if (res <= suitable)
                {
                    suitable = res;
                    SuitableElevator = elevator;
                }
            }
            return SuitableElevator;
        }
    }
}
