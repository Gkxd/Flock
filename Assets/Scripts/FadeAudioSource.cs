using UnityEngine;
using System.Collections;

public class FadeAudioSource : MonoBehaviour {

    public AudioSource audioSource;
    
	void FixedUpdate () {
        audioSource.volume = Mathf.Lerp(audioSource.volume, 0, 0.03f);
	}
}
