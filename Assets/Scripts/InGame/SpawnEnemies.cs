using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class SpawnEnemies : MonoBehaviour
{
    public int perSpawnAmount = 2;
    public float spawnCD = 2;
    public int spawnAmount = 100;
    private int spawnLeft;
    public GameObject prefabEnemies;
    private Vector2 spawnPoint;
    [HideInInspector] public bool spawnBoss;
    //public GameObject boss;
    public GameObject[] prefabBosses;
    //private bool canSpawn;

    //[System.Obsolete]
    void Start()
    {
        //canSpawn = true;
        spawnBoss = true;
        spawnLeft = spawnAmount;
        //SpawnControl();
        StartCoroutine(SpawnControl());
 
    }

    //[System.Obsolete]
    IEnumerator SpawnControl()
    {
        while(spawnLeft > 0)
        {
            Spawn();
            yield return new WaitForSeconds(spawnCD);
        }
        if (spawnBoss==true)
        {
            //Debug.Log("123");
            SpawnBoss();
        }
        else
        {
            GameManager.Win();
        }
    }
    void SpawnBoss()
    {
        spawnPoint = new Vector2(0,97.9f);
        GameObject boss = prefabBosses[0];
        Instantiate(boss, spawnPoint, boss.transform.rotation);
    }
    void Spawn()
    {
        for (int i=0;i<perSpawnAmount;i++)
        {
            spawnPoint = new Vector2(Random.Range(-47f, 47f), 81.2f);
            Instantiate(prefabEnemies, spawnPoint, prefabEnemies.transform.rotation);
        }
        spawnLeft -= perSpawnAmount;
        //canSpawn = true;
    }
}
