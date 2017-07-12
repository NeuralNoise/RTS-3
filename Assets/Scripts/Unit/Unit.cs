using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnitData))]
[RequireComponent(typeof(UnitInteraction))]
public class Unit : MonoBehaviour {
    protected UnitInteraction mInteraction;
    protected UnitData mData;
    protected Transform mMapPoint;

    protected void Awake()
    {
        mInteraction = this.GetComponent<UnitInteraction>();
        mData = this.GetComponent<UnitData>();
    }

    protected void Start () {
        mInteraction.Deselect();
        MapManager.Instance.AddUnit(transform);
	}
}
