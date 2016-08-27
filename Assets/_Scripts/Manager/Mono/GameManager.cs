using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
//    public static GameManager Instance = null;

    private Transform _ballPoint;//球的挂载点
    private List<Ball> _snakeSegment = null;//段数
    private List<Ball> _deleteQueue = null;//消除球队列
    private List<Ball> _backQueue = null;//回退球队列

    void Awake ()
	{
//	    Instance = this;

        _ballPoint = transform.Find("BallPoint(Dynamic)");

        _snakeSegment = new List<Ball>();
        _deleteQueue  = new List<Ball>();
        _backQueue    = new List<Ball>();
	}

    IEnumerator Start()
    {
        StartCoroutine(LoadConfigManager.LoadConfigData());

        yield return new WaitForSeconds(2.0f);

        PlayBackgroundMusic();// 初始背景音效

        PoolManager.GetInstance().InitPool(new object[] { _ballPoint });

        yield return new WaitForSeconds(0.2f);

//        InvokeRepeating("CreateActiveBall", 0.0f, 0.2f);
    }
	
	void Update ()
	{
	    if (FuncManager.ballState == BallState.FirstStart)
	    {
	        
	    }

	    UpdateBallPosition();
        UpdateBallRotation();
	}

    private void PlayBackgroundMusic()
    {
        
    }

    private void UpdateBallPosition()
    {
        
    }

    private void UpdateBallRotation()
    {
        
    }

    private void CreateActiveBall()
    {
        PoolManager.GetInstance().CreateActiveBall();
    }

    private void CheckFirstSegmentAttack()
    {
        if (_snakeSegment.Count == 0)
        {
            Ball b = PoolManager.GetInstance().CreateActiveBall();
            _snakeSegment.Add(b);
            return;
        }

        Ball ptr = _snakeSegment[0];
        if (ptr.IsNotAtStartPoint())
        {
            
        }
    }
}
