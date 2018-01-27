public class RoverController : UnityEngine.MonoBehaviour
{
    public static int CurrentTick;

    [UnityEngine.SerializeField]
    private int tickRate = 1;

    [UnityEngine.SerializeField]
    private int maxTicks = 1024;

    private RoverScript script;

    private Meteor.Core.Utils.Ticker ticker;

    private void Awake()
    {
        this.script = new RoverScript(this.maxTicks);
        this.ticker = new Meteor.Core.Utils.Ticker(this.OnTick, this.tickRate);
    }

	void Start ()
    {
        RoverController.CurrentTick = 0;

        this.PushInstruction(typeof(RoverInstruction_RotateLeft));
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

        return this.script.PushInstructionOnFirstAvailalbleSlot(instruction, RoverController.CurrentTick);
    }

    public bool PushInstruction(System.Type type, int tick)
    {
        if (this.CanPushInstruction(tick))
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
