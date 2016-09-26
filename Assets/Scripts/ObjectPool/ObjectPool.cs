using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game.Pattern
{
    public class ObjectPool : MonoBehaviour
    {
        #region Fields
        /// <summary>
        /// Backing Field of PooledObjectPrefab
        /// TODO: Move this into PrefabFactory
        /// </summary>
        [SerializeField]
        private GameObject _PooledObjectPrefab;

        /// <summary>
        /// The minimum amount of PooledObjects in the pool. 
        /// Business Rule: The pool will be initialized within MinAmount size. If exceeded, the size will grow automatically until it reaches MaxAmount.
        /// </summary>
        [SerializeField]
        private int _MinSize;

        /// <summary>
        /// The maximum amount of PooledObjects in the pool. Note: If MinAmount == MaxAmount, the size of the pool is not allowed to grow.
        /// Business Rule: The pool will be initialized within MinAmount size. If exceeded, the size will extend automatically until it reaches MaxAmount.
        /// </summary>
        [SerializeField]
        private int _MaxSize;

        /// <summary>
        /// The List of the PooledObjects
        /// </summary>
        private List<GameObject> _PooledObjects;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Backing Field of PooledObjectPrefab
        /// TODO: Move this into PrefabFactory
        /// </summary>
        public int Size
        {
            get
            {
                return this._PooledObjects.Count;
            }
        }

        #endregion Properties

        #region MonoBehaviour

        /// <summary>
        /// Enable the instance
        /// </summary>
        protected virtual void OnEnable()
        {
            this._PooledObjects = new List<GameObject>();

            for (int i = 0; i < this._MinSize; i++)
            {
                // TODO: Use PrefabFactory
                GameObject obj = (GameObject)Instantiate(this._PooledObjectPrefab);
                obj.transform.SetParent(this.transform);
                this._PooledObjects.Add(obj);
                obj.SetActive(false);
            }
        }

        #endregion MonoBehaviour

        #region Spawn
        /// <summary>
        /// Spawn a PooledObject
        /// </summary>
        public void Spawn()
        {
            this.Spawn(new Vector3(0f, 0f, 0f), Quaternion.identity);
        }

        /// <summary>
        /// Spawn a PooledObject
        /// </summary>
        public void Spawn(Vector3 position)
        {
            this.Spawn(position, Quaternion.identity);
        }

        /// <summary>
        /// Spawn a PooledObject
        /// </summary>
        public void Spawn(Vector3 position, Vector3 rotation)
        {
            this.Spawn(position, Quaternion.Euler(rotation));
        }

        /// <summary>
        /// Spawn a PooledObject
        /// </summary>
        public void Spawn(Vector3 position, Quaternion rotation)
        {
            GameObject obj = this.GetAvailablePooledObject();
            if (obj != null)
            {
                obj.transform.position = position;
                obj.transform.rotation = rotation;
                obj.SetActive(true);
            }
        }

        /// <summary>
        /// Try to get an available PooledObject
        /// </summary>
        private GameObject GetAvailablePooledObject()
        {
            int i;
            for (i = 0; i < this._PooledObjects.Count; i++)
            {
                if (!this._PooledObjects[i].activeInHierarchy)
                    return this._PooledObjects[i];
            }

            if (i < this._MaxSize)
            {
                // TODO: Use PrefabFactory
                GameObject obj = (GameObject)Instantiate(this._PooledObjectPrefab);
                obj.transform.SetParent(this.transform);
                this._PooledObjects.Add(obj);
                return obj;
            }

            Debug.Log(string.Format("Failed to instantiate more than {0} {1} in {2} Time: {3}", this._MaxSize.ToString(), this._PooledObjectPrefab.name, this.gameObject.name, Time.time.ToString()));
            return null;
        }

        #endregion Spawn

        /// <summary>
        /// Deactive all PooledObjects in the pool
        /// </summary>
        public void DeactiveAll()
        {
            foreach (GameObject obj in this._PooledObjects)
            {
                if (obj.activeInHierarchy)
                    obj.SetActive(false);
            }
        }
    }
}


