//using UnityEngine;
//using UnityEditor;
//using System.Collections.Generic;

//public class FindMissingScriptsInProject : EditorWindow
//{
//    private Vector2 scrollPosition;
//    private List<GameObject> objectsWithMissingScripts = new List<GameObject>();

//    [MenuItem("Custom/Find objects with missing scripts")]
//    public static void ShowWindow()
//    {
//        EditorWindow.GetWindow(typeof(FindMissingScriptsInProject));
//    }

//    private void OnGUI()
//    {
//        GUILayout.Label("Objects with Missing Scripts", EditorStyles.boldLabel);

//        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

//        foreach (GameObject obj in objectsWithMissingScripts)
//        {
//            EditorGUILayout.ObjectField(obj, typeof(GameObject), true);
//        }

//        EditorGUILayout.EndScrollView();

//        if (GUILayout.Button("Find Objects"))
//        {
//            FindObjectsWithMissingScripts();
//        }
//    }

//    private void FindObjectsWithMissingScripts()
//    {
//        objectsWithMissingScripts.Clear();

//        string[] allAssetPaths = AssetDatabase.GetAllAssetPaths();

//        foreach (string assetPath in allAssetPaths)
//        {
//            if (!assetPath.EndsWith(".prefab") && !assetPath.EndsWith(".unity"))
//                continue;

//            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);

//            if (prefab != null)
//            {
//                GameObject prefabInstance = PrefabUtility.LoadPrefabContents(assetPath);

//                GameObject instance = Instantiate(prefabInstance);
//                CheckMissingScripts(instance);
//                DestroyImmediate(instance);

//                PrefabUtility.UnloadPrefabContents(prefabInstance);
//            }
//        }

//        GameObject[] sceneObjects = GameObject.FindObjectsOfType<GameObject>();

//        foreach (GameObject obj in sceneObjects)
//        {
//            CheckMissingScripts(obj);
//        }

//        Repaint();
//    }

//    private void CheckMissingScripts(GameObject obj)
//    {
//        Component[] components = obj.GetComponents<Component>();

//        foreach (Component component in components)
//        {
//            if (component == null)
//            {
//                objectsWithMissingScripts.Add(obj);
//                break;
//            }
//        }
//    }
//}
