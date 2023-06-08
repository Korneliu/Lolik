using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class MonsterNotification : MonoBehaviour
    {
        [SerializeField] private GameObject monsterPrefab;
        [SerializeField] private GameObject notificationPanel;
        [SerializeField] private Text notificationText;
        [SerializeField] private float monsterSpawnDelay = 2f;
        [SerializeField] private float notificationDuration = 5f;
        [SerializeField] private float respawnDelay = 10f;

        private bool canSpawnMonster = true;
        private int killedEnemiesCount = 0;
        private GameObject currentMonster;

        private void Start()
        {
            InvokeRepeating(nameof(SpawnMonster), monsterSpawnDelay, monsterSpawnDelay);
        }

        private void SpawnMonster()
        {
            if (canSpawnMonster)
            {
                notificationText.text = "Монстр появился!";
                notificationPanel.SetActive(true);

                currentMonster = Instantiate(monsterPrefab, transform.position, Quaternion.identity);
                canSpawnMonster = false;

                Invoke(nameof(KillMonster), respawnDelay);
            }

            Invoke(nameof(HideNotification), notificationDuration);
        }

        private void HideNotification()
        {
            notificationPanel.SetActive(false);
        }

        private void KillMonster()
        {
            if (currentMonster != null)
            {
                Destroy(currentMonster);
                canSpawnMonster = true;
            }
        }

        private void EnemyKilled()
        {
            killedEnemiesCount++;
        }
    }
}
