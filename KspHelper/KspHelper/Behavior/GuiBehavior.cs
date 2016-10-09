using System;

namespace KspHelper.Behavior
{
    public class GuiBehavior: KspBehavior
    {
        public Action Draw { get; set; }

        protected override void OnGUI()
        {
            Draw?.Invoke();
        }
    }

    public class GuiBehavior<T>: KspBehavior where T: new() 
    {
        private T _data;
        private bool _isInitialized;

        public void Initialize(T data)
        {
            _data = data;
            _isInitialized = true;
        }

        public Action<T> Draw { get; set; }

        protected override void OnGUI()
        {
            if (!_isInitialized)
            {
                throw new InvalidOperationException("Parameter data is not initialized");
            }

            Draw?.Invoke(_data);
        }
    }


}
