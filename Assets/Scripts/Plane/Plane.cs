using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnitData))]
public class Plane : Unit {
    public float targetHeight = 6; //升空的目标高度
    public float maxRotateSpeed = 500;
    public float rotateAcceration = 200;
    public float flyVerticalSpeed = 3;
    public float moveSpeed = 10;

    private Transform mTop;

    new void Awake()
    {
        base.Awake();
        mTop = transform.Find("Top");
    }

    new void Start()
    {
        base.Start();
        mData.unitType = UnitType.FlyObject;
    }
    
    /// <summary>
    /// 转动螺旋桨
    /// </summary>
    public void RotatePropeller(float rotateSpeed)
    {
        mTop.localEulerAngles += new Vector3(0, Time.deltaTime * rotateSpeed, 0);
    }
}
