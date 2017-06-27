using UnityEngine;
using System.Collections;

public class UnitData : MonoBehaviour {
    private int mHp;
    private Team mTeamSide;

    public Team TeamSide
    {
        get { return mTeamSide; }
        set { mTeamSide = value; }
    } 
    
	void Start () {
        mHp = GlobalDefines.MAX_HP;
        mTeamSide = Team.Team1;
	}
	
	public float HpPercent()
    {
        return 1.0f * mHp / GlobalDefines.MAX_HP;
    }

    public void DecreaseHp(int num)
    {
        mHp -= num;
        mHp = mHp <= 0 ? 0 : mHp;
    }
}
