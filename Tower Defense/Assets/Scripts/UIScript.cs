using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour
{

    /*
        To use, download TextMeshPro from,
        Window->TextMeshPro->Import TMP Essential Resources.

        Make a UI object under:
        GameObject->UI->Button - (TextMeshPro)
        
        Add this script to the "Canvas" object
        Under "Canvas there is "Button"
        Rename the button to "MakeMyPrefab"

        On the Component tab of "MakeMyPrefab", there is a "Button" component.
        At the bottom of that component there is an onClick section.
        
        Click on the "+" symbol to make a new onClick method.
        On the bottom of the new thing that was made in OnClick, there is a small section with "None (Object)".
        Click and drag "Canvas" into that section.
        In the "NoFunction" dropdown menu: do the following: UIScript->OnMakeMyPrefab()

        You will need to make a prefab to put into the script "myPrefab"

        The UI should show up and make a blue cube right now, and delete itself        
    */

    //get the cube we clicked on and assign to this variable
    //not implemented!!
    public GameObject myGameObject;

    public GameObject myPrefab;

    public GameObject redCube;

    private Vector3 cubeLocation;

    private GameObject madeObject;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    //upon clicking on MakeMyPrefab button
    public void OnMakeMyPrefab()
    {
        Debug.Log("Test");

        myGameObject = GameObject.FindWithTag("ClickedObject");

        //destroy the original object
        //Not implemented!!
        cubeLocation = myGameObject.transform.position;

        myGameObject.tag = "Tower";


        if (1 >= gameManager.getCoins())
        {
            Debug.Log("You have too few coins!");
            Destroy(gameObject);
        }
        else
        {
            gameManager.AddCoins(-2);
            Destroy(myGameObject);

            //make the myPrefab
            madeObject = (GameObject)Instantiate(myPrefab, cubeLocation, Quaternion.identity);

            madeObject.tag = "Done";

            //destroy the ui
            Destroy(gameObject);
        }
    }

    public void MakeRedCube()
    {

        cubeLocation = myGameObject.transform.position;

        myGameObject = GameObject.FindWithTag("ClickedObject");

        myGameObject.tag = "Tower";

        if (4 >= gameManager.getCoins())
        {
            Debug.Log("You have too few coins!");
            Destroy(gameObject);
        }
        else
        {
            gameManager.AddCoins(-5);
            Destroy(myGameObject);

            //make redcube
            madeObject = Instantiate(redCube, cubeLocation, Quaternion.identity);

            madeObject.tag = "Done";

            //destroy the ui
            Destroy(gameObject);
        }
    }

    public void GetGameObject(GameObject newGameObject, Vector3 position)
    {
        myGameObject = newGameObject;
        cubeLocation = position;
    }
}
