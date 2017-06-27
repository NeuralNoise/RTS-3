using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnitData))]
public class UnitAttack : MonoBehaviour {
    public int attackRate = 5; //攻击速度（次数/秒）
    public float senseRadius = 7f; //进入该范围才攻击
    public float rotateSpeed = 20f;
    
    public GameObject BulletPrefab;
    public Transform SpawnPos; //子弹发射点

    private Transform mTarget;
    private Transform mWeapon;
    private Vector3 mOriginPos;
    private UnitData mData;
    private float mTimer = 0;

    void Awake()
    {
        mWeapon = transform.Find("Weapon");
        mOriginPos = mWeapon.localPosition;
        mData = this.GetComponent<UnitData>();
    }
    
	void Update () {
	    if(mTarget != null)
        {
            LookAt(mTarget);

            mTimer += Time.deltaTime;
            if(mTimer >= CalculateAttackTimeDelta())
            {
                mTimer = 0;
                Attack(mTarget);
            }
        }
        else
        {
            mWeapon.transform.localEulerAngles = Vector3.zero;
        }
	}

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.8f);
        Gizmos.DrawWireSphere(transform.position, senseRadius);
    }

    public void LockTarget(Transform target)
    {
        mTarget = target;
    }

    public void UnlockTarget()
    {
        mTarget = null; 
    }

    void Attack(Transform target)
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance > senseRadius)
            return;

        GameObject bullet = Instantiate(BulletPrefab) as GameObject;
        bullet.GetComponent<Bullet>().Spawn(SpawnPos.position, target);
    }

    void LookAt(Transform target)
    {
        Vector3 toTarget = target.position - mWeapon.transform.position;
        Quaternion newRotation = Quaternion.LookRotation(toTarget);
        mWeapon.rotation = Quaternion.RotateTowards(mWeapon.rotation, newRotation, rotateSpeed * Time.deltaTime);
    }

    float CalculateAttackTimeDelta()
    {
        if (attackRate <= 0)
            return float.MaxValue;

        return 1.0f / attackRate;
    }
}
