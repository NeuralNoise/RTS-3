using UnityEngine;
using System.Collections;

public class UnitData : MonoBehaviour {
    [SerializeField]
    private int mHp;
    
	void Start () {
        mHp = GlobalDefines.MAX_HP;
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
