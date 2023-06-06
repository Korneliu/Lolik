using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class BossSpawner : MonoBehaviour
    {
        public MonsterNotification monsterNotification;

        public GameObject bossPrefab;  // Префаб босса

        private void Start()
        {
            // Вызываем метод для появления босса через 10 секунд
            Invoke("SpawnBoss", 5f);
        }

        private void SpawnBoss()
        {
            // Создаем экземпляр босса на сцене
            Instantiate(bossPrefab, transform.position, Quaternion.identity);

        }
    }
}
