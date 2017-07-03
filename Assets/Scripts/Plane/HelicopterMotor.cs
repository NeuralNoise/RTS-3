using UnityEngine;
using System.Collections;

public class HelicopterMotor : UnitMotor {
    private Animator mAnimator;
    private Vector3 mTarget;

	new void Awake () {
        base.Awake();
        mAnimator = this.GetComponent<Animator>();
        mAgent.enabled = false;
	}
	
	new void Update () {
        base.Update();
	}

    public override void MoveTo(Vector3 target)
    {
        mAnimator.SetBool("IsMoving", true);
        mTarget = target;
    }

    public void ActuallyMoveToTarget()
    {
        mAgent.enabled = true;
        base.MoveTo(mTarget);
    }

    public bool IsArriveTarget()
    {
        float dist = Vector3.Distance(transform.position, mAgent.destination);
        return dist <= GlobalDefines.MIN_ERROR_RANGE;
    }
}
