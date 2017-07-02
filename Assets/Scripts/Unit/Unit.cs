using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnitData))]
[RequireComponent(typeof(UnitInteraction))]
public class Unit : MonoBehaviour {
    private UnitInteraction mInteraction;
    private UnitData mData;
    private Transform mMapPoint;

    void Awake()
    {
        mInteraction = this.GetComponent<UnitInteraction>();
        mData = this.GetComponent<UnitData>();
    }

	void Start () {
        mInteraction.Deselect();
        MapManager.Instance.Add(transform);
	}
}
