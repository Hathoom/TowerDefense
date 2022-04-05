using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public int coins = 10;
    public TextMeshProUGUI coinUI;

    // Start is called before the first frame update
    void Start()
    {
        coinUI.text = "Coins: " + coins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCoins(int num)
    {
        coins += num;

        coinUI.text = "Coins: " + coins.ToString();
        Debug.Log(coins + " coins");
    }

    public int getCoins()
    {
        return coins;
    }
}
