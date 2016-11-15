using UnityEngine;
using System.Collections;

namespace Game.Pattern
{
    public abstract class Manager<T> : MonoBehaviour
    {
        /// <summary>
        /// Instance
        /// </summary>
        public static T Instance { get; protected set; }

        #region MonoBehaviour
        /// <summary>
        /// Awake
        /// </summary>
        protected virtual void Awake()
        {
            this.SetInstance();
        }

        /// <summary>
        /// Start
        /// </summary>
        protected virtual void Start()
        {
            if (Instance == null)
                throw new System.ApplicationException(string.Format("Expected {0}.Instance to be set in Awake().", this.name));
        }

        /// <summary>
        /// OnDestory
        /// </summary>
        protected virtual void OnDestroy()
        {
            this.ReleaseInstance();

            if (Instance != null)
                throw new System.ApplicationException(string.Format("Expected {0}.Instance == null before destroying it.", this.name));

            Debug.Log(string.Format("Destroyed {0}", this.name));
        }
        #endregion MonoBehaviour

        /// <summary>
        /// Do: T.Instance = this;
        /// </summary>
        protected abstract void SetInstance();

        /// <summary>
        /// Do: T.Instance = null;
        /// </summary>
        protected abstract void ReleaseInstance();
    }
}
