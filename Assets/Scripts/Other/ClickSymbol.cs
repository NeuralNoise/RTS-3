using UnityEngine;
using System.Collections;

public class ClickSymbol : MonoBehaviour {
    public float narrowSpeed = 1f;
    private const float MIN_SCALE = 0.05f;
    
	void Update () {
        transform.localScale *= (1 - Time.deltaTime * narrowSpeed);

        if (transform.localScale.x < MIN_SCALE)
            Destroy(gameObject);
	}
}
