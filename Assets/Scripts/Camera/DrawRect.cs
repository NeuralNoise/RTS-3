using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Camera))]
public class DrawRect : MonoBehaviour {
    public Vector2 point1 = Vector2.zero;
    public Vector2 point2 = Vector2.zero;
    public bool isDrawingRect;

    private List<UnitInteraction> mCheckList = new List<UnitInteraction>();
    private Camera mCam = null;
    private Material mMat = null;

	void Awake () {
        isDrawingRect = false;
        mCam = transform.GetComponent<Camera>();

        mMat = new Material(Shader.Find("Unlit/Color"));
        mMat.SetColor("_Color", Color.green);
        mMat.hideFlags = HideFlags.HideAndDontSave;
        mMat.shader.hideFlags = HideFlags.HideAndDontSave;
    }
	
    void OnGUI()
    {
        if (isDrawingRect)
        {
            GraphicsTool.DrawRect(point1, point2, false, ref mMat);
            CheckSelection();
        }
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
        return IsRectContains(point1, point2, posInScreen);
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
}
