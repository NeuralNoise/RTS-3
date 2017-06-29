using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 用于管理小地图
/// 你需要把下面那4个Border物体放到地图边界
/// </summary>
public class MapManager : MonoBehaviour {
    public static MapManager Instance = null;

    public GameObject CameraRectPrefab;
    public GameObject MapPointPrefab;

    private Dictionary<Transform, RectTransform> mUnit2MiniObj;
    private Vector2 mHorizontalRange;
    private Vector2 mVerticalRange;
    private RectTransform mMiniMap;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        float leftBorderX = transform.Find("LeftBorder").position.x;
        float rightBorderX = transform.Find("RightBorder").position.x;
        float upBorderZ = transform.Find("UpBorder").position.z;
        float downBorderZ = transform.Find("DownBorder").position.z;
        mHorizontalRange = new Vector2(leftBorderX, rightBorderX);
        mVerticalRange = new Vector2(downBorderZ, upBorderZ);

        mMiniMap = GameObject.Find("MiniMap").GetComponent<RectTransform>();
        mUnit2MiniObj = new Dictionary<Transform, RectTransform>();
    }

	void Start () {
	
	}
	
	void Update () {
        Refresh();
	}

    public void Add(Transform unit, bool isCamera = false)
    {
        if(mUnit2MiniObj.ContainsKey(unit) == false)
        {
            if(isCamera)
            {
                RectTransform cameraRect = GameObject.Instantiate(CameraRectPrefab).GetComponent<RectTransform>();
                cameraRect.SetParent(mMiniMap.Find("Container"));
                cameraRect.localScale = new Vector3(1, 1, 1);
                mUnit2MiniObj.Add(unit, cameraRect);
            }
            else
            {
                RectTransform mapPoint = GameObject.Instantiate(MapPointPrefab).GetComponent<RectTransform>();
                mapPoint.SetParent(mMiniMap.Find("Container"));
                mapPoint.localScale = new Vector3(1, 1, 1);
                mUnit2MiniObj.Add(unit, mapPoint);
            }
        }
    }

    public void Remove(Transform unit)
    {
        if (mUnit2MiniObj.ContainsKey(unit))
        {
            RectTransform miniObj = mUnit2MiniObj[unit];
            mUnit2MiniObj.Remove(unit);
            Destroy(miniObj.gameObject);
        }
    }

    void Refresh()
    {
        List<Transform> removeList = new List<Transform>();
        foreach(var pair in mUnit2MiniObj)
        {
            if(pair.Key == null)
            {
                removeList.Add(pair.Key);
            }
            else
            {
                Vector3 mapPos = WorldPos2MapPos(pair.Key.position);
                pair.Value.localPosition = mapPos;
            }
        }

        while(removeList.Count > 0)
        {
            Remove(removeList[0]);
            removeList.RemoveAt(0);
        }
    }

    Vector3 WorldPos2MapPos(Vector3 pos)
    {
        float xPercent = (pos.x - mHorizontalRange.x) / (mHorizontalRange.y - mHorizontalRange.x);
        float yPercent = (pos.z - mVerticalRange.x) / (mVerticalRange.y - mVerticalRange.x);
        Vector2 miniMapSize = mMiniMap.sizeDelta;
        return new Vector3(miniMapSize.x * xPercent, miniMapSize.y * yPercent, 0);
    }
}
