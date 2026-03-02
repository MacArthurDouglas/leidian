using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SkillLevelBar : MonoBehaviour
{
    [SerializeField] protected Sprite level0;
    [SerializeField] protected Sprite level1;
    [SerializeField] protected Sprite level2;
    [SerializeField] protected Sprite level3;
    [SerializeField] protected Sprite level4;
    [SerializeField] protected Sprite level5;
    [SerializeField] protected Sprite maxLevel;
    [FormerlySerializedAs("skillName")] [SerializeField] private SkillType skillType;
    // Start is called before the first frame update
    void Start()
    {
        UpdateGUI();
        
    }

    public void UpdateGUI()
    {
        int level = Main.PlayerData.skills[SkillUtils.GetSkillId(skillType)].level;
        if (SkillUtils.IsMaxLevel(skillType, level))
        {
            this.GetComponent<SpriteRenderer>().sprite = maxLevel;
            return;
        }
        
        
        
        switch (level)
        {
            case 0:
                this.GetComponent<SpriteRenderer>().sprite = level0;
                break;
            case 1:
                this.GetComponent<SpriteRenderer>().sprite = level1;
                break;
            case 2:
                this.GetComponent<SpriteRenderer>().sprite = level2;
                break;
            case 3:
                this.GetComponent<SpriteRenderer>().sprite = level3;
                break;
            case 4:
                this.GetComponent<SpriteRenderer>().sprite = level4;
                break;
            case 5:
                this.GetComponent<SpriteRenderer>().sprite = level5;
                break;
            default:
                Debug.LogError("LevelUIError!");
                break;
        }
    }
}
