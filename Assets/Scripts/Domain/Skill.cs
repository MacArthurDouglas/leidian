using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// 能力
/// </summary>
public class Skill
{
    public SkillType skillType{get; set; }
    public int level{get; set; }
    public void set(SkillType skillType,int level)
    {
        this.skillType = skillType;
        this.level = level;
    }
}