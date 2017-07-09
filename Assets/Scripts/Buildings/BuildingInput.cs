using UnityEngine;
using System.Collections;

public class BuildingInput : MonoBehaviour {

	void Start () {
        InputManager.Instance.ClickUpAction += MouseClick;
    }
	
	void Update () {
	
	}

    void OnDestroy()
    {
        InputManager.Instance.ClickUpAction -= MouseClick;
    }

    void MouseClick()
    {
        Debug.Log("Mouse Click");
    }
}
