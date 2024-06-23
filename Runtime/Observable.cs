using System;
using System.Collections.Generic;

namespace CodeForFun.Patterns
{
    [Serializable]
    public class Observable<T>
    {
        public event Action<T> ValueChanged;
        T value;

        public T Value {
            get => value;
            set => Set(value);
        }

        public static implicit operator T(Observable<T> observable) => observable.value;

        public Observable(T value, Action<T> onValueChanged = null) {
            this.value = value;

            if (onValueChanged != null)
                ValueChanged += onValueChanged;
        }

        public void Set(T newValue) {
            if (EqualityComparer<T>.Default.Equals(value, newValue))
                return;
            value = newValue;
            Invoke();
        }
    
        public void Invoke() => ValueChanged?.Invoke(value);

        public void AddListener(Action<T> handler) => ValueChanged += handler;

        public void RemoveListener(Action<T> handler) => ValueChanged -= handler;

        public void Dispose() {
            ValueChanged = null;
            value = default;
        }
    }
}