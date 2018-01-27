public class PlayerController : UnityEngine.MonoBehaviour, IRoverInterface
{
    [UnityEngine.SerializeField]
    private UnityEngine.Tilemaps.Tilemap tilemap;

    public void Nop()
    {
        UnityEngine.Debug.Log("Noping...");
    }

    public void Rotate(Direction direction)
    {
        UnityEngine.Debug.Log("Rotating to " + direction + "...");
    }

    void Start ()
    {
    }
	
	void Update ()
    {
#if UNITY_EDITOR
        if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.UpArrow))
        {
            this.transform.position += UnityEngine.Vector3.up;
        }
        else if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.DownArrow))
        {
            this.transform.position += UnityEngine.Vector3.down;
        }
        else if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.LeftArrow))
        {
            this.transform.position += UnityEngine.Vector3.left;
        }
        else if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.RightArrow))
        {
            this.transform.position += UnityEngine.Vector3.right;
        }
#endif
    }
}
