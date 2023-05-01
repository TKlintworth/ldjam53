using UnityEngine;
using System.Collections.Generic;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance { get; private set; }
    public List<Order> RequestedOrders;
    public List<Order> UserCreatedOrders;

    public OrderContainer orderContainer;

    public float expirationTime = 30f;
    public float coldTime = 60f;


    private void Awake()
    {
        // Ensure there's only one instance of GameManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        InitializeOrders();
    }

    private void Update()
    {
        UpdateOrderLifeCycle();
    }

    private void InitializeOrders()
    {
        RequestedOrders = new List<Order>();
        UserCreatedOrders = new List<Order>();
    }

    public void RequestOrder()
    {
        // Create a new requested order

        // Add it to the list of requested orders

        // Send the order to the UI?
        Debug.Log("Request Order");
        CreateRequestedOrder("Customer", "Quadrant", new Vector2(0, 0), 10f, "Ready");
        SpawnOrder();
    }

    public void SpawnOrder()
    {
        Debug.Log("Spawn Order");
        orderContainer.AddOrder();
    }

    public void CreateRequestedOrder (string customer, string quadrant, Vector2 coordinates, float price, string status)
    {
        Order order = new Order(customer, quadrant, coordinates, price, status, expirationTime, coldTime);
        RequestedOrders.Add(order);
    }

    public void CreateUserCreatedOrder(string customer, string quadrant, Vector2 coordinates, float price, string status)
    {
        Order order = new Order(customer, quadrant, coordinates, price, status, expirationTime, coldTime);
        UserCreatedOrders.Add(order);
    }

    private void UpdateOrderLifeCycle()
    {
        // Iterate through requested orders
        for (int i = RequestedOrders.Count - 1; i >= 0; i--)
        {
            Order order = RequestedOrders[i];

            // Decrease expiration time
            order.expirationTime -= Time.deltaTime;

            // Check if the order has expired
            if (order.expirationTime <= 0)
            {
                // Remove the order from the list
                Debug.Log("Order Expired");
                RequestedOrders.RemoveAt(i);
                // Handle the expired order (e.g., notify the player, update score, etc.)
                // ...
            }
        }

        // Iterate through user created orders
        for (int i = UserCreatedOrders.Count - 1; i >= 0; i--)
        {
            Order order = UserCreatedOrders[i];

            // Decrease cold time
            order.coldTime -= Time.deltaTime;

            // Check if the order has become cold
            if (order.coldTime <= 0)
            {
                // Remove the order from the list
                Debug.Log("Order Cold");
                UserCreatedOrders.RemoveAt(i);
                // Handle the cold order (e.g., notify the player, update score, etc.)
                // ...
            }
        }
    }

    public void HandleUserCreatedOrder(Order userCreatedOrder)
    {
        // Iterate through the requested orders
        for (int i = 0; i < RequestedOrders.Count; i++)
        {
            Order requestedOrder = RequestedOrders[i];

            // Compare the user-created order with the requested order
            if (OrdersMatch(userCreatedOrder, requestedOrder))
            {
                // The orders match, reward the user
                // ...

                // Remove the matched orders from the respective lists
                RequestedOrders.RemoveAt(i);
                UserCreatedOrders.Remove(userCreatedOrder);

                // Return, as the order was successfully handled
                return;
            }
        }

        // If the code reaches this point, the user-created order did not match any requested order
        // Penalize the user for the incorrect order
        // ...
    }

    private bool OrdersMatch(Order order1, Order order2)
    {
        // You can add more conditions if needed, depending on the properties you want to compare
        return order1.customer == order2.customer &&
            order1.quadrant == order2.quadrant &&
            order1.coordinates == order2.coordinates &&
            order1.price == order2.price;
    }


}