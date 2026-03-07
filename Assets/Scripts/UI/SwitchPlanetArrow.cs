using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using com.yihuitech.Buttons;
using UnityEngine;
using UnityEngine.Serialization;

public class SwitchPlanetArrow : Buttons
{
    [FormerlySerializedAs("directionEnum")]
    [FormerlySerializedAs("directionConstant")]
    [Header("朝向")]
    [SerializeField]
    private Direction direction;

    public GameObject planetIcon;
    public override void OnClick()
    {
        StartCoroutine(SwitchPlanet());
    }

    private IEnumerator SwitchPlanet()
    {
        Activated = false;
        PlanetIcon component = planetIcon.GetComponent<PlanetIcon>();
        Direction moveDirection;
        if (direction==Direction.LEFT)
        {
            moveDirection= Direction.RIGHT;
        }
        else
        {
            moveDirection= Direction.LEFT;
        }
        
        
        yield return component.SwitchPlanet(moveDirection);
        Activated = true;
    }
}
