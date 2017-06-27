using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    public float speed = 40;
    public int demage = 5;
    public float liveTime = 3;

    private float mTimer = 0;
    private Transform mTarget;
    
	void Update () {
        mTimer += Time.deltaTime;
        if (mTimer >= liveTime)
            Destroy(gameObject);

        if (mTarget != null)
            MoveToTarget(mTarget.position);
        else
            gameObject.SetActive(false);
	}

    void OnCollisionEnter(Collision coll)
    {
        if(coll.transform.gameObject == mTarget.gameObject)
        {
            if (coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Player")
            {
                UnitData data = coll.transform.GetComponent<UnitData>();
                data.DecreaseHp(demage);
            }
            else if (coll.gameObject.tag == "Building") //TODO
            {
                //Building b = coll.transform.GetComponent<Building>();
                //b.BeingHit(Demage);
            }
            Destroy(gameObject);
        }
        else
        {
            if (coll.gameObject != gameObject)
                Destroy(gameObject);
        }
    }

    public void Spawn(Vector3 pos, Transform target)
    {
        transform.position = pos;
        mTarget = target;
    }

    void MoveToTarget(Vector3 target)
    {
        Vector3 toTarget = target - transform.position;
        toTarget.Normalize();

        transform.position += toTarget * speed * Time.deltaTime;
    }
}
