using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public static void SelectPlanet()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SelectPlanet");
    }
    public static void Earth()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("InGame");
    }

    public static void EnterMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
}
