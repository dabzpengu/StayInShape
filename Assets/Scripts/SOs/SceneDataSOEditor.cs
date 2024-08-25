using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if (UNITY_EDITOR) 
[CustomEditor(typeof(SceneDataSO))]
public class SceneDataSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var SO = (SceneDataSO)target;

        if (GUILayout.Button("UpdateSceneList", GUILayout.Height(30)))
        {
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(SceneSO).Name);  //FindAssets uses tags check documentation for more info
            List<SceneSO> newSceneList = new List<SceneSO>();
            for (int i = 0; i < guids.Length; i++)         //probably could get optimized 
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                newSceneList.Add(AssetDatabase.LoadAssetAtPath<SceneSO>(path));
            }
            SO.UpdateSceneList(newSceneList);
        }

    }
}
#endif