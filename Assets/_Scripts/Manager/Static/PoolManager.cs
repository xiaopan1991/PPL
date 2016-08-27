using UnityEngine;
using System.Collections;
using Assets._Scripts.Tools;

public class PoolManager
{
    private static PoolManager _instance = null;

    public ObjectPool<Ball> BallPool { get; private set; }
    public ObjectPool<Object> PerfabPool { get; private set; }

    private object[] list = null;

    public static PoolManager GetInstance()
    {
        return _instance ?? (_instance = new PoolManager());
    }

    private PoolManager()
    {
        this.list = null;
    }

    public void InitPool(params object[] list)
    {
        this.list = list;

        InitPrefabPool();   //   初始化球对象池
        InitObjectPool();   //   初始化球预制体池
    }
    private void InitPrefabPool()
    {
        PerfabPool = new ObjectPool<Object>(NewInstancePrefab);
    }

    private void InitObjectPool()
    {
        BallPool = new ObjectPool<Ball>(NewInstanceBall, 100);
    }

    private Object[] NewInstancePrefab()
    {
        return Resources.LoadAll("Prefabs/Ball/");
    }

    private Ball NewInstanceBall()
    {
        GameObject g = Object.Instantiate(GetBallPrefab()) as GameObject;
        g.transform.parent = this.list[0] as Transform;

        Ball ball = new Ball(g);
        return ball;
    }

    private Object GetBallPrefab()
    {
        return PerfabPool.GetObject(FuncManager.RandomBallTypeKey());
    }

    public Ball CreateActiveBall()
    {
        Ball ball = BallPool.GetObject().OnInit(FuncManager.RandomBallType());
        return ball;
    }
}
