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
    public float rectMoveSensity = 3;

    private Vector2 mHorizontalRange;
    private Vector2 mVerticalRange;
    private Dictionary<Transform, RectTransform> mUnit2MiniObj;
    private RectTransform mMiniMap;

    private Transform mCamTrans = null;
    private RectTransform mCamRect = null;

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
    
	void Update () {
        RefreshUnits();
        RefreshCameraRect();
	}

    public Vector3 TopLeft()
    {
        return new Vector3(mHorizontalRange.x, 0, mVerticalRange.y);
    }

    public Vector3 TopRight()
    {
        return new Vector3(mHorizontalRange.y, 0, mVerticalRange.y);
    }

    public Vector3 BottomLeft()
    {
        return new Vector3(mHorizontalRange.x, 0, mVerticalRange.x);
    }

    public Vector3 BottomRight()
    {
        return new Vector3(mHorizontalRange.y, 0, mVerticalRange.x);
    }

    public void AddUnit(Transform unit)
    {
        if(mUnit2MiniObj.ContainsKey(unit) == false)
        {
                RectTransform mapPoint = GameObject.Instantiate(MapPointPrefab).GetComponent<RectTransform>();
                mapPoint.SetParent(mMiniMap.Find("Container"));
                mapPoint.localScale = new Vector3(1, 1, 1);
                mUnit2MiniObj.Add(unit, mapPoint);
        }
    }

    public void AddCamera(Transform camTrans)
    {
        mCamTrans = camTrans;
        mCamRect = GameObject.Instantiate(CameraRectPrefab).GetComponent<RectTransform>();
        mCamRect.SetParent(mMiniMap.Find("Container"));
        mCamRect.localScale = new Vector3(1, 1, 1);
        DragListener listener = mCamRect.Find("Rect").GetComponent<DragListener>();
        listener.DragAction += OnDragCamRect;
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
    
    void RefreshUnits()
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

    void RefreshCameraRect()
    {
        if (mCamTrans == null)
            return;
        
        Vector3 mapPos = WorldPos2MapPos(mCamTrans.position);
        mCamRect.localPosition = mapPos;
    }

    Vector3 WorldPos2MapPos(Vector3 pos)
    {
        float xPercent = (pos.x - mHorizontalRange.x) / (mHorizontalRange.y - mHorizontalRange.x);
        float yPercent = (pos.z - mVerticalRange.x) / (mVerticalRange.y - mVerticalRange.x);
        Vector2 miniMapSize = mMiniMap.sizeDelta;
        return new Vector3(miniMapSize.x * xPercent, miniMapSize.y * yPercent, 0);
    }

    Vector2 MapPos2WorldPos(Vector3 mapPos)
    {
        Vector2 miniMapSize = mMiniMap.sizeDelta;
        float xPercent = mapPos.x / miniMapSize.x;
        float yPercent = mapPos.y / miniMapSize.y;
        float x = mHorizontalRange.x + xPercent * (mHorizontalRange.y - mHorizontalRange.x);
        float y = mVerticalRange.x + yPercent * (mVerticalRange.y - mVerticalRange.x);
        return new Vector2(x, y);
    }
    
    /// <summary>
    /// 思路：算出方框的新位置，然后相应得到摄像机的位置，然后刷新摄像机位置（自动刷新方框位置）
    /// 这样就不需要考虑怎么限制方框的位置了，因为实际控制权还是在摄像机上。
    /// </summary>
    /// <param name="delta">鼠标位置移动变化（像素坐标）</param>
    void OnDragCamRect(Vector2 delta)
    {
        if(mCamTrans != null)
        {
            delta *= rectMoveSensity;
            Vector3 camOriginPos = mCamTrans.position;
            Vector3 rectPos = mCamRect.localPosition;
            Vector3 p = new Vector3(rectPos.x + delta.x, rectPos.y + delta.y, 0);
            Vector2 newPos = MapPos2WorldPos(p);
            Vector3 newCamPos = new Vector3(newPos.x, camOriginPos.y, newPos.y);
            mCamTrans.GetComponent<CameraController>().SetCameraPos(newCamPos);
        }
    }
}
