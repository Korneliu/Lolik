using Controller;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemScriptableObject item;
    public int amount;
    public GameObject buttonE;

    public void OnTriggerEnter(Collider collider)
    {
        //if (collider.gameObject.GetComponent<PlayerController>() != null)
        //{
        //    buttonE.SetActive(true);
        //}
    }
}
