using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BaseBuilding))]
public class BuildingInput : BaseInput {
    private BaseBuilding mBuilding;

    protected override void Start()
    {
        base.Start();
        mBuilding = this.GetComponent<BaseBuilding>();
    }

    protected override void MouseClick()
    {
        RaycastHit hitInfo;
        if (IsClickSomething(out hitInfo))
        {
            GameObject hitGo = hitInfo.collider.gameObject;
            if (hitGo == gameObject)
                HandleClickMyself();
        }
    }
    
    void HandleClickMyself()
    {
        BuildingManager.Instance.Select(mBuilding);
    }
}
