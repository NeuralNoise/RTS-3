using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public float sensity = 0.025f;
    public Vector2 HorizontalBorder; //(minX, maxX)
    public Vector2 VerticalBorder; //(minY, maxY)

    void Start()
    {
        InputManager.Instance.DragAction += DragCamera;
        MapManager.Instance.AddCamera(transform);
    }

    void Destroy()
    {
        InputManager.Instance.DragAction -= DragCamera;
        MapManager.Instance.Remove(transform);
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

    public void SetCameraPos(Vector3 pos)
    {
        Vector3 originPos = transform.position;
        float x = Mathf.Clamp(pos.x, HorizontalBorder.x, HorizontalBorder.y);
        float z = Mathf.Clamp(pos.z, VerticalBorder.x, VerticalBorder.y);
        transform.position = new Vector3(x, originPos.y, z);
    }
}
