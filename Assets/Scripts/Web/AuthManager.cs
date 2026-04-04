using System;

public static class AuthManager
{
    public static string Token { get; private set; }
    
    // 全局401事件
    public static event Action OnUnauthorized;
    
    public static void TriggerUnauthorized()
    {
        Token = null; // 清除无效Token
        OnUnauthorized?.Invoke();
    }

    public static void SaveToken(string token)
    {
        Token = token;
        UnityEngine.PlayerPrefs.SetString("token", token);
        UnityEngine.PlayerPrefs.Save();
    }

    public static void LoadToken()
    {
        Token = UnityEngine.PlayerPrefs.GetString("token", "");
    }

    public static void ClearToken()
    {
        Token = "";
        UnityEngine.PlayerPrefs.DeleteKey("token");
    }
}