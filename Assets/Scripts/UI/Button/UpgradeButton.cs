using System.Collections;
using System.Collections.Generic;
using com.yihuitech.Buttons;
using UnityEngine;

/**
 * “升级”按钮
 */
public class UpgradeButton : Buttons
{
    public override void OnClick()
    {
        int id = UpgradeManager.curChooseIndex;
        //Debug.Log(id);
        if (SkillUtils.IsMaxLevel(SkillUtils.GetSkillType(id),Main.PlayerData.skills[id].level)){
            Debug.Log("已满级！");
            return;
        }
        long skillCost = UpgradeManager.getCost();

        if (Main.PlayerData.gem >=skillCost )
        {
            Debug.Log("升级成功！");
            Main.PlayerData.gem -= skillCost;
            Main.PlayerData.skills[id].level += 1;
            GameObject parentGameObject = transform.parent.gameObject;
            UpgradeManager.Instance.UpdateGUI();


        }
        else
        {
            Debug.Log("金币不足！");
        }
    }
}
