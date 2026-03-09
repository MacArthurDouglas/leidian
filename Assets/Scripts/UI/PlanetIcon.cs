using System;
using System.Collections;
using System.Collections.Generic;
using com.yihui.Buttons;
using UnityEngine;
using UnityEngine.AI;

public class PlanetIcon : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> icons;
    
    private float moveDuration = 0.2f; // 移动持续时间

    public int curPlanetIndex
    {
        get;
        set;
    }
    private Vector3 startPosition;
    

    private SpriteRenderer spriteRenderer;
    private Buttons button;

    private void Start()
    {
        this.curPlanetIndex = 0;
        startPosition = this.transform.position;
        spriteRenderer = this.GetComponent<SpriteRenderer>();
         button= this.GetComponent<Buttons>();
    }

    public IEnumerator SwitchPlanet(Direction direction)
    {
        yield return StartCoroutine(MovePlanet(direction));
        this.transform.position = startPosition;
        this.curPlanetIndex = (this.curPlanetIndex + 1) % this.icons.Count;
        UpdateIcon();
        
        float elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            spriteRenderer.color=  new Color(1f,1f,1f,Mathf.Lerp(0f,1f,elapsedTime/moveDuration));
            yield return null; // 每帧更新一次
        }
        spriteRenderer.color= new Color(1f,1f,1f,1f);
    }

    private void UpdateIcon()
    {
        this.spriteRenderer.sprite = this.icons[this.curPlanetIndex];
    }

    private IEnumerator MovePlanet(Direction direction)
    {
        
        button.Activated = false;
        Vector3 targetPosition = startPosition;

        // 设置目标位置
        if (direction == Direction.LEFT)
        {
            targetPosition.x -= 50f; // 左移
        }
        else if (direction == Direction.RIGHT)
        {
            targetPosition.x += 50f; // 右移
        }

        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            // 使用 Lerp 实现平滑移动
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            spriteRenderer.color=  new Color(1f,1f,1f,Mathf.Lerp(1f,0f,elapsedTime/moveDuration));
            
            yield return null; // 每帧更新一次
        }
        button.Activated = true;
    }
}
