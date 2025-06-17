using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    public static bool isGameStarted; //tap to play
    public GameObject startingText;

    public static int numberOfCoins;
    public Text coinsText;

    private void Start()
    {
        Time.timeScale = 1;
        isGameStarted = false; //tap to play
        numberOfCoins = 0;
    }


    private void Update()
    {
        if(gameOver)
        {
          gameOverPanel.SetActive(true);
            gameOver = false;
          Time.timeScale = 1;
        }

        coinsText.text =  numberOfCoins.ToString();

        //tap to play
        if(SwipeMAnager.tap)
        {
            isGameStarted = true;
            Destroy(startingText);
        }
    }

   
}
