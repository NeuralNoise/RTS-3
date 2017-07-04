using UnityEngine;
using System.Collections;

public class UnitData : MonoBehaviour {
    private int mHp;
    private Team mTeamSide;
    private ObjType mObjType;

    public Team TeamSide
    {
        get { return mTeamSide; }
        set { mTeamSide = value; }
    } 

    public ObjType UnitType
    {
        get { return mObjType; }
        set { mObjType = value; }
    }
    
    void Start () {
        mHp = GlobalDefines.MAX_HP;
        mTeamSide = Team.Team1;
        mObjType = ObjType.GroundObject;
	}
	
	public float HpPercent()
    {
        return 1.0f * mHp / GlobalDefines.MAX_HP;
    }

    public void DecreaseHp(int num)
    {
        mHp -= num;
        if(mHp <= 0)
        {
            mHp = 0;
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
