using UnityEngine;
using System.Collections.Generic;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance { get; private set; }
    public List<Order> RequestedOrders;
    public List<Order> UserCreatedOrders;

    public OrderContainer orderContainer;
    public MenuPage menuPage;

    public float expirationTime = 30f;
    public float coldTime = 60f;

    public float maxPizzasPerOrder = 3f;
    public float maxToppingsPerPizza = 4f;
    public List<string> pizzaToppings = new List<string>{ "Pepperoni", "Sausage", "Mushrooms", "Anchovies", "Eggplant", "Black Olives", "Jalapenos", "Pineapple" };


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

        Order requestedOrder = CreateRequestedOrder();
        SpawnOrder(requestedOrder);
    }

    public void SpawnOrder(Order requestedOrder)
    {
        Debug.Log("Spawn Order");
        orderContainer.AddOrder(requestedOrder);
    }

    public Order CreateRequestedOrder ()
    {
        List<List<string>> orderList = new List<List<string>>();

        int numberOfPizzas = Random.Range(1, (int)maxPizzasPerOrder + 1);

        for (int i = 0; i < numberOfPizzas; i++)
        {
            List<string> randomPizza = GenerateRandomPizza();
            orderList.Add(randomPizza);
        }

        //Order order = new Order(customer, quadrant, coordinates, price, status, expirationTime, coldTime);
        Order order = new Order("Customer", "Quadrant", new Vector2(0, 0), 10f, "Ready", expirationTime, coldTime, orderList);
        RequestedOrders.Add(order);
        return order;
    }

    private List<string> GenerateRandomPizza()
    {
        List<string> pizza = new List<string>();
        int numberOfToppings = Random.Range(0, (int)maxToppingsPerPizza + 1);

        // Add a random topping. Replace the list with the actual toppings available in your game.
        List<string> availableToppings = new List<string>{ "Pepperoni", "Sausage", "Mushrooms", "Anchovies", "Eggplant", "Black Olives", "Jalapenos", "Pineapple" };

        for (int i = 0; i < numberOfToppings; i++)
        {
            if (availableToppings.Count == 0)
            {
                break;
            }
            
            string randomTopping = availableToppings[Random.Range(0, availableToppings.Count)];
            pizza.Add(randomTopping);

            // Remove the selected topping from the list of available toppings
            availableToppings.Remove(randomTopping);
        }

        return pizza;
    }

   
    public void OnOrderClicked(GameObject orderGameObject)
    {
        Order clickedOrder = orderContainer.orderDictionary[orderGameObject];
   

        // Fill the in correct UI elements on the Menu Panel using the order's properties
        menuPage.setActiveOrder(clickedOrder);
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

    /* public void HandleUserCreatedOrder(Order userCreatedOrder)
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
    } */

    public void SetOrderReadyForDelivery(Order order)
    {
        // Set the order's status to "Ready"
        order.status = "Ready";
    }

    public bool CheckUserCreatedOrder(Order activeOrder, List<List<string>> userOrderList)
    {
        if (activeOrder.OrderList.Count != userOrderList.Count)
        {
            return false;
        }

        for (int i = 0; i < activeOrder.OrderList.Count; i++)
        {
            List<string> activePizza = activeOrder.OrderList[i];
            List<string> userPizza = userOrderList[i];

            if (activePizza.Count != userPizza.Count)
            {
                return false;
            }

            foreach (string topping in activePizza)
            {
                if (!userPizza.Contains(topping))
                {
                    return false;
                }
            }
        }

        return true;
    }

    public Order GetNextOrder()
    {
        // Return the next order in the list of requested orders (if any)
        if (RequestedOrders.Count > 0)
        {
            Order nextOrder = RequestedOrders[0];
            RequestedOrders.RemoveAt(0);
            return nextOrder;
        }

        return null;

        
    }


}