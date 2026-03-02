using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBackGround : MonoBehaviour
{
    public Sprite[] backgrounds;
    private void Awake()
    {
        
        if (Main.CurrentLevel == 1)
        {
            this.GetComponent<SpriteRenderer>().sprite=backgrounds[0];
        }
        else if (Main.CurrentLevel==2)
        {
            this.GetComponent<SpriteRenderer>().sprite = backgrounds[1];
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = backgrounds[0];
        }
    }
}
