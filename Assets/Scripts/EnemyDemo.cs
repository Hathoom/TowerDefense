using UnityEngine;
using System.Collections.Generic;
public class EnemyDemo : MonoBehaviour
{
    // todo #1 set up properties

    //   health, speed, coin worth
    public int health = 3;

    public float speed = 3f;

    public int coins = 3;

    public bool isWalking = false;
    public bool isRunning = false;

    //   waypoints
    private int targetWaipointIndex;

    public List<Transform> waypointList;

    //   delegate event for outside code to subscribe and be notified of enemy death
    public delegate void EnemyDied(EnemyDemo enemyDemo);

    public event EnemyDied OnEnemyDied;

    public GameManager gameManager;

    // NOTE! This code should work for any speed value (large or small)

    private Animator animator;

    private static readonly int Walking = Animator.StringToHash("Walking");
    private static readonly int Running = Animator.StringToHash("Running");

    private float timePassed;
    private int lastIndex;

    //healthbar
    public HealthBarBehavior healthBar;
    private int maxHP = 3;

    //-----------------------------------------------------------------------------
    void Start()
    {
        // todo #2
        //   Place our enemy at the starting waypoint
        transform.position = waypointList[0].position;

        targetWaipointIndex = 1;
        lastIndex = 13;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        animator = GetComponent<Animator>();

        healthBar.SetHealth(health, maxHP);
    }

    //-----------------------------------------------------------------------------
    void Update()
    {

        // todo #3 Move towards the next waypoint
        Vector3 targetPostion = waypointList[targetWaipointIndex].position;
        Vector3 movementDir = (targetPostion - transform.position).normalized;

        Vector3 newPosition = transform.position;
        newPosition += movementDir * speed * Time.deltaTime;

        transform.position = newPosition;


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

        // // fix animation bug
        // bool check = animator.GetCurrentAnimatorStateInfo(0).IsName("Running");
        // Debug.Log(check);

        // if(check)
        // {
        //     animator.SetTrigger(Running);
        // }

        // todo #4 Check if destination reaches or passed and change target

        //Debug.Log(movementDir);

        // float distance = Vector3.Distance(transform.position, targetPostion);

        // Debug.Log(distance);

        if ((Vector3.Distance(transform.position, targetPostion) < 0.1f) && (Vector3.Distance(transform.position, targetPostion) > -0.1f))
        {
            Debug.Log("Success");

            //change animation to walk

            //reset the delay
            timePassed = 0f;

            if (!isWalking && isRunning)
            {
                //change the bool states
                isRunning = false;
                isWalking = true;
                //change the animation
                animator.SetTrigger(Running);

                //change the speed
                speed = 3f;
            }

            targetWaipointIndex++;
            if (targetWaipointIndex == lastIndex)
            {
                //stop moving
                speed = 0f;
                targetWaipointIndex = 0;

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

        // if (Vector3.Dot(movementDir, newPosition) < 0.1 && Vector3.Dot(movementDir, newPosition) > -0.1)
        // {
        //     targetWaipointIndex++;

        //     if (targetWaipointIndex == 12)
        //     {
        //         targetWaipointIndex = 1;
        //     }
        // }

        // Debug.Log(Vector3.Dot(movementDir, newPosition));

        bool enemyDied = false;
        if (enemyDied)
        {
            OnEnemyDied.Invoke(this);
        }

    }

    //-----------------------------------------------------------------------------
    private void TargetNextWaypoint()
    {
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
}
