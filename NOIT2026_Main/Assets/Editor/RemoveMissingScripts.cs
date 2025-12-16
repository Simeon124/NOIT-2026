using UnityEngine;
using UnityEditor;

public class RemoveMissingScripts :  MonoBehaviour
{
    [MenuItem("Tools/Remove All Missing Scripts")]
    static void RemoveAll()
    {
        var objects = FindObjectsOfType<GameObject>();
        int count = 0;
        foreach (var go in objects)
        {
            count += GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
        }
        Debug.Log($"Removed {count} missing scripts from {objects.Length} objects.");
    }
}