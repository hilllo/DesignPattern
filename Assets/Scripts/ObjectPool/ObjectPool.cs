using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game.Pattern
{
    public class ObjectPool : MonoBehaviour
    {

        #region Fields

        [SerializeField]
        private GameObject _PooledObjectPrefab;

        [SerializeField]
        private int _MinAmount;

        [SerializeField]
        private int _MaxAmount;

        private List<GameObject> _PooledObjects;

        #endregion Fields

        #region Properties

        public GameObject PooledObjectPrefab
        {
            get
            {
                return this._PooledObjectPrefab;
            }
        }

        public int Amount
        {
            get
            {
                return this._PooledObjects.Count;
            }
        }

        #endregion Properties

        #region MonoBehaviour

        void Start()
        {
            this._PooledObjects = new List<GameObject>();

            for (int i = 0; i < this._MinAmount; i++)
            {
                GameObject obj = (GameObject)Instantiate(this._PooledObjectPrefab);
                obj.transform.SetParent(this.transform);
                this._PooledObjects.Add(obj);
                obj.SetActive(false);
            }
        }

        #endregion MonoBehaviour

        public void Spawn()
        {
            this.Spawn(new Vector3(0f, 0f, 0f), Quaternion.identity);
        }

        public void Spawn(Vector3 position)
        {
            this.Spawn(position, Quaternion.identity);
        }

        public void Spawn(Vector3 position, Vector3 rotation)
        {
            this.Spawn(position, Quaternion.Euler(rotation));
        }

        public void Spawn(Vector3 position, Quaternion rotation)
        {
            GameObject obj = this.GetPooledObject();
            if (obj != null)
            {
                obj.transform.position = position;
                obj.transform.rotation = rotation;
                obj.SetActive(true);
            }
        }

        public void DeactiveAll()
        {
            foreach (GameObject obj in this._PooledObjects)
            {
                if (obj.activeInHierarchy)
                    obj.SetActive(false);
            }
        }

        private GameObject GetPooledObject()
        {
            int i;
            for (i = 0; i < this._PooledObjects.Count; i++)
            {
                if (!this._PooledObjects[i].activeInHierarchy)
                    return this._PooledObjects[i];
            }

            if (i < this._MaxAmount)
            {
                GameObject obj = (GameObject)Instantiate(this._PooledObjectPrefab);
                obj.transform.SetParent(this.transform);
                this._PooledObjects.Add(obj);
                return obj;
            }

            Debug.Log(string.Format("Failed to instantiate more than {0} {1} in {2}", this._MaxAmount.ToString(), this._PooledObjectPrefab.name, this.gameObject.name));
            return null;
        }
    }
}


