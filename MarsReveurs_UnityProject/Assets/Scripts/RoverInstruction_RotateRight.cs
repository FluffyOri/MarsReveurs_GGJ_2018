public class RoverInstruction_RotateRight : RoverInstruction
{
    public override void Execute(IRoverInterface rover)
    {
        rover.Rotate(Direction.Right);
    }
}