namespace SimulationScreen.Actuators;

public class Led : IActuator
{
    private int _brightness;
    
    /// <summary>
    /// Sets the brightness of the led
    /// </summary>
    /// <param name="values">The first index is the brightness in lumen</param>
    public void SetValue(int[] values)
    {
        _brightness = values[0];
        Console.WriteLine($"The LED is shining at a brightness of {_brightness} lumen");
    }
}