using UnityEngine;
using System.Collections;
using System;

//TODO：物理公式这里有问题，虽然游戏表现正常。

/// <summary>
/// Move around a parabola.
/// Just like a rocket gun.
/// </summary>
public class ParaboraAttackMode : BaseAttackMode {
    public float maxHeight = 10;

    protected Vector3 mStartPos;
    protected Vector3 mPointOfFall; //it lock target point other than target, it cant follow target
    protected Vector3 mInitSpeed = Vector3.zero;
    protected Vector3 mVerticalSpeed = Vector3.zero;
    protected float mTimeScale = 1.0f;
    protected float mUpSpeedScale = 1.0f;
    protected float mDownSpeedScale = 1.0f;
    protected Vector3 mCurPos;
    
    protected override void CheckToDestroy()
    {
        base.CheckToDestroy();

        if (transform.position.y <= 0) //if under ground
            Die();
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
        mCurPos += mVerticalSpeed * deltaTime + Vector3.up * (0.5f * GlobalDefines.G * deltaTime * deltaTime); //vt + 1/2 * a * t^2

        UpdateObject();
    }

    protected void UpdateObject()
    {
        transform.LookAt(mCurPos);
        transform.Rotate(new Vector3(90, 0, 0));
        transform.position = mCurPos;
    }

    public void SetMaxHeight(float maxHeight)
    {
        float distance = (mPointOfFall - mStartPos).magnitude;
        float totalTime = Mathf.Sqrt(Mathf.Abs(2.0f * this.maxHeight / GlobalDefines.G)); //h = 0.5 * g * t^2

        mInitSpeed = new Vector3(
            (mPointOfFall.x - mStartPos.x) / totalTime, //Vx = Sx / t
            (mPointOfFall.y - mStartPos.y) / totalTime - GlobalDefines.G * totalTime, //Vy = Sy / t - 0.5 * g * t
            (mPointOfFall.z - mStartPos.z) / totalTime
        );
    }

    public void SetSpeedScale(float upScale, float downScale)
    {
        mUpSpeedScale = upScale;
        mDownSpeedScale = downScale;
    }
}
