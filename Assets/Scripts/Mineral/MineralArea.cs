using UnityEngine;
using System.Collections.Generic;

public class MineralArea : MonoBehaviour {
    public GameObject mineralPrefab;
    public int totalNum;
    [Range(0, 1)]
    public float percent;

    private List<Transform> mMinerals;
    private Transform mEntireArea;
    private float mWidth;
    private float mHeight;

    void Awake()
    {
        mMinerals = new List<Transform>();
        mEntireArea = transform.Find("EntireArea");
        Bounds b = mEntireArea.GetComponent<MeshCollider>().bounds;
        mWidth = b.size.x;
        mHeight = b.size.z;
    }

	void Start () {
        CreateAllMinerals();
	}
	
	void Update () {
        int num = Mathf.FloorToInt(totalNum * percent);
        ClampOrExtendMinerals(num);
	}

    void CreateAllMinerals()
    {
        for(int i = 0; i < totalNum; i++)
        {
            mMinerals.Add(RandomCreateMineral());
        }
    }

    Transform RandomCreateMineral()
    {
        float randomX = Random.Range(-mWidth / 2, mWidth / 2);
        float randomZ = Random.Range(-mHeight / 2, mHeight / 2);
        Transform mineral = GameObject.Instantiate(mineralPrefab).transform;
        mineral.SetParent(mEntireArea);
        mineral.localPosition = new Vector3(randomX, 0, randomZ);
        return mineral;
    }

    void ClampMinerals(int num)
    {
        num = num > 0 ? num : 0;
        while(mMinerals.Count > num)
        {
            Transform mineral = mMinerals[0];
            GameObject.Destroy(mineral.gameObject);
            mMinerals.RemoveAt(0);
        }
    }

    void ExtendMinerals(int num)
    {
        num = num > 0 ? num : 0;
        while(mMinerals.Count < num)
        {
            mMinerals.Add(RandomCreateMineral());
        }
    }

    void ClampOrExtendMinerals(int num)
    {
        ClampMinerals(num);
        ExtendMinerals(num);
    }
}
