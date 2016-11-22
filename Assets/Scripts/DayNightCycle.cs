using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour {
    
    public AudioSource dayAudioSource;
    public AudioSource nightAudioSource;
	
	void Update () {
        if (!GameState.IsGameEnd)
        {
            transform.eulerAngles = new Vector3((Time.time * 3) % 360, 32, 0);

            RenderSettings.skybox.SetFloat("_TimeOfDay", (Time.time * 3) % 360);

            float timeOfDay = Time.time % 120;

            if (timeOfDay < 50)
            {
                dayAudioSource.volume = 1;
                nightAudioSource.volume = 0;
            }
            else if (timeOfDay < 60)
            {
                dayAudioSource.volume = 1 - (timeOfDay - 50) / 10;
                nightAudioSource.volume = 0.3f * ((timeOfDay - 50) / 10);
            }
            else if (timeOfDay < 110)
            {
                dayAudioSource.volume = 0;
                nightAudioSource.volume = 0.3f;
            }
            else
            {
                dayAudioSource.volume = (timeOfDay - 110) / 10;
                nightAudioSource.volume = 0.3f * (1 - (timeOfDay - 110) / 10);
            }
        }
	}
}
