using UnityEngine;
using System.Collections;

public enum BallType
{
    Black = 0,
    Blue = 1,
    Green = 2,
    Purple = 3,
    Red = 4,
    Yellow = 5,
    Color = 6,
}

public enum BallState
{
    FirstStart=0,   //第一个球前进
    LastBack,       //最后一个球回缩
    Pause,          //停滞
}

public class FuncManager {

    public static BallState ballState = BallState.FirstStart;

    public static BallType RandomBallType()
    {
        int type = RandomBallTypeIndex();
        return (BallType)type;
    }

    public static string RandomBallTypeKey()
    {
        return string.Format("Ball_{0}", RandomBallTypeIndex());
    }

    public static int RandomBallTypeIndex()
    {
        return Random.Range(0, ConfigDataManager.GetInstance().GetBallDataKindNumber());
    }
}
