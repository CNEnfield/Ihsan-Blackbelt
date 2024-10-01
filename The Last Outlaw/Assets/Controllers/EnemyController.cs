using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;

    Transform target;
    public NavMeshAgent agent;

    [SerializeField] private float timer = 5;
    private float bulletTime;

    public GameObject enemyBullet;
    public Transform spawnPoint;
    public float enemySpeed;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (agent)
        {
            if (distance <= lookRadius)
            {
                agent.SetDestination(target.position);

                if (distance <= agent.stoppingDistance)
                {
                    // Attack the target
                    // Face the target
                    FaceTarget();
                    ShootAtPlayer();
                }
            }
        }
        else
        {
            if (distance <= lookRadius)
            {
                FaceTarget();
                ShootAtPlayer();
            }


        }


    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    void ShootAtPlayer()
    {
        bulletTime -= Time.deltaTime;

        if (bulletTime > 0) return;

        bulletTime = timer;

        GameObject bulletObj = Instantiate(enemyBullet, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();

        Vector3 dir = target.position - spawnPoint.position;

        Debug.DrawRay(spawnPoint.position, dir * 200,Color.red,20);
        bulletRig.AddForce(dir * enemySpeed);
        //Destroy(bulletObj, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Target playerHealth = other.GetComponent<Target>();
            playerHealth.health -= 10;

            if (playerHealth.health <= 0)
            {
                //This needs to be updated to take us to the new scene using scenemanager
                SceneManager.LoadScene(3);
            }

            Destroy(gameObject);            
            
        }
    }
}
