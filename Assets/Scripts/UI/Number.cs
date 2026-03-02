using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/**
 * 挂载到空对象
 */
public class Number : MonoBehaviour
{
    [SerializeField]
    private float spacing = 1f; // 数字之间的间距
    
    [SerializeField]
    private float scale = 1f; // 数字缩放比例

    [SerializeField]
    private GameObject digitObjectPrefab;

    //是不是百分数
    [SerializeField]
    private bool isPercent=false;

    public long value
    {
        get;
        set;
    }


    private void Awake()
    {
        UpdateNumber(false);
    }
    public void DisplayNumber(long number)
    {
        this.value = number;
        UpdateNumber(false);
    }
    public void DisplayNumberPercent(long number)
    {
        this.value = number;
        
        UpdateNumber(true);
    }


    // 显示数字
    public void UpdateNumber(bool isPercent)
    {
        // 清除之前显示的数字
        ClearNumber();
        this.isPercent =isPercent;
        // 处理负数情况
        bool isNegative = value < 0;
        long absValue = (value<0)?-value:value;
        
        // 特殊情况处理：0
        if (absValue == 0)
        {
            CreateDigit(0, 0);
            if (this.isPercent)
            {
                CreateDigit(-2, 1);
            }
            return;
        }
        
        // 分解数字的每一位
        List<int> digits = new List<int>();
        while (absValue > 0)
        {
            digits.Add((int)absValue % 10);
            absValue /= 10;
        }
        
        // 反转数字顺序
        digits.Reverse();
        
        // 如果是负数，在最前面添加负号
        if (isNegative)
        {
            digits.Insert(0, -1); // 使用-1表示负号
        }
        
        // 创建每个数字
        for (int i = 0; i < digits.Count; i++)
        {
            CreateDigit(digits[i], i);
        }

        if (this.isPercent)
        {
            CreateDigit(-2, digits.Count);
        }
    }

    // 创建单个数字
    private void CreateDigit(int digit, int position)
    {
        // 检查数字是否有效
        if (digit < -2 || digit > 9)
        {
            Debug.LogWarning($"Invalid digit: {digit}");
            return;
        }
        
        // 创建新的数字对象
        GameObject digitObj =Instantiate(digitObjectPrefab, this.transform, true);
        digitObj.transform.localPosition = new Vector3(position * spacing, 0, 0);
        digitObj.transform.localScale*= scale;

        // 添加SpriteRenderer组件
        SpriteRenderer digitRenderer = digitObj.GetComponent<SpriteRenderer>();
        Digit digitComponent = digitObj.GetComponent<Digit>();
        
        // 设置负号或数字图片
        if (digit == -1)
        {
            // 这里假设负号是第10个图片，如果没有可以自行添加
            if (digitComponent.MinusSignPic)
            {
                digitRenderer.sprite = digitComponent.MinusSignPic;
            }
            else
            {
                Debug.LogWarning("没有负号图片，无法显示负数！");
            }
        }
        else if (digit == -2)
        {
            if (digitComponent.PercentPic)
            {
                digitRenderer.sprite = digitComponent.PercentPic;
            }
            else
            {
                Debug.LogWarning("没有百分比图片，无法显示百分数！");
            }
        }
        else //dgit为0-9
        {
            if (digit < digitComponent.NumberPics.Count)
            {
                digitRenderer.sprite = digitComponent.NumberPics[digit];
            }
            else
            {
                Debug.LogWarning($"No sprite available for digit: {digit}");
            }
        }
        
    }

    // 清除当前显示的数字
    public void ClearNumber()
    {
        List<GameObject> objs = new List<GameObject>();
        foreach (Transform objTrans in transform)
        {
            var obj = objTrans.gameObject;
            objs.Add(obj);
        }

        foreach (var obj in objs)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying) {
                DestroyImmediate(obj);
                continue;
            }
#endif
            Destroy(obj);
        }

        
    }

}