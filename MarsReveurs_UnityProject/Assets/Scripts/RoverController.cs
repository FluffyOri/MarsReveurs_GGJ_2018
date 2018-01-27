public class RoverController : Singleton<RoverController>
{
    public static int CurrentTick;

    [UnityEngine.SerializeField]
    private double tickRate = 1d;

    [UnityEngine.SerializeField]
    private int maxTicks = 1024;

    private RoverScript script;
    private IRoverInterface rover;
    private Meteor.Core.Utils.Ticker ticker;

    private void Awake()
    {
        this.rover = UnityEngine.GameObject.FindObjectOfType<PlayerController>();
        if (this.rover == null)
        {
            throw new System.NullReferenceException("IRoverInterface");
        }

        this.script = new RoverScript(this.rover, this.maxTicks);
        this.ticker = new Meteor.Core.Utils.Ticker(this.OnTick, this.tickRate);
    }

	void Start ()
    {
        RoverController.CurrentTick = 0;

        this.PushInstruction(typeof(RoverInstruction_Nop));
        this.PushInstruction(typeof(RoverInstruction_RotateRight));
        this.PushInstruction(typeof(RoverInstruction_Nop));
        this.PushInstruction(typeof(RoverInstruction_RotateRight));
    }

	void Update ()
    {
        this.ticker.Update();
	}

    public System.Collections.Generic.IEnumerable<RoverInstruction> EnumerateInstructions(int fromTick, int toTick)
    {
        return this.script.EnumerateInstructions(fromTick, toTick);
    }

    public bool PushInstruction(System.Type type)
    {
        RoverInstruction instruction = System.Activator.CreateInstance(type) as RoverInstruction;

        if (instruction == null)
        {
            UnityEngine.Debug.LogError("Cannot create instance of " + type.Name);
            return false;
        }

        return this.script.PushInstructionAfterLast(instruction, RoverController.CurrentTick, 16);
    }

    public bool PushInstruction(System.Type type, int tick)
    {     
        if (!this.CanPushInstruction(tick))
        {
            return false;
        }

        RoverInstruction instruction = System.Activator.CreateInstance(type) as RoverInstruction;

        if (instruction == null)
        {
            UnityEngine.Debug.LogError("Cannot create instance of " + type.Name);
            return false;
        }

        return this.script.PushInstruction(instruction, tick);
    }

    private bool CanPushInstruction(int tick)
    {
        if (tick <= RoverController.CurrentTick)
        {
            return false;
        }

        return true;
    }

    private void OnTick(double deltaTime)
    {
        this.script.Tick(RoverController.CurrentTick++);
    }
}
