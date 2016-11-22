using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeFromWhite : MonoBehaviour
{
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void FixedUpdate()
    {
        Color c = image.color;
        c.a = Mathf.Lerp(c.a, 0, 0.02f);
        
        image.color = c;
    }
}
