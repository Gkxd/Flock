using UnityEngine;
using System.Collections;

public class RestartGame : MonoBehaviour {

    public GameObject screenTransition;

	void Update () {
	    if (Input.GetKeyDown(KeyCode.X))
        {
            screenTransition.SetActive(true);
        }
	}
}
