using UnityEngine;
using System.Collections;

public class BuildingManager : MonoBehaviour {
    public static BuildingManager Instance = null;
    private BaseBuilding mCurSelectedBuilding;
    private BuildingUIPanel mUI;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        mUI = UIManager.Instance.GetBuildingUI();
    }
    
    public void Select(BaseBuilding building)
    {
        mCurSelectedBuilding = building;
        SetBuildingUI();
    }

    public string AnalysisId(int id)
    {
        switch(id) //TODO:放到配置表
        {
            case 1001:
                return "Infantry";
            default:
                Debug.Log(id + "不存在！");
                return string.Empty;
        }
    }

    public void CreateObject(int id)
    {
        if (mCurSelectedBuilding != null)
            mCurSelectedBuilding.CreateObject(id);
    }

    void SetBuildingUI()
    {
        if(mCurSelectedBuilding != null)
            mUI.LoadItems(mCurSelectedBuilding.Data);
    }
}
