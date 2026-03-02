using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string SaveName = "Save01";
        PlayerData save=Main.LoadData(SaveName);
        if (save != null)
        {
            Main.PlayerData = save;
        }
        else
        {
            Main.PlayerData = new PlayerData();
            
        }
        Main.UpdateAttribute();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
