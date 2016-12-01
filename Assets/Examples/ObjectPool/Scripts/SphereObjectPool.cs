using UnityEngine;
using System.Collections;
using System;

namespace Game.ObjectPool
{
    public class SphereObjectPool : ObjectPool
    {
        protected override GameObject InstantiatePooledObject()
        {
            UnityEngine.Object obj = PrefabFactory.Instance.InstantiatePrefab<PooledObject>();
            GameObject gameObj = obj as GameObject;
            return gameObj;
        }
    }
}

