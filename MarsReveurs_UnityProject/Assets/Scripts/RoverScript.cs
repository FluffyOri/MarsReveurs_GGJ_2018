﻿public sealed class RoverScript
{
    private RoverInstruction[] instructions;

    public RoverScript(int maxTicks)
    {
        this.instructions = new RoverInstruction[maxTicks];
    }
    
    public bool PushInstruction(System.Type type, int tick)
    {
        RoverInstruction instruction = System.Activator.CreateInstance(type) as RoverInstruction;

        if (instruction == null)
        {
            UnityEngine.Debug.LogError("Cannot create instance of " + type.Name);
            return false;
        }

        return this.PushInstruction(instruction, tick);
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