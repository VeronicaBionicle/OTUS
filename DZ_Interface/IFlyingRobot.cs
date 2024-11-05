namespace DZ_Interface
{
    interface IFlyingRobot : IRobot
    {
        new string GetRobotType()
        {
            return "I am a flying robot.";
        }
    }
}
