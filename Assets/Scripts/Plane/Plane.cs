using UnityEngine;
using System.Collections;

public class Plane : MonoBehaviour {
    public float targetHeight = 6; //升空的目标高度
    public float maxRotateSpeed = 500;
    public float rotateAcceration = 200;
    public float flyUpSpeed = 3;
    public float moveSpeed = 10;

    private Transform mTop;

    void Awake()
    {
        mTop = transform.Find("Top");
    }
    
    /// <summary>
    /// 转动螺旋桨
    /// </summary>
    public void RotatePropeller(float rotateSpeed)
    {
        mTop.localEulerAngles += new Vector3(0, Time.deltaTime * rotateSpeed, 0);
    }
}
