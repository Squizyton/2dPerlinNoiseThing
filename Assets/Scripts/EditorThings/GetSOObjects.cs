using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GetSOObjects : MonoBehaviour
{


    public static ScriptableObject[] GetAllSOsAtFilePath(string filePath)
    {
        string fullPath = Application.dataPath + filePath;

        List<ScriptableObject> assets = new List<ScriptableObject>();
        string[] assetPaths = Directory.GetFiles(fullPath, "*.asset");
        for(int i = 0; i < assetPaths.Length; i++)
        {
            //Need relative project path with "Asset/" for LoadAssetAtPath so split and re-add
            string path = assetPaths[i];
            path = path.Split(Application.dataPath)[1];
            path = Path.Join("Assets", path);

            ScriptableObject asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);
            if(asset == null)
            {
                Debug.LogError("Null YourScriptableObject. Path may be wrongly set in code");
                continue;
            }
            assets.Add(asset);
        }

        return assets.ToArray();

    }
}
