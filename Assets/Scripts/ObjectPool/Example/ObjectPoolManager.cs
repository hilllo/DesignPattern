using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Pattern;

namespace Example
{
    public class ObjectPoolManager : Singleton<ObjectPoolManager>
    {
        /// <summary>
        /// Serialize all pools in the project here
        /// </summary>
        #region Fields

        /// <summary>
        /// Used for testing
        /// </summary>
        [SerializeField]
        private ObjectPool SpherePool;

        #endregion Fields

        /// <summary>
        /// Used for testing
        /// </summary>
        void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                this.SpherePool.Spawn();
            }
        }
    }
}

