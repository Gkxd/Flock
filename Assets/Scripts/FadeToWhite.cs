using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeToWhite : MonoBehaviour
{

    public string nextScene;
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void FixedUpdate()
    {
        Color c = image.color;
        c.a = Mathf.Lerp(c.a, 1, 0.02f);

        if (c.a >= 0.99f)
        {
            SceneManager.LoadScene(nextScene);
        }
        image.color = c;
    }
}
