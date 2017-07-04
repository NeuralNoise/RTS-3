using UnityEngine;
using System.Collections;

public class PlaneMoveState : StateMachineBehaviour {
    private Plane mPlane;
    private Transform mTrans;
    private PlaneMotor mMotor;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mPlane = animator.GetComponent<Plane>();
        mTrans = animator.transform;
        mMotor = animator.GetComponent<PlaneMotor>();
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mPlane.RotatePropeller(mPlane.maxRotateSpeed);
        mMotor.ActuallyMoveToTarget();
        if (mMotor.IsArriveTarget())
            animator.SetBool("IsMoving", false);
    }
}
