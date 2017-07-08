using UnityEngine;
using System.Collections;

public class UnitData : MonoBehaviour {
    public Team teamSide;
    public int hp;
    public UnitType unitType;
    
    void Awake () {
        hp = GlobalDefines.MAX_HP;
        teamSide = Team.Team1;
        unitType = UnitType.GroundObject;
	}
	
	public float HpPercent()
    {
        return 1.0f * hp / GlobalDefines.MAX_HP;
    }

    public void DecreaseHp(int num)
    {
        hp -= num;
        if(hp <= 0)
        {
            hp = 0;
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
