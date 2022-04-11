using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;

    private float spawnDelay = 0f;

    private int spawnsleft = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnDelay += Time.deltaTime;

        if (spawnDelay >= 1f && spawnsleft > 0)
        {
            spawnsleft = spawnsleft - 1;

            Instantiate(Enemy);
        }
    }
}
