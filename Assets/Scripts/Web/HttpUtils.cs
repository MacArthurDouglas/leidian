using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class HttpUtils
{
    public static readonly string ApiBaseUrl = ApplicationConfig.BackendUrl;
    public static readonly string LeiDianApiBaseUrl = ApplicationConfig.BackendUrl+"/leidian";
    public static readonly string SSOApiBaseUrl = ApplicationConfig.BackendUrl+"/sso";
    private const int DefaultTimeout = 10;
    static HttpUtils()
    {
        Debug.Log(ApiBaseUrl);
    }
    
     public static IEnumerator Get(
        string pathOrUrl,
        Dictionary<string, string> queryParams = null,
        Action<Result> onSuccess = null,
        Action<string> onError = null)
    {
        string url = BuildFullUrl(pathOrUrl);
        url = BuildUrlWithQueryParams(url, queryParams);
        using UnityWebRequest webRequest = UnityWebRequest.Get(url);
        ApplyCommonSettings(webRequest);
        yield return SendRequest(webRequest, onSuccess, onError);
    }
    public static IEnumerator Post(
        string pathOrUrl,
        object bodyObj = null,
        Action<Result> onSuccess = null,
        Action<string> onError = null
        )
    {
        string url = BuildFullUrl(pathOrUrl);
        string jsonStr = bodyObj == null ? "{}" : JsonConvert.SerializeObject(bodyObj);
        using UnityWebRequest webRequest = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStr);
        webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");
        ApplyCommonSettings(webRequest);
        yield return SendRequest(webRequest, onSuccess, onError);
    }
    
    
    public static IEnumerator Put(
        string pathOrUrl,
        object bodyObj = null,
        Action<Result> onSuccess = null,
        Action<string> onError = null
    )
    {
        string url = BuildFullUrl(pathOrUrl);
        string jsonStr = bodyObj == null ? "{}" : JsonConvert.SerializeObject(bodyObj);
        using UnityWebRequest webRequest = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPUT);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStr);
        webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");
        ApplyCommonSettings(webRequest);
        yield return SendRequest(webRequest, onSuccess, onError);
    }
    
    
    public static IEnumerator Delete(
        string pathOrUrl,
        object bodyObj = null,
        Action<Result> onSuccess = null,
        Action<string> onError = null
    )
    {
        string url = BuildFullUrl(pathOrUrl);
        string jsonStr = bodyObj == null ? "{}" : JsonConvert.SerializeObject(bodyObj);
        using UnityWebRequest webRequest = new UnityWebRequest(url, UnityWebRequest.kHttpVerbDELETE);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStr);
        webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");
        ApplyCommonSettings(webRequest);
        yield return SendRequest(webRequest, onSuccess, onError);
    }
    
    
    private static IEnumerator SendRequest(
        UnityWebRequest webRequest,
        Action<Result> onSuccess,
        Action<string> onError)
    {
        yield return webRequest.SendWebRequest();
        string responseText = webRequest.downloadHandler != null
            ? webRequest.downloadHandler.text 
            : "";
        
        // ✅ 检测401，触发全局跳转
        if (webRequest.responseCode == 401)
        {
            AuthManager.TriggerUnauthorized();
            onError?.Invoke("登录已过期，请重新登录");
            yield break;
        }
        
        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            try
            {
                Result response = JsonConvert.DeserializeObject<Result>(responseText);
                onSuccess?.Invoke(response);
            }
            catch (Exception e)
            {
                onError?.Invoke($"JSON 解析失败: {e.Message}\n原始响应: {responseText}");
            }
        }
        else
        {
            string errorMsg =
                $"URL: {webRequest.url}\n" +
                $"Code: {webRequest.responseCode}\n" +
                $"Error: {webRequest.error}\n" +
                $"Response: {responseText}";
            onError?.Invoke(errorMsg);
        }
    }
    private static void ApplyCommonSettings(UnityWebRequest webRequest)
    {
        webRequest.timeout = DefaultTimeout;
        if (!string.IsNullOrEmpty(AuthManager.Token))
        {
            webRequest.SetRequestHeader("Authorization", AuthManager.Token);
        }
    }
    
    
    /**
     * 建立完整的URL
     */
    private static string BuildFullUrl(string pathOrUrl)
    {
        if (string.IsNullOrEmpty(pathOrUrl))
            return LeiDianApiBaseUrl;
        if (pathOrUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
            pathOrUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            return pathOrUrl;
        }
        return ApiBaseUrl.TrimEnd('/') +"/"+ pathOrUrl.TrimStart('/');
    }
    private static string BuildUrlWithQueryParams(string baseUrl, Dictionary<string, string> queryParams)
    {
        if (queryParams == null || queryParams.Count == 0)
            return baseUrl;
        List<string> encodedParams = new List<string>();
        foreach (var param in queryParams)
        {
            string key = UnityWebRequest.EscapeURL(param.Key);
            string value = UnityWebRequest.EscapeURL(param.Value ?? "");
            encodedParams.Add($"{key}={value}");
        }
        string separator = baseUrl.Contains("?") ? "&" : "?";
        return $"{baseUrl}{separator}{string.Join("&", encodedParams)}";
    }
}
