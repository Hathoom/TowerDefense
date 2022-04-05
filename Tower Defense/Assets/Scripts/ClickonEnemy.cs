using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickonEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                Debug.Log(hitInfo.collider.gameObject.name);

                if (hitInfo.collider.gameObject.name == ("Enemy"))
                {
                    hitInfo.collider.gameObject.GetComponent<EnemyFinal>().ReduceHP(1);
                }
            }
            else
            {
                Debug.Log("Nothing hit.");
            }
        }
    }
}
