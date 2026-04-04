using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalAuthListener : MonoBehaviour
{
    // 登录场景名称
    private const string LoginSceneName = "Login";
    
    // 防止重复跳转
    private bool _isRedirecting = false;
    private void Awake()
    {
        // 全局唯一，切换场景不销毁
        DontDestroyOnLoad(gameObject);
    }
    private void OnEnable()
    {
        AuthManager.OnUnauthorized += HandleUnauthorized;
    }
    private void OnDisable()
    {
        AuthManager.OnUnauthorized -= HandleUnauthorized;
    }
    private void HandleUnauthorized()
    {
        if (_isRedirecting) return;
        _isRedirecting = true;
        Debug.LogWarning("检测到401，跳转登录页...");
        // 可以先弹提示框，再跳转
        ShowToastAndRedirect();
    }
    private void ShowToastAndRedirect()
    {
        // 如果你有UI提示框，在这里显示
        // UIManager.ShowToast("登录已过期，请重新登录");
        // 延迟跳转，让用户看到提示
        StartCoroutine(RedirectAfterDelay(1.5f));
    }
    private IEnumerator RedirectAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _isRedirecting = false;
        SceneManager.LoadScene(LoginSceneName);
    }
    
    
    
}