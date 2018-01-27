public sealed class RoverScript
{
    private RoverInstruction[] instructions;

    public RoverScript(int maxTicks)
    {
        this.instructions = new RoverInstruction[maxTicks];
    }

    public bool PushInstruction(RoverInstruction instruction, int tick)
    {
        if (tick >= this.instructions.Length)
        {
            UnityEngine.Debug.LogError("Rover script memory violation ^_^.");
            return false;
        }

        if (this.instructions[tick] != null)
        {
            return false;
        }

        this.instructions[tick] = instruction;

        return true;
    }

    public bool PushInstructionOnFirstAvailalbleSlot(RoverInstruction instruction, int tick)
    {
        while(this.instructions[tick] != null)
        {
            if (tick >= this.instructions.Length)
            {
                UnityEngine.Debug.LogError("Rover script memory violation ^_^.");
                return false;
            }

            tick++;
        }

        this.instructions[tick] = instruction;

        return true;
    }

    public void Tick(int tick)
    {
        if (tick >= this.instructions.Length)
        {
            UnityEngine.Debug.LogError("Rover script memory violation ^_^.");
            return;
        }

        if (this.instructions[tick] == null)
        {
            return;
        }

        this.instructions[tick].Execute();
    }
}