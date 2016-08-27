//====================================================================================
//
// Author: xiaopan
//
// Purpose: 对象池
//
//====================================================================================


using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Assets._Scripts.Tools
{
    public class ObjectPool<T>
    {
        private const int DefaultIncrease = 3;
        private readonly Queue<T> _pool = new Queue<T>();
        private readonly Dictionary<string, T> _dictionary = new Dictionary<string, T>();
        private readonly System.Func<T> _instanceFunc;
        private readonly System.Func<T[]> _Func;

        /// <summary>
        /// pool是个队列
        /// </summary>
        /// <param name="func"></param>
        /// <param name="initNumber"></param>
        public ObjectPool(System.Func<T> func, int initNumber)
        {
            this._instanceFunc = func;
            InstanceObjectNumber(initNumber);
        }
        /// <summary>
        /// pool是个字典
        /// </summary>
        /// <param name="func"></param>
        public ObjectPool(System.Func<T[]> func)
        {
            this._Func = func;
            var prefabs = _Func();
            GameObject prefab = null;
            foreach (var pre in prefabs)
            {
                prefab = pre as GameObject;
                if (prefab != null)
                {
                    _dictionary.Add(prefab.name, pre);
                }
            }
            prefabs = null;
            prefab = null;
        }

        /// <summary>
        /// 从队列中获取对象
        /// </summary>
        /// <returns></returns>
        public T GetObject()
        {
            while (true)
            {
                if (_pool.Count > 0)
                {
                    return _pool.Dequeue();
                }

                InstanceObjectNumber(DefaultIncrease);
            }
        }
        /// <summary>
        /// 从字典中获取对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetObject(string key)
        {
            T value = default(T);
            if (!_dictionary.TryGetValue(key, out value))
            {
                Debug.LogErrorFormat("error: {0}",key);
            }
            return value;
        }

        /// <summary>
        /// 放置对象
        /// </summary>
        /// <param name="obj"></param>
        public void SetObject(T obj)
        {
            _pool.Enqueue(obj);
        }

        /// <summary>
        /// 生成对象，并加入对象池
        /// </summary>
        /// <param name="n"></param>
        void InstanceObjectNumber(int n)
        {
            var i = n;
            while (i-- > 0)
            {
                _pool.Enqueue(_instanceFunc());
            }
        }

        public int GetPoolLength()
        {
            return _pool.Count;
        }
    }
}
