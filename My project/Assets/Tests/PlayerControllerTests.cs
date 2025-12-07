using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class PlayerControllerTests 
{
    // Test Case: Checking the forward speed increase over time
    [UnityTest]
    public IEnumerator ForwardSpeedIncreasesOverTime()
    {
        // 1. Arrange (Setup)
        GameObject playerObject = new GameObject();
        
        // ADD NECESSARY COMPONENTS to the temporary test object:
        playerObject.AddComponent<CharacterController>(); 
        playerObject.AddComponent<Animator>(); // Required by the PlayerController script

        PlayerController controller = playerObject.AddComponent<PlayerController>();
        
        //INITIALIZE ALL PUBLIC FIELDS to prevent NullReferenceExceptions 
        
        // 1. Initialize Animator reference
        controller.animator = playerObject.GetComponent<Animator>(); 
        
        // 2. Initialize groundCheck (Transform)
        // Create a dummy object to serve as the ground check position
        GameObject groundCheckObject = new GameObject("GroundCheck");
        controller.groundCheck = groundCheckObject.transform;
        
        // 3. Initialize groundLayer (LayerMask)
        // Set to 0  as we are not performing actual physics checks here
        controller.groundLayer = 0; 
        
        // Expect the warning about the Animator missing a Controller, 
        // which does not affect the core speed logic.
        UnityEngine.TestTools.LogAssert.Expect(LogType.Warning, new System.Text.RegularExpressions.Regex("Animator is not playing an AnimatorController"));

        // Simulate game start to allow the Update method's logic to execute
        PlayerManager.isGameStarted = true; 
    
        // Set initial speed
        controller.forwardSpeed = 10f; 
        float initialSpeed = controller.forwardSpeed;

        // Wait for 3 seconds of game time
        // This allows the Update() function to increment the speed.
        yield return new WaitForSeconds(3.0f); 

        // Assert (Verification)
        float finalSpeed = controller.forwardSpeed;
    
        // Check if speed has increased
        Assert.Greater(finalSpeed, initialSpeed, "Speed must increase after 3 seconds.");
        
        // Check if speed respects the MaxSpeed limit
        Assert.LessOrEqual(finalSpeed, controller.MaxSpeed, "Speed must not exceed the maximum speed.");

        // Cleanup
        PlayerManager.isGameStarted = false;
        Object.Destroy(groundCheckObject);
        Object.Destroy(playerObject);
    }
}