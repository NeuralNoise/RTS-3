using UnityEngine;
using System.Collections;

/// <summary>
/// 物体选中后的表现
/// </summary>
public class UnitInteraction : MonoBehaviour {
    private bool mIsSelected = false;

    [SerializeField]
    private GameObject mSelectedSymbol;

    void Start()
    {
        GameObject mainCam = GameObject.FindGameObjectWithTag(GlobalDefines.MAIN_CAMERA_TAG);
        DrawRect drawRect = mainCam.GetComponent<DrawRect>();
        drawRect.AddToCheckList(this);
    }

    public bool IsSelected
    {
        get { return mIsSelected; }
        set { mIsSelected = value; }
    }

	public void Select()
    {
        mIsSelected = true;
        mSelectedSymbol.SetActive(true);
    }

    public void Deselect()
    {
        mIsSelected = false;
        mSelectedSymbol.SetActive(false);
    }
}
