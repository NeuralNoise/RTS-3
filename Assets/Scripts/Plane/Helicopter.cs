using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class Helicopter : MonoBehaviour {
    public float maxHeight = 6;
    public float maxRotateSpeed = 500;
    public float rotateAcceration = 200;
    public float flyUpSpeed = 3;
    public float moveSpeed = 10;

    public bool testOn = true;

    private bool mMoveSetDone = false;
    private bool mIsFlying = true;
    private float mCurRotateSpeed = 0;
    private Transform mTop;
    private Vector3 mTargetPos = new Vector3(0, -100, 0);
    private NavMeshAgent mAgent;
    private bool mIsFirstFrame;

    void Awake()
    {
        mTop = transform.Find("Top");
        mAgent = this.GetComponent<NavMeshAgent>();
        mAgent.enabled = false;
        mAgent.Warp(transform.position);
    }
    
	void Update () {
        if(mIsFirstFrame)
        {
            mIsFirstFrame = false;
            Init();
        }

        if(testOn)
        {
            testOn = false;
            mTargetPos = transform.position + new Vector3(10, 0, 10);
        }
        
        RotatePropeller();
        FlyVertical();
        FlyHorizontal();
	}

    public void MoveTo(Vector3 pos)
    {
        mMoveSetDone = false;
        mIsFlying = true;
        mTargetPos = pos;
        mAgent.Resume();
    }

    public void Stop()
    {
        mIsFlying = false;
    }

    void Init()
    {
        mAgent.speed = moveSpeed;
        mAgent.angularSpeed = 1000;
        mAgent.acceleration = 1000;
    }

    /// <summary>
    /// 转动螺旋桨
    /// </summary>
    void RotatePropeller()
    {
        if (mIsFlying)
            mCurRotateSpeed += Time.deltaTime * rotateAcceration;
        else
            mCurRotateSpeed -= Time.deltaTime * rotateAcceration;

        mCurRotateSpeed = Mathf.Clamp(mCurRotateSpeed, 0, maxRotateSpeed);
        mTop.localEulerAngles += new Vector3(0, Time.deltaTime * mCurRotateSpeed, 0);
    }

    /// <summary>
    /// 垂直升降
    /// </summary>
    void FlyVertical()
    {
        bool isRotateSpeedEnough = Mathf.Approximately(mCurRotateSpeed, maxRotateSpeed);
        Vector3 oldPos = transform.localPosition;
        float height = oldPos.y;
        if (isRotateSpeedEnough)
        {
            height += Time.deltaTime * flyUpSpeed;
        }
        else
        {
            height -= Time.deltaTime * flyUpSpeed;
        }
        height = Mathf.Clamp(height, 0, maxHeight);
        transform.localPosition = new Vector3(oldPos.x, height, oldPos.z);
    }

    /// <summary>
    /// 水平方向的飞
    /// </summary>
    void FlyHorizontal()
    {
        if (mMoveSetDone)
            return;

        if(mIsFlying && IsEnoughHigh() && mTargetPos.y > -10)
        {
            LegalizeTargetPos();
            mAgent.enabled = true;
            mAgent.SetDestination(mTargetPos);
            mMoveSetDone = true;
        }
    }

    bool IsEnoughHigh()
    {
        return Mathf.Approximately(transform.localPosition.y, maxHeight);
    }
    
    void LegalizeTargetPos()
    {
        if (mTargetPos == null)
            return;

        Vector3 oldPos = mTargetPos; 
        mTargetPos = new Vector3(oldPos.x, maxHeight, oldPos.z);
    }
}
