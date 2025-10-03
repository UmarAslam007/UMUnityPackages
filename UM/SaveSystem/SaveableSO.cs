using Sirenix.OdinInspector;
using UM.Saving;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UM.SaveSystem
{
    /// <summary>
    /// Abstract base ScriptableObject with GUID support and auto save/load lifecycle
    /// </summary>
    public abstract class SaveableSO<T> : ScriptableObject, ISaveable
    {
        [SerializeField, ReadOnly] private string variableName;
        [Title("Default Value")]
        [SerializeField] protected T defaultValue;

        [Title("Runtime (auto loaded/saved)")]
        [ReadOnly][SerializeField] private T value;

        public T Value
        {
            get => value;
            set => this.value = value;
        }
        [Button(ButtonSizes.Medium)]
        private void ResetToDefault()
        {
            Value = defaultValue;
            Save();
        }


#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            // Cache GUID at edit-time so it's available in builds
            string path = AssetDatabase.GetAssetPath(this);
            variableName = $"{AssetDatabase.AssetPathToGUID(path)}-{name}";
        }
#endif

        public string VariableName => variableName;

        private void OnEnable()
        {
          if(Application.isPlaying)  UMObjectDataSaver.Instance.Register(this);
            Load();
        }

        private void OnDisable()
        {
            Save();
            if(Application.isPlaying) UMObjectDataSaver.Instance.Unregister(this);
        }

        public abstract void Save();
        public abstract void Load();
    }
}
