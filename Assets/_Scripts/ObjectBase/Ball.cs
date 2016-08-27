using UnityEngine;
using System.Collections;
using System.Threading;
using Assets._Scripts.Funciton;
using UnityEditor;

public class Ball
{
    private readonly GameObject _ball;
    private readonly Transform _transform;
    private BallData _ballData;

    private readonly BallRotation _ballRotation = null;

    public BallType Type { get; private set; }

    public Ball(GameObject obj)
    {
        this._ball = obj;
        this._transform = obj.transform;
//        this._ball.SetActive(false);
        RandomPos();
        this._ballRotation = this._ball.GetComponent<BallRotation>();
    }


    public void RandomPos()
    {
        this._transform.position = new Vector3(
            5 * Random.value,
            7 * Random.value,
            9 * Random.value);
    }

    public Ball OnInit(BallType t)
    {
        this.Type = t;
        RandomPos();
        
        Pre = null;
        Next = null;
        IsNextDelete = false;

        this._ballData = ConfigDataManager.GetInstance().GetBallDataByType((int)t);
        this._ball.SetActive(true);
        return this;
    }

    /*private void ResetMaterials(BallType t)
    {
        RandomPos();
        
        var offset = this._ballData.offset.Split(',');
        _materialsArr = this._ball.GetComponent<MeshRenderer>().materials;
        _materialsArr[0].mainTextureOffset = new Vector2(float.Parse(offset[0]), float.Parse(offset[1]));
    }*/

    public bool IsNextDelete = false;
    public Ball Pre { get; private set; }
    public Ball Next { get; private set; }
    public void SetNext(Ball ball) { Next = ball; }
    public void SetPre(Ball ball) { Pre = ball; }

    public Ball Head
    {
        get
        {
            Ball ptr = this;
            do
            {
                if (ptr.Pre == null) return ptr;
                ptr = ptr.Pre;
            } while (true);
        }
    }

    public Ball Tail
    {
        get
        {
            Ball ptr = this;
            do
            {
                if (ptr.Next == null) return ptr;
                ptr = ptr.Next;
            } while (true);
        }
    }

    public int TheSameColorQueue(out Ball _pre, out Ball _next)
    {
        Ball ptr;
        int count = 1;
        
        ptr = _pre = _next = this;

        do
        {
            if (ptr.Pre != null && ptr.Pre.Type == Type)
            {
                ptr = _pre = ptr.Pre;
                count++;
            }
            else
            {
                break;
            }
        } while (ptr != null);

        ptr = this;
        do
        {
            if (ptr.Next != null && ptr.Next.Type == Type)
            {
                ptr = _next = ptr.Next;
                count++;
            }
            else
            {
                break;
            }
        } while (ptr != null);

        return count;
    }

    public void SetBallPauseRotation(bool pause)
    {
        this._ballRotation.Pause = pause;
    }

    public void ChangeBallRotaionRate(bool quick)
    {
        this._ballRotation.Rate = quick ? 10 : 1;
    }

    public bool IsNotAtStartPoint()
    {
        return true;
    }

    public bool IsNotAtFailurePoint()
    {
        return false;
    }
}
