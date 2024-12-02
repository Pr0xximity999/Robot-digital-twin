using Timer = System.Windows.Forms.Timer;

namespace SimulationScreen;

public partial class SimScreen : Form
{
    //Local variables
    private static readonly int TickDelay = 20;
    private static readonly int TickLimit = 5000;
    private static List<Robot>? _robots;
    private readonly Timer _simulationTimer;
    private int _tick = 0;
    
    public SimScreen()
    {
        InitializeComponent();
        //Setting the paint event and screen height to the simulation boundaries
        Paint += paint;
        this.Width = (int)(SimValues.Boundaries.X * 2.5f); 
        this.Height = (int)(SimValues.Boundaries.Y * 3f);
    
        
        //Creates multiple robots
        _robots = new()
        {
            new Robot(SimValues.StartRotation, SimValues.StartPosition),
            new Robot(SimValues.StartRotation, SimValues.StartPosition),
        };
        
        //Sets up the main loop timer (thread.sleep doesnt work well on winforms)
        _simulationTimer = new();
        _simulationTimer.Interval = TickDelay;
        _simulationTimer.Tick += OnTick!;
    }
    //Fires once the form loads
    private void Form1_Shown(object sender, EventArgs e)
    {
        //Starts the run once method and then the main loop
        Setup();
        _simulationTimer.Start();
    }
    
    //runs once
    private static void Setup()
    {
        foreach (var robot in _robots)
        {
            robot.SetSpeed(1);
            robot.Rotate(Random.Shared.Next(0, 360));
        }
    }
    
    //Runs every tick
    private void OnTick(object sender, EventArgs e)
    {
        //If the tick limit has been reached, stop the simulation
        if (_tick > TickLimit)
        {
            _simulationTimer.Stop();
            Console.WriteLine("End of simulation");
            return;
        }

        try
        { 
            Loop(); //Calls the main loop
        }
        catch (Exception exception) //If anything bad happens, just shut it down
        {
            _simulationTimer.Stop();
            MessageBox.Show(exception.Message, "Oopsies", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Console.WriteLine("End of simulation, exception occurred");
            return;
        }
        this.Invalidate(); //Refreshes the screen
        _tick++;
    }
    
    
    //Main loop
    private static void Loop()
    {   
        //Loops over every robot
        foreach (var robot in _robots)
        {
            //Reads the position data of the robot 
            var robot_pos = robot.GetPosition();
            
            //Reads every distance sensor data
            var robot_distanceF = robot.DistanceSensorFront.Measure()[0];
            var robot1_distanceR = robot.DistanceSensorRight.Measure()[0];
            var robot1_distanceL = robot.DistanceSensorLeft.Measure()[0];
            
            //The the distance sensor detects something witin 10 cm
            if (robot_distanceF < 10 || robot1_distanceR < 10 || robot1_distanceL < 10)
            {
                //Rotate the robot and also set the led to 200 lumen because school assignment
                robot.Rotate(Random.Shared.Next(70, 100));
                robot.Led1.SetValue([Random.Shared.Next(0, 6000)]);
            }
            robot.CalcPosition();  //Calculate the robot's next position
        }
    }
    
    private void paint(object sender, PaintEventArgs e)
    {
        var pen = new Pen(Color.Black);
        
        //Middle of the screen is 0, 0
        var zeroX = this.Width / 2;
        var zeroY = this.Height / 2;
        
        //Outer walls
        e.Graphics.DrawRectangle(pen, 
            new(zeroX - (int)SimValues.Boundaries.X, zeroY - (int)SimValues.Boundaries.Y, 
                (int)SimValues.Boundaries.X * 2, 
                (int)SimValues.Boundaries.Y * 2));
        
        //Robot drawings
        foreach (var robot in _robots)
        {
            //Calculates all the sensor lines
            var sensor_front = robot.DistanceSensorFront.CalculteLinePosition();            
            var sensor_right = robot.DistanceSensorRight.CalculteLinePosition();           
            var sensor_left = robot.DistanceSensorLeft.CalculteLinePosition();      
            
            //Sensor lines
            e.Graphics.DrawLine(pen,zeroX + robot.GetPosition().X, zeroY+ robot.GetPosition().Y, zeroX + sensor_front.X, zeroY+ sensor_front.Y);
            e.Graphics.DrawLine(pen,zeroX + robot.GetPosition().X, zeroY+ robot.GetPosition().Y, zeroX + sensor_right.X, zeroY+ sensor_right.Y);
            e.Graphics.DrawLine(pen,zeroX + robot.GetPosition().X, zeroY+ robot.GetPosition().Y, zeroX + sensor_left.X, zeroY+ sensor_left.Y);
            
            //Robot speck (little guy)
            e.Graphics.DrawRectangle(pen, new(zeroX + (int)robot.GetPosition().X, zeroY + (int)robot.GetPosition().Y, 1, 1));
        }
    }

}