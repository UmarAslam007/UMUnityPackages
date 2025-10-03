using System.Collections.Generic;
using UM.SaveSystem;
using UnityEngine;

#if UNITY_WEBGL && !UNITY_EDITOR
    using System.Runtime.InteropServices;
#endif 

namespace UM.Saving
{
    public class UMObjectDataSaver : MonoBehaviour
    {
    #if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void RegisterBeforeUnload();
    #endif
        
        private static UMObjectDataSaver _instance;
        public static UMObjectDataSaver Instance
        {
            get
            {
                if (_instance == null)
                {
                    if(!Application.isPlaying) return null;
                    
                    
                    var go = new GameObject("UMObjectDataSaver");
                    _instance = go.AddComponent<UMObjectDataSaver>();
                    Debug.Log($"you manger is created : {go.name} : ");
                    DontDestroyOnLoad(go);
                }
                return _instance;
            }
        }

        private readonly HashSet<ISaveable> saveables = new();

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        
        #if UNITY_WEBGL && !UNITY_EDITOR
                RegisterBeforeUnload();
        #endif

            Application.quitting += OnQuit;
        }

        private void OnDestroy()
        {
            Application.quitting -= OnQuit;
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                SaveAll();
            }
        }

        private void OnApplicationQuit()
        {
            SaveAll();
        }

        private void OnQuit()
        {
            SaveAll();
        }

        public void Register(ISaveable so)
        {
            saveables.Add(so);
        }

        public void Unregister(ISaveable so)
        {
            saveables.Remove(so);
        }

        public void SaveAll()
        {
            foreach (var so in saveables)
            {
                so.Save();
            }
            PlayerPrefs.Save();
            Debug.Log("[UMObjectDataSaver] All data saved.");
        }
    }
}