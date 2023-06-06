using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG
{
    public class MonsterNotification : MonoBehaviour
    {
        public GameObject monsterPrefab; // ������ �������
        public GameObject notificationPanel; // ������ �����������
        public Text notificationText; // ����� �����������
        public float monsterSpawnDelay = 10f; // �������� ����� ���������� ������� (� ��������)
        public float notificationDuration = 5f; // ������������ ����������� ����������� (� ��������)

        private void Start()
        {
            // �������� ����� ��� ��������� ������� ����� �������� ��������
            Invoke("SpawnMonster", monsterSpawnDelay);
        }

        private void SpawnMonster()
        {
            // ������� ��������� ������� �� �����
            Instantiate(monsterPrefab, transform.position, Quaternion.identity);

            // ������������� ����� �����������
            notificationText.text = "������ ��������!";

            // �������� ������ �����������
            notificationPanel.SetActive(true);

            // ��������� ������ ��� ������� ������ ����� �������� ������������
            Invoke("HideNotification", notificationDuration);
        }

        private void HideNotification()
        {
            // ��������� ������ �����������
            notificationPanel.SetActive(false);
        }
    }
}
