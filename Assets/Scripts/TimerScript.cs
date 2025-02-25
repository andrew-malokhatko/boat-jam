using UnityEngine;
using TMPro; // Подключаем TextMeshPro

public class GameTime : MonoBehaviour
{
    public TextMeshProUGUI timerLabel; // Переменная для UI-элемента
    private float elapsedTime = 0f;

    void Update()
    {
        elapsedTime += Time.deltaTime; // Увеличиваем время
        timerLabel.text = "Время: " + elapsedTime.ToString("F2") + " сек"; // Обновляем текст
    }
}