using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class LoginManager : MonoBehaviour
{
    [Header("UI引用")]
    public TMP_InputField usernameField;
    public TMP_InputField passwordField;
    public UnityEngine.UI.Button loginButton;
    public TextMeshProUGUI statusText;
    public RectTransform loginPanel;
    
    private static string authUrl = HttpUtils.SSOApiBaseUrl+"/auth";
    private static string userUrl = HttpUtils.SSOApiBaseUrl+"/user";
    private static string loginUrl = authUrl+"/login";
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

    private void OnLoginButtonClick()
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

        StartCoroutine(LoginCoroutine(username, password));
    }

    private IEnumerator LoginCoroutine(string username, string password)
    {
        _isLoading = true;
        SetLoadingState(true);
        ShowStatus("登录中...", Color.gray);

        // 构建请求体
        LoginRequest requestData = new LoginRequest
        {
            login = username,
            password = password
        };
        string jsonBody = JsonUtility.ToJson(requestData);

        yield return HttpUtils.Post(loginUrl, jsonBody,
            onSuccess: (result) =>
            {
                LoginResponse response = JsonUtility.FromJson<LoginResponse>(result.data);

                if (result.code == 200 && response?.token != null)
                {
                    OnLoginSuccess(response.token);
                }
                else
                {
                    ShowError("用户名或密码错误");
                    ShakePanel();
                    _isLoading = false;
                    SetLoadingState(false);
                }
            },
            onError: (error) =>
            {
                ShowError("网络错误，请重试");
                ShakePanel();
                _isLoading = false;
                SetLoadingState(false);
            });

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


