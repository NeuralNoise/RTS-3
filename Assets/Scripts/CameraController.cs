using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public float sensity = 0.0005f;
    public Vector2 HorizontalBorder; //(minX, maxX)
    public Vector2 VerticalBorder; //(minY, maxY)

    void Start()
    {
        InputManager.Instance.DragAction += DragCamera;
    }

    void Destroy()
    {
        InputManager.Instance.DragAction -= DragCamera;
    }

    void DragCamera(Vector3 dragBegin, Vector3 dragEnd)
    {
        Vector3 delta = dragBegin - dragEnd;
        delta *= sensity;
        delta = new Vector3(delta.x, 0, delta.y);
        Move(delta);
    }

    void Move(Vector3 delta)
    {
        Vector3 pos = transform.position;

        float x = pos.x + delta.x;
        x = Mathf.Clamp(x, HorizontalBorder.x, HorizontalBorder.y);
        float z = pos.z + delta.z;
        z = Mathf.Clamp(z, VerticalBorder.x, VerticalBorder.y);
        transform.position = new Vector3(x, pos.y, z);
    }
}
