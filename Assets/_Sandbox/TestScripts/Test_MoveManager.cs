using UnityEngine;
using System.Collections;
using DG.Tweening;
using SWS;

public class Test_MoveManager : MonoBehaviour {

    public GameObject walker;

    private splineMove moveRef;
    private Test_PathInput input;

    private bool reverse = false;
    private bool changeSpeed = false;
    private float initSpeed = 0.0f;

	void Start ()
	{
	    moveRef = walker.GetComponent<splineMove>();
	    input = walker.GetComponent<Test_PathInput>();
	    initSpeed = input.speed;
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}
    
    void OnGUI()
    {
        GUI.Label(new Rect(10, 30, 40, 30), "Test:");

        if (moveRef.tween != null && moveRef.tween.IsPlaying()
            && GUI.Button(new Rect(50, 25, 100, 30), "Pause"))
        {
            input.Pause();
        }

        if (moveRef.tween != null && !moveRef.tween.IsPlaying()
            && GUI.Button(new Rect(50, 25, 100, 30), "Resume"))
        {
            input.Pause();
        }

        if (moveRef.tween != null && GUI.Button(new Rect(50, 65, 100, 30), "Reverse"))
        {
            reverse = !reverse;
            input.SetReverse(reverse);
        }

        if (moveRef.tween != null && GUI.Button(new Rect(50, 105, 100, 30), "ChangeSpeed"))
        {
            changeSpeed = !changeSpeed;
            float speed = changeSpeed ? 3.0f : initSpeed;
            input.ChangeSpeed(speed);
        }
    }
}
