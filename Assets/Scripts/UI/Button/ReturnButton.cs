using System.Collections;
using System.Collections.Generic;
using com.yihuitech.Buttons;
using UnityEngine;

public class ReturnButton:Buttons
{
    public Sprite pictureMouseEnter;
    public Sprite pictureMouseExit;
    public override void MouseEnter()
    {
        this.spriteRenderer.sprite=pictureMouseEnter;
    }
    public override void MouseExit()
    {
        this.spriteRenderer.sprite=pictureMouseExit;
    }
    public override void OnClick()
    {
        SceneLoader.SelectPlanet();
    }
}