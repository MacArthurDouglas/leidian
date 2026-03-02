using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameObject win;
    [SerializeField] private GameObject wining;
    public static GameManager Instance;
    private void Start()
    {
        win = wining;
        win.SetActive(false);
        Instance = this;
    }
    public static void Replay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
    public static void Win()
    {
        if (Main.PlayerData.level == Main.CurrentLevel)
        {
            Main.PlayerData.level++;
        }
        Instance.StartCoroutine(WinAndShow());
        

    }
    static IEnumerator WinAndShow()
    {
        win.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneLoader.SelectPlanet();
    }
}
