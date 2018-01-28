﻿using UnityEngine;

public class PlayerController : UnityEngine.MonoBehaviour, IRoverInterface
{
    public Transform CameraTransform;

    [UnityEngine.SerializeField]
    private UnityEngine.Tilemaps.Tilemap tilemap;

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
    }

    public void MoveForward()
    {
        this.transform.position += this.transform.up;
    }

    void Start ()
    {
    }
	
	void Update ()
    {
        this.CameraTransform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.CameraTransform.position.z);

#if UNITY_EDITOR
        if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.LeftArrow))
        {
            this.transform.Rotate(new UnityEngine.Vector3(0, 0, 1), 90f);
        }
        else if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.RightArrow))
        {
            this.transform.Rotate(new UnityEngine.Vector3(0, 0, 1), -90f);
        }
        else if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.UpArrow))
        {
            this.transform.position += this.transform.up;
        }
#endif
    }
}
