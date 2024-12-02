
namespace SimulationScreen.Actuators;

public class Buzzer : IActuator
{
    private int _pitch;
    private int _volume;
    
    /// <summary>
    /// Sets the pitch and volume of the buzzer
    /// </summary>
    /// <param name="values">The first index is the pitch in hertz, the second is the volume in decibells</param>
    public void SetValue(int[] values)
    {
        _pitch = values[0];
        _volume = values[1];

        Console.WriteLine($@"The buzzer is playing a sound at a pitch of {_pitch}hz and a volume of {_volume} db");
    }
}