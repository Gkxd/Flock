using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleTextEffect : MonoBehaviour {

    public AnimationCurve curve;
    public Gradient color;

    private Image image;
    private float alpha = 1;


	void Start () {
        image = GetComponent<Image>();
	}
	
    void FixedUpdate() {
        if (GameState.IsPlaying)
        {
            alpha *= 0.97f;
            if (alpha < 0.01f)
            {
                gameObject.SetActive(false);
            }
        }
    }

	void Update () {
        Color c = color.Evaluate(curve.Evaluate(Time.time));
        c.a *= alpha;

        image.color = c;
	}
}
