using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game.Pattern
{
    public class ObjectPool : MonoBehaviour
    {
        public enum ObjectPoolType
        {
            BLOCK,  // Stop generating when the pool is full
            RECYCLE // Recycle objects in pool based on their TimeStamp
        }

        #region Fields      

        /// <summary>
        /// Backing Field of Type
        /// Warning: Using RECYCLE is expensive!!
        /// </summary>
        [SerializeField]
        private ObjectPoolType _Type;

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
        /// Back Field of the PooledObjectType Property
        /// Note: Override this while creating specific pool
        /// </summary>
        private System.Type _PooledObjectSystemType = typeof(PooledObject);

        /// <summary>
        /// The List of the PooledObjects
        /// </summary>
        private List<GameObject> _PooledObjects;        

        #endregion Fields

        #region Properties

        /// <summary>
        /// The type of the PooledObject
        /// </summary>
        public System.Type PooledObjectSystemType
        {
            get
            {
                return this._PooledObjectSystemType;
            }
        }

        /// <summary>
        /// The Size of the pool
        /// </summary>
        public int Size
        {
            get
            {
                return this._PooledObjects.Count;
            }
        }

        /// <summary>
        /// Type of the ObjectPool
        /// </summary>
        public ObjectPoolType Type
        {
            get
            {
                return this._Type;
            }
        }

        #endregion Properties

        #region MonoBehaviour

        private void Start()
        {
            this._PooledObjects = new List<GameObject>(this._MinSize);
            GameObject obj;

            for (int i = 0; i < this._MinSize; i++)
            {
                obj = this.AddObject();
                obj.SetActive(false);
            }
        }

        /// <summary>
        /// Disable the instance
        /// </summary>
        protected virtual void OnDisable()
        {
            this.DeactiveAll();
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
            GameObject obj = null;
            for (i = 0; i < this._PooledObjects.Count; i++)
            {
                if (!this._PooledObjects[i].activeInHierarchy)
                    obj = this._PooledObjects[i];
            }

            if (obj == null && i < this._MaxSize)
            {
                obj = this.AddObject();
                this._PooledObjects.Add(obj);
            }

            if(obj == null)
            {
                if (this.Type == ObjectPoolType.BLOCK)
                    Debug.Log(string.Format("Failed to instantiate more than {0} PooledObject in {1} on time: {2}", this._MaxSize.ToString(), this.gameObject.name, Time.time.ToString()));
                if (this.Type == ObjectPoolType.RECYCLE)
                    obj = this.transform.GetChild(0).gameObject;
            }

            // TODO: Needs optimization
            if(this.Type == ObjectPoolType.RECYCLE && obj != null)
                obj.transform.SetAsFirstSibling();

            return obj;
        }

        #endregion Spawn

        /// <summary>
        /// Add a new Object in the pool.
        /// </summary>
        protected virtual GameObject AddObject()
        {
            GameObject obj = PrefabFactory.Instance.InstantiatePrefab(this._PooledObjectSystemType);
            obj.transform.SetParent(this.transform);
            this._PooledObjects.Add(obj);
            return obj;
        }

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

        /// <summary>
        /// Destory the pool and release all PooledObject
        /// Warning: This pool is not available after calling this method!!!
        /// </summary>
        public void DestroyAll()
        {
            foreach(GameObject obj in this._PooledObjects)
            {
                Destroy(obj);
            }

            this._PooledObjects.Clear();
            Destroy(this.gameObject);
        }
    }
}


