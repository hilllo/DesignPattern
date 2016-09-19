using UnityEngine;
using System.Collections;

namespace Game.Pattern
{
    abstract public class PooledObject : MonoBehaviour
    {
        public abstract void Deactive();
    }
}

