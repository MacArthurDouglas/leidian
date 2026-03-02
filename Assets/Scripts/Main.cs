using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
/**
 * 存储全局数据
 */
public static class Main
{
    public static PlayerAttribute PlayerAttribute = new PlayerAttribute();
    
    public static PlayerData PlayerData=new PlayerData();
    


    //当前星球
    public static int CurrentPlanetIndex;
    //当前关卡
    public static int CurrentLevel;

    public static void SaveData(string fileName,PlayerData data)
    {
        string json=JsonUtility.ToJson(data);
        File.WriteAllText(Path.Combine(Application.persistentDataPath,fileName),json);
    }
    public static PlayerData LoadData(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<PlayerData>(json);
        }
        return null;
    }

    public static void UpdateAttribute()
    {
        PlayerAttribute.UpdateAttribute();
    }
}
