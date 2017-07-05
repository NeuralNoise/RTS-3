using UnityEngine;
using System.Collections;

public class ResManager : MonoBehaviour {
    public static ResManager Instance = null;

    public GameObject Infantry;
    public GameObject Helicopter;

    void Awake()
    {
        Instance = this;
    }

	void Start () {
	
	}
	
	void Update () {
	
	}
}
