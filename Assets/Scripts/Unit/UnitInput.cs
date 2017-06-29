using UnityEngine;
using System.Collections;

/// <summary>
/// 处理输入，比如点击
/// </summary>
[RequireComponent(typeof(UnitInteraction))] //点击自己选择
[RequireComponent(typeof(UnitMotor))] //点击地面移动
[RequireComponent(typeof(UnitAttack))] //点击敌人攻击
public class UnitInput : MonoBehaviour {
    public GameObject clickSymbol;

    private float mLastClickTime = 0; //上次点击的时间
    private bool mIsLastTimeSelected = false;

    private UnitInteraction mInteraction;
    private UnitMotor mMotor;
    private UnitAttack mAttack;

	void Start () {
        mInteraction = this.GetComponent<UnitInteraction>();
        mMotor = this.GetComponent<UnitMotor>();
        mAttack = this.GetComponent<UnitAttack>();

        InputManager.Instance.ClickUpAction += MouseClick;
	}

    void OnDestroy()
    {
        InputManager.Instance.ClickUpAction -= MouseClick;
    }
	
    void MouseClick()
    {
        bool isLastTimeSelected = mIsLastTimeSelected;
        mIsLastTimeSelected = false;

        RaycastHit hitInfo;
        if (IsClickSomething(out hitInfo))
        {
            GameObject hitGo = hitInfo.collider.gameObject;
            if (hitGo == gameObject)
                HandleClickMyself(isLastTimeSelected);
            else if (hitGo.layer == LayerMask.NameToLayer(GlobalDefines.GROUND_LAYER))
                HandleClickGround(hitInfo.point);
            else if (hitGo.tag == GlobalDefines.ENEMY_TAG)
                HandleClickEnemy(hitGo);
            else if (hitGo.tag == GlobalDefines.BUILDING_TAG)
                HandleClickBuilding(hitGo);
        }

        mLastClickTime = Time.time;
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
            Debug.Log("双击了"); //TODO
        }
    }

    void HandleClickGround(Vector3 pos)
    {
        if (mInteraction.IsSelected)
        {
            GameObject clickSign = Instantiate(clickSymbol) as GameObject;
            clickSign.transform.position = pos + Vector3.up * 0.01f; //稍微高出地面一点以免被地挡住

            mMotor.MoveTo(pos);
            mAttack.UnlockTarget();
        }
    }

    void HandleClickEnemy(GameObject enemy)
    {
        if(mInteraction.IsSelected)
        {
            UnitInteraction enemyInteraction = enemy.GetComponent<UnitInteraction>();
            enemyInteraction.Select();
            mAttack.LockTarget(enemy.transform);
        }
    }

    void HandleClickBuilding(GameObject building)
    {
        if(mInteraction.IsSelected)
        {
            mAttack.LockTarget(building.transform);
        }
    }
}
