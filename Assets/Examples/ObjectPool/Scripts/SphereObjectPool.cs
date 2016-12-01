using UnityEngine;
using System.Collections;
using Game;
using Game.ObjectPool;

namespace Example
{
    public class SphereObjectPool : ObjectPool
    {
        protected override GameObject InstantiatePooledObject()
        {
            return PrefabFactory.Instance.InstantiatePrefab(PrefabFactory.Instance.ExampleGameObjectPrefab);
        }
    }
}

