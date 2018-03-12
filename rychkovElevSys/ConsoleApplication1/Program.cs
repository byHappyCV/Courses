using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Building building = new Building();

            Elevator elev1 = new Elevator(12, Status.Down, 1);
            Elevator elev2 = new Elevator(5, Status.Up, 6);
            Elevator elev3 = new Elevator(1, Status.Up, 10);
            Elevator elev4 = new Elevator(8, Status.Down, 1);
            Elevator elev5 = new Elevator(8, Status.Down, 2);

            //act

            building.AddElevator(elev1);
            building.AddElevator(elev2);
            building.AddElevator(elev3);
            building.AddElevator(elev4);
            building.AddElevator(elev5);
            var actual = building.Call(new Person { Location = 8, Direction = Status.Up });
            Console.WriteLine(actual);

            Console.ReadKey();
        }
    }
}
