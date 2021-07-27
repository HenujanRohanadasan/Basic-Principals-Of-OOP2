using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy//Inheritence
{
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();

        destroyItself();

        if (PlayerController.hasSmashPower)
        {
            SmashAtack();
        }
    }

    public override void SmashAtack()//polymorphism
    {
        Vector3 awayFromPlayer = (transform.position - player.transform.position).normalized;

        if (awayFromPlayer.z < 3f || awayFromPlayer.x < 3f)
        {
            enemyRb.AddForce(awayFromPlayer * strengthUpSpeed, ForceMode.Impulse);
        }
    }
}
