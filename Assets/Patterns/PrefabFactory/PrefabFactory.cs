using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game.Pattern
{
    /// <summary>
    /// Note: PrefabFactory prefab **HAS TO** be in the hierarchy of the first scene
    /// </summary>
    
    // TODO: So far cannot generated different prefabs with same component.

    public class PrefabFactory : Singleton<PrefabFactory> {

        #region Prefab Fields 

        // Example
        [SerializeField]
        private GameObject PooledObjectPrefab;

        #endregion Prefab Fields

        #region Fields

        /// <summary>
        /// Dictionary of all prefabs
        /// </summary>
        private Dictionary<System.Type, GameObject> PrefabDictionary;

        #endregion Fields

        #region MonoBehaviour

        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            this.PrefabDictionary = new Dictionary<System.Type, GameObject>();

            // Note: Add all prefabs in PrefabDictionary below
            // Example
            this.PrefabDictionary.Add(typeof(PooledObject), this.PooledObjectPrefab);            
        }

        #endregion MonoBehaviour

        #region Instantiate

        public T InstantiatePrefab<T>()
        {
            if (!this.PrefabDictionary.ContainsKey(typeof(T)))
                throw new System.ApplicationException(string.Format("{0} prefab is expected to be set on PrefabFactory.", typeof(T).ToString()));

            GameObject value;
            this.PrefabDictionary.TryGetValue(typeof(T), out value);
            GameObject obj = Instantiate(value);

            T t = obj.GetComponent<T>();

            if(t == null)
                throw new System.ApplicationException(string.Format("{0} component is expected to be set on {1} prefab.", typeof(T).ToString(), obj.name));

            return t;
        }

        #endregion Instantiate
    }
}

