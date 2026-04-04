using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;
using Newtonsoft.Json;

public class LoginManager : MonoBehaviour
{
    [Header("UI引用")]
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    public UnityEngine.UI.Button loginButton;
    public TextMeshProUGUI statusText;
    public RectTransform loginPanel;
    
    public static string AuthUrl => HttpUtils.SSOApiBaseUrl+"/auth";
    public static string UserUrl => HttpUtils.SSOApiBaseUrl+"/user";
    public static string LoginUrl => AuthUrl+"/login";
    private static string mainSceneName = "Main";

    private bool _isLoading = false;

    private void Start()
    {
        // 面板入场动画
        loginPanel.localScale = Vector3.zero;
        loginPanel.DOScale(Vector3.one, 0.4f)
            .SetEase(Ease.OutBack);

        // 清空状态文字
        statusText.text = "";

        // 绑定按钮
        loginButton.onClick.AddListener(OnLoginButtonClick);

        // 回车键登录
        passwordField.onSubmit.AddListener(_ => OnLoginButtonClick());
    }

    private async void OnLoginButtonClick()
    {
        if (_isLoading) return;

        string username = usernameField.text.Trim();
        string password = passwordField.text;

        // 本地验证
        if (string.IsNullOrEmpty(username))
        {
            ShowError("请输入用户名");
            ShakePanel();
            return;
        }

        if (string.IsNullOrEmpty(password))
        {
            ShowError("请输入密码");
            ShakePanel();
            return;
        }

        _isLoading = true;
        SetLoadingState(true);
        ShowStatus("登录中...", Color.gray);

        // 构建请求体
        LoginRequest requestData = new LoginRequest
        {
            login = username,
            password = password
        };

        var result =await HttpUtils.Post<LoginResponse>(LoginUrl, requestData);
        if (result.Code==200)
        {
            OnLoginSuccess(result.Data.token);
        }
        else
        {
            ShowError(result.Message);
            ShakePanel();
            _isLoading = false;
            SetLoadingState(false);
        }

        

        
    }
    
    private void OnLoginSuccess(string token)
    {
        // 保存Token
        AuthManager.SaveToken(token);

        ShowStatus("登录成功！", Color.green);

        // 成功动画后跳转
        loginPanel.DOScale(Vector3.zero, 0.3f)
            .SetEase(Ease.InBack)
            .SetDelay(0.5f)
            .OnComplete(() =>
            {
                SceneManager.LoadScene(mainSceneName);
            });
    }

    // ========== UI工具方法 ==========

    private void ShowError(string message)
    {
        ShowStatus(message, new Color(0.9f, 0.2f, 0.2f));
    }

    private void ShowStatus(string message, Color color)
    {
        statusText.text = message;
        statusText.color = color;

        // 淡入效果
        statusText.DOFade(1f, 0.2f);
    }

    private void ShakePanel()
    {
        // 面板左右抖动（密码错误反馈）
        loginPanel.DOShakePosition(0.4f, new Vector3(15, 0, 0), 20, 90, false, true);
    }

    private void SetLoadingState(bool isLoading)
    {
        // 按钮禁用/启用
        loginButton.interactable = !isLoading;

        // 按钮文字变化
        var btnText = loginButton.GetComponentInChildren<TextMeshProUGUI>();
        if (btnText != null)
        {
            btnText.text = isLoading ? "登录中..." : "登 录";
        }

        // 输入框禁用
        usernameField.interactable = !isLoading;
        passwordField.interactable = !isLoading;
    }
}


