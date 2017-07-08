using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Camera))]
public class DrawRect : MonoBehaviour {
    private List<UnitInteraction> mCheckList = new List<UnitInteraction>();
    private Camera mCam = null;
    private Material mMat = null;
    private Vector2 mPoint1 = Vector2.zero;
    private Vector2 mPoint2 = Vector2.zero;
    private bool mIsDrawingRect = false;

    void Awake () {
        mIsDrawingRect = false;
        mCam = transform.GetComponent<Camera>();

        mMat = new Material(Shader.Find("Unlit/Color"));
        mMat.SetColor("_Color", Color.green);
    }

    void Start()
    {
        InputManager.Instance.TwoTouchAction += TwoTouchAction;
    }

    void Update()
    {
        PCMultiChoose();
    }
	
    void OnGUI()
    {
        if (mIsDrawingRect)
        {
            GraphicsTool.DrawRect(mPoint1, mPoint2, false, ref mMat);
            CheckSelection();
        }
    }

    void OnDestroy()
    {
        Destroy(mMat);
        mMat = null;
    }

    public void AddToCheckList(UnitInteraction item)
    {
        if (mCheckList.Contains(item) == false)
            mCheckList.Add(item);
    }

    public void RemoveFromCheckList(UnitInteraction item)
    {
        if (mCheckList.Contains(item))
            mCheckList.Remove(item);
    }
    
    bool IsRectContains(Vector2 p1, Vector2 p2, Vector2 target)
    {
        float minX = Mathf.Min(p1.x, p2.x);
        float maxX = Mathf.Max(p1.x, p2.x);
        float minY = Mathf.Min(p1.y, p2.y);
        float maxY = Mathf.Max(p1.y, p2.y);
        bool isInX = (target.x >= minX) && (target.x <= maxX);
        bool isInY = (target.y >= minY) && (target.y <= maxY);
        return isInX && isInY;
    }
    
    bool IsDrawingRectContains(Transform trans)
    {
        Vector3 pos = mCam.WorldToScreenPoint(trans.position);
        Vector2 posInScreen = new Vector2(pos.x, pos.y);
        return IsRectContains(mPoint1, mPoint2, posInScreen);
    }

    void CheckSelection()
    {
        for(int i = 0; i < mCheckList.Count; i++)
        {
            Transform trans = mCheckList[i].transform;
            if(IsDrawingRectContains(trans))
            {
                mCheckList[i].Select();
            }
        }
    }

    void TwoTouchAction(Vector2 p1, Vector2 p2)
    {
        mIsDrawingRect = true;
        mPoint1 = p1;
        mPoint2 = p2;
    }

    void PCMultiChoose()
    {
        if (Application.platform != RuntimePlatform.WindowsEditor &&
            Application.platform != RuntimePlatform.WindowsPlayer)
            return;

        if (Input.GetKey(KeyCode.LeftControl) == false)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            mIsDrawingRect = true;
            mPoint1 = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            mPoint2 = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mIsDrawingRect = false;
        }
    }
}
