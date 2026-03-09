using com.yihui.Buttons;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.Animation;
using UnityEngine.UIElements;

[AddComponentMenu("Buttons/LevelButton")]
public class LevelButton : Buttons
{
    
    [SerializeField] protected Sprite opened;
    [SerializeField] protected Sprite locked;
    [SerializeField] protected bool bossLevel = false;
    public int level;

    public override void NextStart()
    {
        //spriteRenderer.sprite = opened;
        if (level > Main.PlayerData.level)
        {
            spriteRenderer.sprite = locked;
            Activated = false;
        }
        else
        {
            spriteRenderer.sprite = opened;
            Activated = true;
        }
    }

    public void EnterLevel()
    {
        if (level <= 13)
        {
            SceneManager.LoadScene("InGame");
            Main.CurrentLevel = level;
        }
        else
        {
            Debug.LogError("LevelNotFound!");
        }
    }
}