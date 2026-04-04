using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    private static GameBootstrap instance;
    private void Awake()
    {
        if (instance!=null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        Initialize();
    }
    
    private void Initialize()
    {
        var applicationConfig = ApplicationConfig.Instance;
        applicationConfig.Init();
        Debug.Log($"后端地址: {applicationConfig.BackendUrl}");
    }
    

}