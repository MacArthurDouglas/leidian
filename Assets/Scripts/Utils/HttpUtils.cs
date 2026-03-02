using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class HttpUtils
{
    public static string baseUrl;
    static HttpUtils()
    {
        baseUrl=ApplicationConfig.Instance.startech.backend.url;
        Debug.Log(baseUrl);
    }
    
    public static IEnumerator Get(string url, System.Action<string> onSuccess = null,
        System.Action<string> onError = null)
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            onSuccess?.Invoke(webRequest.downloadHandler.text);
        }
        else
        {
            onError?.Invoke(webRequest.error);
        }
    }

    /// <summary>
    /// 发起带参数的 GET 请求
    /// </summary>
    /// <param name="baseUrl">基础 URL（不带参数）</param>
    /// <param name="queryParams">查询参数字典（Key-Value 对）</param>
    /// <param name="onSuccess">成功回调（返回响应文本）</param>
    /// <param name="onError">失败回调（返回错误信息）</param>
    public static IEnumerator Get(
        string baseUrl,
        Dictionary<string, string> queryParams,
        System.Action<string> onSuccess = null,
        System.Action<string> onError = null
    )
    {
        // 1. 拼接 URL 参数
        string urlWithParams = BuildUrlWithQueryParams(baseUrl, queryParams);

        // 2. 发起请求
        using UnityWebRequest webRequest = UnityWebRequest.Get(urlWithParams);
        yield return webRequest.SendWebRequest();

        // 3. 处理结果
        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            onSuccess?.Invoke(webRequest.downloadHandler.text);
        }
        else
        {
            onError?.Invoke(webRequest.error);
        }
    }

    /// <summary>
    /// 拼接 URL 和查询参数
    /// </summary>
    private static string BuildUrlWithQueryParams(string baseUrl, Dictionary<string, string> queryParams)
    {
        if (queryParams == null || queryParams.Count == 0)
            return baseUrl;

        // 使用 System.Web.HttpUtility 进行 URL 编码（确保特殊字符正确处理）
        var encodedParams = new List<string>();
        foreach (var param in queryParams)
        {
            string key = UnityEngine.Networking.UnityWebRequest.EscapeURL(param.Key);
            string value = UnityEngine.Networking.UnityWebRequest.EscapeURL(param.Value);
            encodedParams.Add($"{key}={value}");
        }

        return $"{baseUrl}?{string.Join("&", encodedParams)}";
    }
    /// <summary>
    /// 发起带 JSON 参数的 POST 请求
    /// </summary>
    /// <param name="url">请求 URL</param>
    /// <param name="jsonStr">要发送的 JSON 数据</param>
    /// <param name="onSuccess">成功回调（返回响应文本）</param>
    /// <param name="onError">失败回调（返回错误信息）</param>
    public static IEnumerator Post(
        string url,
        string jsonStr=null,
        System.Action<string> onSuccess = null,
        System.Action<string> onError = null
    )
    {
        // 1. 创建请求并设置 JSON 内容
        using UnityWebRequest webRequest = new UnityWebRequest(url, "POST");
        if (jsonStr != null)
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonStr);
            webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);
        }

        webRequest.downloadHandler = new DownloadHandlerBuffer();
    
        // 2. 设置请求头
        webRequest.SetRequestHeader("Content-Type", "application/json");
    
        // 3. 发起请求
        yield return webRequest.SendWebRequest();

        // 4. 处理结果
        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            onSuccess?.Invoke(webRequest.downloadHandler.text);
        }
        else
        {
            onError?.Invoke(webRequest.error);
        }
    }

}