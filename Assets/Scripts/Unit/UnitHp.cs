using UnityEngine;
using System.Collections;

/// <summary>
/// 控制血条
/// </summary>
[RequireComponent(typeof(UnitData))]
public class UnitHp : MonoBehaviour {
    private Transform mHp;
    private UnitData mData;

    void Awake()
    {
        mHp = transform.Find("HpPanel/Hp");
        mData = this.GetComponent<UnitData>();
    }
    
	void Update () {
        mHp.localScale = new Vector3(1, 1, mData.HpPercent());
	}
}
