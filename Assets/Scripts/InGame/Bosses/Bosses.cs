using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DoingSkills();

public class Bosses : MonoBehaviour
{
    public Vector2 startPosition;
    public GameObject prefabAmmo, gunPosition1, gunPosition2;
    protected SpriteRenderer spriteRenderer;
    public long maxHealth;
    protected long currentHealth;
    protected bool canSkill;
    protected bool onHitting;
    public int skillCount;
    public static BossConfig BossConfig;
    protected int id;
    public DoingSkills[] doingSkills;
 
    void Awake()
    {

        BossConfig = ConfigUtils.LoadConfig<BossConfig>("Bosses");
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        BeforeAwake();
        
        onHitting = false;
        maxHealth = BossConfig.maxHp[id-1];
        doingSkills = new DoingSkills[skillCount];


        NextStart();
    }
    public virtual void BeforeAwake()
    {
    }
    public virtual void NextStart() { }
    public void OnHit()
    {
        BulletDamaged();
        if (!onHitting)
        {
            StartCoroutine(OnHitting());
        }
        
    }
    /// <summary>
    /// 承伤
    /// </summary>
    protected virtual void BulletDamaged() {
        this.AddHealth(-Main.PlayerAttribute.attack);
    }
    protected virtual IEnumerator OnHitting()
    {
        onHitting = true;
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.65f);
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = new Color(1f,1f,1f,1f);
        onHitting=false;

    }
    protected void OnEnable()
    {
        this.transform.position = startPosition;
        currentHealth = maxHealth;
        StartCoroutine(Enter());
        StartCoroutine(Dying());
    }
    public void AddHealth(long value)
    {
        currentHealth += value;
    }
    protected IEnumerator Dying()
    {
        
        while (currentHealth > 0)
        {
            yield return 0;
        }
        Debug.Log("BOSS死亡");
        GameManager.Win();
        Destroy(gameObject);
    }
    protected IEnumerator Enter()
    {
        for (int i = 1; i <= 10; i++)
        {
            Vector2 vector2 = this.transform.position;
            vector2.y -= 2;
            this.transform.position = vector2;
            yield return new WaitForSeconds(0.05f);
        }
        StartCoroutine(Skill());
    }
    /// <summary>
    /// BOSS从技能表里面随机抽技能执行。
    /// </summary>
    /// <returns></returns>
    protected IEnumerator Skill()
    {
        if (skillCount == 0)
        {
            yield break;
        }
        
        canSkill = true;
        while (true)
        {

            int skillId = Random.Range(0, skillCount-1);
            doingSkills[skillId]();
            canSkill = false;
            //Debug.Log("23");
            while (!canSkill)
            {
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(3f);
        }
    }
    

    
}
