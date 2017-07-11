using UnityEngine;
using System.Collections;

/// <summary>
/// 控制物体移动
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class UnitMotor : MonoBehaviour {
    public float speed = 10f;

    protected NavMeshAgent mAgent;
    protected bool mIsFirstFrame = true;

    protected void Awake()
    {
        mAgent = this.GetComponent<NavMeshAgent>();
        mAgent.Warp(transform.position);
    }

    protected void Update () {
        if (mIsFirstFrame)
        {
            mIsFirstFrame = false;
            Init();
        }
	}

    protected void Init()
    {
        mAgent.speed = speed;
        mAgent.angularSpeed = 1000;
        mAgent.acceleration = 1000;
    }

    public virtual void MoveTo(Vector3 target)
    {
        mAgent.Resume();
        mAgent.SetDestination(target);
    }

    public virtual void Stop()
    {
        mAgent.Stop();
    }
}
