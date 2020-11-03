using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LowTeeGames
{
    public abstract class LTSingleton<T> : MonoBehaviour where T : Component 
    {
        private static T instance;

        public static T Instance => instance;

        protected virtual void Awake()
        {
            SetAsSingleton();
        }

        private void SetAsSingleton()
        {
            instance = this as T;
        }
    }
}
