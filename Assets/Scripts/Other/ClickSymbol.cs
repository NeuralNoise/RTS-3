using UnityEngine;
using System.Collections;

public class ClickSymbol : MonoBehaviour {
    //由Animation调用
	public void Destroy()
    {
        Destroy(gameObject);
    }
}
