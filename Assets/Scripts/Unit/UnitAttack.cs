using UnityEngine;

[RequireComponent(typeof(UnitData))]
public class UnitAttack : MonoBehaviour {
    public int attackRate = 5; //攻击速度（次数/秒）
    public float senseRadius = 10; //进入该范围才攻击
    public float rotateSpeed = 300f;
    
    public GameObject BulletPrefab;
    public Transform SpawnPos; //子弹发射点

    private Transform mTarget;
    private Transform mWeapon;
    private UnitData mData;
    private float mTimer = 0;
    
    void Awake()
    {
        mWeapon = transform.Find("Weapon");
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
        //绘制攻击范围
        Gizmos.color = new Color(1, 0, 0, 0.8f);
        Gizmos.DrawWireSphere(transform.position, senseRadius);
    }

    public void LockTarget(Transform target)
    {
        UnitData data = target.GetComponent<UnitData>();
        if (IsCanLock(transform, target))
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

    //TODO：实现太丑了，找个好的数据结构或者用配置表
    bool IsCanLock(Transform sourceTrans, Transform targetTrans)
    {
        UnitType source = sourceTrans.GetComponent<UnitData>().unitType;
        UnitType target = targetTrans.GetComponent<UnitData>().unitType;

        if (source == UnitType.GroundObject && target == UnitType.GroundObject)
            return true;
        if (source == UnitType.GroundObject && target == UnitType.FlyObject)
        {
            if (targetTrans.position.y <= GlobalDefines.GROUND_ATTACK_MIN_HEIGHT)
                return true;
            else
                return false;
        }
        if (source == UnitType.GroundObject && target == UnitType.SuspendedObject)
            return true;

        if (source == UnitType.FlyObject && target == UnitType.GroundObject)
            return true;
        if (source == UnitType.FlyObject && target == UnitType.FlyObject)
            return true;
        if (source == UnitType.FlyObject && target == UnitType.SuspendedObject)
            return true;

        if (source == UnitType.SuspendedObject && target == UnitType.GroundObject)
            return true;
        if (source == UnitType.SuspendedObject && target == UnitType.FlyObject)
        {
            if (targetTrans.position.y <= GlobalDefines.GROUND_ATTACK_MIN_HEIGHT)
                return true;
            else
                return false;
        }
        if (source == UnitType.SuspendedObject && target == UnitType.SuspendedObject)
            return true;

        return false;
    }
}
