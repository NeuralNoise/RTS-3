using UnityEngine;
using System.Collections;
using System;

//TODO

/// <summary>
/// Move around a parabola.
/// Just like a rocket gun.
/// </summary>
public class ParaboraAttackMode : BaseAttackMode {
    public float maxHeight;

    private Vector3 mStartPos;
    private Vector3 mPointOfFall; //it lock target point other than target, it cant follow target
    private Vector3 mInitSpeed = Vector3.zero;
    private Vector3 mVerticalSpeed = Vector3.zero;
    private float mTimeScale = 1.0f;
    private float mUpSpeedScale = 1.0f;
    private float mDownSpeedScale = 1.0f;
    private Vector3 mCurPos;
    
    protected override void CheckToDestroy()
    {
        base.CheckToDestroy();

        if (transform.position.y <= 0) //if under ground
            Destroy(gameObject);
    }

    public override void Spawn(Vector3 pos, Transform target)
    {
        base.Spawn(pos, target);
        mCurPos = mStartPos = pos;
        mPointOfFall = target.position;
        SetMaxHeight(maxHeight);
    }

    protected override void MoveToTarget()
    {
        float deltaTime = Time.deltaTime * mTimeScale;

        if (mInitSpeed.y + mVerticalSpeed.y > 0)
            deltaTime *= mUpSpeedScale;
        else
            deltaTime *= mDownSpeedScale;

        mTimer += deltaTime;
        mVerticalSpeed.y = GlobalDefines.G * mTimer;
        mCurPos += mInitSpeed * deltaTime;
        mCurPos += mVerticalSpeed * deltaTime;

        UpdateObject();
    }

    private void UpdateObject()
    {
        transform.position = mCurPos;
    }

    public void SetMaxHeight(float maxHeight)
    {
        float distance = (mPointOfFall - mStartPos).magnitude;
        float totalTime = Mathf.Sqrt(Mathf.Abs(2.0f * this.maxHeight / GlobalDefines.G)); //h = 0.5 * g * t^2

        mInitSpeed = new Vector3(
            (mPointOfFall.x - mStartPos.x) / totalTime, //Vx = Sx / t
            (mPointOfFall.y - mStartPos.y) / totalTime - 0.5f * GlobalDefines.G * totalTime, //Vy = Sy / t - 0.5 * g * t
            (mPointOfFall.z - mStartPos.z) / totalTime
        );
        Debug.Log(maxHeight + " - " + distance + " - " + totalTime + " - " + mInitSpeed + " - " + mStartPos + " - " + mPointOfFall);
        //SetSpeed(speed);
    }

    public void SetSpeedScale(float upScale, float downScale)
    {
        mUpSpeedScale = upScale;
        mDownSpeedScale = downScale;
    }
}
