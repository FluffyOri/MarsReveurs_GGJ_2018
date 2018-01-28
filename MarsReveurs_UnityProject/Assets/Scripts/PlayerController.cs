using UnityEngine;

public class PlayerController : UnityEngine.MonoBehaviour, IRoverInterface
{
	public Transform Camera;

    [UnityEngine.SerializeField]
    private UnityEngine.Tilemaps.Tilemap tilemap;

    public float Energy
    {
        get;
        private set;
    }

    public void Awake()
    {
        this.Energy = 1f;
    }

    public void Nop()
    {
        this.MoveForward();
    }

    public void Rotate(Direction direction)
    {
        switch (direction)
        {
            case Direction.Left:
                this.transform.Rotate(new UnityEngine.Vector3(0, 0, 1), 90f);
                break;

            case Direction.Right:
                this.transform.Rotate(new UnityEngine.Vector3(0, 0, 1), -90f);
                break;

            case Direction.Back:
            case Direction.Front:
                throw new System.InvalidOperationException(direction.ToString());

            default:
                throw new System.ArgumentOutOfRangeException(direction.ToString());
        }

        SoundPlayer.Instance.PostEvent(SoundEvent.Rotate);
    }

    public void MoveForward()
    {
        this.transform.position += this.transform.up;
    }

    public void SlapTheCuteRover(float energyLoss)
    {
        if (energyLoss < 0f)
        {
            UnityEngine.Debug.LogWarning("The damage value is supposed to be positive you idiot!");
            return;
        }

        this.Energy = UnityEngine.Mathf.Clamp01(this.Energy - energyLoss);

        if (UnityEngine.Mathf.Abs(this.Energy) < float.Epsilon)
        {
            this.Die(DeathReason.ZeroEnergy);
        }
    }

    public void Die(DeathReason reason = DeathReason.YouDeserveIt)
    {
        this.Energy = 0f;

        UnityEngine.Debug.Log("You died because " + reason);
    }

    void Update ()
    {
		this.Camera.position = new Vector3 (this.transform.position.x, this.transform.position.y, this.Camera.position.z);

#if UNITY_EDITOR
        if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.LeftArrow))
        {
            this.Rotate(Direction.Left);
        }
        else if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.RightArrow))
        {
            this.Rotate(Direction.Right);
        }
        else if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.UpArrow))
        {
            this.MoveForward();
        }

        //UnityEngine.Debug.Log(this.transform.position);
#endif
    }

    private void OnGUI()
    {
        UnityEngine.GUI.Label(new UnityEngine.Rect(10f, 10f, 100f, 25f), this.Energy.ToString("0.00"));
    }

    void OnTriggerEnter2D(UnityEngine.Collider2D coll)
    {
        this.SlapTheCuteRover(.1f);
    }
}
