using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 direction = Vector3.up;
    public float speed;
    public float lifeTime;
    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
       

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "EnemiesAmmo":return;
            case "Planes":return;
            case "Enemies":
                Enemies enemies = collision.gameObject.GetComponent<Enemies>();
                enemies.OnHit();
                break;
            case "Bosses":
                Bosses bosses = collision.GetComponent<Bosses>();
                bosses.OnHit();
                break;
            default:break;
        }
        DestroyMe();

    }
    void DestroyMe()
    {
        Destroy(gameObject);
    }
}
