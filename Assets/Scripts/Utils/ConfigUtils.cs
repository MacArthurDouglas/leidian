using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using UnityEngine;

public class ConfigUtils
{
    /**
     * 读取配置文件
     */
    public static T LoadConfig<T>(string configName)
    {
        // 构建资源路径（不需要加.json扩展名）
        string resourcePath = Path.Combine("Config/", configName);
    
        // 从Resources加载文本资源
        TextAsset textAsset = Resources.Load<TextAsset>(resourcePath);
    
        if (textAsset == null)
        {
            Debug.LogError($"{configName}配置文件未找到！路径: {resourcePath}");
            return default(T);
        }

        // T config=JsonUtility.FromJson<T>(textAsset.text);
        T config=JsonSerializer.Deserialize<T>(textAsset.text);
        
        return config;
        
    }
    /**
     * 从服务器端读取配置文件
     */
    public static T LoadConfigOnline<T>(string configName)
    {
        return default(T);
        
    }

}