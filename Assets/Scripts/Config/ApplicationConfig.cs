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
    public string BackendUrl{get; set; }
    
    private static ApplicationConfig instance;
    public static ApplicationConfig Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ApplicationConfig();
            }
            return instance;
        }
    }

    public void Init()
    {
        LoadConfig();
    }
    

    private void LoadConfig()
    {
        try
        {
            string yaml = LoadYamlFromFile();

            BackendUrl = YamlUtils.BindByPrefix<string>(yaml, "yihui.backend.url");

        }
        catch (Exception e)
        {
            Debug.LogError($"加载application.yaml失败: {e.Message}");
            return;
        }
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
        throw new FileNotFoundException("未找到application.yaml！");
    }
    
    
    
    
}
