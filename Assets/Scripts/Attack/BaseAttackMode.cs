using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// A base class for attack mode
/// </summary>
public abstract class BaseAttackMode : MonoBehaviour {
    public int demage = 5;
    public float liveTime = 10;

    protected Action mAttackCallback;
    protected float mTimer = 0;
    protected Transform mTarget = null;
    
	protected void Update () {
        CheckToDestroy();
        MoveOrHide();
	}

    protected virtual void CheckToDestroy()
    {
        mTimer += Time.deltaTime;
        if (mTimer >= liveTime)
            Die();
    }

    protected void MoveOrHide()
    {
        if (mTarget != null)
            MoveToTarget();
        else
            gameObject.SetActive(false);
    }

    //TODO: 感觉有点特殊处理
    protected virtual void OnCollisionEnter(Collision coll)
    {
        if (coll.transform.gameObject == mTarget.gameObject)
        {
            if (coll.gameObject.tag == GlobalDefines.MOVING_OBJ_TAG ||
                coll.gameObject.tag == GlobalDefines.PLAYER_TAG ||
                coll.gameObject.tag == GlobalDefines.BUILDING_TAG)
            {
                AttackTarget(coll.transform);
            }
            Die();
        }
        else
        {
            //if (coll.gameObject != gameObject)
            //    Die();
        }
    }

    public virtual void Spawn(Vector3 pos, Transform target)
    {
        transform.position = pos;
        mTarget = target;
    }

    protected abstract void MoveToTarget();

    protected void AttackTarget(Transform target)
    {
        UnitData data = target.GetComponent<UnitData>();
        data.DecreaseHp(demage);

        if (mAttackCallback != null)
            mAttackCallback();
    }

    protected void Die()
    {
        Destroy(gameObject);
    }

    public void AddAttackCallback(Action callback)
    {
        mAttackCallback += callback;
    }
}
