using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using UnityEngine;


public class Enemies : MonoBehaviour
{
    public int speed;
    public Vector3 direction = Vector3.down;
    public Rigidbody2D rb;
    public long maxHp;
    public long hp;
    public static EnemiesConfig enemiesConfig;
    public bool onHitting;
    public SpriteRenderer spriteRenderer;
    
    void Start()
    {
        maxHp = enemiesConfig.maxHp[0];
        hp = maxHp;
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        rb.velocity = new Vector2(0, 0 - speed);
    }
    public virtual void BulletDamaged()
    {
        long lostHp=(long)Main.PlayerAttribute.attack;
        hp -= lostHp;
        if(hp <= 0)
        {
            DestroyMe();
        }
    }
    protected virtual IEnumerator OnHitting()
    {
        onHitting = true;
        spriteRenderer.color = new Color(1f, 1f, 1f, 0.65f);
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        onHitting = false;

    }
    public void OnHit()
    {
        BulletDamaged();
        if (!onHitting)
        {
            StartCoroutine(OnHitting());
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        switch (collision.gameObject.tag)
        {
/*            case "Ammo":
                DestroyMe();
                break;*/
            case "Scenes":
                DestroyMe();
                break;
            case "Planes":
                PlayerControl pc = collision.GetComponent<PlayerControl>();
                pc.ChangeHealth(-10);
                DestroyMe();
                break;
        }
    }
    void DestroyMe()
    {
        Destroy(gameObject);
    }
}
