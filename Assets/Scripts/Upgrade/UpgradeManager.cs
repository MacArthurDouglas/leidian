using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;


/**
 * 升级控制器
 */
public class UpgradeManager : MonoBehaviour
{
    
    //按id存储的升级槽
    private GameObject[] levelBars;
    
    

    private CostConfig costConfig;
    public static int curChooseIndex;
    public static long[,] costs;
    
    
    public static UpgradeManager Instance;
    
    [SerializeField]
    private UpgradeText upgradeText;

    [SerializeField]
    private Number costGem;

    [SerializeField]
    private GameObject costGemObj;
    
    
    
    
    private void Awake()
    {
        curChooseIndex=0;
        Instance = this;
    }

    /**
     * 更新升级相关GUI
     */
    public void UpdateGUI()
    {
        var skillLevelBar = levelBars[curChooseIndex].GetComponent<SkillLevelBar>();
        skillLevelBar.UpdateGUI();
        upgradeText.UpdateText();
        if (!SkillUtils.IsMaxLevel(SkillUtils.GetSkillType(curChooseIndex),Main.PlayerData.skills[curChooseIndex].level))
        {
            costGemObj.SetActive(true);
            costGem.DisplayNumber(getCost());
        }
        else
        {
            costGemObj.SetActive(false);
        }

        
    }
    private void Start()
    {
        costs = new long[SkillConstants.SKILL_COUNT,SkillConstants.MAX_SKILL_LEVEL];
        costConfig = ConfigUtils.LoadConfig<CostConfig>("Costs");
        // levelBars = new List<GameObject>(SkillConstants.SKILL_COUNT+5);

        SkillType[] skillNames = {
        SkillType.START_FIRE,SkillType.SHIELD,SkillType.SKILL_TIME,SkillType.POWER,SkillType.WING_POWER,SkillType.GEM
        };
        
        foreach(SkillType skillName in skillNames)
        {
            ReadDataFromConfig(skillName);
        }
        
        levelBars=new GameObject[transform.childCount];
        for (int i = 0; i<transform.childCount; i++) {
            Transform child = transform.GetChild(i);
            
            var upgradeSelection = child.GetComponent<SkillUpgradeButton>();
            int id = upgradeSelection.id;
            levelBars[id] = child.Find("levelBar").gameObject;
        }
        UpdateGUI();

    }
    void ReadDataFromConfig(SkillType skillType)
    {
        List<int> data;
        switch (skillType)
        {
            case SkillType.START_FIRE:
                data = costConfig.addStartFire;
                break;
            case SkillType.SHIELD:
                data = costConfig.addShield;
                break;
            case SkillType.SKILL_TIME:
                data = costConfig.addSkillTime;
                break;
            case SkillType.POWER:
                data = costConfig.addPower;
                break;
            case SkillType.WING_POWER:
                data = costConfig.addWingPower;
                break;
            case SkillType.GEM:
                data = costConfig.addGem;
                break;
            default:
                Debug.LogError("Error To Read Data");
                data = null;
                return;
        }
        for (int i = 0; i < data.Count; i++)
        {
            costs[SkillUtils.GetSkillId(skillType), i] = data[i];
        }
        
    }

    public static void ChangeChoosing(int id)
    {
        UpgradeSelectBox.Instance.Choose(id);
        Instance.UpdateGUI();
        
    }


    public static long getCost()
    {
        //Debug.Log(id);
        if (Main.PlayerData.skills[curChooseIndex].level >= 5){
            Debug.Log("已满级！");
            return -1;
        }
        var skillCost = UpgradeManager.costs[curChooseIndex, Main.PlayerData.skills[curChooseIndex].level];
        return skillCost;
    }
    
    
    
}
