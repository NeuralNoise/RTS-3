using UnityEngine;
using System.Collections;

/// <summary>
/// 处理输入，比如点击
/// </summary>
[RequireComponent(typeof(UnitInteraction))]
public class UnitInput : MonoBehaviour {
    public GameObject clickSymbol;

    private float mLastClickTime = 0; //上次点击的时间
    private bool mIsLastTimeSelected = false;

    private UnitInteraction mInteraction;

	void Start () {
        mInteraction = this.GetComponent<UnitInteraction>();

        InputManager.Instance.ClickUpAction += MouseClick;
	}

    void Destroy()
    {
        InputManager.Instance.ClickUpAction -= MouseClick;
    }
	
	void Update () {
        bool isLastTimeSelected = mIsLastTimeSelected;
        mIsLastTimeSelected = false;

        RaycastHit hitInfo;
        if(IsClickSomething(out hitInfo))
        {
            GameObject hitGo = hitInfo.collider.gameObject;
            if (hitGo == gameObject)
                HandleClickMyself(isLastTimeSelected);
        }

        mLastClickTime = Time.time;
	}

    void MouseClick()
    {

    }

    bool IsClickSomething(out RaycastHit hitInfo)
    {
        Ray ray = GameObject.FindGameObjectWithTag(GlobalDefines.MAIN_CAMERA_TAG).GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitInfo))
        {
            return true;
        }

        return false;
    }

    void HandleClickMyself(bool isLastTimeSelected)
    {
        mIsLastTimeSelected = true;
        mInteraction.Select();

        bool isDoubleClick = isLastTimeSelected && (Time.time - mLastClickTime <= GlobalDefines.DOUBLE_CLICK_GAP);
        if(isDoubleClick)
        {
            Debug.Log("双击了");
        }
    }
}
