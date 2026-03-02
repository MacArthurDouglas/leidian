using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSelectBox : MonoBehaviour
{
    public static UpgradeSelectBox Instance;
    void Start()
    {
        Instance = this;
        Choose(0);
    }


    /**
     * 将选择框移动到指定位置，并更新Upgrade.curChooseIndex
     */
    public void Choose(int id)
    {
        Vector2 location=new Vector2();
        if (id>=0&&id<=5)
        {
            location.Set(-4.6f+1.79f*id,-0.01f);
        }
        else
        {
            Debug.LogError("Select Not Found!");
        }
        /*switch (id)
        {
            case 0: 
                location.Set(-4.6f,-0.01f);
                break;
            case 1:
                location.Set(-3.05f, -2.3f);
                break;
            case 2:
                location.Set(-1.26f, -2.3f);
                break;
            case 3:
                location.Set(0.53f, -2.3f);
                break;
            case 4:
                location.Set(2.32f, -2.3f);
                break;
            case 5:
                location.Set(4.11f, -2.3f);
                break;
            default:
                Debug.LogError("Select Not Found!");
                break;

        }*/
        transform.position = new Vector3(location.x, location.y, transform.position.z);
        UpgradeManager.curChooseIndex = id;
    }

}
