using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public int coins = 10;
    public int playerLives = 10;

    public TextMeshProUGUI coinText;
    public TextMeshProUGUI livesText;

    // Start is called before the first frame update
    void Start()
    {
        coinText.text = "Coins: " + coins.ToString();
        livesText.text = "Lives: " + playerLives.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCoins(int num)
    {
        coins += num;

        coinText.text = "Coins: " + coins.ToString();
        Debug.Log(coins + " coins");
    }

    public int getCoins()
    {
        return coins;
    }

    public void RemoveLives(int num)
    {
        playerLives = playerLives - num;

        livesText.text = "Lives: " + playerLives.ToString();

        if (playerLives <= 0)
        {
            Debug.Log("You lose!");
            SceneManager.LoadScene("LoseScreen");
        }
    }

    public void LoadMainLevel()
    {
        SceneManager.LoadScene("SceneMain");
    }
}
