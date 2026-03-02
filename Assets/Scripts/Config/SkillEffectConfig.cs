using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffectConfig
{
    public static readonly SkillEffectConfig Instance = ConfigUtils.LoadConfig<SkillEffectConfig>("SkillEffect");
    
    
    
    public class Shield
    {
        public List<int> count{get;set;}
    }

    public class WingPower
    {
        public List<int> count{get;set;}
        public List<int> power{get;set;}
    }
    
    
    
    
    public List<long> power{get;set;}
    public Shield shield{get;set;}
    public List<int> startFire{get;set;}
    public List<int> skillTime{get;set;}
    public WingPower wingPower{get;set;}
    public List<int> gem{get;set;}
    
    
    
    

}