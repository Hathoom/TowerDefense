using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFinal : MonoBehaviour
{
    //target to move to
    public Transform target;

    //basic stats of enemy
    public int health = 3;
    private int maxHP = 3;
    public int coins = 3;

    public float speed = 3;

    //animations stuff
    public bool isWalking = false;
    public bool isRunning = false;
    private Animator animator;

    private static readonly int Walking = Animator.StringToHash("Walking");
    private static readonly int Running = Animator.StringToHash("Running");

    private float timePassed;
    public GameManager gameManager;

    //healthbar
    public HealthBarBehavior healthBar;

    private NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        animator = GetComponent<Animator>();

        healthBar.SetHealth(health, maxHP);

        agent = GetComponent<NavMeshAgent>();

        Vector3 meshPosition = GetNavmeshPosition(target.position);

        agent.SetDestination(meshPosition);
    }

    // Update is called once per frame
    void Update()
    {
        // animator stuff
        timePassed += Time.deltaTime;

        //set walking animation
        if (speed == 3f && !isWalking)
        {
            isWalking = true;
            animator.SetTrigger(Walking);
        }
        
        // begin running after 2 seconds
        if (timePassed >= 2f && isWalking)
        {
            isWalking = false;
            isRunning = true;
            animator.SetTrigger(Running);
            speed = 6f;
        }

        //if the enemy reaches the target have them do victory animation
        if ((Vector3.Distance(transform.position, target.position) < 0.1f) && (Vector3.Distance(transform.position, target.position) > -0.1f))
        {
            Debug.Log("Made it");

            //reset the delay
            timePassed = 0f;
            //stop moving
            speed = 0f;

            //set enemy victory animation
            if(isWalking)
            {
                isWalking = false;
                animator.SetTrigger(Walking);
            }
            if(isRunning)
            {
                isRunning = false;
                animator.SetTrigger(Running);
                animator.SetTrigger(Walking);
            }
        }
    }


    public void ReduceHP(int damage)
    {
        health = health - damage;

        healthBar.SetHealth(health, maxHP);

        if (health <= 0)
        {
            //OnEnemyDied.Invoke(this);
            Destroy(this.gameObject);

            gameManager.AddCoins(coins);
        }
    }


    Vector3 GetNavmeshPosition(Vector3 samplePosition)
    {
        // todo #2 - place enemy at closest navmesh point
        NavMesh.SamplePosition(samplePosition, out NavMeshHit hitInfo, 100f, -1);
        return hitInfo.position;
    }
}
