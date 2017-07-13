using UnityEngine;
using System.Collections;
using System;

//TODO

/// <summary>
/// Move around a parabola.
/// Just like a rocket gun.
/// </summary>
public class ParaboraAttackMode : BaseAttackMode {
    public float speed = 5;
    public float height = 5; //the max height of the parabola

    private Vector3 mPointOfFall; //it lock target point other than target, it cant follow target
    private Vector2 mDirToTarget;
    private float mVerticalSpeed;

    protected override void CheckToDestroy()
    {
        base.CheckToDestroy();

        if (transform.position.y < -1) //if under ground
            Destroy(gameObject);
    }

    public override void Spawn(Vector3 pos, Transform target)
    {
        base.Spawn(pos, target);
        mPointOfFall = target.position;
        CalcVerticalSpeed();
    }

    protected override void MoveToTarget()
    {
        float horizontalMoveDist = speed * Time.deltaTime;
        Vector2 horizontalDelta = mDirToTarget * horizontalMoveDist;

        float originSpeed = mVerticalSpeed;
        mVerticalSpeed += GlobalDefines.G * Time.deltaTime;
        float verticalDelta = ((mVerticalSpeed * mVerticalSpeed) - (originSpeed * originSpeed)) / (2 * GlobalDefines.G);

        Vector3 delta = new Vector3(horizontalDelta.x, verticalDelta, horizontalDelta.y);
        transform.position += delta;
    }

    void CalcVerticalSpeed()
    {
        Vector2 horizontalTarget = new Vector2(mPointOfFall.x, mPointOfFall.z);
        Vector2 horizontalSrc = new Vector2(transform.position.x, transform.position.z);
        mDirToTarget = (horizontalTarget - horizontalSrc).normalized;
        float horizontalDistance = (horizontalSrc - horizontalTarget).magnitude;
        float t = horizontalDistance / speed / 2;
        float a = 2 * height / (t * t); //1/2 * a * t^2 = h 
        mVerticalSpeed = a * t;
    }
}
