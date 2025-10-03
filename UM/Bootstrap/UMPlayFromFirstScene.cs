using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class UMPlayFromFirstScene
{
    private const string MenuPath = "Tools/UM/Play From First Scene";
    private const string PrefKey = "UM_PlayFromFirstSceneEditorScript";

    static UMPlayFromFirstScene()
    {
        // Restore menu checked state from EditorPrefs
        Menu.SetChecked(MenuPath, EditorPrefs.GetBool(PrefKey, false));

        // Hook into play mode change
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    [MenuItem(MenuPath)]
    private static void Toggle()
    {
        bool enabled = !Menu.GetChecked(MenuPath);
        Menu.SetChecked(MenuPath, enabled);
        EditorPrefs.SetBool(PrefKey, enabled);
    }

    private static void OnPlayModeChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode &&
            EditorPrefs.GetBool(PrefKey, false))
        {
            // Save current scene before switching
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                // Always load scene 0 when entering Play
                EditorSceneManager.playModeStartScene = 
                    AssetDatabase.LoadAssetAtPath<SceneAsset>(
                        SceneUtility.GetScenePathByBuildIndex(0));
            }
            else
            {
                // Cancel play if user doesn't save
                EditorApplication.isPlaying = false;
            }
        }

        if (state == PlayModeStateChange.EnteredEditMode)
        {
            // Reset after play so Unity goes back to previous scene
            EditorSceneManager.playModeStartScene = null;
        }
    }
}