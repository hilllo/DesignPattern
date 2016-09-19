using UnityEngine;
using System.Collections;
using Game.Pattern;

namespace Example
{
    public class PooledSphere : PooledObject
    {
        [SerializeField]
        private float _Life;

        private float _StartTime;

        void OnEnable()
        {
            this._StartTime = Time.time;
        }

        void Update()
        {
            this.Deactive();
        }

        public override void Deactive()
        {
            if (Time.time - this._StartTime > this._Life)
                this.gameObject.SetActive(false);
        }
    }
}


