using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace UM.EventsSystem
{
    [CreateAssetMenu(menuName = "UM/Events/Void Event")]
    public class UMEvent : UMEventBase
    {
        [SerializeField] private UnityEvent inspectorListeners= new UnityEvent();

        private void Awake()
        {
            inspectorListeners ??= new UnityEvent();
        }

        public void Register(UnityAction listener)
        {
            listeners.Add(listener);
            inspectorListeners.AddListener(listener);
        }

        public void Unregister(UnityAction listener)
        {
            if (listeners.Remove(listener))
                inspectorListeners.RemoveListener(listener);
        }
        [Button]
        public virtual void Raise()
        {
            inspectorListeners?.Invoke();
        }
    }
}