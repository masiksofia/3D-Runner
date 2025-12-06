using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class PlayerManagerTests 
{
    // Тест-кейс: Перевірка початкових значень
    [UnityTest]
    public IEnumerator InitialGameStateIsCorrect()
    {
        // 1. Аранжування (Setup): Створення об'єкта, який має PlayerManager.
        GameObject managerObject = new GameObject();
        PlayerManager manager = managerObject.AddComponent<PlayerManager>();

        // Чекаємо один кадр, щоб викликалась функція Start()
        yield return null; 

        // 2. Дія (Act): Функція Start() вже викликана.

        // 3. Перевірка (Assert): Перевіряємо, чи змінні мають очікувані значення.
        // Перевіряємо, що гра не почата (очікуємо false)
        Assert.IsFalse(PlayerManager.isGameStarted, "Гра повинна бути у стані 'Не розпочато' при запуску.");
        
        // Перевіряємо, що монети = 0
        Assert.AreEqual(0, PlayerManager.numberOfCoins, "Кількість монет має бути 0 при запуску.");

        // Очищення сцени
        Object.Destroy(managerObject);
    }
}