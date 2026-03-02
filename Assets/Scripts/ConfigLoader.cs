using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class ConfigLoader
{
    public static IEnumerator LoadConfig<T>(string configName, System.Action<T> callback)
    {
        string path = System.IO.Path.Combine(Application.streamingAssetsPath, "Config", configName + ".config");

        UnityWebRequest request = UnityWebRequest.Get(path);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            T config = JsonUtility.FromJson<T>(json);
            callback(config);
        }
        else
        {
            Debug.LogError("Config Not Found or Failed to Load: " + request.error);
            callback(default(T));
        }
    }
}
