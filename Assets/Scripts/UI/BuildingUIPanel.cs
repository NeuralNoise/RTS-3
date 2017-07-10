using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuildingUIPanel : MonoBehaviour {
    private Transform mScrollContent;
    private GameObject mItemBtnTemplate;

    void Awake()
    {
        InitUI();
        Test();
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
            item.GetComponent<Button>().onClick.AddListener(OnClickItem);
        }
    }

    void OnClickItem()
    {
        Debug.Log("OnClickItem");
        ObjectManager.Instance.CreateObject("Infantry");
    }
}
