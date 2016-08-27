//====================================================================================
//
// Author: xiaopan
//
// Purpose: 对应BallFireData配置表
//
//====================================================================================

using System;
using UnityEngine;
using System.Collections;

public class BallFireData
{
    public string id;

    public int type;

    public float hurt;

    public override string ToString()
    {
        return String.Format("{0}-->id:{1}, type:{2}, hurt:{3}", "BallFireData", this.id, this.type, this.hurt);
    }
}
