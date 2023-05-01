using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class OrderContainer : MonoBehaviour
{
    [SerializeField] private GameObject orderPrefab;
    public List<GameObject> orders;
    public Dictionary<GameObject, Order> orderDictionary;
    public List<GameObject> pages;
    [SerializeField] private List<GameObject> arrowButtonNumbers;
    [SerializeField] private OrderManager orderManager;

    public int ordersPerPage = 5;


    private void Awake()
    {
        orders = new List<GameObject>();
        pages = new List<GameObject>();
        orderDictionary = new Dictionary<GameObject, Order>();
        //orderManager = OrderManager.Instance;
    }

    public void AddOrder(Order order)
    {
        // If the current page is full, create a new page
        if (orders.Count % ordersPerPage == 0)
        {
            GameObject newPage = new GameObject("Page_" + (pages.Count + 1));
            newPage.transform.position = transform.position;
            newPage.transform.SetParent(transform);
            pages.Add(newPage);
        }

        // Get the current page
        GameObject currentPage = pages[pages.Count - 1];

        // Instantiate the Order object as a child of the current page
        GameObject newOrderGameObject = Instantiate(orderPrefab, currentPage.transform);


        // Subscribe to the click event
        Debug.Log("newOrderGameObject: " + newOrderGameObject);
        Debug.Log("OrderClickHandler component: " + newOrderGameObject.GetComponent<OrderClickHandler>());
        Debug.Log("orderManager: " + orderManager);
        OrderClickHandler clickHandler = newOrderGameObject.GetComponent<OrderClickHandler>();
        clickHandler.OnOrderClicked.AddListener(orderManager.OnOrderClicked);

        // Set the position of the new Order object based on the number of existing orders
        float orderHeight = newOrderGameObject.GetComponent<SpriteRenderer>().bounds.size.y;
        int orderIndexInPage = orders.Count % ordersPerPage;
        float yPos = -orderIndexInPage * orderHeight;
        Vector3 newPosition = new Vector3(0f, yPos, -1f);
        newOrderGameObject.transform.localPosition = newPosition;

        // Add the new Order object to the list of orders
        orders.Add(newOrderGameObject);
        orderDictionary.Add(newOrderGameObject, order);


    }




    public void TogglePageVisibility(int pageIndex, bool isVisible)
    {
        if (pageIndex < 0 || pageIndex >= pages.Count)
        {
            Debug.LogError("Invalid page index: " + pageIndex);
            return;
        }

        GameObject page = pages[pageIndex];
        page.SetActive(isVisible);
    }

    public void UpdateArrowButtonNumber(int pageIndex)
    {
        for (int i = 0; i < arrowButtonNumbers.Count; i++)
        {
            arrowButtonNumbers[i].SetActive(i == pageIndex);
        }
    }

    public void NextPage()
    {
        // Return if there are no pages
        if (pages.Count == 0)
        {
            return;
        }
        // Hide the current page
        int currentPageIndex = pages.IndexOf(pages.Find(page => page.activeSelf));
        TogglePageVisibility(currentPageIndex, false);

        // Show the next page
        int nextPageIndex = currentPageIndex + 1;
        if (nextPageIndex >= pages.Count)
        {
            nextPageIndex = 0;
        }
        TogglePageVisibility(nextPageIndex, true);
        // Update the visible page number by setting visibility of ArrowButtonNumbers_n
        UpdateArrowButtonNumber(nextPageIndex);

    }

    public void PreviousPage()
    {
         // Return if there are no pages
        if (pages.Count == 0)
        {
            return;
        }
        // Hide the current page
        int currentPageIndex = pages.IndexOf(pages.Find(page => page.activeSelf));
        TogglePageVisibility(currentPageIndex, false);

        // Show the previous page
        int previousPageIndex = currentPageIndex - 1;
        if (previousPageIndex < 0)
        {
            previousPageIndex = pages.Count - 1;
        }
        TogglePageVisibility(previousPageIndex, true);
        UpdateArrowButtonNumber(previousPageIndex);
    }
}
