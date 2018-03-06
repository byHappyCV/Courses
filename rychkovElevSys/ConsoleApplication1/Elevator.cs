using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Elevator
    {
        public string ElevatorNumber {get;set;}
        public int CurrentFloor = 1; 
        public int EndPoint { get; set; }
        public Status Status = Status.Staing;

        public Elevator() { }
        public Elevator(int currentFloor, Status status, int endPoint)
        {
            CurrentFloor = currentFloor;
            Status = status;
            EndPoint = endPoint;
        }
        public bool Suitable(Person person)
        {
            if (person.Direction == Status.Up && person.Direction == Status && person.Location >= CurrentFloor)
            {
                if (person.Location > EndPoint)
                    return false;
                return true;
            }
            if (person.Direction == Status.Up && Status == Status.Staing && 
                (person.Location <= CurrentFloor || person.Location >= CurrentFloor))
            {
                return true;
            }
            
            if (person.Direction == Status.Down && person.Direction == Status && person.Location <= CurrentFloor)
            {
                if (person.Location < EndPoint)
                    return false;
                return true;
            }
            if (person.Direction == Status.Down && Status == Status.Staing && 
                (person.Location >= CurrentFloor || person.Location <= CurrentFloor))
            {
                return true;
            }
            return false; 
        }

        public override string ToString()
        {
            return "Elevator number " + ElevatorNumber + " from " + CurrentFloor;
        }

    }
}
