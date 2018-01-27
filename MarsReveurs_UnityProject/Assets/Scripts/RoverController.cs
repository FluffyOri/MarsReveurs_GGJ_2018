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
        RoverController.CurrentTick = 0;
        this.script = new RoverScript(this.maxTicks);
    }

	void Start ()
    {
        this.ticker = new Meteor.Core.Utils.Ticker(this.OnTick, 1*this.tickRate);
	}

	void Update ()
    {
        this.ticker.Update();
	}

    public bool PushInstruction(System.Type type)
    {
        return this.PushInstruction(type, RoverController.CurrentTick + 1);
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
