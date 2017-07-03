using UnityEngine;
using System.Collections;

public class PlaneMoveState : StateMachineBehaviour {
    private Plane mPlane;
    private Transform mTrans;
    private HelicopterMotor mMotor;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mPlane = animator.GetComponent<Plane>();
        mTrans = animator.transform;
        mMotor = animator.GetComponent<HelicopterMotor>();
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mPlane.RotatePropeller(mPlane.maxRotateSpeed);
        mMotor.ActuallyMoveToTarget();
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
