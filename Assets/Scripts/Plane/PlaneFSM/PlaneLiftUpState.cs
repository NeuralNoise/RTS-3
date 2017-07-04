using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Plane))]
public class PlaneLiftUpState : StateMachineBehaviour {
    private Plane mPlane;
    private Transform mTrans;
    private float mCurRotateSpeed;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mPlane = animator.GetComponent<Plane>();
        mTrans = animator.transform;
        mCurRotateSpeed = 0;
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        RotatePropeller();
        LiftUp();

        float height = mTrans.localPosition.y;
        if (Mathf.Approximately(height, mPlane.targetHeight))
        {
            animator.SetBool("IsHighEnough", true);
        }
    }
    
    /// <summary>
    /// 转动螺旋桨
    /// 逐渐加速直到最大值
    /// </summary>
    void RotatePropeller()
    {
        mCurRotateSpeed += Time.deltaTime * mPlane.rotateAcceration;
        mCurRotateSpeed = Mathf.Clamp(mCurRotateSpeed, 0, mPlane.maxRotateSpeed);
        mPlane.RotatePropeller(mCurRotateSpeed);
    }

    /// <summary>
    /// 垂直升空
    /// 螺旋桨转的速度到定值后才能升起来
    /// </summary>
    void LiftUp()
    {
        bool isRotateSpeedEnough = Mathf.Approximately(mCurRotateSpeed, mPlane.maxRotateSpeed);
        Vector3 oldPos = mTrans.localPosition;
        float height = oldPos.y;
        if (isRotateSpeedEnough)
        {
            height += Time.deltaTime * mPlane.flyVerticalSpeed;
        }
        height = Mathf.Clamp(height, 0, mPlane.targetHeight);
        mTrans.localPosition = new Vector3(oldPos.x, height, oldPos.z);        
    }
    
}
