using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;
using System.Text;

public class APIController : MonoBehaviour
{
    [Header("TMP 文本组件")]
    [SerializeField] private TMP_Text displayText; // TextMeshPro Text组件

    [Header("API 设置")]
    [SerializeField] private string apiUrl = "https://www.smartyihui.com/api/leidian/test/user";
    [SerializeField] private float timeout = 10f; // 请求超时时间（秒）

    [Header("显示设置")]
    [SerializeField] private bool autoFetchOnStart = true; // 启动时自动获取
    [SerializeField] private string loadingMessage = "加载中..."; // 加载中显示文本
    [SerializeField] private string errorMessagePrefix = "错误: "; // 错误信息前缀

    // 用于反序列化的数据结构类
    [System.Serializable]
    public class ApiResponse
    {
        public int code;
        public string msg;
        public UserData data;
    }

    [System.Serializable]
    public class UserData
    {
        public string id;
        public string nickname;
        public string avatar;
        public string username;
        public string phone;
        public string email;
        public string status;
    }

    private void Start()
    {
        if (autoFetchOnStart)
        {
            StartCoroutine(FetchDataCoroutine());
        }
    }

    // 公开方法供UI按钮调用
    public void FetchData()
    {
        StartCoroutine(FetchDataCoroutine());
    }

    IEnumerator FetchDataCoroutine()
    {
        // 显示加载状态
        if (displayText != null)
        {
            displayText.text = loadingMessage;
            displayText.color = Color.gray;
        }

        // 创建UnityWebRequest
        using (UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl))
        {
            // 设置超时
            webRequest.timeout = (int)timeout;

            // 添加请求头（如果需要）
            // webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Authorization", AuthManager.Token);

            // 发送请求
            yield return webRequest.SendWebRequest();

            // 处理响应
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = webRequest.downloadHandler.text;
                Debug.Log($"API响应: {jsonResponse}");

                // 解析JSON
                try
                {
                    ApiResponse response = JsonUtility.FromJson<ApiResponse>(jsonResponse);
                    
                    if (response != null)
                    {
                        HandleResponse(response);
                    }
                    else
                    {
                        SetErrorText("解析响应失败: 响应为空");
                    }
                }
                catch (System.Exception e)
                {
                    SetErrorText($"解析JSON失败: {e.Message}");
                    Debug.LogError($"JSON解析错误: {e.Message}\n原始响应: {jsonResponse}");
                }
            }
            else
            {
                HandleRequestError(webRequest);
            }
        }
    }

    // 处理成功响应
    private void HandleResponse(ApiResponse response)
    {
        if (response == null || displayText == null) return;

        // 检查响应代码
        if (response.code == 200)
        {
            // 根据您的需求格式化显示信息
            StringBuilder formattedText = new StringBuilder();
            
            // 添加成功消息
            formattedText.AppendLine($"<color=green>✓ {response.msg}</color>");
            formattedText.AppendLine();
            
            // 添加用户信息
            if (response.data != null)
            {
                formattedText.AppendLine($"<b>用户信息</b>");
                formattedText.AppendLine($"──────────────");
                formattedText.AppendLine($"ID: {response.data.id}");
                formattedText.AppendLine($"用户名: {response.data.username}");
                
                // 处理可能为null的昵称
                string nickname = string.IsNullOrEmpty(response.data.nickname) ? 
                    "未设置" : response.data.nickname;
                formattedText.AppendLine($"昵称: {nickname}");
                
                formattedText.AppendLine($"电话: {response.data.phone}");
                formattedText.AppendLine($"邮箱: {response.data.email}");
                
                // 根据状态设置颜色
                string statusColor = response.data.status == "active" ? 
                    "green" : "orange";
                formattedText.AppendLine($"状态: <color={statusColor}>{response.data.status}</color>");
            }
            
            displayText.text = formattedText.ToString();
            displayText.color = Color.white;
        }
        else
        {
            // 非200状态码处理
            SetErrorText($"请求失败: {response.msg} (代码: {response.code})");
        }
    }

    // 处理请求错误
    private void HandleRequestError(UnityWebRequest webRequest)
    {
        string errorMsg = "";
        
        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
                errorMsg = "网络连接错误，请检查网络设置";
                break;
            case UnityWebRequest.Result.ProtocolError:
                errorMsg = $"HTTP错误: {webRequest.responseCode}";
                break;
            case UnityWebRequest.Result.DataProcessingError:
                errorMsg = "数据处理错误";
                break;
            default:
                errorMsg = $"未知错误: {webRequest.error}";
                break;
        }
        
        SetErrorText(errorMsg);
        Debug.LogError($"请求错误: {errorMsg}\n详细错误: {webRequest.error}");
    }

    // 设置错误文本
    private void SetErrorText(string message)
    {
        if (displayText != null)
        {
            displayText.text = $"{errorMessagePrefix}{message}";
            displayText.color = Color.red;
        }
    }

    // 手动设置API地址（可选）
    public void SetApiUrl(string url)
    {
        if (!string.IsNullOrEmpty(url))
        {
            apiUrl = url;
            Debug.Log($"API地址已更新: {apiUrl}");
        }
    }

    // 获取当前用户数据（供其他脚本使用）
    public UserData GetCurrentUserData()
    {
        // 注意：这需要先成功获取数据
        // 您可能需要添加一个字段来保存当前数据
        return null;
    }
}