using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlaneUpgrade: MonoBehaviour
{
    public Sprite opened;
    public Sprite locked;
    public bool unlocked;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (unlocked)
        {
            spriteRenderer.sprite = opened;
        }
        else
        {
            spriteRenderer.sprite = locked;
        }
    }
    public void EnterUpgrade()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("PlaneUpgrade");
    }
}
