using System.Collections;
using System.Collections.Generic;
using com.yihuitech.Buttons;
using UnityEngine;


/**
 * 选择升级哪个
 */
public class SkillUpgradeButton : Buttons
{

    /*[SerializeField]
    private int id;*/

    [SerializeField]
    private SkillType skillType;
    

    private GameObject title;
    

    public override void NextStart()
    {
        SwitchToBrightMaterial();
        title= transform.Find("title").gameObject;
    }

    public int id
    {
        get => SkillUtils.GetSkillId(skillType);
    }

    public override void OnClick()
    {
        UpgradeManager.ChangeChoosing(id);
    }

    public override void MouseEnter()
    {
        DefaultBrightOn(1.3f);
        title.SetActive(true);
    }

    public override void MouseExit()
    {
        DefaultBrightOff();
        title.SetActive(false);
        
    }
}