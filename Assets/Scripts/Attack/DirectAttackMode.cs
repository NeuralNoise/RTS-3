using UnityEngine;
using System.Collections;

/// <summary>
/// Direct move to target and attack it.
/// Just like a bullet.
/// </summary>
public class DirectAttackMode : BaseAttackMode {
    public float speed = 40;

    protected override void MoveToTarget()
    {
        Vector3 toTarget = mTarget.position - transform.position;
        toTarget.Normalize();

        transform.position += toTarget * speed * Time.deltaTime;
    }
}
