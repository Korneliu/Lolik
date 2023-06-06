using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class BossSpawner : MonoBehaviour
    {
        public MonsterNotification monsterNotification;

        public GameObject bossPrefab;  // ������ �����

        private void Start()
        {
            // �������� ����� ��� ��������� ����� ����� 10 ������
            Invoke("SpawnBoss", 5f);
        }

        private void SpawnBoss()
        {
            // ������� ��������� ����� �� �����
            Instantiate(bossPrefab, transform.position, Quaternion.identity);

        }
    }
}
