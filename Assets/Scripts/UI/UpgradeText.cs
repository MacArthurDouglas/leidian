using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UpgradeText : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> texts;

    [SerializeField]
    private GameObject numberObj1;
    [SerializeField]
    private GameObject numberObj2;
    
    
    private Number number1;
    private Number number2;
    
    
    

    private SpriteRenderer spriteRenderer;
    
    
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
                Debug.LogWarning("SpriteRenderer not found!", this);
            else
                UnityEditor.EditorUtility.SetDirty(this);  // 标记对象为“已修改”
        }
    }
#endif

    

    private void Awake()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        this.number1 = numberObj1.GetComponent<Number>();
        this.number2 = numberObj2.GetComponent<Number>();
        
    }
    
    public void UpdateText()
    {
        int curChooseIndex = UpgradeManager.curChooseIndex;
        SkillType skillType= SkillUtils.GetSkillType(curChooseIndex);

        spriteRenderer.sprite = texts[curChooseIndex];
        switch (skillType)
        {
            case SkillType.POWER:
                numberObj1.SetActive(true);
                numberObj2.SetActive(false);
                numberObj1.transform.position=new Vector3(1.9f,-3.75f,0.02184181f);
                number1.DisplayNumberPercent(SkillEffectConfig.Instance.power[Main.PlayerData.skills[SkillUtils.GetSkillId(skillType)].level]);
                
                
                
                break;
            case SkillType.SHIELD:
                numberObj1.SetActive(true);
                numberObj2.SetActive(false);
                numberObj1.transform.position=new Vector3(1.9f,-3.75f,0.02184181f);
                number1.DisplayNumber(SkillEffectConfig.Instance.shield.count[Main.PlayerData.skills[SkillUtils.GetSkillId(skillType)].level]);
                
                
                
                break;
            case SkillType.START_FIRE:
                numberObj1.SetActive(true);
                numberObj2.SetActive(false);
                numberObj1.transform.position=new Vector3(1.8f,-3.75f,0.02184181f);
                number1.DisplayNumber(SkillEffectConfig.Instance.startFire[Main.PlayerData.skills[SkillUtils.GetSkillId(skillType)].level]);
                
                
                
                break;
            case SkillType.SKILL_TIME:
                numberObj1.SetActive(true);
                numberObj2.SetActive(false);
                numberObj1.transform.position=new Vector3(1.68f,-3.75f,0.02184181f);
                
                number1.DisplayNumber(SkillEffectConfig.Instance.skillTime[Main.PlayerData.skills[SkillUtils.GetSkillId(skillType)].level]);
                
                
                break;
            case SkillType.WING_POWER:
                numberObj1.SetActive(true);
                numberObj2.SetActive(true);
                numberObj1.transform.position=new Vector3(-2.26f,-3.32f,0.02184181f);
                numberObj2.transform.position=new Vector3(-0.45f,-4.17f,0.02184181f);
                
                number1.DisplayNumber(SkillEffectConfig.Instance.wingPower.count[Main.PlayerData.skills[SkillUtils.GetSkillId(skillType)].level]);
                number2.DisplayNumberPercent(SkillEffectConfig.Instance.wingPower.power[Main.PlayerData.skills[SkillUtils.GetSkillId(skillType)].level]);
                
                
                break;
            case SkillType.GEM: 
                numberObj1.SetActive(true);
                numberObj2.SetActive(false);
                numberObj1.transform.position=new Vector3(-1.53f,-4.22f,0.02184181f);
                
                number1.DisplayNumber(SkillEffectConfig.Instance.gem[Main.PlayerData.skills[SkillUtils.GetSkillId(skillType)].level]);
                
                
                break;
        }
    }
}