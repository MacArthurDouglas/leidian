using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl: MonoBehaviour
{
    public int playerId;
    public Rigidbody2D rb;
    public float speed;
    //public int maxHealth;
    private long currentHealth;
    public float reloadDelay;
    public GameObject prefabAmmo = null;
    public GameObject gunPosition = null;
    public Animator animator;
    // Update is called once per frame
    private void Start()
    {
        currentHealth = Main.PlayerAttribute.maxHp;
        StartCoroutine(Shoot());
        StartCoroutine(Dying());
    }
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"), 0f);
        animator.SetFloat("Horizontal",movement.x);
        rb.velocity = new Vector2(movement.x*speed, movement.y*speed);
    }
    IEnumerator Dying()
    {
        while (currentHealth > 0)
        {
            yield return 0;
        }
        Debug.Log("you are dead");
    }
    IEnumerator Shoot()
    {
        while (true)
        {
            Instantiate(prefabAmmo, gunPosition.transform.position, prefabAmmo.transform.rotation);
            yield return new WaitForSeconds(reloadDelay);
        }
        
    }
    public void ChangeHealth(int value)
    {
        currentHealth += value;
        //Debug.Log("oh hurt！");
    }
}
