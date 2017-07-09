using UnityEngine;
using System.Collections;

public class BuildingUIManager : MonoBehaviour {
    public static BuildingUIManager Instance = null;

    private Transform mScrollContent;
    private GameObject mItemBtnTemplate;

    void Awake()
    {
        Instance = this;

        InitUI();
        Test();
    }

	void Start () {
	    
	}
	
	void Update () {
	
	}

    void InitUI()
    {
        mScrollContent = transform.Find("ItemList/Viewport/Content");
        mItemBtnTemplate = mScrollContent.Find("ItemBtnTemplate").gameObject;
        mItemBtnTemplate.SetActive(false);
    }

    void Test()
    {
        for(int i = 0; i < 10; i++)
        {
            GameObject item = GameObject.Instantiate(mItemBtnTemplate);
            item.transform.SetParent(mScrollContent);
            item.SetActive(true);
        }
    }
}
