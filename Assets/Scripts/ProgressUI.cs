using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;

public class ProgressUI : MonoBehaviour {
    
    public Text text;
    public Image image;

    private float alpha;
    private int progress;

	// Use this for initialization
	void Start () {

	}
	
    void FixedUpdate()
    {
        alpha = Mathf.Lerp(alpha, 0, 0.03f);
        Color c = Color.white;
        c.a = alpha;
        text.color = c;
        image.color = c;
    }

	// Update is called once per frame
	void Update () {
	    if (progress != GameState.FlockSize - 1)
        {
            progress = GameState.FlockSize - 1;
            alpha = 1;

            string textString = progress + "/60";
            textString = Regex.Replace(textString, ".{1}", "$0 ").Trim();
            text.text = textString;
        }
	}
}
