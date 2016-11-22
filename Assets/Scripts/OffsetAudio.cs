using UnityEngine;
using System.Collections;

public class OffsetAudio : MonoBehaviour {

    public float time;

    private AudioSource audioSource;
    
	void Start () {
        audioSource = GetComponent<AudioSource>();
        audioSource.time = time;
	}
}
