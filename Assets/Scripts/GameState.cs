using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class GameState : MonoBehaviour
{

    public static int FlockSize;
    public static bool IsPlaying;
    public static bool DoNotSpeedUp;
    public static bool IsGameEnd;
    public static float StartTime;
    public static float TotalTime;

    public static float FlockPercent { get { return (FlockSize - 1f) / 60; } }

    public ColorCorrectionCurves ccc;
    public GameObject sceneTransition;

    void Start()
    {
        FlockSize = 1;
        IsPlaying = false;
        DoNotSpeedUp = false;
        IsGameEnd = false;
    }

    void Update()
    {
        if (!IsGameEnd)
        {
            if (!IsPlaying)
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    IsPlaying = true;
                    DoNotSpeedUp = true;
                    StartTime = Time.time;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    DoNotSpeedUp = false;
                }
            }
        }

        if (FlockSize == 61)
        {
            IsPlaying = false;
            IsGameEnd = true;
            TotalTime = Time.time - StartTime;
            sceneTransition.SetActive(true);
        }
        ccc.saturation = Mathf.Lerp(0.1f, 0.78f, FlockPercent);
    }
}
