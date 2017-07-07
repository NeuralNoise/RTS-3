using UnityEngine;
using System.Collections;

/// <summary>
/// 处理输入，比如点击
/// </summary>
[RequireComponent(typeof(UnitInteraction))] //点击自己选择
[RequireComponent(typeof(UnitMotor))] //点击地面移动
[RequireComponent(typeof(UnitAttack))] //点击敌人攻击
[RequireComponent(typeof(UnitData))]
public class UnitInput : MonoBehaviour {
    public GameObject clickSymbol;
    
    private UnitInteraction mInteraction;
    private UnitMotor mMotor;
    private UnitAttack mAttack;
    private UnitData mData;

	void Start () {
        mInteraction = this.GetComponent<UnitInteraction>();
        mMotor = this.GetComponent<UnitMotor>();
        mAttack = this.GetComponent<UnitAttack>();
        mData = this.GetComponent<UnitData>();

        InputManager.Instance.ClickUpAction += MouseClick;
	}

    void OnDestroy()
    {
        InputManager.Instance.ClickUpAction -= MouseClick;
    }
	
    void MouseClick()
    {
        RaycastHit hitInfo;
        if (IsClickSomething(out hitInfo))
        {
            GameObject hitGo = hitInfo.collider.gameObject;
            if (hitGo == gameObject)
                HandleClickMyself();
            else if (hitGo.layer == LayerMask.NameToLayer(GlobalDefines.GROUND_LAYER))
                HandleClickGround(hitInfo.point);
            else if (hitGo.layer == LayerMask.NameToLayer(GlobalDefines.SEA_LAYER))
                HandleClickSea(hitInfo.point);
            else if (hitGo.tag == GlobalDefines.PLAYER_TAG)
                HandleClickOtherPlayers(hitGo);
            else if (hitGo.tag == GlobalDefines.MOVING_OBJ_TAG)
                HandleClickMovingObject(hitGo);
            else if (hitGo.tag == GlobalDefines.BUILDING_TAG)
                HandleClickBuilding(hitGo);
        }
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

    void HandleClickMyself()
    {
        mInteraction.Select();
    }

    void HandleClickGround(Vector3 pos)
    {
        Debug.Log(gameObject.name + " Click Ground");
        if (mInteraction.IsSelected)
        {
            GameObject clickSign = Instantiate(clickSymbol) as GameObject;
            clickSign.transform.position = pos + Vector3.up * 0.01f; //稍微高出地面一点以免被地挡住

            mMotor.MoveTo(pos);
            mAttack.UnlockTarget();
        }
    }

    void HandleClickSea(Vector3 pos)
    {
        if(mInteraction.IsSelected && mData.unitType==UnitType.FlyObject)
        {
            GameObject clickSign = Instantiate(clickSymbol) as GameObject;
            clickSign.transform.position = pos + Vector3.up * 0.01f; 

            mMotor.MoveTo(pos);
            mAttack.UnlockTarget();
        }
    }

    void HandleClickMovingObject(GameObject movingObj)
    {
        if(mInteraction.IsSelected)
        {
            UnitData data = movingObj.GetComponent<UnitData>();
            if (mData.teamSide != data.teamSide)
            {
                mAttack.LockTarget(movingObj.transform);
            }
        }
    }

    void HandleClickBuilding(GameObject building)
    {
        if(mInteraction.IsSelected)
        {
            mAttack.LockTarget(building.transform);
        }
    }

    void HandleClickOtherPlayers(GameObject player)
    {
        mInteraction.Deselect();
        player.GetComponent<UnitInteraction>().Select();
    }
}
