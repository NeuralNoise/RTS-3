using UnityEngine;
using System.Collections;

/// <summary>
/// 控制物体移动
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class UnitMotor : MonoBehaviour {
    public float speed = 10f;

    private NavMeshAgent mAgent;
    private bool mIsFirstFrame = true;

    void Awake()
    {
        mAgent = this.GetComponent<NavMeshAgent>();
        mAgent.Warp(transform.position);
    }
    
	void Update () {
        if (mIsFirstFrame)
        {
            mIsFirstFrame = false;
            Init();
        }
	}

    void Init()
    {
        mAgent.speed = speed;
        mAgent.angularSpeed = 1000;
        mAgent.acceleration = 1000;
    }

    public void MoveTo(Vector3 target)
    {
        mAgent.Resume();
        mAgent.SetDestination(target);
    }

    //TODO
    public void MoveUntilSense(Vector3 target)
    {
        mAgent.Resume();
        mAgent.SetDestination(target);
        //mAgent.
    }
}
