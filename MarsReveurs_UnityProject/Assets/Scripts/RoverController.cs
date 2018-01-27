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

    public bool PushInstruction(System.Type type, int tick)
    {
        if (this.CanPushInstruction(tick))
        {
            return false;
        }

        return this.script.PushInstruction(type, tick);
    }

    public bool PushInstruction(RoverInstruction instruction, int tick)
    {
        if (this.CanPushInstruction(tick))
        {
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
