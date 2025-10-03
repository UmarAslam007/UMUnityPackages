using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UM.EventsSystem
{
    /// <summary>
    /// Abstract base class for ScriptableObject events.
    /// </summary>
    public abstract class UMEventBase : ScriptableObject
    {
        protected readonly HashSet<System.Delegate> listeners = new HashSet<System.Delegate>();
    
        [ShowInInspector, ReadOnly, FoldoutGroup("Runtime")]
        private List<string> Listeners
        {
            get
            {
                var list = new List<string>();
                foreach (var listener in listeners)
                {
                    if (listener.Method.DeclaringType != null)
                        list.Add(listener.Method.DeclaringType.Name + "." + listener.Method.Name);
                }
                return list;
            }
        }
        
        /// <summary>
        /// Clear all listeners (both inspector and code).
        /// </summary>
        public virtual void Clear()
        {
            listeners.Clear();
        }
    }
}
