using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMainMenu : MonoBehaviour
{
    public void EnterMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
}
