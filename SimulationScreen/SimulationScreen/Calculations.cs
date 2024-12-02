using System.Numerics;

namespace SimulationScreen;

public static class Calculations
{
    
    //Very proud of this
    
    /// <summary>
    /// Calculates the end point of a straight line given the angle, max length, position, and boundaries(square)
    /// </summary>
    /// <param name="rotation">The rotation of the starting point</param>
    /// <param name="position">The starting position of the line</param>
    /// <param name="maxRange">The maximum range of the point</param>
    /// <param name="xBoundsMax">The boundary of the line on the right</param>
    /// <param name="xBoundsMin">The boundary of the line on the left</param>
    /// <param name="yBoundsMax">The boundary of the line on the top</param>
    /// <param name="yBoundsMin">The boundary of the line on the bottom</param>
    /// <returns>The end position of the line</returns>
    public static Vector2 CalculateLineWithBounds(Vector3 rotation, Vector2 position, double maxRange, double xBoundsMax, double xBoundsMin, double yBoundsMax, double yBoundsMin)
    {
         //Starting values
        var rot = rotation;
        var pos = position;
        var xMax = xBoundsMax;
        var xMin = xBoundsMin;
        var yMax = yBoundsMax;
        var yMin = yBoundsMin;
        
        //Calculate the angle of the line
        var dx = Math.Round(float.Cos(float.DegreesToRadians(rot.Y)), 4);
        var dy = Math.Round(float.Sin(float.DegreesToRadians(rot.Y)), 4);
        
        //Check if the rotation of the line faces
        if(rot.Y is > 90 and < 270)
        {
            dx *= -1;
            dy *= -1;
        }
        
        //Calculate the end of the line using the angle and max distance
        var xEnd = pos.X + maxRange * dx;
        var yEnd = pos.Y + maxRange * dy;
        
        //horizontal detection
        if (dx != 0)
        {
            //Right wall detection
            if (dx > 0 && xEnd > xMax)
            {
                xEnd = xMax;
                yEnd = pos.Y + (xMax - pos.X) * dy / dx;
            }
            
            //Left wall detection
            else if (dx < 0 && xEnd < xMin)
            {
                xEnd = xMin;
                yEnd = pos.Y + (xMin - pos.X) * dy / dx;
            }
        }        
        
        //Vertical detection
        if (dy != 0)
        {
            //Top wall detection
            if (dy > 0 && yEnd > yMax)
            {
                yEnd = yMax;
                xEnd = pos.X + (yMax - pos.Y) * dx / dy;
            }
            
            //Bottom wall detection
            else if (dy < 0 && yEnd < yMin)
            {
                yEnd = yMin;
                xEnd = pos.X + (yMin - pos.Y) * dx / dy;
            }
        }

        Vector2 endPosition = new((float)xEnd, (float)yEnd);

        return endPosition;
    }
}