using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] float speed = 5.0f;
	public Rigidbody enemyRb;

    protected GameObject player;
    public float strengthUpSpeed = 15.0f;

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

        if(PlayerController.hasSmashPower)
        {
            SmashAtack();
        }
    }

	public virtual void SmashAtack()//polymorphism
    {
        Vector3 awayFromPlayer = (transform.position - player.transform.position).normalized;

        if (awayFromPlayer.z < 7.5f || awayFromPlayer.x < 7.5f)
        {
            enemyRb.AddForce(awayFromPlayer * strengthUpSpeed, ForceMode.Impulse);
        }
    }

    protected void EnemyMovement()//Abstraction
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;

        enemyRb.AddForce(lookDirection * speed);
    }

    protected void destroyItself()//Abstraction
    {
        if (transform.position.y < -2)
        {
            Destroy(gameObject);
        }
    }
}
