using System.Numerics;
using SimulationScreen.Actuators;
using SimulationScreen.Sensors;

namespace SimulationScreen;

public class Robot
{
    private double _speed;
    private Vector2 _position;
    private Vector3 _rotation;

    public Gyroscope Gyro;
    public UltrasonicDistanceSensor DistanceSensorFront;
    public UltrasonicDistanceSensor DistanceSensorRight;
    public UltrasonicDistanceSensor DistanceSensorLeft;
    public Led Led1;

    public Robot(Vector2 position, Vector3 rotation)
    {
        _speed = 0;
        _position = position;
        _rotation = rotation;
        DistanceSensorFront = new(new(0, 0, 0), this);
        DistanceSensorRight = new(new(0, 45, 0), this);
        DistanceSensorLeft = new(new(0, 315, 0), this);
        Gyro = new(this);
        Led1 = new();
    }

    public Vector3 GetRotation() => _rotation;
    public Vector2 GetPosition() => _position;

    /// <summary>
    /// Sets how much the robot rotates each tick
    /// </summary>
    /// <param name="angle">How much the robot moves each tick</param>
    public void Rotate(int angle)
    {
        _rotation.Y += angle % 360;
    }

    /// <summary>
    /// Sets the speed at which the robot moves each tick
    /// </summary>
    /// <param name="speed">The speed at which the robot moves each tick</param>
    public void SetSpeed(double speed)
    {
        _speed = speed;
    }

    /// <summary>
    /// Calculates the next position of the robot
    /// </summary>
    public void CalcPosition()
    {
        _position = Calculations.CalculateLineWithBounds(
            _rotation,
            _position,
            _speed,
            SimValues.Boundaries.X,
            -SimValues.Boundaries.X,
            SimValues.Boundaries.Y,
            -SimValues.Boundaries.Y
        );
    }
}