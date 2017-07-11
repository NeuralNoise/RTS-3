using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {
    public static UIManager Instance = null;

    private Button mMiniBtn;
    private Vector2 mMiniBtnShowPos = new Vector2(323.65f, 176f);
    private Vector2 mMiniBtnHidePos = new Vector2(926.5f, 176f);
    private Transform mBuildingUIPanel;
    private bool mIsHidingPanel; //是否正在隐藏建筑UI面板
    
    void Awake()
    {
        Instance = this;
        mIsHidingPanel = false;
        InitUI();
        //OnClickMini(); //默认隐藏面板
    }
    
    void InitUI()
    {
        mMiniBtn = transform.Find("MiniBtn").GetComponent<Button>();
        mMiniBtn.onClick.AddListener(OnClickMini);
        mBuildingUIPanel = transform.Find("BuildingUI");
    }

    void OnClickMini()
    {
        mIsHidingPanel = !mIsHidingPanel;
        SetMiniBtnState(mIsHidingPanel);
        if (mIsHidingPanel)
            mMiniBtn.GetComponent<RectTransform>().anchoredPosition = mMiniBtnHidePos;
        else
            mMiniBtn.GetComponent<RectTransform>().anchoredPosition = mMiniBtnShowPos;
    }

    void SetMiniBtnState(bool hidePanel)
    {
        mBuildingUIPanel.gameObject.SetActive(!hidePanel);
        mMiniBtn.transform.Find("HideText").gameObject.SetActive(!hidePanel);
        mMiniBtn.transform.Find("ShowText").gameObject.SetActive(hidePanel);
    }

    public BuildingUIPanel GetBuildingUI()
    {
        return mBuildingUIPanel.GetComponent<BuildingUIPanel>();
    }
}
