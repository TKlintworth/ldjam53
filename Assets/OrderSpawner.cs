using UnityEngine;
using UnityEngine.UI;

public class OrderSpawner : MonoBehaviour
{
    public GameObject orderPrefab;
    public Transform content;

    public void AddOrder()
    {
        Debug.Log("Adding an order");
        GameObject newOrder = Instantiate(orderPrefab, content);
        // Inform the OrderManager that the new order has been created
        
        // Customize the newOrder here, e.g., set its properties or change its sprite
    }

    public void Start()
    {
        // Add an order when the game starts
        Debug.Log("Content: " + content.name);
        //AddOrder();
    }
}