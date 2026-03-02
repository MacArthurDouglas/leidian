using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameInit : MonoBehaviour
{
    
    
    void Awake()
    {
        Enemies.enemiesConfig = ConfigUtils.LoadConfig<EnemiesConfig>("Enemies");
        Bosses.BossConfig = ConfigUtils.LoadConfig<BossConfig>("Bosses");
        Main.UpdateAttribute();
    }

    
}
