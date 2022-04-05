using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobaEnemy : MonoBehaviour
{
    public Transform target;

    public int health = 3;
    public int coinReward = 2;

    // todo #1 - create & get a reference to the NavMeshAgent

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        Vector3 meshPosition = GetNavmeshPosition(target.position);

        agent.SetDestination(meshPosition);
        // todo #3 - place enemy at the closest navmesh point (create GetNavmesh position)
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 meshPosition = GetNavmeshPosition(target.position);

        // agent.SetDestination(meshPosition);

        // if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        // {
        //     // todo #4 - raycast from the mouse and pick a new destination for the agent.   
        //     Ray pickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     if (Physics.Raycast(pickRay, out RaycastHit hitInfo))
        //     {
        //         agent.SetDestination(hitInfo.point);
        //     }
        // }
    }

    Vector3 GetNavmeshPosition(Vector3 samplePosition)
    {
        // todo #2 - place enemy at closest navmesh point
        NavMesh.SamplePosition(samplePosition, out NavMeshHit hitInfo, 100f, -1);
        return hitInfo.position;
    }
}
