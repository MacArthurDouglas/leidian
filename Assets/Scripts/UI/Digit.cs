using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digit : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> numberPics;

    [SerializeField]
    private Sprite minusSignPic;
    [SerializeField]
    private Sprite percentPic;
    public List<Sprite> NumberPics
    {
        get => numberPics;
        set => numberPics = value;
    }

    public Sprite MinusSignPic
    {
        get => minusSignPic;
        set => minusSignPic = value;
    }

    public Sprite PercentPic
    {
        get => percentPic;
        set => percentPic = value;
    }
}