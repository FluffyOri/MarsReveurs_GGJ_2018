public class RoverInstruction_RotateLeft : RoverInstruction
{
    public override void Execute()
    {
        UnityEngine.Debug.Log("Rotate Left " + RoverController.CurrentTick);
    }
}