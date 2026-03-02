using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject dailyGift;
    void Start()
    {
        dailyGift = GameObject.Find("DailyGift");
        DailyGifts dailyGifts = dailyGift.GetComponent<DailyGifts>();
        if (dailyGifts.canEarned)
        {
            dailyGift.SetActive(true);
        }
    }
}
