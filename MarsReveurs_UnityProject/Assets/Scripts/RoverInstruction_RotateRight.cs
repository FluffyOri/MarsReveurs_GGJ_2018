public class RoverInstruction_RotateRight : RoverInstruction
{
    public override void Execute()
    {
        UnityEngine.Debug.Log("Rotate Right " + RoverController.CurrentTick);
    }
}