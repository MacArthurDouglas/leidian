using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ChangeDiff : MonoBehaviour
{
    [SerializeField]
    private Sprite easy;
    [SerializeField]
    private Sprite normal;
    [SerializeField]
    private Sprite hard;
    
    [FormerlySerializedAs("difficultyEnum")] [FormerlySerializedAs("difficultyConstant")] 
    [SerializeField]
    private Difficulty difficulty;
    public void Start()
    {
        UpdateSprite();
        
    }

    /**
     * 根据难度切换对应的图片
     */
    public void UpdateSprite()
    {
        switch (difficulty)
        {
            case Difficulty.EASY:
                this.GetComponent<SpriteRenderer>().sprite = easy;
                break;
            case Difficulty.NORMAL:
                this.GetComponent<SpriteRenderer>().sprite = normal;
                break;
            case Difficulty.HARD:
                this.GetComponent<SpriteRenderer>().sprite = hard;
                break;
        }
    }
    public void OnClick()
    {
        switch (difficulty)
        {
            case Difficulty.EASY:
                difficulty = Difficulty.NORMAL;
                break;
            case Difficulty.NORMAL:
                difficulty = Difficulty.HARD;
                break;
            case Difficulty.HARD:
                difficulty = Difficulty.EASY;
                break;
        }
        UpdateSprite();
    }
}
