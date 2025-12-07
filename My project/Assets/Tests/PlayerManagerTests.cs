using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class PlayerManagerTests 
{
    // Test Case: Checking the initial state and coin count upon scene load.
    [UnityTest]
    public IEnumerator InitialGameStateIsCorrect()
    {
        // 1. Arrange (Setup): 
        GameObject managerObject = new GameObject();
    
        // Create auxiliary UI objects needed by PlayerManager's Update() method
        GameObject coinTextObject = new GameObject("CoinText");
        GameObject gameOverPanelObject = new GameObject("GameOverPanel");
        GameObject startingTextObject = new GameObject("StartingText");
        
        // Attach PlayerManager component
        PlayerManager manager = managerObject.AddComponent<PlayerManager>();
    
        // !!! FIX: INITIALIZE ALL PUBLIC UI FIELDS to prevent NullReferenceExceptions in Update()
        manager.coinsText = coinTextObject.AddComponent<UnityEngine.UI.Text>();
        manager.gameOverPanel = gameOverPanelObject;
        manager.startingText = startingTextObject;

        // By initializing all fields, we avoid the need for LogAssert.Expect.

        // Wait one frame to ensure the Start() function is called by Unity
        yield return null; 
        

        // 3. Assert (Verification): 
        // Check if game start flag is false
        Assert.IsFalse(PlayerManager.isGameStarted, "The game state should be 'Not Started' on launch.");
        
        // Check if coin count is zero
        Assert.AreEqual(0, PlayerManager.numberOfCoins, "The number of coins must be 0 on launch.");

        // Cleanup
        Object.Destroy(managerObject);
        Object.Destroy(coinTextObject);
        Object.Destroy(gameOverPanelObject);
        Object.Destroy(startingTextObject);
    }
}