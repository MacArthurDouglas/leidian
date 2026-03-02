using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainGold : MonoBehaviour
{
    public void AddGold()
    {
        Main.PlayerData.gem += 10000;
        Debug.Log("金币已增加，当前金币为"+Main.PlayerData.gem);
    }
}
