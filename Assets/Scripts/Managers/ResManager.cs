using UnityEngine;
using System.Collections.Generic;

public class ResManager : MonoBehaviour {
    public static ResManager Instance = null;

    public GameObject Infantry;
    public GameObject Helicopter;

    private Dictionary<string, GameObject> mName2Prefab;

    void Awake()
    {
        Instance = this;
        InitData();
    }

    void InitData()
    {
        mName2Prefab = new Dictionary<string, GameObject>();
        mName2Prefab.Add("Infantry", Infantry);
        mName2Prefab.Add("Helicopter", Helicopter);
    }
    
    public GameObject GetPrefab(string name)
    {
        if (mName2Prefab.ContainsKey(name))
            return mName2Prefab[name];
        else
            return null;
    }
}
