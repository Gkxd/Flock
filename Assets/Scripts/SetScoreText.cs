using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;

public class SetScoreText : MonoBehaviour {

    private Text text;

	void Start () {
        text = GetComponent<Text>();

        int minutes = (int)(GameState.TotalTime / 60);
        int seconds = (int)(GameState.TotalTime % 60);

        string time = minutes.ToString("D2") + ":" + seconds.ToString("D2");
        time = Regex.Replace(time, ".{1}", "$0 ").Trim();


        text.text = time;
    }
}
