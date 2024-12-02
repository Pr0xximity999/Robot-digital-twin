using System.Numerics;

namespace SimulationScreen;

public static class SimValues
{
    public static Vector3 StartPosition = new(0, 0, 0);
    public static Vector2 StartRotation = new(0, 0);
    
    public static List<Vector3> RobotPositions = new();
    public static List<Vector3> RobotRotations = new();

    public static readonly Vector2 Boundaries = new(100, 100); //cm
    
    public static readonly float UltrasonicDistanceRange = 40; //cm



}