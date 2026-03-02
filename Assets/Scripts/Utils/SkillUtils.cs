using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUtils
{
    public static int GetSkillId(SkillType skillType)
    {
        return (int)skillType;
    }
    
    public static SkillType GetSkillType(int skillId)
    {
        return (SkillType)skillId;
    }


    public static bool IsMaxLevel(SkillType skillType, int level)
    {
        if (skillType==SkillType.START_FIRE)
        {
            
            if (level>=SkillConstants.MAX_START_FIRE_LEVEL)
            {

                return true;
            }
        }
        else
        {
            if (level>=SkillConstants.MAX_SKILL_LEVEL)
            {
                return true;
            }
        }

        return false;
    }
}