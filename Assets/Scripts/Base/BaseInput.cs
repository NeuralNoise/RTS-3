using UnityEngine;
using System.Collections;

public abstract class BaseInput : MonoBehaviour {

    protected virtual void Start()
    {
        InputManager.Instance.ClickUpAction += MouseClick;
    }

    protected void OnDestroy()
    {
        InputManager.Instance.ClickUpAction -= MouseClick;
    }

	protected bool IsClickSomething(out RaycastHit hitInfo)
    {
        Ray ray = GameObject.FindGameObjectWithTag(GlobalDefines.MAIN_CAMERA_TAG).GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitInfo))
        {
            return true;
        }

        return false;
    }

    protected abstract void MouseClick();
}
