using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.IO;
using com.yihui.Buttons;
using UnityEngine.Serialization;

public class DailyGifts : MonoBehaviour
{
    public GameObject[] awarded;
    public GameObject[] select;
    public GameObject button;
    public int continueDay;
    private int actuallyDay;
    SpriteRenderer spriteRenderer;
    public bool canEarned;
    // Start is called before the first frame update

    void getDailyGiftsData()
    {
        Dictionary<string, string> queryParams = new Dictionary<string, string>();
        queryParams.Add("name", "赵六");
        var enumerator = HttpUtils.Get<string>(
            "/test",
            queryParams,
            (result) =>
            {
                Debug.Log(result);
                Debug.Log(result.Data);
                
            },
            (error) =>
            {
                Debug.LogError(error);
            }
        );
        StartCoroutine(enumerator);
    }
    
    
    void postDailyGiftsData()
    {
        TestDto testDto = new TestDto();
        testDto.name = "张三";
        var enumerator = HttpUtils.Post<string>(
            "/test",
            testDto,
            (result) =>
            {
                Debug.Log(result);
                Debug.Log(result.Data);
                
            },
            (error) =>
            {
                Debug.LogError(error);
            }
        );
        StartCoroutine(enumerator);
    }
    
    void putDailyGiftsData()
    {
        TestDto testDto = new TestDto();
        testDto.name = "李四";
        var enumerator = HttpUtils.Put<string>(
            "/test",
            testDto,
            (result) =>
            {
                Debug.Log(result);
                Debug.Log(result.Data);
                
            },
            (error) =>
            {
                Debug.LogError(error);
            }
        );
        StartCoroutine(enumerator);
    }
    
    void deleteDailyGiftsData()
    {
        TestDto testDto = new TestDto();
        testDto.name = "王五";
        var enumerator = HttpUtils.Delete<string>(
            "/test",
            testDto,
            (result) =>
            {
                Debug.Log(result);
                Debug.Log(result.Data);
                
            },
            (error) =>
            {
                Debug.LogError(error);
            }
        );
        StartCoroutine(enumerator);
    }


    void getUser()
    {
        var enumerator = HttpUtils.Get<UserInfoVo>("/test/user", null,
            (result =>
            {
                Debug.Log(result);
                Debug.Log(result.Data);
            }),
            (error) =>
            {
                Debug.LogError(error);
            });
        StartCoroutine(enumerator);
    }
    
    
    
    void Start()
    {
        getDailyGiftsData();
        postDailyGiftsData();
        putDailyGiftsData();
        deleteDailyGiftsData();
        getUser();
        //canEarned = true;
        if (!canEarned)
        {
            DestroyMe();
        }
        //Debug.Log("12300");
        for (int i = 0; i <= 6; i++)
        {
            spriteRenderer = awarded[i].GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        }
        if (continueDay <= 7)
        {
            actuallyDay = continueDay;
        }
        else
        {
            actuallyDay = 7;
        }
        for (int i = 0; i <= actuallyDay-2; i++)
        {
            spriteRenderer = awarded[i].GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }
        if (actuallyDay != 7)
        {
            select[0].transform.position =awarded[actuallyDay-1].transform.position;
            select[0].SetActive(true);
            Destroy(select[1]);
        }
        else
        {
            select[1].transform.position = awarded[6].transform.position;
            select[1].SetActive(true);
            Destroy(select[0]);
        }
        
        
    }
    IEnumerator Destroying()
    {
        
        float transparency = 1f;
        for (int i = 1; i <= 10; i++)
        {
            transparency -= 0.1f;
            //Debug.Log(count);
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, transparency);
            foreach (Transform child in transform)
            {
                spriteRenderer = child.gameObject.GetComponent<SpriteRenderer>();
                spriteRenderer.color = new Color(1f, 1f, 1f, transparency);
            }
            yield return new WaitForSeconds(0.05f);
        }
        DestroyMe();
        
        
    }
    private void DestroyMe()
    {
        
        for (int i = 0; i < 7; i++)
        {
            Destroy(awarded[i]);

        }
        for (int i = 0; i < 1; i++)
        {
            Destroy(select[i]);
        }
        Destroy(gameObject);
    }
    public void Click()
    {

        button.GetComponent<Buttons>().Activated = false;
        if (canEarned)
        {
            canEarned = false;
            continueDay += 1;
            spriteRenderer = awarded[actuallyDay - 1].GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            Debug.Log("领取礼物!");
            StartCoroutine(Destroying());

        }
        else
        {
            Debug.Log("已经领取！");
        }
        
    }
}
