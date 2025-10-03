using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace UM.EventsSystem
{
   // [CreateAssetMenu(menuName = "UM/Events/EventT")]
    public class UMEvent<T> : UMEventBase
    {
        [SerializeField] private UnityEvent<T> inspectorListeners = new UnityEvent<T>();

        public void Register(UnityAction<T> listener)
        {
            listeners.Add(listener);
            inspectorListeners.AddListener(listener);
        }

        public void Unregister(UnityAction<T> listener)
        {
            if (listeners.Remove(listener))
                inspectorListeners.RemoveListener(listener);
        }
        
        [Button]
        public virtual void Raise(T value)
        {
            inspectorListeners?.Invoke(value);
        }
    }
}