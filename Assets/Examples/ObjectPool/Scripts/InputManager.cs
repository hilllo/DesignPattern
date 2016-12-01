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
        private SphereObjectPool SphereObjectPool;

        #endregion Fields

        /// <summary>
        /// Used for testing
        /// </summary>
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.SphereObjectPool.Spawn();
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

