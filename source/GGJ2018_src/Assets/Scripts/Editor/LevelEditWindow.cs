using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelEditWindow : EditorWindow
{
    Vector2 scroll = Vector2.zero;

    /// <summary>
    /// Window drawing operations
    /// </summary>
    void OnGUI()
    {
        scroll = EditorGUILayout.BeginScrollView(scroll);

        LevelInfo[] levels = Resources.LoadAll<LevelInfo>("Levels");
        for(int i = 0; i < levels.Length; i++)
        {
            LevelInfo level = levels[i];
            EditorGUILayout.BeginHorizontal();
            level.levelNumber = EditorGUILayout.IntField("Level", level.levelNumber);
            EditorGUILayout.EndHorizontal();

        }

        EditorGUILayout.EndScrollView();
    }
    

    /// <summary>
    /// Retrives the TransformUtilities window or creates a new one
    /// </summary>
    [MenuItem("Tools/Level Manager")]
    static void Init()
    {
        LevelEditWindow window = (LevelEditWindow)EditorWindow.GetWindow(typeof(LevelEditWindow));
        window.Show();
    }
}
