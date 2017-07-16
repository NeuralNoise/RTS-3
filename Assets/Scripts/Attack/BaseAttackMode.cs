using UnityEngine;
using System.Collections;

/// <summary>
/// A base class for attack mode
/// </summary>
public abstract class BaseAttackMode : MonoBehaviour {
    public int demage = 5;
    public float liveTime = 10;

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

    protected void OnCollisionEnter(Collision coll)
    {
        if (coll.transform.gameObject == mTarget.gameObject)
        {
            if (coll.gameObject.tag == GlobalDefines.MOVING_OBJ_TAG ||
                coll.gameObject.tag == GlobalDefines.PLAYER_TAG ||
                coll.gameObject.tag == GlobalDefines.BUILDING_TAG)
            {
                UnitData data = coll.transform.GetComponent<UnitData>();
                data.DecreaseHp(demage);
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

    protected void Die()
    {
        Debug.Log("Die");
        Destroy(gameObject);
    }
}
