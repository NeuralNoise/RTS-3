using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BuildingUIPanel : MonoBehaviour {
    private const int PRE_LOAD_NUM = 10; //预先加载的item数量
    private const string ITEM_NAME = "item";

    private Transform mScrollContent;
    private GameObject mItemBtnTemplate;
    private List<GameObject> mItemList = new List<GameObject>();
    private List<int> mItemIds = new List<int>();

    void Awake()
    {
        InitUI();
    }
    
    void InitUI()
    {
        mScrollContent = transform.Find("ItemList/Viewport/Content");
        mItemBtnTemplate = mScrollContent.Find("ItemBtnTemplate").gameObject;
        mItemBtnTemplate.SetActive(false);
        for (int i = 0; i < PRE_LOAD_NUM; i++)
        {
            GameObject item = GameObject.Instantiate(mItemBtnTemplate);
            item.name = ITEM_NAME + i;
            item.transform.SetParent(mScrollContent);
            item.SetActive(false);
            item.GetComponent<Button>().onClick.AddListener(delegate() { OnClickItem(item); });
            mItemList.Add(item);
        }
    }
    
    void OnClickItem(GameObject item)
    {
        int index = GetIndex(item);
        int id = mItemIds[index];
        BuildingManager.Instance.CreateObject(id);
    }

    int GetIndex(GameObject item)
    {
        int startIndex = ITEM_NAME.Length;
        string strIndex = item.name.Substring(startIndex);
        return int.Parse(strIndex);
    }

    public void LoadItems(List<int> itemIds)
    {
        mItemIds.Clear();
        mItemIds = itemIds;

        HideAllItems();
        for(int i = 0; i < itemIds.Count; i++)
        {
            mItemList[i].SetActive(true);
        }
    }

    void HideAllItems()
    {
        for(int i = 0; i < mItemList.Count; i++)
        {
            mItemList[i].SetActive(false);
        }
    }
}
