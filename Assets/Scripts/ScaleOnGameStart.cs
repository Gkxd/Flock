using UnityEngine;
using System.Collections;

public class ScaleOnGameStart : MonoBehaviour {

    private float scale = 1;
    
	
	void Update () {
	    if (GameState.IsPlaying)
        {
            if (scale < 4)
            {
                scale += scale * 0.5f * Time.deltaTime;
                transform.localScale = new Vector3(scale, scale, 1);
            }
        }
	}
}
