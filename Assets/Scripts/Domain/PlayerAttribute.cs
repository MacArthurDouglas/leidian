using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家实时属性
/// </summary>
public class PlayerAttribute
{
    public static long PlaneInitialAttack=15L;
    public static double PlaneInitialFireInterval=0.2d;
    
    
    public long attack{get;set;}
    public long maxHp{get;set;}
    public double attackSpeed{get;set;}
    public double fireInterval{get;set;}
    
    //宝石加成
    public double gemBonus{get;set;} 
    
    public void UpdateAttribute()
    {

        
        this.maxHp = 100;
        this.attackSpeed = 100.0;
        this.fireInterval =  PlaneInitialFireInterval/ (this.attackSpeed / 100.0d);
        int PowerLevel = Main.PlayerData.skills[(int)SkillType.POWER].level;
        double attackBonus = SkillEffectConfig.Instance.power[PowerLevel];
        this.attack = (long)(PlaneInitialAttack*(100.0d+attackBonus)/100.0d);
    }
    
}