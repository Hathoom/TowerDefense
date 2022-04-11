using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedTower : MonoBehaviour
{
    private Queue<GameObject> targets;
    private GameObject currentTarget;

    private float delayShot = 0f;

    private AudioSource firesound;


    // Start is called before the first frame update
    void Start()
    {
        targets = new Queue<GameObject>();
        currentTarget = null;

        firesound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        delayShot += Time.deltaTime;

        if (currentTarget != null)
        {
            // target is in range
            if ((Vector3.Distance(transform.position, currentTarget.transform.position) < 5f) && (Vector3.Distance(transform.position, currentTarget.transform.position) > -5f))
            {
                //Debug.Log("Target in range");

                //fire after 1 second
                if (delayShot >= 1.5f)
                {
                    firesound.Play();
                    delayShot = 0f;
                    currentTarget.GetComponent<EnemyFinal>().ReduceHP(2);
                }
            }
            // no target, get the next target
            else
            {
                if (targets.Peek() != null)
                {
                    currentTarget = targets.Dequeue();
                }
            }
        }
    }

    //assign new enemy target
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Trigger");

        if (collider.gameObject.tag == "Enemy")
        {
            targets.Enqueue(collider.gameObject);
        }

        // if tower has no target, give target with first enemy that enters range.
        if (currentTarget == null)
        {
            currentTarget = targets.Dequeue();
        }
    }
}
