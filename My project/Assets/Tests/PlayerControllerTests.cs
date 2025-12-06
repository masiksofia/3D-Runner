using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class PlayerControllerTests 
{
    // Тест-кейс: Перевірка збільшення швидкості
    [UnityTest]
    public IEnumerator ForwardSpeedIncreasesOverTime()
    {
        // 1. Аранжування (Setup)
        GameObject playerObject = new GameObject();
        PlayerController controller = playerObject.AddComponent<PlayerController>();
        
        // Моделюємо, що гра вже розпочата, інакше Update() повернеться (return)
        PlayerManager.isGameStarted = true; 

        // Встановлюємо початкові значення, як у вашому скрипті (forwardSpeed = 0)
        controller.forwardSpeed = 10f; // Встановлюємо початкову швидкість
        float initialSpeed = controller.forwardSpeed;

        // 2. Дія (Act): Чекаємо кілька кадрів (наприклад, 3 секунди)
        // Для спрощення тестування, можна змоделювати час.
        float waitTime = 3.0f; 
        
        // В PlayMode, Time.deltaTime - це реальний час між кадрами
        yield return new WaitForSeconds(waitTime); 

        // 3. Перевірка (Assert)
        float finalSpeed = controller.forwardSpeed;
        
        // Швидкість має бути більшою за початкову
        Assert.Greater(finalSpeed, initialSpeed, "Швидкість повинна збільшитися після 3 секунд.");
        
        // Швидкість не повинна перевищувати MaxSpeed
        Assert.LessOrEqual(finalSpeed, controller.MaxSpeed, "Швидкість не повинна перевищувати максимальну швидкість.");

        // Очищення
        PlayerManager.isGameStarted = false;
        Object.Destroy(playerObject);
    }
}