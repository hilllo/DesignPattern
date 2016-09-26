using UnityEngine;
using System.Collections;

namespace Game.Pattern
{
    public class PooledObject : MonoBehaviour
    {
        public enum Type
        {
            NONE = 0,       // Immortal
            LIFETIME = 1,   // Life determined by both lifetime
            CONDITION = 2,  // Life determined by condition
            BOTH = 3        // Life determined by both lifetime and condition
        }

        #region Fields

        /// <summary>
        /// Backing Field of Type
        /// </summary>
        [SerializeField]
        private Type _Type;

        /// <summary>
        /// Backing Field of the Lifetime of this object. 
        /// </summary>
        [SerializeField]
        private float _Lifetime;

        /// <summary>
        /// Backing Field of TimeStamp of OnEnable
        /// </summary>
        private float _OnEnableTimeStamp;

        #endregion Fields

        #region MonoBehaviour

        /// <summary>
        /// OnEnable this instance
        /// </summary>
        protected virtual void OnEnable()
        {
            // TODO: Use Global time from GameManager
            this._OnEnableTimeStamp = Time.time;
        }

        /// <summary>
        /// Update this instance
        /// </summary>
        void Update()
        {
            this.CheckDeactive();
        }

        #endregion MonoBehaviour

        /// <summary>
        /// Check if this object is going to be deactive
        /// </summary>
        private void CheckDeactive()
        {
            if (this._Type == Type.NONE)
                return;

            if((this._Type & Type.LIFETIME ) != 0)
            {
                // TODO: Use Global time from GameManager
                if (Time.time - this._OnEnableTimeStamp > this._Lifetime)
                    this.gameObject.SetActive(false);
            }

            if ((this._Type & Type.CONDITION) != 0)
            {
                if(this.CheckDeactiveCondition())
                    this.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Check if this object is going to be deactive by condition. 
        /// </summary>
        public virtual bool CheckDeactiveCondition()
        {
            // Override this function in subclass if there's any
            return false;            
        }
    }
}

