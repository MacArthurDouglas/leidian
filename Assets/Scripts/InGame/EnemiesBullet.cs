using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesBullet : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 direction;
    public float speed;
    public float lifeTime;
    public GameObject plane;
    private float magnitude;
    private Quaternion rotation;
    //private float 
    //public GameObject plane;
    // Update is called once per frame
    private void Start()
    {
        plane = GameObject.Find("Plane");
        speed = 100;
        direction = plane.transform.position;
        direction -= transform.position;
        //transform.forward = direction;
        direction.z = 0;

        float angle = Vector3.SignedAngle(Vector3.down,direction, Vector3.forward);
        //transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, 0, angle),1);
        rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;
        //Debug.Log(transform.forward);
        magnitude = Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2);
        magnitude = Mathf.Sqrt(magnitude);
        direction /= magnitude;
        direction.z = 0;
        //rotation = Quaternion.Euler(direction);
        
    }
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.tag);
        switch (collision.gameObject.tag)
        {
            case "Bosses": return;
            case "EnemiesBullet": return;
            case "Enemies": return;
            case "Ammo": return;
            case "Planes":
                    PlayerControl playerControl = plane.GetComponent<PlayerControl>();
                    playerControl.ChangeHealth(-10);
                    break;
            default: break;
        }
        DestroyMe();

    }
    void DestroyMe()
    {
        Destroy(gameObject);
    }
}
