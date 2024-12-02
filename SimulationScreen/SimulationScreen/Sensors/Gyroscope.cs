using System.Numerics;

namespace SimulationScreen.Sensors;

public class Gyroscope : ISensor
{
    private Vector3 _rotation;
    private Robot _robot;

    public Gyroscope(Robot robot)
    {
        _robot = robot;
    }

    public double[] Measure()
    {
        _rotation = _robot.GetRotation();
        return RotationToArray();
    }

    private double[] RotationToArray() => [_rotation.X, _rotation.Y, _rotation.Z];
}