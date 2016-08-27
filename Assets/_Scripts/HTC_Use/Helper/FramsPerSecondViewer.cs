﻿
using UnityEngine;
using UnityEngine.UI;

public class FramsPerSecondViewer : MonoBehaviour
{
    public bool displayFPS = true;
    public int targetFPS = 90;
    public int fontSize = 32;
    public Vector3 position = Vector3.zero;
    public Color goodColor = Color.green;
    public Color warnColor = Color.yellow;
    public Color badColor = Color.red;

    private const float updateInterval = 0.5f;
    private int framesCount;
    private float framesTime;
    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
        text.fontSize = fontSize;
        text.transform.localPosition = position;
    }

    private void Update()
    {
        framesCount++;
        framesTime += Time.unscaledDeltaTime;

        if (framesTime > updateInterval)
        {
            if (text != null)
            {
                if (displayFPS)
                {
                    float fps = framesCount / framesTime;
                    text.text = string.Format("{0:F2} FPS", fps);
                    text.color = (fps > (targetFPS - 5) ? goodColor :
                                    (fps > (targetFPS - 30) ? warnColor :
                                    badColor));
                }
                else
                {
                    text.text = "";
                }
            }
            framesCount = 0;
            framesTime = 0;
        }


        if (Input.GetKeyDown(KeyCode.F))
        {
            displayFPS = !displayFPS;
        }
    }
}