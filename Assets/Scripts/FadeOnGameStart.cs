using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOnGameStart : MonoBehaviour {

    private Image image;

    private float alpha;

	void Start () {
        image = GetComponent<Image>();
        alpha = image.color.a;
	}


    void FixedUpdate()
    {
        if (GameState.IsPlaying)
        {
            alpha *= 0.97f;
            if (alpha < 0.01f)
            {
                gameObject.SetActive(false);
            }
            Color c = image.color;
            c.a = alpha;
            image.color = c;
        }
    }
}
