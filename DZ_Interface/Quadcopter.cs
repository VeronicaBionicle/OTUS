
namespace DZ_Interface
{
    public class Quadcopter : IFlyingRobot, IChargable
    {
        List<string> _components = new List<string> { "rotor1", "rotor2", "rotor3", "rotor4" };

        public List<string> GetComponents()
        {
            return _components;
        }

        string IChargable.GetInfo()
        {
            return "I am chargable.";
        }

        string IRobot.GetInfo()
        {
            return "Flying robot-quadrocopter";
        }

        public string GetInfo()
        {
            return "Serial number: 999999, Model XX48";
        }

        public string GetRobotType()
        {
            return "I am a quadcopter.";
        }

        public void Charge()
        {
            Console.WriteLine("Charging...");
            Thread.Sleep(3000);
            Console.WriteLine("Charged!");
        }
    }
}
