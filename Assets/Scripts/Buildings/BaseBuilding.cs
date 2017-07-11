using UnityEngine;
using System.Collections.Generic;

public class BaseBuilding : MonoBehaviour {
    protected List<int> mData = new List<int>(); //存放该建筑物能生产的东西ID
    protected Transform mSpawnPos;

    public List<int> Data
    {
        get { return mData; }
    }

    protected virtual void Start()
    {
        InitData();
        mSpawnPos = transform.Find("SpawnPos");
    }

    protected virtual void InitData() { }

    public void CreateObject(int id)
    {
        string name = BuildingManager.Instance.AnalysisId(id);
        GameObject go = ObjectManager.Instance.CreateObject(name);
        if(go != null)
            go.transform.position = mSpawnPos.position;
    }
}
