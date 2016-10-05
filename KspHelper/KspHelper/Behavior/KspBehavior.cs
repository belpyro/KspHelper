﻿using UnityEngine;

namespace KspHelper.Behavior
{
    public abstract class KspBehavior : MonoBehaviour
    {
        protected virtual void Awake() { }

        protected virtual void Start() { }

        protected virtual void Update() { }

        protected virtual void OnGUI() { }

        protected virtual void OnDestroy() { }

        protected virtual void OnEnable() { }

        protected virtual void FixedUpdate() { }

        protected virtual void LateUpdate() { }
    }
}
