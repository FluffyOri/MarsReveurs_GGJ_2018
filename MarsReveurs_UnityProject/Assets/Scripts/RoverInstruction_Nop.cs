public class RoverInstruction_Nop : RoverInstruction
{
    public override void Execute(IRoverInterface rover)
    {
        rover.Nop();
    }
}