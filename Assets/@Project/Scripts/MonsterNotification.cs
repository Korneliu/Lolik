using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class MonsterNotification : MonoBehaviour
    {
        public GameObject monsterPrefab; // Префаб монстра
        public GameObject notificationPanel; // Панель уведомления
        public Text notificationText; // Текст уведомления
        public float monsterSpawnDelay = 10f; // Задержка перед появлением монстра (в секундах)
        public float notificationDuration = 5f; // Длительность отображения уведомления (в секундах)

        private void Start()
        {
            // Вызываем метод для появления монстра через заданную задержку
            Invoke("SpawnMonster", monsterSpawnDelay);
        }

        private void SpawnMonster()
        {
            // Создаем экземпляр монстра на сцене
            Instantiate(monsterPrefab, transform.position, Quaternion.identity);

            // Устанавливаем текст уведомления
            notificationText.text = "Монстр появился!";

            // Включаем панель уведомления
            notificationPanel.SetActive(true);

            // Запускаем таймер для скрытия панели после заданной длительности
            Invoke("HideNotification", notificationDuration);
        }

        private void HideNotification()
        {
            // Выключаем панель уведомления
            notificationPanel.SetActive(false);
        }
    }
}
