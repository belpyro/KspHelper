using UnityEngine;

namespace KspHelper.Behavior
{
    public abstract class BehaviorSingleton<T> : KspBehavior where T : KspBehavior
    {
        private static T _instance;

        protected override void Awake()
        {
            base.Awake();

            if (_instance != null || _instance != this)
            {
               Destroy(this.gameObject); 
            }

            _instance = gameObject.AddComponent<T>();
            DontDestroyOnLoad(this);
        }

        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;

                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    var obj = new GameObject();
                    _instance = obj.AddComponent<T>();
                }

                DontDestroyOnLoad(_instance.gameObject);

                return _instance;
            }
        }
    }
}
