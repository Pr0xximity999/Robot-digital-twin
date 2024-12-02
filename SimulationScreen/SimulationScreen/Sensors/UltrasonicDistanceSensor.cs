using System.Numerics;

namespace SimulationScreen.Sensors;

public class UltrasonicDistanceSensor : ISensor
{
    private Vector3 Rotation;
    private Robot _robot;
    public UltrasonicDistanceSensor(Vector3 startingRotation, Robot robot)
    {
        Rotation = startingRotation;
        _robot = robot;
    }
    
    public UltrasonicDistanceSensor(Robot robot) : this(new Vector3(), robot) { }

    /// <summary>
    /// Returns the position of the ended raycast, i was too tired to keep filling the draw method with a bunch of redundant calculations
    /// </summary>
    /// <returns>The position of the end of the sensor, either max range or an intersection</returns>
    public Vector2 CalculteLinePosition()
    {
        return Calculations.CalculateLineWithBounds(
            _robot.GetRotation() + Rotation,
            _robot.GetPosition(),
            SimValues.UltrasonicDistanceRange,
            SimValues.Boundaries.X,
            -SimValues.Boundaries.X,
            SimValues.Boundaries.Y,
            -SimValues.Boundaries.Y
        );
    }
    
    /// <summary>
    /// Measures the distance readout of the ultrasonic sensor
    /// </summary>
    /// <returns></returns>
    public double[] Measure()
    {
        Vector2 position = CalculteLinePosition();
        //Calculate the distance
        double distance = Math.Sqrt(Math.Pow(position.X - _robot.GetPosition().X, 2) + Math.Pow(position.Y - _robot.GetPosition().Y, 2));

        return [distance];
    }
}