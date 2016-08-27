using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConfigDataManager
{
    private static ConfigDataManager _instance = null;

    private readonly Dictionary<int, BallData> _ballDataDic = null;
    private readonly Dictionary<int, BallFireData> _ballFireDataDic = null;

    public static ConfigDataManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new ConfigDataManager();
        }
        return _instance;
    }

    private ConfigDataManager()
    {
        if (_ballDataDic == null)
        {
            _ballDataDic = new Dictionary<int, BallData>();
            _ballFireDataDic = new Dictionary<int, BallFireData>();
        }
    }

    public void AddToDictionary(int id, object obj, int rowCount)
    {
        if (obj.GetType() == typeof(BallData))
        {
            _ballDataDic.Add(id, (BallData)obj);
        }
        else if (obj.GetType() == typeof(BallFireData))
        {
            _ballFireDataDic.Add(id, (BallFireData)obj);
        }
    }

    public int GetBallDataKindNumber()
    {
        return this._ballDataDic.Count;
    }

    public BallData GetBallDataByType(int key)
    {
        BallData value = null;
        if (!_ballDataDic.TryGetValue(key, out value))
        {
            Debug.LogErrorFormat("BallData.xls中不存在key为{0}的数据", key);
        }
        return value;
    }

    //TODO  Test
    public void TestPrint()
    {
        IDictionaryEnumerator enumerator = _ballDataDic.GetEnumerator();
        while (enumerator.MoveNext())
        {
            var key = enumerator.Key.ToString();
            var value = enumerator.Value.ToString();
            Debug.Log(value);
        }

        enumerator = _ballFireDataDic.GetEnumerator();
        while (enumerator.MoveNext())
        {
            var key = enumerator.Key.ToString();
            var value = enumerator.Value.ToString();
            Debug.Log(value);
        }
    }
}
