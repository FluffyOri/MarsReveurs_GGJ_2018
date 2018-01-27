public sealed class RoverScript
{
    private RoverInstruction[] instructions;

    public RoverScript(int maxTicks)
    {
        this.instructions = new RoverInstruction[maxTicks];
    }

    public void Tick(int tick)
    {
        // UnityEngine.Debug.Log("Script.Tick " + tick);
    }
}