public class PlayerController : UnityEngine.MonoBehaviour, IRoverInterface
{
    public void Rotate(Direction direction)
    {
        UnityEngine.Debug.Log("Rotating to " + direction + "...");
    }

    void Start ()
    {
	}
	
	void Update ()
    {
	}
}
