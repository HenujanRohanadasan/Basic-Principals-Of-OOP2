using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFast : Enemy//Inheritence
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

        if (awayFromPlayer.z < 5f || awayFromPlayer.x < 5f)
        {
            enemyRb.AddForce(awayFromPlayer * strengthUpSpeed, ForceMode.Impulse);
        }
    }
}
