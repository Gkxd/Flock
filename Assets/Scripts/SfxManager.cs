using UnityEngine;
using System.Collections;

public class SfxManager : MonoBehaviour {

    private static SfxManager instance;

    public GameObject butterflySfx;
    
	void Start () {
        instance = this;
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            PlayButterflySfx();
        }
    }

    private static int[] possibleNotes = { 0, 5, 9 };

    public static void PlayButterflySfx()
    {
        if (instance)
        {
            GameObject sfx = (GameObject)Instantiate(instance.butterflySfx, instance.transform);
            sfx.GetComponent<AudioSource>().pitch *= Mathf.Pow(1.05946f, possibleNotes[Random.Range(0, 3)]);
            Destroy(sfx, 5);
        }
    }
}
