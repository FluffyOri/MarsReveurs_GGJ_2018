public class RoverInstruction_RotateLeft : RoverInstruction
{
    public override void Execute(IRoverInterface rover)
    {
        rover.Rotate(Direction.Left);
    }
}