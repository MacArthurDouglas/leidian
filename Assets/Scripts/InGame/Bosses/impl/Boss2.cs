using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : Bosses
{
    public override void BeforeAwake()
    {
        id = 2;
        skillCount = 1;
    }
    public override void NextStart()
    {
        doingSkills[0]=JiGuang;
    }
    private void JiGuang()
    {

    }
    private void MoveAndFire()
    {
        float destination = Random.Range(-30, 30);
        StartCoroutine(Moving(destination));

    }
    private IEnumerator Moving(float desitination)
    {
        float start = this.transform.position.x;
        float current = start;
        Vector2 vector2 = this.transform.position;
        for (int i = 1; i <= 10; i++)
        {
            current += (desitination - start) / 10;
            vector2.x = current;
            this.transform.position = vector2;
            yield return new WaitForSeconds(0.05f);
            //yield return new WaitForSeconds(5f);
        }
        Instantiate(prefabAmmo, gunPosition1.transform.position, prefabAmmo.transform.rotation);
        Instantiate(prefabAmmo, gunPosition2.transform.position, prefabAmmo.transform.rotation);
        StartCoroutine(MovingBack());

    }
    private IEnumerator MovingBack()
    {
        float start = this.transform.position.x;
        float current = start;
        Vector2 vector2 = this.transform.position;
        for (int i = 1; i <= 10; i++)
        {
            current += (0 - start) / 10;
            vector2.x = current;
            this.transform.position = vector2;
            yield return new WaitForSeconds(0.05f);
        }
        canSkill = true;
    }
}
