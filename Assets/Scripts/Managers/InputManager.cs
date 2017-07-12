using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections;

/// <summary>
/// 统一管理输入
/// </summary>
public class InputManager : MonoBehaviour {
    public static InputManager Instance = null;

    public float dragThreshold = 2f;

    public Action ClickDownAction;
    public Action ClickUpAction;
    public Action<Vector3, Vector3> DragAction;
    public Action<Vector2, Vector2> TwoTouchAction;

    private bool mIsDraging = false;
    private Vector3 mLastDragPos = Vector3.zero;
    private Vector3 mClickDownPos = Vector3.zero;

    [SerializeField]
    private bool mIsLock = false;
    public bool IsLock
    {
        get { return mIsLock; }
        set { mIsLock = value; }
    }
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
	void Update () {
        if (mIsLock)
            return;
        
        //点击UI
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.touchCount == 2)
            HandleTwoTouch();
        else
        {
            HandleClick();

            if (mIsDraging && DragAction != null && !Input.GetKey(KeyCode.LeftControl))
            {
                DragAction(mLastDragPos, Input.mousePosition);
                mLastDragPos = Input.mousePosition;
            }
        }
    }

    void HandleClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            mIsDraging = true;
            mLastDragPos = Input.mousePosition;
            mClickDownPos = Input.mousePosition;

            if (ClickDownAction != null)
                ClickDownAction();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            mIsDraging = false;
            float dragDist = (Input.mousePosition - mClickDownPos).magnitude;
            if (dragDist < dragThreshold && ClickUpAction != null) //拖动过大时，就不算点击了
            { 
                ClickUpAction();
            }
        }
    }

    void HandleTwoTouch()
    {
        Vector2 firstTouchPos = Input.GetTouch(0).position;
        Vector2 secondTouchPos = Input.GetTouch(1).position;
        if (TwoTouchAction != null)
            TwoTouchAction(firstTouchPos, secondTouchPos);
    }
}
