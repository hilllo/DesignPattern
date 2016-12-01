using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.ObjectPool;
using System.Reflection;

namespace Game
{
    /// <summary>
    /// Note: PrefabFactory prefab **HAS TO** be in the hierarchy of the first scene
    /// </summary>
    
    // TODO: So far cannot generated different prefabs with same component.

    public class PrefabFactory : Singleton<PrefabFactory> {

        #region Prefab Fields 

        // Example
        public PooledObject PooledObjectPrefab;
        public GameObject PooledObjectGameObjectPrefab;

        #endregion Prefab Fields

        #region Fields

        /// <summary>
        /// Dictionary of all prefabs
        /// </summary>
        private Dictionary<System.Type, UnityEngine.Object> PrefabDictionary;

        #endregion Fields

        #region MonoBehaviour

        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            // Update everytime increasing PrefabDictionary
            int expectedSize = 1;

            // Build Dictionary
            this.PrefabDictionary = new Dictionary<System.Type, UnityEngine.Object>(expectedSize);

            // Note: Add all prefabs in PrefabDictionary below
            // Example
            this.PrefabDictionary.Add(typeof(PooledObject), this.PooledObjectPrefab);            
        }

        #endregion MonoBehaviour

        #region Instantiate

        public T Instantiate<T>() where T : UnityEngine.MonoBehaviour
        {
            System.Type type = typeof(T);
            UnityEngine.Object value;
            if(!this.PrefabDictionary.TryGetValue(type, out value))
                throw new System.ApplicationException(string.Format("Expected Field {0} has been set on PrefabFactory.", type.ToString()));

            if (value == null)
                throw new System.ApplicationException(string.Format("Expected Prefab {0} has been set on PrefabFactory.", type.ToString()));

            GameObject instance = Instantiate(value) as GameObject;

            return instance.GetComponent<T>();
        }

        public T Instantiate<T>(GameObject prefab) where T : UnityEngine.MonoBehaviour
        {
            GameObject newGameObject = Instantiate(prefab);
            T component = newGameObject.GetComponent<T>();
            if (component == null)
                throw new System.ApplicationException(string.Format("Expected Component {0} on {1}", typeof(T).Name, prefab.name));

            return component;
        }

        public UnityEngine.Object InstantiatePrefab<T>()
        {
            System.Type type = typeof(T);
            UnityEngine.Object value;
            if (!this.PrefabDictionary.TryGetValue(type, out value))
                throw new System.ApplicationException(string.Format("Expected Field {0} has been set on PrefabFactory.", type.ToString()));

            if (value == null)
                throw new System.ApplicationException(string.Format("Expected Prefab {0} has been set on PrefabFactory.", type.ToString()));

            UnityEngine.Object instance = Instantiate(value);

            MethodInfo awakeMethod = instance.GetType().GetMethod("Awake", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (awakeMethod != null)
                awakeMethod.Invoke(instance, null);

            MethodInfo startMethod = instance.GetType().GetMethod("Start", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (startMethod != null)
                startMethod.Invoke(instance, null);

            //MethodInfo onEnableMethod = instance.GetType().GetMethod("OnEnable", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            //if (onEnableMethod != null)
            //    onEnableMethod.Invoke(instance, null);

            return instance;
        }

        #endregion Instantiate
    }
}

