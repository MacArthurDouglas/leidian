using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Number))]
public class Gem: MonoBehaviour
{
    private Number number;
    private long gemNumCache;
    private void Awake()
    {
        this.number=GetComponent<Number>();
    }

    private void Update()
    {
        if (gemNumCache == Main.PlayerData.gem)
        {
            return;
        }
        gemNumCache = Main.PlayerData.gem;
        number.DisplayNumber(Main.PlayerData.gem);
    }
}