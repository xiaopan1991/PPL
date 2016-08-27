//====================================================================================
//
// Author: xiaopan
//
// Purpose: 对应BallData配置表
//
//====================================================================================

using System;
using UnityEngine;
using System.Collections;

public class BallData
{
    public int id;

    public float hurt;

    public string color;

    public string offset;

    public override string ToString()
    {
        return String.Format("{0}-->id(type):{1}, hurt:{2}, color:{3}, material:{4}", 
            "BallData", this.id, this.hurt, this.color, this.offset);
    }
}
