using UnityEngine;
using UnityEngine.UI;

public class NotificationController : MonoBehaviour
{
    public Transform playerTransform;
    public Transform monsterTransform;
    public float thresholdDistance = 10.0f;
    public GameObject notificationPanel;

    private Text notificationText;

    void Start()
    {
        notificationText = notificationPanel.GetComponentInChildren<Text>();
        notificationPanel.SetActive(false);
    }

    void Update()
    {
        float distance = Vector3.Distance(playerTransform.position, monsterTransform.position);
        if (distance <= thresholdDistance)
        {
            notificationPanel.SetActive(true);
            notificationText.text = "A monster is nearby!";
        }
        else
        {
            notificationPanel.SetActive(false);
        }
    }
}
