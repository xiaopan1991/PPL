using UnityEngine;
using System.Collections;
using DG.Tweening;
using SWS;

public class Test_PathInput : MonoBehaviour
{

    public float speed = 1.0f;
    private float _speed;
    
    private float progress = 0f;
    private bool reverse = false;
    private bool pause = false;
    private splineMove move;

	
	void Start () {
        move = GetComponent<splineMove>();
        move.StartMove();
        move.Pause();
        progress = 0f;
	    _speed = speed;
	}

    public void SetReverse(bool b)
    {
        this.reverse = b;
    }

    public void ChangeSpeed(float value)
    {
        if (!pause)
        {
            speed = value;
        }
        _speed = value;
    }

    public void Pause()
    {
        pause = !pause;
        speed = pause ? 0 : _speed;
    }
	
	
	void Update () {

        float duration = move.tween.Duration();

	    if (!reverse)
	    {
            progress += Time.deltaTime * speed;
            progress = Mathf.Clamp(progress, 0, duration);
            move.tween.fullPosition = progress;
	    }
	    else
	    {
            progress -= Time.deltaTime * speed;
            progress = Mathf.Clamp(progress, 0, duration);
            move.tween.fullPosition = progress;
	    }
	}
}
