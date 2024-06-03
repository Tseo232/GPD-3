using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    private Transform player;

    private NavMeshAgent agent;

    public float enemyDistance = 0.7f;

    public int enemyHealth = 100;

    public GameObject lootDropFire;
    public GameObject lootDropIce;

    private bool isDead = false;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);

        agent.SetDestination(player.transform.position);

        if (Vector3.Distance(transform.position, player.position) < enemyDistance)
        {
            gameObject.GetComponent<NavMeshAgent>().velocity = Vector3.zero;
        }

        Death();
    }

    private void Death()
    {
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
            isDead = true;
        }

        int randomNum = Random.Range(1, 3);
        
        if (randomNum == 1 && isDead)
        {
            Instantiate(lootDropFire, transform.position, Quaternion.identity);
            isDead=false;
        }
        else if (randomNum == 2 && isDead)
        {
            Instantiate(lootDropIce, transform.position, Quaternion.identity);
            isDead=false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Sword")
        {
            enemyHealth = enemyHealth - 20;
        }
    }
    
}
