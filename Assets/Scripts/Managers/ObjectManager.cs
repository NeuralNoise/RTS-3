using UnityEngine;
using System.Collections;

/// <summary>
/// 管理坦克等游戏物体
/// </summary>
public class ObjectManager : MonoBehaviour {
    public static ObjectManager Instance = null;
    public Transform ObjectParent;

    void Awake()
    {
        Instance = this;
    }

	public GameObject CreateObject(string name)
    {
        GameObject prefab = ResManager.Instance.GetPrefab(name);
        if(prefab != null)
        {
            GameObject go = Instantiate(prefab);
            go.transform.SetParent(ObjectParent);
            go.transform.position = Vector3.zero;
            return go;
        }
        else
        {
            return null;
        }
    }
}
