using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Unity.Netcode;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class ApplicationConfig
{
    public static string backendUrl;
    
    


    static ApplicationConfig()
    {
        LoadConfig();
    }

    private static void LoadConfig()
    {
        try
        {
            string yaml = LoadYamlFromFile();

            backendUrl = YamlUtils.BindByPrefix<string>(yaml, "yihui.backend.url");

        }
        catch (Exception e)
        {
            Debug.LogError($"加载application.yaml失败: {e.Message}");
            // 提供默认配置
            GetDefaultConfig();
        }
    }

    private static void GetDefaultConfig()
    {
        backendUrl = "https://api.leidian.mystartech.top";
    }


    private static string LoadYamlFromFile()
    {
        // 优先从 StreamingAssets 加载（可以外部修改）
        string streamingPath = Path.Combine(Application.streamingAssetsPath, "application.yaml");
        
#if UNITY_ANDROID && !UNITY_EDITOR
        // Android 需要特殊处理
        var www = UnityEngine.Networking.UnityWebRequest.Get(streamingPath);
        www.SendWebRequest();
        while (!www.isDone) { }
        if (www.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
        {
            return www.downloadHandler.text;
        }
#else
        if (File.Exists(streamingPath))
        {
            return File.ReadAllText(streamingPath);
        }
#endif
        // 回退到 Resources（打包在游戏内）
        TextAsset textAsset = Resources.Load<TextAsset>("application");
        if (textAsset != null)
        {
            return textAsset.text;
        }
        throw new FileNotFoundException("application.yaml not found");
    }
    
    
    
    
}
