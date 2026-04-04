using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class HttpUtils
{
    public static string ApiBaseUrl => ApplicationConfig.Instance.BackendUrl;
    public static string LeiDianServiceBaseUrl => ApplicationConfig.Instance.BackendUrl+"/leidian";
    public static string SSOApiBaseUrl => ApplicationConfig.Instance.BackendUrl+"/sso";
    private const int DefaultTimeout = 10;
    
    
     public static UniTask<Result<T>> Get<T>(
        string pathOrUrl,
        Dictionary<string, string> queryParams = null)
    {
        string url = BuildFullUrl(pathOrUrl);
        url = BuildUrlWithQueryParams(url, queryParams);
        var request = UnityWebRequest.Get(url);
        ApplyCommonSettings(request);
        return SendRequest<T>(request);
    }
    public static UniTask<Result<T>> Post<T>(
        string pathOrUrl,
        object bodyObj = null)
    {
        string url = BuildFullUrl(pathOrUrl);
        string jsonStr = bodyObj == null ? "{}" : JsonConvert.SerializeObject(bodyObj);
        var request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST);
        AttachJsonBody(request, jsonStr);
        ApplyCommonSettings(request);
        return SendRequest<T>(request);
    }
    
    
    public static UniTask<Result<T>> Put<T>(
        string pathOrUrl,
        object bodyObj = null)
    {
        string url = BuildFullUrl(pathOrUrl);
        string jsonStr = bodyObj == null ? "{}" : JsonConvert.SerializeObject(bodyObj);
        var request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPUT);
        AttachJsonBody(request, jsonStr);
        ApplyCommonSettings(request);
        return SendRequest<T>(request);
    }
    
    
    public static UniTask<Result<T>> Delete<T>(
        string pathOrUrl,
        object bodyObj = null)
    {
        string url = BuildFullUrl(pathOrUrl);
        string jsonStr = bodyObj == null ? "{}" : JsonConvert.SerializeObject(bodyObj);
        var request = new UnityWebRequest(url, UnityWebRequest.kHttpVerbDELETE);
        AttachJsonBody(request, jsonStr);
        ApplyCommonSettings(request);
       return SendRequest<T>(request);
    }
    
    //核心发送
    private static async UniTask<Result<T>> SendRequest<T>(UnityWebRequest request)
    {
        try
        {
            await request.SendWebRequest();
            
        }
        catch (UnityWebRequestException e)
        {
            // ✅ 检测401，触发全局跳转
            if (e.ResponseCode == 401)
            {
                AuthManager.TriggerUnauthorized();
                throw new HttpException(401, "登录已过期，请重新登录", request.url);
            }
            throw new HttpException((int)e.ResponseCode, e.Message, request.url);
        }
        string responseText = request.downloadHandler != null
            ? request.downloadHandler.text 
            : "";
        
        
        
        if (request.result == UnityWebRequest.Result.Success)
        {
            try
            {
                return JsonConvert.DeserializeObject<Result<T>>(responseText);
            }
            catch (Exception e)
            {
                throw new HttpException(0, $"JSON 解析失败: {e.Message}\n原始响应: {responseText}", request.url);
            }
        }
        string errorMsg =
            $"URL: {request.url}\n" +
            $"Code: {request.responseCode}\n" +
            $"Error: {request.error}\n" +
            $"Response: {responseText}";
        throw new HttpException((int)request.responseCode, errorMsg, request.url);

    }
    
    private static void AttachJsonBody(UnityWebRequest request, string jsonStr)
    {
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonStr);
        request.uploadHandler   = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
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
            return LeiDianServiceBaseUrl;
        if (pathOrUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
            pathOrUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            return pathOrUrl;
        }
        return LeiDianServiceBaseUrl.TrimEnd('/') +"/"+ pathOrUrl.TrimStart('/');
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
