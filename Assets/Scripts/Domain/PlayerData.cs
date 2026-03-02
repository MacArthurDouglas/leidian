using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// 玩家数据
/// </summary>
[System.Serializable]
public class PlayerData
{
    public string name{get;set;}
    public long gem{get;set;}
    public int level{get;set;}
    
    //玩家技能等级
    public List<Skill> skills{get;set;} 
    public PlayerData()
    {
        name = "UNNAMED";
        gem = 0;
        level = 1;
        // skills= new Skill[10];
        skills= new List<Skill>();
        for(int i = 0; i < SkillConstants.SKILL_COUNT; i++)
        {
            // skills[i]= new Skill();
            skills.Add(new Skill());
        }
        skills[0].set(SkillType.START_FIRE, 0);
        skills[1].set(SkillType.SHIELD, 0);
        skills[2].set(SkillType.SKILL_TIME, 0);
        skills[3].set(SkillType.POWER, 0);
        skills[4].set(SkillType.WING_POWER, 0);
        skills[5].set(SkillType.GEM, 0);
        
    }

}