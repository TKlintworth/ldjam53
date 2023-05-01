using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPage : MonoBehaviour
{
    public OrderManager orderManager;
    private Order activeOrder;
    private List<string> currentPizza;
    [SerializeField] private List<string> userPizza;
    [SerializeField] private List<List<string>> userOrderList;

    public GameObject pizzaImage;
    public List<GameObject> pizzaIngredientImages;
    public RectTransform pizza1IngredientsLocation;
    public RectTransform pizza2IngredientsLocation;
    public RectTransform pizza3IngredientsLocation;

    // Start is called before the first frame update
    void Start()
    {
        // Set all the pizza ingredient images to inactive
        foreach (GameObject pizzaIngredientImage in pizzaIngredientImages)
        {
            pizzaIngredientImage.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setActiveOrder(Order order)
    {
        activeOrder = order;
        Debug.Log("MenuPage.setActiveOrder");
        Debug.Log("Clicked order: " + order.ToString());
        // Print the properties of the clicked order
        Debug.Log("Customer: " + order.Customer);
        Debug.Log("Quadrant: " + order.Quadrant);
        Debug.Log("Coordinates: " + order.Coordinates);
        Debug.Log("Price: " + order.Price);
        Debug.Log("Status: " + order.Status);
        Debug.Log("Expiration Time: " + order.ExpirationTime);
        Debug.Log("Cold Time: " + order.ColdTime);
        Debug.Log("Pizza List: " + order.OrderList);
        order.PrintOrderList();
        // Display the toppings for each pizza in the pizza ingredient locations

        SetActivePizza(order.OrderList, 0);
        // Set the current pizza to a copy of the first pizza in the order
        

        // Start building the User Created Order

    }

    public void SetActivePizza(List<List<string>> orderList, int index)
    {
        currentPizza = new List<string>(orderList[index]);

        userPizza = new List<string>();

        userOrderList = new List<List<string>>();

        Debug.Log("MenuPage.SetActivePizza");
        Debug.Log("Current Pizza: " + currentPizza);
    }

    public void AddIngredientToPizza(string ingredientName)
    {
        // Add the named ingredient to the current pizza image
        //find the first inactive pizza ingredient image that matches the ingredient name
        foreach (GameObject pizzaIngredientImage in pizzaIngredientImages)
        {
            if (pizzaIngredientImage.name == ingredientName && !pizzaIngredientImage.activeSelf)
            {
                pizzaIngredientImage.SetActive(true);
                // Add to the user pizza list
                userPizza.Add(ingredientName);

                break;
            }
            // If the ingredient is already on the pizza, remove it
            else if (pizzaIngredientImage.name == ingredientName && pizzaIngredientImage.activeSelf)
            {
                pizzaIngredientImage.SetActive(false);
                // Remove from the user pizza list
                userPizza.Remove(ingredientName);

                break;
            }
        }
        

        // Add the named ingredient to the user pizza list

    }

    public void DoneButtonPressed()
    {
        Debug.Log("MenuPage.DoneButtonPressed");
        // Add the user pizza list to the user order list
        userOrderList.Add(userPizza);

        // If there are more pizzas in the active order, set the current pizza to the next pizza in the order
        if (activeOrder.OrderList.Count > userOrderList.Count)
        {
            SetActivePizza(activeOrder.OrderList, userOrderList.Count);
        }
        else
        {
            // If there are no more pizzas in the order, send the user order list to the order manager
            bool orderSuccess = orderManager.CheckUserCreatedOrder(activeOrder, userOrderList);

            if (orderSuccess)
            {
                activeOrder.Status = "Success";
                Debug.Log("Order completed successfully");
            }
            else
            {
                activeOrder.Status = "Failed";
                Debug.Log("Order failed");
            }

            // Set the order as ready for delivery
            orderManager.SetOrderReadyForDelivery(activeOrder);

            ResetOrderAndUI();

            // Get the next order from the OrderManager
            Order nextOrder = orderManager.GetNextOrder();

            if (nextOrder != null)
            {
                setActiveOrder(nextOrder);
            }
        }
    }


    private void ResetOrderAndUI()
    {
        // Reset user order list
        userOrderList = new List<List<string>>();
        // Reset user pizza 
        userPizza = new List<string>();
        // Hide the pizza ingredient images
        foreach (GameObject pizzaIngredientImage in pizzaIngredientImages)
        {
            pizzaIngredientImage.SetActive(false);
        }
    }


}
