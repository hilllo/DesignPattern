using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game;
using Game.ObjectPool;
using Game.Manager;
using System;

namespace Example
{
    public class InputManager : Manager<InputManager>
    {
        /// <summary>
        /// Serialize all pools in the project here
        /// </summary>
        #region Fields
        /// <summary>
        /// Used for testing
        /// </summary>
        [SerializeField]
        private KeyCode _SpawnKey;

        /// <summary>
        /// Used for testing
        /// </summary>
        [SerializeField]
        private SphereObjectPool SphereObjectPool;

        #endregion Fields

        /// <summary>
        /// Used for testing
        /// </summary>
        void Update()
        {
            if (Input.GetKeyDown(this._SpawnKey))
            {
                this.SphereObjectPool.Spawn(new Vector3(UnityEngine.Random.Range(-10.0f, 10.0f), UnityEngine.Random.Range(-10.0f, 10.0f), UnityEngine.Random.Range(-10.0f, 10.0f)));
            }
        }


        protected override void ReleaseInstance()
        {
            InputManager.Instance = null;
        }

        protected override void SetInstance()
        {
            InputManager.Instance = this;
        }
    }
}

