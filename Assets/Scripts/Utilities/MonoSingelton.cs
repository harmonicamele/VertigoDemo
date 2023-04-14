using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.SingeltonSystem
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T instance = null;

        public static T Instance
        {
            get
            {
                if (!instance)
                {
                    instance = FindObjectOfType(typeof(T)) as T;
                }
                return instance;
            }
        }

    }
}
