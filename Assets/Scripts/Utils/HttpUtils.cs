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
    private static readonly string BaseUrl = ApplicationConfig.BackendUrl;
    private const int DefaultTimeout = 10;
    static HttpUtils()
    {
        Debug.Log(BaseUrl);
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
        string responseText = webRequest.downloadHandler != null ? webRequest.downloadHandler.text : "";
        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            try
            {
                Result response = JsonConvert.DeserializeObject<Result>(responseText);
                if (response == null)
                {
                    onError?.Invoke("响应解析失败：返回为空");
                    yield break;
                }
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
            return BaseUrl;
        if (pathOrUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
            pathOrUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            return pathOrUrl;
        }
        if (BaseUrl.EndsWith("/") && pathOrUrl.StartsWith("/"))
            return BaseUrl + pathOrUrl.Substring(1);
        if (!BaseUrl.EndsWith("/") && !pathOrUrl.StartsWith("/"))
            return BaseUrl + "/" + pathOrUrl;
        return BaseUrl + pathOrUrl;
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
