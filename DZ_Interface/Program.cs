namespace DZ_Interface
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var dj = new Quadcopter();
            Console.WriteLine(dj.GetRobotType());
            Console.WriteLine(dj.GetInfo());
            Console.WriteLine(string.Join(", ", dj.GetComponents()));
            dj.Charge();

            Console.WriteLine(((IFlyingRobot)dj).GetInfo());
            Console.WriteLine(((IRobot)dj).GetInfo());
            Console.WriteLine(((IChargable)dj).GetInfo());
        }
    }
}
