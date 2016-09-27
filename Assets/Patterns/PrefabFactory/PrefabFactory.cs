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
        public GameObject PooledObjectPrefab;

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

            // Build Dictionary
            this.PrefabDictionary = new Dictionary<System.Type, GameObject>();

            // Note: Add all prefabs in PrefabDictionary below
            // Example
            this.PrefabDictionary.Add(typeof(PooledObject), this.PooledObjectPrefab);            
        }

        #endregion MonoBehaviour

        #region Instantiate

        public GameObject InstantiatePrefab(System.Type type)
        {
            if (!this.PrefabDictionary.ContainsKey(type))
                throw new System.ApplicationException(string.Format("{0} Field is expected to be set on PrefabFactory.", type.ToString()));

            GameObject value;
            this.PrefabDictionary.TryGetValue(type, out value);
            
            if(value == null)
                throw new System.ApplicationException(string.Format("{0} Prefab is expected to be set on PrefabFactory.", type.ToString()));

            return Instantiate(value);
        }

        #endregion Instantiate
    }
}

