using System.Collections;
using System.Collections.Generic;
using com.startech.Buttons;
using UnityEngine;

public class PlanetButton : Buttons
{
    private PlanetIcon planetIcon;
    public void EnterPlanet()
    {
        if (planetIcon==null)
        {
            planetIcon = gameObject.GetComponent<PlanetIcon>();
        }

        var curPlanetIndex = planetIcon.curPlanetIndex;
        switch (curPlanetIndex)
        {
            case 0:
                Main.CurrentPlanetIndex = 0;
                UnityEngine.SceneManagement.SceneManager.LoadScene("Earth");
                break;
            case 1:
                Debug.LogWarning("未完成！");
                Main.CurrentPlanetIndex = 1;
                // UnityEngine.SceneManagement.SceneManager.LoadScene("Mars");
                break;
        }
        
    }
    public override void OnClick()
    {
        EnterPlanet();
    }
}