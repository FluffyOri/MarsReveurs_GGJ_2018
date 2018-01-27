public sealed class RoverScript
{
    private IRoverInterface rover;
    private RoverInstruction[] instructions;

    public RoverScript(IRoverInterface rover, int maxTicks)
    {
        this.rover = rover;
        this.instructions = new RoverInstruction[maxTicks];
    }

    public System.Collections.Generic.IEnumerable<RoverInstruction> EnumerateInstructions(int fromTick, int toTick)
    {
        if (fromTick > toTick)
        {
            throw new System.ArgumentException();
        }

        for (int index = fromTick; index < toTick; ++index)
        {
            yield return this.instructions[index];
        }
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
            this.rover.Nop();
            return;
        }

        this.instructions[tick].Execute(this.rover);
    }
}